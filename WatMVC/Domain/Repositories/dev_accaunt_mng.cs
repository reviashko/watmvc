using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using Domain.Entities;

namespace Domain
{
    public class dev_accaunt_mng
    {
        private string _sessionUserKey = "user";
        private char _hashSeparator = '|';
        public static string CookieUserIdKey = string.Format("User_{0}", dev_const.SiteUrl);
        private ICookieManager cmng;
        private IDataBase db;

        public dev_accaunt_mng(ICookieManager cmng, IDataBase db)
        {
            this.cmng = cmng;
            this.db = db;
        }

        public dev_users GetSessionUser()
        {
            dev_users retval = new dev_users();

            if (HttpContext.Current.Session[_sessionUserKey] != null)
            {
                dev_users tmp_retval = HttpContext.Current.Session[_sessionUserKey] as dev_users;
                if (tmp_retval != null)
                {
                    retval = tmp_retval;
                }

            }

            return retval;
        }

        //TODO: rewrite it
        public int GetUserID()
        {
            int retval = 0;

            string cookieSrc = "";
            string cookie = cmng.ReadCookie(dev_accaunt_mng.CookieUserIdKey);
            string[] src = cookie.Split(_hashSeparator);

            if (src.Length == 2 && dev_security.GetMD5(src[0]).Equals(src[1]))
            {
                cookieSrc = src[0];
            }

            int.TryParse(cookieSrc, out retval);

            return retval;
        }

        public void ClearUser()
        {
            cmng.WriteCookie(dev_accaunt_mng.CookieUserIdKey, "0", -1);
            HttpContext.Current.Session.Remove(_sessionUserKey);
        }

        public void SetSessionUser(int user_id)
        {
            dev_users user;

            if (user_id == 0)
            {
                user = new dev_users();
            }
            else
            {
                user = GetUser(user_id);
            }

            SetSessionUser(user);
        }

        public void SetSessionUser(dev_users user)
        {
            // if registered user then try to serf basket items
            if (user.Id > 0)
            {
                string cookieSrc = string.Format("{0}{1}{2}", user.Id.ToString(), _hashSeparator, dev_security.GetMD5(user.Id.ToString()));
                cmng.WriteCookie(dev_accaunt_mng.CookieUserIdKey, cookieSrc, cmng.CookieExpire);

                // if serf is well then drop user goods at session
                dev_basket_mng sbmng = new dev_basket_mng(0);
                if (sbmng.GetBasket().Count > 0)
                {
                    dev_basket_mng ubmng = new dev_basket_mng(user.Id);
                    if (ubmng.UpdItems(sbmng.GetBasket()) > 0)
                    {
                        sbmng.ClearBasket();
                    }
                }
            }

            HttpContext.Current.Session[_sessionUserKey] = user;
        }

        public bool isLoginExist(string login)
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@Login", login));
            db.SetStoredProcedure("PersonalCabinet.CheckLogin");
            return int.Parse(db.GetScalarValue()) > 0;
        }

        public dev_users GetUserByLogData(string login, string pswd)
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@Login", login));
            db.AddParameter(new SqlParameter("@Pswd", pswd));
            db.SetStoredProcedure("PersonalCabinet.CheckLoginPass");
            DataTable dt = db.GetDataTable();

            return dt.Rows.Count > 0 ? new dev_users(dt.Rows[0]) : new dev_users();
        }

        public dev_users GetUser(int user_id)
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@User_id", user_id));
            db.SetStoredProcedure("PersonalCabinet.User_Get");
            DataTable dt = db.GetDataTable();
            return dt.Rows.Count > 0 ? new dev_users(dt.Rows[0]) : new dev_users();
        }

        public int AddUser(dev_users usr, bool isWithoutRegistration)
        {
            if (isWithoutRegistration || !isLoginExist(usr.Login))
            {
                return SaveUser(usr);
            }

            return 0;
        }

        public int SaveUser(dev_users usr)
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@Id", usr.Id));
            db.AddParameter(new SqlParameter("@Login", usr.Login));
            db.AddParameter(new SqlParameter("@Adres", usr.Adres));
            db.AddParameter(new SqlParameter("@Deleted", usr.Deleted));
            db.AddParameter(new SqlParameter("@Dop_info", usr.Dop_info));
            db.AddParameter(new SqlParameter("@Email", usr.Email));
            db.AddParameter(new SqlParameter("@Fname", usr.Fname));
            db.AddParameter(new SqlParameter("@Lname", usr.Lname));
            db.AddParameter(new SqlParameter("@Mname", usr.Mname));
            db.AddParameter(new SqlParameter("@Phone", usr.Phone));
            db.AddParameter(new SqlParameter("@Pswd", usr.Pswd));
            db.SetStoredProcedure("PersonalCabinet.User_Save");
            return db.GetReturnValue<int>();
        }

    }
}
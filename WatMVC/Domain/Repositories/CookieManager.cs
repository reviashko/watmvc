using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Domain
{
    public class dev_cookie_mng : ICookieManager
    {
        private string _cookieNameKey = "";
        private int _cookieExpire = 30;

        public int CookieExpire
        {
            get { return _cookieExpire; }
        }

        public dev_cookie_mng()
        {
            _cookieNameKey = string.Format("Cookie_{0}", dev_const.SiteUrl);
        }

        public bool WriteCookie(string name, string data, int days)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = data;
            cookie.Expires = DateTime.Now.AddDays(days);
            HttpContext.Current.Response.Cookies.Add(cookie);

            return true;
        }

        public string ReadCookie(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie != null)
            {
                return cookie.Value.ToString();
            }

            return "";
        }

    }
}
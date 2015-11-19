using System;
using System.Configuration;
using System.Data;
//using System.Web.Mail;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;

namespace Domain
{
    public class dev_talk_item
    {
        private bool _isClient = true;
        public bool IsClient
        {
            get { return _isClient; }
        }

        private string _mess = "";
        public string Mess
        {
            get { return _mess; }
        }

        public dev_talk_item(bool isClient, string mess)
        {
            _isClient = isClient;
            _mess = mess;
        }

    }

    public class dev_talk_mng
    {
        private string _session_name = "dev_talk_mng";
        private List<dev_talk_item> _items = new List<dev_talk_item>();

        public DataTable GetTalkSrc()
        {
            DataTable retval = new DataTable();
            retval.Columns.Add(new DataColumn("isclient", typeof(bool)));
            retval.Columns.Add(new DataColumn("mess", typeof(string)));

            foreach (dev_talk_item it in _items)
            {
                DataRow dr = retval.NewRow();
                dr["isclient"] = it.IsClient.ToString();
                dr["mess"] = it.Mess.ToString();
                retval.Rows.Add(dr);
            }

            return retval;
        }

        public void Add(bool isClient, string mess)
        {
            _items.Add(new dev_talk_item(isClient, mess));
            SaveToSession();
        }

        private void SaveToSession()
        {
            if (_items.Count > 0)
            {
                if (HttpContext.Current.Session[_session_name] != null)
                {
                    HttpContext.Current.Session.Remove(_session_name);
                }

                HttpContext.Current.Session.Add(_session_name, _items);
            }
        }

        private List<dev_talk_item> LoadFromSession()
        {
            if (HttpContext.Current.Session[_session_name] != null)
            {
                List<dev_talk_item> retval = HttpContext.Current.Session[_session_name] as List<dev_talk_item>;
                if (retval != null && retval.Count > 0)
                {
                    return retval;
                }
            }

            return new List<dev_talk_item>();
        }

        public dev_talk_mng()
        {
            _items = LoadFromSession();
        }
    }


    public class dev_talker
    {
        IDataBase db;

        public dev_talker(IDataBase db)
        {
            this.db = db;
        }

        public DataTable GetClientData(string session_id)
        {
            //check for length
            // 35 - session_id

            db.ClearParams();
            db.AddParameter(new SqlParameter("@Session_id", session_id));
            db.SetStoredProcedure("DevTalk.GetClientData");
            return db.GetDataTable();
        }

        public DataTable SendClientData(string session_id, string mess)
        {
            return SendClientData(session_id, mess, true);
        }

        public DataTable SendClientData(string session_id, string mess, bool isclient)
        {
            //check for length
            // 35 - session_id
            // 50 - mess

            db.ClearParams();
            db.AddParameter(new SqlParameter("@Session_id", session_id));
            db.AddParameter(new SqlParameter("@Mess", mess));
            db.AddParameter(new SqlParameter("@IsClient", isclient));
            db.SetStoredProcedure("DevTalk.SendClientData");
            return db.GetDataTable();
        }


        public DataTable GetActiveSessions(int minutes)
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@Minutes", minutes));
            db.SetStoredProcedure("DevTalk.GetActiveSessions");
            return db.GetDataTable();
        }

        public DataTable GetHistory(string session_id)
        {

            db.ClearParams();
            db.AddParameter(new SqlParameter("@Session_id", session_id));
            db.SetStoredProcedure("DevTalk.GetHistory");
            return db.GetDataTable();
        }

        public DataTable ClearSessionHistory(string session_id)
        {

            db.ClearParams();
            db.AddParameter(new SqlParameter("@Session_id", session_id));
            db.SetStoredProcedure("DevTalk.ClearSessionHistory");
            return db.GetDataTable();
        }

        public DataTable GetSessionTalk(string session_id)
        {

            db.ClearParams();
            db.AddParameter(new SqlParameter("@Session_id", session_id));
            db.SetStoredProcedure("DevTalk.GetSessionTalk");
            return db.GetDataTable();
        }

    }
}
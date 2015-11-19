using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Domain
{
    public class dev_stat_mng
    {
        IDataBase db;

        public dev_stat_mng(IDataBase db)
        {
            this.db = db;
        }

        public void SaveStatistic(string session_id, string page_url, int event_id, string prms, string referer_url, string ip_addr, string searchKeyword, string searchFrom, int foreignSiteCount, int visitFromForeignSiteCount, int viewedPagesCount, int visitCount, DateTime firstVisit)
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@Session_id", session_id));
            db.AddParameter(new SqlParameter("@Page_url", page_url));
            db.AddParameter(new SqlParameter("@Event_id", event_id));
            db.AddParameter(new SqlParameter("@Params", prms));
            db.AddParameter(new SqlParameter("@Referer_url", referer_url));
            db.AddParameter(new SqlParameter("@Ip_addr", ip_addr));
            db.AddParameter(new SqlParameter("@Firstvisit", firstVisit));
            db.AddParameter(new SqlParameter("@Visitcount", visitCount));
            db.AddParameter(new SqlParameter("@Viewedpages", viewedPagesCount));
            db.AddParameter(new SqlParameter("@Foreignsitevisit", visitFromForeignSiteCount));
            db.AddParameter(new SqlParameter("@Foreignsrccount", foreignSiteCount));
            db.AddParameter(new SqlParameter("@Finder", searchFrom));
            db.AddParameter(new SqlParameter("@Keyword", searchKeyword));
            db.SetStoredProcedure("WebSite.SaveStatistic");
            db.GetDataTable();
        }

    }
}
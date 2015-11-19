using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Domain.Entities;

namespace Domain
{
    public class dev_page_metadata_mng
    {
        IDataBase db;

        public dev_page_metadata_mng(IDataBase db)
        {
            this.db = db;
        }

        public dev_page_metadata GetPageMetaDataByID(int id, bool isTag)
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@Id", id));
            db.AddParameter(new SqlParameter("@IsTag", isTag));
            db.SetStoredProcedure("[Adminka].[GetPageMetaDataByID]");
            DataTable retval = db.GetDataTable();
            return retval != null && retval.Rows.Count > 0 ? new dev_page_metadata(retval.Rows[0]) : new dev_page_metadata();
        }

        public int SaveMetaDataPage(dev_page_metadata pages)
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@Addr", pages.Addr));
            db.AddParameter(new SqlParameter("@BotArticle", pages.BotArticle));
            db.AddParameter(new SqlParameter("@TopArticle", pages.TopArticle));
            db.AddParameter(new SqlParameter("@Title", pages.Title));
            db.AddParameter(new SqlParameter("@Keywords", pages.Keywords));
            db.AddParameter(new SqlParameter("@Descr", pages.Descr));
            db.AddParameter(new SqlParameter("@MobileArticle", pages.MobileArticle));
            db.AddParameter(new SqlParameter("@IsTag", pages.IsTag));
            db.SetStoredProcedure("Adminka.SavePage");
            return int.Parse(db.GetScalarValue());
        }

        public DataTable GetMetatagsInfo()
        {
            db.ClearParams();
            db.SetStoredProcedure("Adminka.GetMetatagsInfo");
            return db.GetDataTable();
        }
    }
}
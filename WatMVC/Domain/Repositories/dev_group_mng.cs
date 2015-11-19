using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Domain
{
    public class dev_group_mng
    {
        IDataBase db;

        public dev_group_mng(IDataBase db)
        {
            this.db = db;
        }

        public DataTable GetSiteMapData()
        {
            db.ClearParams();
            db.SetStoredProcedure("Adminka.GetSiteMap");
            return db.GetDataTable();
        }

        public DataTable GetYMLData(int select_param)
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@Groups", select_param));
            db.SetStoredProcedure("Adminka.GetYMLData");
            return db.GetDataTable();
        }


    }
}
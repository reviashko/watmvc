using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Web;

namespace Domain
{
    public class dev_path_mng
    {
        private IDataBase db;

        private string _catalogPath = "";
        public string CatalogPath
        {
            get { return _catalogPath; }
        }

        private bool _currentIsSimple = true;
        public bool CurrentIsSimple
        {
            get { return _currentIsSimple; }
        }

        private string _catalogName = "";
        public string CatalogName
        {
            get { return _catalogName; }
        }

        public int Rows_count { get; set; }

        public int CatalogType { get; set; }

        public dev_path_mng(IDataBase db)
        {
            this.db = db;
        }

        private DataTable GetCatalogPath(int catalog_id)
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@Group_id", catalog_id));
            db.SetStoredProcedure("WebSite.GetCatalogPath");
            return db.GetDataTable();
        }

        public string GetCatalogPath(int catalog_id, bool linkForLast)
        {
            StringBuilder retval = new StringBuilder();
            List<string> names = new List<string>();

            UrlManager umng = UrlManager.getInstance(new MSSql());

            DataTable src = GetCatalogPath(catalog_id);

            Rows_count = src.Rows.Count;
            int current_row = 0;

            foreach (DataRow dr in src.Rows)
            {
                current_row++;

                names.Add(dr["name"].ToString());

                if (current_row == Rows_count)
                {
                    _currentIsSimple = dr["isSimple"].ToString() == "1";
                    CatalogType = int.Parse(dr["ctype_id"].ToString());
                }

                retval.AppendFormat(current_row == Rows_count && !linkForLast ? "{1}" : "<a{2} href=\"{0}\">{1}</a> &gt; ",
                    //urlWraper.GetVirtualUrl(int.Parse(dr["id"].ToString()), db), 
                    umng.GetVirtualUrl(int.Parse(dr["id"].ToString()), 0),
                    dr["name"].ToString(),
                    current_row == Rows_count ? " id=\"current\"" : ""
                    );
            }

            if (names.Count > 0)
            {

                foreach (string str in names)
                {
                    _catalogPath = string.Format("{0} - {1}", _catalogPath, str);
                }

                _catalogName = names[names.Count - 1];
            }

            return retval.ToString();
        }

    }
}
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Xml;
using System.Text;

namespace Domain
{
    public interface IDataBase
    {
        string storedProcedure { get; set; }
        List<SqlParameter> queryParameters { get; set; }

        void SetStoredProcedure(string storedProcedure);
        void AddParameter(SqlParameter param);
        void ClearParams();
        DataTable GetDataTable();
        DataSet GetDataSet();
        T GetReturnValue<T>();
        string GetScalarValue();
    }
}

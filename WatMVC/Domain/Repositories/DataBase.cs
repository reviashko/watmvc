using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using Dapper;
using System.Linq;

namespace Domain
{
    abstract public class ADataBase : IDataBase
    {
        protected string connectionString;
        public string storedProcedure { get; set; }
        public List<SqlParameter> queryParameters { get; set; }

        public ADataBase(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddParameter(SqlParameter param)
        {
            queryParameters.Add(param);
        }

        public void ClearParams()
        {
            queryParameters.Clear();
        }

        public void SetStoredProcedure(string storedProcedure)
        {
            this.storedProcedure = storedProcedure;
        }

        public string GetScalarValue()
        {
            DataTable src = GetDataTable();
            if (src == null || src.Rows.Count == 0)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in src.Rows)
            {
                sb.Append(dr[0].ToString());
            }

            return sb.ToString();
        }

        public abstract DataTable GetDataTable();

        public abstract DataSet GetDataSet();

        public abstract T GetReturnValue<T>();

        public abstract List<T> Query<T>();
    }

    public class MSSql : ADataBase
    {
        public MSSql()
            : base(ConfigurationManager.ConnectionStrings["inetshop"].ConnectionString)
        {
            queryParameters = new List<SqlParameter>();
        }

        public MSSql(string connectionstring)
            : base(connectionstring)
        {
            queryParameters = new List<SqlParameter>();
        }

        override public DataSet GetDataSet()
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(this.storedProcedure, con);

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandTimeout = 60;

                foreach (SqlParameter param in queryParameters)
                {
                    da.SelectCommand.Parameters.Add(param);
                }

                da.Fill(ds);
            }

            return ds;
        }

        override public DataTable GetDataTable()
        {
            DataSet ds = GetDataSet();

            if (ds == null || ds.Tables.Count == 0)
            {
                return new DataTable();
            }

            return ds.Tables[0];
        }

        override public T GetReturnValue<T>()
        {
            object paramObject = 0;
            SqlParameter returnValueParameter = new SqlParameter("returnValue", paramObject);
            returnValueParameter.Direction = ParameterDirection.ReturnValue;

            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(this.storedProcedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 60;

                    cmd.Parameters.Add(returnValueParameter);

                    foreach (SqlParameter param in queryParameters)
                    {
                        cmd.Parameters.Add(param);
                    }

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            return (T)Convert.ChangeType(returnValueParameter.Value, typeof(T));
        }

        override public List<T> Query<T>()
        {
            object paramObject = 0;
            SqlParameter returnValueParameter = new SqlParameter("returnValue", paramObject);
            returnValueParameter.Direction = ParameterDirection.ReturnValue;

            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                var parameters = new DynamicParameters();
                //queryParameters.ForEach(p => parameters.Add(p.ParameterName, p.Value, p.DbType, p.Direction, p.Size));

                foreach (SqlParameter param in queryParameters)
                {
                    parameters.Add(param.ParameterName, param.Value, param.DbType, param.Direction, param.Size);
                }

                con.Open();
                var result = con.Query<T>(this.storedProcedure, parameters,
                        commandType: CommandType.StoredProcedure).ToList();

                return result;
            }
        }
    }
}
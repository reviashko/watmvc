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
    public class CachebaleMSSql : IDataBase
    {
        public string storedProcedure { get; set; }
        public List<SqlParameter> queryParameters { get; set; }

        private IDataBase db;
        private ICache cache;

        public CachebaleMSSql()
        {
            this.db = new MSSql();
            this.cache = new InProcCache();
        }

        public void ClearParams()
        {
            db.ClearParams();
        }

        private string CreateKey()
        {
            StringBuilder sb = new StringBuilder(db.storedProcedure + ";");
            foreach (SqlParameter param in db.queryParameters)
            {
                if (param.Value is DataTable)
                {
                    sb.AppendFormat("{0}:[", param.ParameterName.ToString());

                    foreach (DataRow dr in ((DataTable)param.Value).Rows)
                    {
                        sb.AppendFormat("_{0}", dr["id"].ToString());
                    }

                    sb.AppendFormat("{0};", "]");
                }
                else
                {
                    sb.AppendFormat("{0}:{1};", param.ParameterName.ToString(), param.Value.ToString());
                }
            }

            return sb.ToString();
        }

        public void AddParameter(SqlParameter param)
        {
            db.queryParameters.Add(param);
        }

        public void SetStoredProcedure(string storedProcedure)
        {
            db.SetStoredProcedure(storedProcedure);
        }

        public DataSet GetDataSet()
        {
            string itemName = CreateKey();
            DataSet cacheItem = cache.GetKey<DataSet>(itemName);

            if (cacheItem == null)
            {
                cacheItem = db.GetDataSet();
                cache.KeyAdd(itemName, cacheItem, 10, 30);
            }

            return cacheItem;
        }

        public DataTable GetDataTable()
        {
            string itemName = CreateKey();
            DataTable cacheItem = cache.GetKey<DataTable>(itemName);

            if (cacheItem == null)
            {
                cacheItem = db.GetDataTable();
                cache.KeyAdd(itemName, cacheItem, 10, 30);
            }

            return cacheItem;
        }

        public T GetReturnValue<T>()
        {
            return db.GetReturnValue<T>();
        }

        public string GetScalarValue()
        {
            string itemName = CreateKey();
            string cacheItem = cache.GetKey<string>(itemName);

            if (cacheItem == null)
            {
                DataTable src = db.GetDataTable();

                StringBuilder sb = new StringBuilder();
                foreach (DataRow dr in src.Rows)
                {
                    sb.Append(dr[0].ToString());
                }

                cacheItem = sb.ToString();

                cache.KeyAdd(itemName, cacheItem, 10, 30);
            }

            return cacheItem;
        }
    }

    public interface ICache
    {
        void Clear();
        void KeyRemove(string cacheKey);
        void KeyAdd(string cacheKey, object cacheItem, int timeout_min, int timeout_max);
        T GetKey<T>(string cacheKey);
        List<string> GetAllKey();
    }


    public class InProcCache : ICache
    {
        private System.Web.Caching.Cache cache;

        public InProcCache()
        {
            this.cache = System.Web.HttpRuntime.Cache;
        }

        public void Clear()
        {
            IDictionaryEnumerator dictionaryEnumerator = cache.GetEnumerator();
            while (dictionaryEnumerator.MoveNext())
            {
                KeyRemove(dictionaryEnumerator.Key.ToString());
            }
        }

        public List<string> GetAllKey()
        {
            List<string> rval = new List<string>();

            IDictionaryEnumerator dictionaryEnumerator = cache.GetEnumerator();
            while (dictionaryEnumerator.MoveNext())
            {
                rval.Add(dictionaryEnumerator.Key.ToString());
            }

            return rval;
        }

        public void KeyRemove(string cacheKey)
        {
            cache.Remove(cacheKey);
        }

        public void KeyAdd(string cacheKey, object cacheItem, int timeout_min, int timeout_max)
        {
            if (cacheItem != null)
            {
                Random random = new Random();
                cache.Insert(cacheKey, cacheItem, null, DateTime.Now.AddMinutes(random.Next(timeout_min, timeout_max)), System.Web.Caching.Cache.NoSlidingExpiration);
            }
        }

        public T GetKey<T>(string cacheKey)
        {
            return (T)cache[cacheKey];
        }
    }
}
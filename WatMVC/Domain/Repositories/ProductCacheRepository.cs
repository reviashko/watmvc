using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using Domain.Entities;

namespace Domain
{
    public class ProductCacheRepository 
    {
        private Dictionary<int, Product> _cache;

        private static ProductCacheRepository _instance;

        private ProductCacheRepository()
        {            
            _cache = new Dictionary<int, Product>();
        }

        public static ProductCacheRepository GetInstance()
        {
            if(_instance == null)
            {
                _instance = new ProductCacheRepository();
            }

            return _instance;
        }

        public void SaveProducts(List<Product> products)
        {
            foreach(Product product in products)
            {
                if (!_cache.ContainsKey(product.Articul))
                {
                    _cache.Add(product.Articul, product);
                }
            }
        }

        public List<int> GetCacheLessOnly(List<int> articuls)
        {
            List<int> retval = new List<int>();

            foreach (int articul in articuls)
            {
                if (!_cache.ContainsKey(articul))
                {
                    retval.Add(articul);
                }
            }

            return retval;
        }

        public List<Product> GetProducts(List<int> articuls)
        {
            List<Product> retval = new List<Product>();

            foreach (int articul in articuls)
            {
                if(_cache.ContainsKey(articul))
                {
                    retval.Add(_cache[articul]);
                }
            }
            
            return retval;
        }
    }
}
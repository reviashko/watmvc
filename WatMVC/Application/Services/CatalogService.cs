using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Domain;
using System.Configuration;
using System.Data;
using Domain.Entities;

namespace Application
{
    public class CatalogService : ICatalogService
    {
        ICatalogRepository _catalogRepository;
        ProductCacheRepository _cache;


        public CatalogService(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
            _cache = ProductCacheRepository.GetInstance();
        }

        private byte[] GetBytes(List<int> items)
        {
            List<byte> bytes = new List<byte>(items.Count * sizeof(byte));
            foreach (Int32 integer in items)
            {
                byte[] tmp = BitConverter.GetBytes(integer);
                byte[] norm = new byte[4];

                norm[0] = tmp[3];
                norm[1] = tmp[2];
                norm[2] = tmp[1];
                norm[3] = tmp[0];

                bytes.AddRange(norm);
            }

            return bytes.ToArray();
        }

        public List<int> GetArticulsByMenuId(int menu_id)
        {
            //кеш тута
            return _catalogRepository.GetArticulsByMenuId(menu_id);
        }

        private List<Product> GetProducts(List<int> articuls)
        {
            byte[] items = GetBytes(articuls);

            return _catalogRepository.GetArticulsByMenuId(items);
        }


        public List<Product> GetGoodsByMenuId(int menu_id, int page_num, int page_size)
        {
            List <int> items = GetArticulsByMenuId(menu_id);

            List<int> cacheLessArticuls = _cache.GetCacheLessOnly(items);

            //put to cache here
            if (cacheLessArticuls.Count > 0)
            {
                List<Product> cacheLassProducts = GetProducts(cacheLessArticuls);
                _cache.SaveProducts(cacheLassProducts);
            }

            List<int> view_items = new List<int>();

            int index = 0;
            foreach(int articul in items)
            {
                if (
                        index >= (page_num - 1) * page_size
                        && index < (page_num - 1) * page_size + page_size
                    )
                {
                    view_items.Add(articul);
                }

                index++;
            }

            return _cache.GetProducts(view_items);
        }
        
        public Product GetProductByArticul(int articul, string brand_name)
        {
            List<int> items = new List<int>();
            items.Add(articul);

            List<int> cacheLessArticuls = _cache.GetCacheLessOnly(items);

            //put to cache here
            if (cacheLessArticuls.Count > 0)
            {
                List<Product> cacheLassProducts = GetProducts(cacheLessArticuls);
                _cache.SaveProducts(cacheLassProducts);
            }


            Product retval = _cache.GetProducts(items).FirstOrDefault();

            if (retval == null || !retval.Brand_name.ToLower().Equals(brand_name))
            {
                return new Product();
            }

            return retval;
        }

    }
}

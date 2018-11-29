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

            return _catalogRepository.GetArticulsByArticuls(items);
        }

        private List<int> CutItemsByPage(List<int> items, int page_num, int page_size)
        {
            List<int> retval = new List<int>();

            int start_position = (page_num - 1) * page_size;
            int finish_position = start_position + page_size;

            if(finish_position > items.Count)
            {
                finish_position = items.Count - 1;
            }

            if(start_position < 0 || finish_position < start_position)
            {
                return retval;
            }

            for(int index = start_position; index <= finish_position; index++)
            {
                retval.Add(items[index]);
            }

            return retval;
        }

        private void FillCacheByArticuls(List<int> articuls)
        {
            List<int> cacheLessArticuls = _cache.GetCacheLessOnly(articuls);

            //put to cache here
            if (cacheLessArticuls.Count > 0)
            {
                List<Product> cacheLassProducts = GetProducts(cacheLessArticuls);
                _cache.SaveProducts(cacheLassProducts);
            }
        }


        public List<Product> GetGoodsByMenuId(int menu_id, int page_num, int page_size)
        {
            List <int> catalog_items = GetArticulsByMenuId(menu_id);

            return GetProductsByArticuls(catalog_items, page_num, page_size);
        }

        public List<Product> GetProductsByArticuls(List<int> articuls, int page_num, int page_size)
        {
            List<int> view_items = CutItemsByPage(articuls, page_num, page_size);

            FillCacheByArticuls(view_items);

            return _cache.GetProducts(view_items);
        }
        
        public Product GetProductByArticul(int articul, string brand_name)
        {
            List<int> items = new List<int>();
            items.Add(articul);

            FillCacheByArticuls(items);


            Product retval = _cache.GetProducts(items).FirstOrDefault();

            if (retval == null || !retval.Brand_name.ToLower().Equals(brand_name))
            {
                return new Product();
            }

            return retval;
        }

    }
}

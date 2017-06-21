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

        public CatalogService(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        public List<Product> GetGoodsByCategoryBrandSeriaArticul(int categoty_id, int brand_id, int seria_id, string articul)
        {
            // кеш тута
            return _catalogRepository.GetGoodsByCategoryBrandSeriaArticul(categoty_id, brand_id, seria_id, articul);
        }

        public List<Product> GetGoodsByCategoryBrandSeria(int categoty_id, int brand_id, int seria_id)
        {
            // кеш тута
            return _catalogRepository.GetGoodsByCategoryBrandSeria(categoty_id, brand_id, seria_id);
        }

        public List<Product> GetGoodsByCategoryBrand(int categoty_id, int brand_id)
        {
            // кеш тута
            return _catalogRepository.GetGoodsByCategoryBrand(categoty_id, brand_id);
        }

        public List<Product> GetGoodsByCategory(int category_id)
        {
            // кеш тута
            return _catalogRepository.GetGoodsByCategory(category_id);
        }

    }
}

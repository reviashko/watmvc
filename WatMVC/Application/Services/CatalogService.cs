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

        public List<Product> GetGoodsByMenuId(int menu_id)
        {
            // кеш тута
            return _catalogRepository.GetGoodsByMenuId(menu_id);
        }

    }
}

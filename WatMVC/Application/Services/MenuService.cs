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
    public class MenuService : IMenuService
    {
        IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public CatalogMenuItem GetCatalogMenuItemByCategoryBrandSeria(string category_name, string brand_name, string seria_name)
        {
            CatalogMenuItem retval = _menuRepository.GetCatalogMenuItemByCategoryBrandSeria(category_name, brand_name, seria_name);
            return retval;
        }
        public CatalogMenuItem GetCatalogMenuItemByCategoryBrand(string category_name, string brand_name)
        {
            return _menuRepository.GetCatalogMenuItemByCategoryBrand(category_name, brand_name);
        }
        public CatalogMenuItem GetCatalogMenuItemByCategory(string category_name)
        {
            return _menuRepository.GetCatalogMenuItemByCategory(category_name);
        }

        public List<MainMenuItem> GetMainMenuItems()
        {
            // кеш тута
            return _menuRepository.GetMainMenuItems();
        }

        public List<CatalogMenuItem> GetCatalogMenuItems(int category_id)
        {
            // кеш тута
            return _menuRepository.GetCatalogMenuItems(category_id);
        }

        public List<CatalogMenuItem> Get(int category_id)
        {
            // кеш тута
            return _menuRepository.GetCatalogMenuItems(category_id);
        }

        //public CatalogMenuItem GetCatalogMenuItemByBrandSeria(string brand_name, string seria_name)
        //{
        //return _menuRepository.GetCatalogMenuItemByBrandSeria(brand_name, seria_name);
        //}

        public CatalogMenuItem GetCatalogMenuItemById(int menu_id)
        {
            return _menuRepository.GetCatalogMenuItemById(menu_id);
        }

        public MainMenuItem GetMainMenuItemById(int menu_id)
        {
            return _menuRepository.GetMainMenuItemById(menu_id);
        }

    }
}

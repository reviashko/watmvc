using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Domain.Entities;

namespace Domain
{
    public class MenuRepository : IMenuRepository
    {
        private List<MainMenuItem> _main_menu;

        private List<CatalogMenuItem> _catalog_menu;

        public MenuRepository()
        {
            _main_menu = new List<MainMenuItem>();
            _main_menu.Add(new MainMenuItem { Item_name = "home", Item_url = "/", Item_id = 1, Item_tag = "_hm_" });
            _main_menu.Add(new MainMenuItem { Item_name = "basket", Item_url = "/basket", Item_id = 2, Item_tag = "_bsk_" });

            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.CatalogMenu_Get");
            db.AddParameter(new SqlParameter("@category_id", 0));
            _catalog_menu = db.Query<CatalogMenuItem>();
        }

        public List<MainMenuItem> GetMainMenuItems()
        {
            return _main_menu;
        }

        public List<CatalogMenuItem> GetCatalogMenuItems(int category_id)
        {
            List<CatalogMenuItem> revtal = _catalog_menu.Where(
                item =>
                item.Category_id == category_id
                ).ToList();
            return revtal;
        }

        public CatalogMenuItem GetCatalogMenuItemByCategoryBrandSeria(string category_name, string brand_name, string seria_name)
        {
            CatalogMenuItem revtal = _catalog_menu.Where(
                item =>
                item.Category_name.Equals(category_name)
                && item.Brand_name.Equals(brand_name)
                && item.Seria_name.Equals(seria_name)).FirstOrDefault();
            return revtal;
        }

        public CatalogMenuItem GetCatalogMenuItemByCategoryBrand(string category_name, string brand_name)
        {
            CatalogMenuItem revtal = _catalog_menu.Where(
                item =>
                item.Category_name.Equals(category_name)
                && item.Brand_name.Equals(brand_name)).FirstOrDefault();
            return revtal;
        }

        public CatalogMenuItem GetCatalogMenuItemByCategory(string category_name)
        {
            CatalogMenuItem revtal = _catalog_menu.Where(
                item =>
                item.Category_name.Equals(category_name)).FirstOrDefault();
            return revtal;
        }

        public CatalogMenuItem GetCatalogMenuItemById(int menu_id)
        {
            return _catalog_menu.Where(item => item.Item_id == menu_id).FirstOrDefault();
        }

        public MainMenuItem GetMainMenuItemById(int menu_id)
        {
            return _main_menu.Where(item => item.Item_id == menu_id).FirstOrDefault();
        }

    }
}

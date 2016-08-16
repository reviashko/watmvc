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
            _main_menu.Add(new MainMenuItem { Item_name = "home", Item_url = "/", Item_id = 1 });
            _main_menu.Add(new MainMenuItem { Item_name = "basket", Item_url = "/basket", Item_id = 2 });

            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.CatalogMenu_Get");
            _catalog_menu =  db.Query<CatalogMenuItem>();
    }

        public List<MainMenuItem> GetMainMenuItems()
        {
            return _main_menu;
        }

        public List<CatalogMenuItem> GetCatalogMenuItems()
        {
            return _catalog_menu;
        }

        public CatalogMenuItem GetCatalogMenuItemByAttr(string brand_name, string seria_name)
        {
            foreach (CatalogMenuItem item in _catalog_menu)
            {
                if (item.Brand_name.Equals(brand_name) && item.Seria_name.Equals(seria_name))
                    return item;
            }

            return new CatalogMenuItem();
        }

        public CatalogMenuItem GetCatalogMenuItemById(int menu_id)
        {
            foreach(CatalogMenuItem item in _catalog_menu)
            {
                if (item.Item_id == menu_id)
                    return item;

            }

            return new CatalogMenuItem();
        }

        public MainMenuItem GetMainMenuItemById(int menu_id)
        {
            foreach (MainMenuItem item in _main_menu)
            {
                if (item.Item_id == menu_id)
                    return item;

            }

            return new MainMenuItem();
        }

    }
}

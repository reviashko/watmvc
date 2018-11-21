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
            db.SetStoredProcedure("MVCWeb.CatalogsMenu_GetItems");
            _catalog_menu = db.Query<CatalogMenuItem>();
        }

        public List<MainMenuItem> GetMainMenuItems()
        {
            return _main_menu;
        }

        public MainMenuItem GetMainMenuItemById(int menu_id)
        {
            return _main_menu.Where(item => item.Item_id == menu_id).FirstOrDefault();
        }

        public List<CatalogMenuItem> GetCatalogMenuItems(int menu_pid)
        {
            return _catalog_menu;
        }

        public CatalogMenuItem GetCatalogMenuItemById(int menu_id)
        {
            CatalogMenuItem retval = _catalog_menu.Where(
                item =>
                item.Menu_id == menu_id
                ).ToList().FirstOrDefault();
            return retval;
        }

        public CatalogMenuItem GetCatalogMenuItemByUrl(string menu_url)
        {
            CatalogMenuItem retval = _catalog_menu.Where(
                item =>
                item.Menu_url.ToLower().Equals(menu_url)
                ).ToList().FirstOrDefault();
            return retval;
        }

    }
}

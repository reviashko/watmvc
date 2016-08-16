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

        public List<MainMenuItem> GetMainMenuItems()
        {
            // кеш тута
            return _menuRepository.GetMainMenuItems();
        }

        public List<CatalogMenuItem> GetCatalogMenuItems()
        {
            // кеш тута
            return _menuRepository.GetCatalogMenuItems();
        }

        public CatalogMenuItem GetCatalogMenuItemByAttr(string brand_name, string seria_name)
        {
            return _menuRepository.GetCatalogMenuItemByAttr(brand_name, seria_name);
        }

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

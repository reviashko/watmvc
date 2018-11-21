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

        public MainMenuItem GetMainMenuItemById(int menu_id)
        {
            return _menuRepository.GetMainMenuItemById(menu_id);
        }


        public List<CatalogMenuItem> GetCatalogMenuItems(int menu_pid)
        {
            // кеш тута
            return _menuRepository.GetCatalogMenuItems(menu_pid);
        }
        public CatalogMenuItem GetCatalogMenuItemByUrl(string menu_url)
        {
            // кеш тута
            return _menuRepository.GetCatalogMenuItemByUrl(menu_url);
        }

        public CatalogMenuItem GetCatalogMenuItemById(int menu_id)
        {
            // кеш тута
            return _menuRepository.GetCatalogMenuItemById(menu_id);
        }

    }
}

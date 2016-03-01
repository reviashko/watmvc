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

        public List<MenuItem> GetMainMenuItems()
        {
            // кеш тута
            return _menuRepository.GetMainMenuItems();
        }

        public List<MenuItem> GetLeftMenuItems()
        {
            // кеш тута
            return _menuRepository.GetLeftMenuItems();
        }
    }
}

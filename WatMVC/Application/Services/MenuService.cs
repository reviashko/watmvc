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
    public class MenuService
    {

        public List<MenuItem> GetMenuItems()
        {
            var pr = new MenuRepository();
            // кеш тута
            return pr.GetMenuItems();
        }

    }
}

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
    public interface IMenuService
    {
        List<MainMenuItem> GetMainMenuItems();
        List<CatalogMenuItem> GetCatalogMenuItems();
        CatalogMenuItem GetCatalogMenuItemByAttr(string brand_name, string seria_name);
        CatalogMenuItem GetCatalogMenuItemById(int menu_id);
        MainMenuItem GetMainMenuItemById(int menu_id);

    }
}

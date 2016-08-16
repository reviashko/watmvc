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
    public interface IMenuRepository
    {
        List<MainMenuItem> GetMainMenuItems();
        List<CatalogMenuItem> GetCatalogMenuItems();
        CatalogMenuItem GetCatalogMenuItemById(int menu_id);
        MainMenuItem GetMainMenuItemById(int menu_id);
        CatalogMenuItem GetCatalogMenuItemByAttr(string brand_name, string seria_name);

    }
}

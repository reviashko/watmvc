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
        MainMenuItem GetMainMenuItemById(int menu_id);


        List<CatalogMenuItem> GetCatalogMenuItems(int menu_pid);
        CatalogMenuItem GetCatalogMenuItemByUrl(string menu_url);
        CatalogMenuItem GetCatalogMenuItemById(int menu_id);


    }
}

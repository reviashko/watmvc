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
        List<MenuItem> GetMainMenuItems();
        List<MenuItem> GetLeftMenuItems();

    }
}

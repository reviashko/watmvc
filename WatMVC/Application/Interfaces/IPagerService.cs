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
    public interface IPagerService
    {
        int GetPagerSize();
        int GetPageSize();
        List<PagerItem> GetPagerItems(int items_count, int page_size, int current_page, int visible_count);
    }
}

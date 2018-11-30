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
    public class PagerService : IPagerService
    {
        public PagerService()
        {

        }

        public int GetPageSize()
        {
            return 3;
        }

        public int GetPagerSize()
        {
            return 10;
        }

        public List<PagerItem> GetPagerItems(int items_count, int page_size, int current_page, int visible_count)
        {
            List<PagerItem> retval = new List<PagerItem>();

            var pages_count = items_count / page_size;
            if(items_count % page_size > 0)
            {
                pages_count++;
            }

            if(pages_count <= 1)
            {
                return retval;
            }

            var start_index = current_page - visible_count / 2;

            if(start_index < 1)
            {
                start_index = 1;
            }

            var finish_index = start_index + visible_count;
            if(pages_count < finish_index)
            {
                finish_index = pages_count;
            }

            for (int index = start_index; index <= finish_index; index++)
            {
                retval.Add(new PagerItem() { Is_current = (current_page == index), Page_id = index, Page_name = index.ToString() });
            }

            return retval;
        }

    }
}

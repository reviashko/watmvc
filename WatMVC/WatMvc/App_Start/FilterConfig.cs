using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System;


namespace WatMvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            var list = new List<int>();
            var result = list.Where(item => item > 100).Select(item => item.ToString()).OrderBy(item => item).ToList();
            var result2 = (from item in list
                                    where item > 100
                                    orderby item ascending
                                    select item.ToString()).ToList();
                                   
            

            Proccessing(Do, 5);
            Proccessing(Do2, 10);

            filters.Add(new HandleErrorAttribute());
        }

        private static int Do(int x)
        {
            return x * x;
        }

        private static int Do2(int x)
        {
            return x * x * x;
        }

        private static int Proccessing(Func<int, int> func, int x)
        {
            return func(x) + 100;
        }
    }
}

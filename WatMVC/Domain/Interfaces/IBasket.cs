using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace Domain
{
    public interface IBasket
    {
        int Quantity { get; set; }
        decimal Sum { get; set; }
        decimal Weight { get; set; }
        DataTable bskSrc { get; set; }

        void FillTotals();
        int Update(List<IBasketItem> items);
        int Add(List<IBasketItem> items);
        int Delete(List<IBasketItem> items);
        void Clear();
        List<IBasketItem> Get();
    }
}

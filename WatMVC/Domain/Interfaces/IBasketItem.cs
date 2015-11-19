using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public interface IBasketItem
    {

        int Price_id { get; set; }

        int Quantity { get; set; }

        decimal Weight { get; set; }

        decimal Cost { get; set; }

        string Name { get; set; }

        char Separator { get; set; }

        void setItemValues(int price_id, int quantity, decimal weight, decimal cost, string name, char separator);
    }
}

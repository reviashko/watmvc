using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Domain.Entities
{
    public class dev_basket_item
    {
        private char _basketItemSeparator = '-';

        private int _price_id = 0;
        public int Price_id
        {
            get { return _price_id; }
            set { _price_id = value; }
        }

        private int _quantity = 0;
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public string CookieItem
        {
            get { return string.Format("{0}{1}{2}", _price_id, _basketItemSeparator, _quantity); }
        }

        public dev_basket_item()
        {
            //
        }

        public dev_basket_item(string cookie)
        {
            string[] src = cookie.Split(_basketItemSeparator);
            if (src.Length == 2)
            {
                int.TryParse(src[0], out _price_id);
                int.TryParse(src[1], out _quantity);
            }
        }

        public dev_basket_item(int price_id, int quantity)
        {
            _price_id = price_id;
            _quantity = quantity;
        }

    }
}
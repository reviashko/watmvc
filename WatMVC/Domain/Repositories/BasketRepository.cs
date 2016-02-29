using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using Domain.Entities;

namespace Domain
{
    public class BasketRepository
    {
        private IBasket bsk;

        public int Quantity
        {
            get { return bsk.Quantity; }
        }

        public decimal Sum
        {
            get { return bsk.Sum; }
        }

        public decimal Weight
        {
            get { return bsk.Weight; }
        }

        public BasketRepository(int user_id)
        {
            MSSql msdb = new MSSql();

            if (user_id > 0)
            {
                bsk = new UserBasket(user_id, msdb);
            }
            else
            {
                //bsk = new SessionBasket(msdb, new dev_cookie_mng(), dev_const.SiteUrl, 30);
                
            }
        }

        public void FillTotals()
        {
            bsk.FillTotals();
        }

        public int UpdItems(List<IBasketItem> items)
        {
            return bsk.Update(items);
        }

        public int AddItems(List<IBasketItem> items)
        {
            return bsk.Add(items);
        }

        public int DelItems(List<IBasketItem> items)
        {
            return bsk.Delete(items);
        }

        public decimal GetRowTotal(int price_id)
        {
            if (price_id > 0)
            {
                foreach (IBasketItem it in bsk.Get())
                {
                    if (it.Price_id.Equals(price_id))
                    {
                        return it.Quantity * it.Cost;
                    }
                }
            }

            return 0;
        }

        public List<IBasketItem> GetBasket()
        {
            return bsk.Get();
        }

        public DataTable GetBasketSrc()
        {
            return bsk.bskSrc;
        }

        public void ClearBasket()
        {
            bsk.Clear();
        }
    }
}
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace Domain
{
    abstract public class ABasket : IBasket
    {
        //internal basket vars
        public int Quantity { get; set; }
        public decimal Sum { get; set; }
        public decimal Weight { get; set; }
        protected List<IBasketItem> basketItems;
        public DataTable bskSrc { get; set; }

        public ABasket()
        {
            Quantity = 0;
            Sum = 0;
            Weight = 0;
            this.basketItems = new List<IBasketItem>();
        }

        public void FillTotals()
        {
            Quantity = 0;
            Weight = Sum = 0;

            foreach (IBasketItem item in basketItems)
            {
                Quantity = Quantity + item.Quantity;
                Weight = Weight + item.Weight;
                Sum = Sum + item.Cost * item.Quantity;
            }
        }

        public int Update(List<IBasketItem> items)
        {
            foreach (IBasketItem forUpdate in items)
            {
                if (!UpdateBasketItem(forUpdate))
                {
                    AddBasketItem(forUpdate);
                }
            }

            return Save();
        }

        private bool UpdateBasketItem(IBasketItem item)
        {
            int itemPosition = GetItemIndex(item);

            if (itemPosition < 0)
            {
                return false;
            }

            IBasketItem tmp_item = basketItems[itemPosition];
            tmp_item.Quantity = item.Quantity;
            basketItems[itemPosition] = tmp_item;

            return true;
        }

        public void Clear()
        {
            basketItems.Clear();
            Save();
        }

        public int Add(List<IBasketItem> items)
        {
            foreach (IBasketItem forAdd in items)
            {
                AddBasketItem(forAdd);
            }

            return Save();
        }

        private void AddBasketItem(IBasketItem item)
        {
            int itemPosition = GetItemIndex(item);

            if (itemPosition < 0)
            {
                basketItems.Add(item);
            }
            else
            {
                IBasketItem bskItem = basketItems[itemPosition];
                bskItem.setItemValues(bskItem.Price_id, bskItem.Quantity + item.Quantity, bskItem.Weight, bskItem.Cost, bskItem.Name, '-');
                basketItems[itemPosition] = bskItem;
            }
        }

        private int GetItemIndex(IBasketItem item)
        {
            int currentIndex = -1;

            foreach (IBasketItem it in basketItems)
            {
                currentIndex++;

                if (it.Price_id.Equals(item.Price_id))
                {
                    return currentIndex;
                }
            }

            return -1;
        }

        public int Delete(List<IBasketItem> items)
        {
            int deletedCount = 0;

            foreach (IBasketItem forDelete in items)
            {
                for (int delIndex = basketItems.Count - 1; delIndex > -1; delIndex--)
                {
                    if (basketItems[delIndex].Price_id.Equals(forDelete.Price_id))
                    {
                        basketItems.Remove(basketItems[delIndex]);
                        deletedCount++;
                    }
                }
            }

            return deletedCount > 0 ? Save() : 0;
        }

        public List<IBasketItem> Get()
        {
            return basketItems;
        }

        abstract protected int Save();

        abstract protected bool InitBasket();
    }

    public class SessionBasket : ABasket
    {
        private string initData;

        private IDataBase db;

        private ICookieManager cmng;

        private string cookieName;

        private int cookieExpiration;

        public SessionBasket(IDataBase db, ICookieManager cmng, string siteName, int cookieExpiration)
            : base()
        {
            this.cmng = cmng;
            this.db = db;
            this.cookieName = string.Format("Cookie_{0}", siteName);
            this.cookieExpiration = cookieExpiration;
            this.initData = cmng.ReadCookie(this.cookieName);

            if (InitBasket())
            {
                FillTotals();
            }
        }

        override protected int Save()
        {
            StringBuilder retval = new StringBuilder();
            foreach (IBasketItem item in basketItems)
            {
                retval.AppendFormat("{0}-{1}_", item.Price_id, item.Quantity);
            }

            cmng.WriteCookie(this.cookieName, retval.ToString(), this.cookieExpiration);

            return basketItems.Count;
        }

        override protected bool InitBasket()
        {
            DataTable queryData = new DataTable();
            queryData.Columns.Add(new DataColumn("price_id"));
            queryData.Columns.Add(new DataColumn("quantity"));

            string[] cookieData = initData.Split('_');
            if (cookieData != null && cookieData.Length > 0)
            {
                try
                {
                    foreach (string item in cookieData)
                    {
                        string[] cookieItem = item.Split('-');
                        if (cookieItem != null && cookieItem.Length == 2)
                        {
                            DataRow dr = queryData.NewRow();
                            dr["price_id"] = int.Parse(cookieItem[0]);
                            dr["quantity"] = int.Parse(cookieItem[1]);
                            queryData.Rows.Add(dr);
                        }
                    }
                }
                catch
                {
                    queryData.Rows.Clear();
                }
            }

            if (queryData.Rows.Count > 0)
            {
                db.ClearParams();
                db.AddParameter(new SqlParameter("@Dt", queryData));
                db.SetStoredProcedure("Basket.GetByDT");
                bskSrc = db.GetDataTable();

                if (bskSrc != null && bskSrc.Rows.Count > 0)
                {
                    basketItems.Clear();

                    foreach (DataRow dr in bskSrc.Rows)
                    {
                        IBasketItem bit = new BasketItem();
                        bit.setItemValues(int.Parse(dr["price_id"].ToString())
                                               , int.Parse(dr["quantity"].ToString())
                                               , decimal.Parse(dr["weight"].ToString())
                                               , decimal.Parse(dr["price"].ToString())
                                               , string.Empty
                                               , ';');
                        basketItems.Add(bit);
                    }
                }
            }

            return basketItems.Count > 0;

        }
    }

    public class UserBasket : ABasket
    {
        private int userID;

        private IDataBase db;

        public UserBasket(int userID, IDataBase db)
            : base()
        {
            this.userID = userID;
            this.db = db;

            if (InitBasket())
            {
                FillTotals();
            }
        }

        override protected int Save()
        {
            DataTable src = new DataTable();
            src.Columns.Add(new DataColumn("price_id"));
            src.Columns.Add(new DataColumn("quantity"));

            foreach (IBasketItem item in basketItems)
            {
                DataRow dr = src.NewRow();
                dr["price_id"] = item.Price_id;
                dr["quantity"] = item.Quantity;
                src.Rows.Add(dr);
            }

            db.ClearParams();
            db.AddParameter(new SqlParameter("@User_id", userID));
            db.AddParameter(new SqlParameter("@Dt", src));
            db.SetStoredProcedure("Basket.UpdateFromDT_V2");
            return db.GetReturnValue<int>();
        }

        override protected bool InitBasket()
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@User_id", userID));
            db.SetStoredProcedure("Basket.Get");
            bskSrc = db.GetDataTable();

            if (bskSrc != null && bskSrc.Rows.Count > 0)
            {
                basketItems.Clear();

                foreach (DataRow dr in bskSrc.Rows)
                {
                    IBasketItem bit = new BasketItem();
                    bit.setItemValues(int.Parse(dr["price_id"].ToString())
                                         , int.Parse(dr["quantity"].ToString())
                                         , decimal.Parse(dr["weight"].ToString())
                                         , decimal.Parse(dr["price"].ToString())
                                         , string.Empty
                                         , ';');
                    basketItems.Add(bit);
                }
            }

            return basketItems.Count > 0;
        }
    }

    public class BasketItem : IBasketItem
    {
        public int Price_id { get; set; }

        public int Quantity { get; set; }

        public decimal Weight { get; set; }

        public decimal Cost { get; set; }

        public string Name { get; set; }

        public char Separator { get; set; }

        public void setItemValues(int price_id, int quantity, decimal weight, decimal cost, string name, char separator)
        {
            Price_id = price_id;
            Quantity = quantity;
            Weight = weight;
            Cost = cost;
            Name = name;
            Separator = separator;
        }

        public BasketItem() { }
    }
}
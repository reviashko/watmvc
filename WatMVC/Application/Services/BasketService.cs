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
    public class BasketService : IBasketService
    {
        IBasketRepository _basketRepository;

        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public List<BasketItem> Get(int client_id)
        {
            // кеш тута
            return _basketRepository.Get(client_id);
        }

        public List<int> GetArticuls(int client_id)
        {
            List<BasketItem> basketItems = Get(client_id);
            List<int> retval = new List<int>();

            foreach(BasketItem item in basketItems)
            {
                retval.Add(item.Articul);
            }

            return retval;
        }

        public bool Add(int client_id, int articul, byte count)
        {
            return _basketRepository.Add(client_id, articul, count);
        }

        public bool Remove(int client_id, int articul)
        {
            return _basketRepository.Remove(client_id, articul);
        }

        public int SaveOrder(int client_id, string pay_type)
        {
            return _basketRepository.SaveOrder(client_id, pay_type);
        }
    }
}

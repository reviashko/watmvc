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

        public List<Product> Get(int client_id)
        {
            // кеш тута
            return _basketRepository.Get(client_id);
        }

        public bool Add(int client_id, int goods_id, byte count)
        {
            return _basketRepository.Add(client_id, goods_id, count);
        }

        public bool Remove(int client_id, int basket_id)
        {
            return _basketRepository.Remove(client_id, basket_id);
        }

    }
}

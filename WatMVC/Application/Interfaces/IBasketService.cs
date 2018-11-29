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
    public interface IBasketService
    {
        List<BasketItem> Get(int client_id);
        bool Add(int client_id, int articul, byte count);
        bool Remove(int client_id, int basket_id);
        int SaveOrder(int client_id, string pay_type);
        List<int> GetArticuls(int client_id);

    }
}

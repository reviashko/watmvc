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
    public interface IBasketRepository
    {
        bool Add(int client_id, int articul, byte count);
        List<BasketItem> Get(int client_id);
        bool Remove(int client_id, int articul);
        int SaveOrder(int client_id, string pay_type);
    }
}
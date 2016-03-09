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
        bool Add(int client_id, int goods_id, byte count);

        List<Product> Get(int client_id);
    }
}
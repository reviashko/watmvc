using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Domain
{
    public interface ICookieManager
    {
        int CookieExpire { get; }
        bool WriteCookie(string name, string data, int days);
        string ReadCookie(string key);
    }
}

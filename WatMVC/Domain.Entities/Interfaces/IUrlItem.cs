using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Domain.Entities
{
    public interface IUrlItem
    {

        int getId();

        string getVirtualUrl();

        string getRealUrl();

        int getUrlType();

        string get301Url();

        string getParams();

        byte getFilterMap();

        byte getUrlKind();

        void initValues(int id, string virtualUrl, int urlType, string u301Url, string prms, byte filterMap, byte urlKind);
    }
}

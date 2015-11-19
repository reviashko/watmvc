using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Domain.Entities
{
    public class UrlItem : IUrlItem
    {
        private int _id;
        private string _virtualUrl;
        private int _urlType;
        private string _301Url;
        private string _params;
        private byte _filterMap;
        private byte _urlKind;

        public UrlItem()
        {
            _id = 0;
            _virtualUrl = "";
            _urlType = 0;
            _301Url = "";
            _params = "";
            _filterMap = 0;
            _urlKind = 0;
        }

        public int getId()
        {
            return _id;
        }

        public string getVirtualUrl()
        {
            return _virtualUrl;
        }

        public string getRealUrl()
        {
            return string.Format("/{0}.aspx?id={1}{2}{3}{4}{5}"
                        , _urlType == 0 ? "detail" : "catalog"
                        , _id
                        , string.Format("&urlType={0}", _urlType)
                        , _params
                        , string.Format("&filterMap={0}", _filterMap)
                        , string.Format("&urlKind={0}", _urlKind)
                        );
        }

        public string get301Url()
        {
            return _301Url;
        }

        public int getUrlType()
        {
            return _urlType;
        }

        public string getParams()
        {
            return _params;
        }

        public byte getFilterMap()
        {
            return _filterMap;
        }

        public byte getUrlKind()
        {
            return _urlKind;
        }

        public void initValues(int id, string virtualUrl, int urlType, string u301Url, string prms, byte filterMap, byte urlKind)
        {
            this._id = id;
            this._virtualUrl = virtualUrl.ToLower();
            this._urlType = urlType;
            this._301Url = u301Url;
            this._params = prms;
            this._filterMap = filterMap;
            this._urlKind = urlKind;
        }
    }
}
using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Data.SqlClient;
using Domain.Entities;

namespace Domain
{
    public class UrlManager : IUrlManager
    {
        private List<IUrlItem> _urls;
        private List<IUrlItem> _u301;
        private List<string> _excludeStartsUrls;
        private List<string> _excludeContainsUrls;
        static private UrlManager _instance;

        private UrlManager(IDataBase db)
        {

            if (this._excludeStartsUrls == null)
            {
                _excludeStartsUrls = new List<string>();
                _excludeStartsUrls.Add("img/");
                _excludeStartsUrls.Add("gimg/");
                _excludeStartsUrls.Add("css/");
                _excludeStartsUrls.Add("js/");
            }

            if (this._excludeContainsUrls == null)
            {
                _excludeContainsUrls = new List<string>();
                _excludeContainsUrls.Add(".axd");
                _excludeContainsUrls.Add(".ico");
                _excludeContainsUrls.Add("404.aspx");
                _excludeContainsUrls.Add("500.aspx");
                _excludeContainsUrls.Add(".asmx");
            }

            if (this._u301 == null)
            {
                this._u301 = new List<IUrlItem>();
                db.ClearParams();
                db.SetStoredProcedure("WebSite.Get301Urls");
                DataTable src = db.GetDataTable();

                if (src != null && src.Rows.Count > 0)
                {
                    foreach (DataRow dr in src.Rows)
                    {
                        IUrlItem uit = new UrlItem();
                        uit.initValues(
                            0
                            , dr["url"].ToString().ToLower()
                            , 0
                            , dr["url301"].ToString().ToLower()
                            , ""
                            , 0
                            , 0
                            );

                        this._u301.Add(uit);
                    }
                }
            }

            if (this._urls == null)
            {
                this._urls = new List<IUrlItem>();

                db.ClearParams();
                db.SetStoredProcedure("WebSite.GetRedirectUrl");
                DataTable src = db.GetDataTable();

                if (src != null && src.Rows.Count > 0)
                {
                    foreach (DataRow dr in src.Rows)
                    {
                        IUrlItem uit = new UrlItem();
                        uit.initValues(
                            int.Parse(dr["id"].ToString())
                            , dr["url_name"].ToString().ToLower()
                            , int.Parse(dr["ctype_id"].ToString())
                                        , ""
                            , dr["params"].ToString().ToLower()
                            , byte.Parse(dr["filter_map"].ToString())
                            , byte.Parse(dr["url_kind"].ToString())
                            );

                        this._urls.Add(uit);
                    }
                }
            }
        }

        static public void Reset()
        {
            _instance = null;
        }

        static public UrlManager getInstance(IDataBase db)
        {
            if (_instance == null)
            {
                _instance = new UrlManager(db);
            }

            return _instance;
        }

        public string Check301(string url)
        {
            bool contains = false;
            foreach (string str in _excludeContainsUrls)
            {
                if (url.Contains(str))
                {
                    contains = true;
                }
            }

            bool starts = false;
            if (!contains)
            {
                foreach (string str in _excludeStartsUrls)
                {
                    if (url.StartsWith(str))
                    {
                        starts = true;
                    }
                }
            }

            if (!contains && !starts)
            {
                foreach (IUrlItem it in _u301)
                {
                    if (it.getVirtualUrl().Equals(url))
                    {
                        return it.get301Url();
                    }
                }
            }
            return url;
        }

        public string GetNewGoodsUrl(int link_id)
        {
            foreach (IUrlItem url in _urls)
            {
                if (url.getId() == link_id && url.getUrlType() == 1)
                    return url.getVirtualUrl();
            }

            return "/";
        }

        public string GetVirtualUrl(int id, byte urlKind)
        {
            foreach (IUrlItem url in _urls)
            {
                if (url.getId() == id && url.getUrlKind() == urlKind)
                    return url.getVirtualUrl();
            }

            return "/";
        }

        public string GetRealUrl(string virtualUrl)
        {
            string tmp = virtualUrl.ToLower();

            foreach (IUrlItem url in _urls)
            {
                if (url.getVirtualUrl().Equals(tmp))
                    return url.getRealUrl();
            }

            return "/404.aspx";
        }

        public string GetParamUrl(string requestPath)
        {
            string qs = "";
            string url = requestPath;
            bool is301error = false;

            if (requestPath.Equals("/"))
                return "/default.aspx";

            int tmp_pos = requestPath.IndexOf("?");
            if (tmp_pos > 0)
            {
                qs = requestPath.Substring(tmp_pos + 1);
                url = requestPath.Replace("?" + qs, "");
            }

            //throw new Exception(requestPath);

            if (requestPath.Contains("/find.aspx"))
            {
                string[] temp = requestPath.Split('/');
                if (temp.Length == 4)
                    return string.Format("/{0}?id={1}&seo={2}", temp[3], temp[1], temp[2]);

                if (temp.Length == 5)
                    return string.Format("/{0}?id={1}&page={2}&seo={3}", temp[4], temp[1], temp[3], temp[2]);
            }

            // check for 301
            url = Check301(url.ToLower());
            if (!requestPath.Contains(url))
            {
                is301error = true;
            }


            // custom 301 for old goods urls
            if (url.Contains(".asmx") && !url.Contains("wildservice"))
            {
                int link_id = int.Parse(url.Substring(1).Replace(".asmx", ""));

                is301error = true;
                url = GetNewGoodsUrl(link_id);
            }

            bool lastIsSlash = url.Substring(url.Length - 1, 1).Equals("/");
            if (!lastIsSlash)
            {
                url = url + "/";
                is301error = true;
            }

            string vurl = GetRealUrl(url);
            //throw new Exception(vurl);

            if (vurl.Contains("/catalog.aspx") || vurl.Contains("/detail.aspx"))
            {
                qs = (vurl.IndexOf("?") > 0 ? "" : "?par=0") + (qs.Length > 0 ? string.Format("&{0}", qs) : "") + (is301error ? ("&e301=" + url) : "");
                return vurl + qs;
            }

            return requestPath;
        }
    }
}
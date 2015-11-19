using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Domain.Entities
{
    public class dev_page_metadata
    {
        public int Id { get; set; }

        public string BotArticle { get; set; }

        public string TopArticle { get; set; }

        public string MidArticle { get; set; }

        public string MobileArticle { get; set; }

        public string Addr { get; set; }

        public string Keywords { get; set; }

        public string Title { get; set; }

        public bool IsTag { get; set; }

        public string Descr { get; set; }

        public bool Exists
        {
            get { return Id > 0; }
        }

        public dev_page_metadata()
        {
            Id = 0;
            Addr = "";
            TopArticle = "";
            MidArticle = "";
            BotArticle = "";
            MobileArticle = "";
            Keywords = "";
            Title = "";
            Descr = "";
            IsTag = false;
        }

        public dev_page_metadata(DataRow row)
        {
            int tmp_id = 0;
            int.TryParse(row["id"].ToString(), out tmp_id);
            Id = tmp_id;

            Addr = row["addr"].ToString();
            TopArticle = row["top_art"].ToString();
            MidArticle = "";
            BotArticle = row["bot_art"].ToString();
            MobileArticle = row["mobile_article"].ToString();
            Keywords = row["keywords"].ToString();
            Title = row["title"].ToString();
            Descr = row["descr"].ToString();
            IsTag = bool.Parse(row["isTag"].ToString());
        }

    }
}
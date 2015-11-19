using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain
{

    public class dev_xml_goods
    {

        public int Link_id { get; set; }

        public decimal Weight { get; set; }

        public byte Sale { get; set; }

        public int Brand_id { get; set; }

        public string Brand_name { get; set; }

        public string Name { get; set; }

        public string Descr { get; set; }

        public string Descr_Seo { get; set; }

        public int Group_id { get; set; }

        public int Root { get; set; }

        public int Consists_id { get; set; }

        public string Consists_name { get; set; }

        public int Country_id { get; set; }

        public string Country_name { get; set; }

        public int Gtype_id { get; set; }

        public string Gtype_name { get; set; }

        public bool HasImg { get; set; }

        public int Seria_id { get; set; }

        public int Sound { get; set; }

        public string UT_video { get; set; }

        public int Info { get; set; }

        public string Seria_name { get; set; }

        public List<dev_xml_subgoods> Items { get; set; }

        public List<dev_xml_mechs> Mechs { get; set; }

        public List<dev_xml_sames> Sames { get; set; }

        public dev_xml_goods()
        {
            Link_id = 0;
            Weight = 0;
            Sale = 0;
            Brand_id = 0;
            Brand_name = "";
            Name = "";
            Descr = "";
            Descr_Seo = "";
            Group_id = 0;
            Root = 0;
            Sound = 0;
            UT_video = "";
            Consists_id = 0;
            Consists_name = "";
            Country_id = 0;
            Country_name = "";
            Gtype_id = 0;
            Gtype_name = "";
            Seria_id = 0;
            Seria_name = "";
            HasImg = false;
            Items = new List<dev_xml_subgoods>();
            Mechs = new List<dev_xml_mechs>();
            Sames = new List<dev_xml_sames>();
        }
    }
}

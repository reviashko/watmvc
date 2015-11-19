using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Domain.Entities
{
    public class dev_users
    {
        private int _id = 0;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private decimal _sale = 0;
        public decimal Sale
        {
            get { return _sale; }
            set { _sale = value; }
        }

        private bool _deleted = false;
        public bool Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }

        private string _login = "";
        public string Login
        {
            get { return _login; }
            set { _login = value.Length > 80 ? value.Substring(0, 79) : value; }
        }

        private string _email = "";
        public string Email
        {
            get { return _email; }
            set { _email = value.Length > 50 ? value.Substring(0, 50) : value; }
        }

        private string _pswd = "";
        public string Pswd
        {
            get { return _pswd; }
            set { _pswd = value.Length > 40 ? value.Substring(0, 40) : value; }
        }

        private string _fname = "";
        public string Fname
        {
            get { return _fname; }
            set { _fname = value.Length > 25 ? value.Substring(0, 25) : value; }
        }

        private string _lname = "";
        public string Lname
        {
            get { return _lname; }
            set { _lname = value.Length > 25 ? value.Substring(0, 25) : value; }
        }

        private string _mname = "";
        public string Mname
        {
            get { return _mname; }
            set { _mname = value.Length > 25 ? value.Substring(0, 25) : value; }
        }

        private string _adres = "";
        public string Adres
        {
            get { return _adres; }
            set { _adres = value.Length > 255 ? value.Substring(0, 255) : value; }
        }

        private string _dop_info = "";
        public string Dop_info
        {
            get { return _dop_info; }
            set { _dop_info = value.Length > 255 ? value.Substring(0, 255) : value; }
        }

        private string _phone = "";
        public string Phone
        {
            get { return _phone; }
            set { _phone = value.Length > 16 ? value.Substring(0, 16) : value; }
        }

        private DateTime _lastVisit = DateTime.Now;
        public DateTime LastVisit
        {
            get { return _lastVisit; }
        }

        private DateTime _regdate = DateTime.Now;
        public DateTime Regdate
        {
            get { return _regdate; }
        }

        public dev_users()
        {
            //
        }

        public dev_users(DataRow row)
        {
            _id = Convert.ToInt32(row["id"].ToString());
            _deleted = Convert.ToBoolean(row["deleted"].ToString());
            _email = row["email"].ToString();
            _pswd = row["pswd"].ToString();
            _fname = row["fname"].ToString();
            _mname = row["mname"].ToString();
            _lname = row["lname"].ToString();
            _adres = row["adres"].ToString();
            _dop_info = row["dop_info"].ToString();
            _phone = row["phone"].ToString();
            _login = row["login"].ToString();
            _lastVisit = Convert.ToDateTime(row["lastvisit"].ToString());
            _regdate = Convert.ToDateTime(row["regdate"].ToString());
            _sale = decimal.Parse(row["sale"].ToString());
        }
    }
}
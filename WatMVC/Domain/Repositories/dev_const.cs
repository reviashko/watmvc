using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Domain.Entities
{
    public class dev_const
    {
        public static string CatalogTemplate
        {
            get
            {
                return ConfigurationManager.AppSettings["CatalogTemplate"];
            }
        }

        public static string BrandTemplate
        {
            get
            {
                return ConfigurationManager.AppSettings["BrandTemplate"];
            }
        }

        public static int QiwiID
        {
            get
            {
                string tmp = ConfigurationManager.AppSettings["qiwiID"];
                int tmp_int = 0;
                int.TryParse(tmp, out tmp_int);
                return tmp_int;
            }
        }


        public static int NDS
        {
            get
            {
                int tmp_int = 0;
                int.TryParse(ConfigurationManager.AppSettings["NDS"], out tmp_int);

                return tmp_int;
            }
        }

        public static bool SaveStatistic
        {
            get
            {
                return false;
            }
        }


        public static string SessionCatalogPathKey
        {
            get
            {
                return ConfigurationManager.AppSettings["SessionCatalogPathKey"];
            }
        }

        public static string ProductTemplate
        {
            get
            {
                return ConfigurationManager.AppSettings["ProductTemplate"];
            }
        }

        public static string SiteUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["SiteUrl"];
            }
        }

        public static string TechnicalSupportPhone
        {
            get
            {
                return string.Format(dev_const.TechnicalSupportPhoneTemplate, " ");
            }
        }

        public static string TechnicalSupportPhoneTemplate
        {
            get
            {
                return ConfigurationManager.AppSettings["TechnicalSupportPhone"];
            }
        }

        public static string TechnicalSupportDopPhone
        {
            get
            {
                return string.Format(ConfigurationManager.AppSettings["TechnicalSupportDopPhone"], " ");
            }
        }

        public static string TechnicalSupportEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["TechnicalSupportEmail"];
            }
        }


        public static string ContactEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["ContactEmail"];
            }
        }


        private static string _loginPage = "loginForm.aspx";
        public static string LoginPage
        {
            get
            {
                return string.Format("{0}{1}", SiteUrl, _loginPage);
            }
        }

        public static string LoginPageWithBadAuth
        {
            get
            {
                return string.Format("{0}?ba=1", LoginPage);
            }
        }

        private static string _registrationPage = "regForm.aspx";
        public static string RegistrationPage
        {
            get
            {
                return _registrationPage;
            }
        }

        private static string _orderPage = "order.aspx";
        public static string OrderPage
        {
            get
            {
                return _orderPage;
            }
        }

        private static string _basketPage = "basket.aspx";
        public static string BasketPage
        {
            get
            {
                return _basketPage;
            }
        }

        public static string ClearRegistrationPage
        {
            get
            {
                return string.Format("{0}?clear=1", _registrationPage);
            }
        }

        private static string _userCabinetPage = "cabinet.aspx";
        public static string UserCabinetPage
        {
            get
            {
                return _userCabinetPage;
            }
        }

        public dev_const()
        {
            //
        }

    }
}
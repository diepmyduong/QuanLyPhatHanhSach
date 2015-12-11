using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class Constants
    {
        public class ID
        {
            public const string USERNAME = "idUsername";
            public const string PASSWORD = "idPassword";
            public const string REMEBER_LOGIN = "idRememberLogin";
            public const string CONFIRM_PASSWORD = "idConfirmPassword";
            public const string OLD_PASSWORD = "idOldPassword";
            public const string NEW_PASSWORD = "idNewPassword";
            public const string IS_ADMIN = "idIsAdmin";
        }
        public class SESSION
        {
            public const string USERNAME = "sessionUsername";
            public const string SHOPPING_CART = "sessionShoppingCart";
            public const string BOOKS = "sessionDataBooks";
            public const string PAGE_SIZE = "sessionPageSize";
        }
        public class TEMPDATA
        {
            public const string ERRORS = "tempDataErrors";
            public const string SUCCESS = "tempDataSuccess";
            public const string BOOKS = "tempDataBooks";
        }
        public class COOKIE
        {
            public const string BOOKS = "cookieBooks";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BIZ
{
    public class DataDisplayHelper
    {
        private static CultureInfo _culture;

        private static CultureInfo Culture
        {
            get
            {
                if(_culture == null)
                {
                    _culture = CultureInfo.GetCultureInfo("vi-VN");
                }
                return _culture;
            }
        }


        public static string displayMoney(decimal number) {
            return String.Format(Culture, "{0:c}", number);
        }

        public static string displayNumber(decimal number)
        {
            return String.Format("{0:0}", number);
        }

        public static string dislayShortDate(DateTime date)
        {
            return String.Format("{0:dd/MM/yyyy}", date);
        }

        public static string dislayMonth(DateTime date)
        {
            return String.Format("{0:MM/yyyy}", date);
        }
    }
}

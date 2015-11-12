using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BIZ
{
    public class FilterHelper
    {
        
        public static bool compare(dynamic a, dynamic b, string method, bool isString)
        {
            try
            {
                bool result;
                switch (method)
                {
                    case "=":
                        if (isString)
                        {
                            result = a.ToLower().Contains(b.ToLower());
                        }
                        else
                        {
                            result = a.Equals(b);
                        }
                        break;
                    case ">=":
                        result = ((dynamic)a >= (dynamic)b);
                        break;
                    case "<=":
                        result = ((dynamic)a <= (dynamic)b);
                        break;
                    case "!=":
                        if (isString)
                        {
                            result = !a.ToLower().Contains(b.ToLower());
                        }
                        else
                        {
                            result = !a.Equals(b);
                        }
                        break;
                    case ">":
                        result = ((dynamic)a > (dynamic)b);
                        break;
                    case "<":
                        result = ((dynamic)a < (dynamic)b);
                        break;
                    default:
                        return false;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static Dictionary<string,int> Monthes
        {
            get
            {
                var monthes = new Dictionary<string, int>();
                for(var i = 1; i <= 12; i++)
                {
                    monthes.Add( "Tháng " + i,i);
                }
                return monthes;
            }
        }

        public static Dictionary<string,int> Years
        {
            get
            {
                var years = new Dictionary<string, int>();
                for(var i = DateTime.Now.Year; i >= 1990; i--)
                {
                    years.Add(i.ToString(), i);
                }
                return years;
            }
        }
    }
}

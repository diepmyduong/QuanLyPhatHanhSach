using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;
using System.Text.RegularExpressions;

namespace Core.DAL
{
    public class LinhVucManager
    {
        public static partial class Properties
        {
            public const string MaSoLinhVuc = "Mã Số Lĩnh Vực";
            public const string TenLinhVuc = "Tên Lĩnh Vực";
            public const string Sach = "Sách";
            public const string TrangThai = "Trạng Thái";
        }
        public static List<LinhVuc> getAll()
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from lv in db.LINHVUCs
                                select new LinhVuc(lv);
                return linqQuery.ToList();
            };

        }
        public static List<LinhVuc> getAllALive()
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from lv in db.LINHVUCs
                                where lv.trangthai == null
                                select new LinhVuc(lv);
                return linqQuery.ToList();
            };

        }
        public static LinhVuc find(int masolinhvuc)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from lv in db.LINHVUCs
                                where lv.masolinhvuc.Equals(masolinhvuc)
                                select new LinhVuc(lv);
                return linqQuery.SingleOrDefault();
            };
        }
        public static List<LinhVuc> findBy(Dictionary<string,dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;

                var linqQuery = getAll()
                                 .Where(lv => lv.MaSoLinhVuc.Equals(
                                        Params.TryGetValue(Properties.MaSoLinhVuc, out value) ? value as int?
                                        : lv.MaSoLinhVuc
                                 )).Where(lv => lv.MaSoLinhVuc.Equals(
                                        Params.TryGetValue(Properties.TenLinhVuc, out value) ? value as string
                                        : lv.TenLinhVuc
                                 )).Where(lv => lv.TrangThai.Equals(
                                        Params.TryGetValue(Properties.TrangThai, out value) ? value as int?
                                        : lv.TrangThai
                                 ));
                return linqQuery.ToList();
            }
        }
        public static List<LinhVuc> filter(string request, List<LinhVuc> DMLinhVuc)
        {

            if (Regex.IsMatch(request, @"[{=<>!}]"))
            {
                var linqQuery = from lv in DMLinhVuc
                                select lv;

                MatchCollection args = Regex.Matches(request, @"({).*?(})");
                foreach (var arg in args)
                {
                    MatchCollection Params = Regex.Matches(arg.ToString(), @"\w+");
                    string method = Regex.Match(arg.ToString(), @"[=<>!]+").ToString();
                    string param = "";
                    for (int i = 1; i < Params.Count; i++)
                    {
                        param += " " + Params[i];
                    }
                    param = param.Trim();
                    switch (Params[0].ToString())
                    {
                        case nameof(Properties.MaSoLinhVuc):
                            linqQuery = linqQuery.Where(lv => FilterHelper.compare(lv.MaSoLinhVuc, Int32.Parse(param), method, false));
                            break;
                        case nameof(Properties.TenLinhVuc):
                            linqQuery = linqQuery.Where(lv => FilterHelper.compare(lv.TenLinhVuc, param, method, true));
                            break;
                    }
                }
                return linqQuery.ToList();
            }
            else
            {
                int number;
                bool isNumber = Int32.TryParse(request, out number);
                request = request.ToLower();
                if (isNumber)
                {
                    var linqQuery = DMLinhVuc.Where
                    (lv => lv.MaSoLinhVuc.Equals(number)
                    );
                    return linqQuery.ToList();
                }
                else
                {
                    var linqQuery = DMLinhVuc.Where
                    (lv => lv.TenLinhVuc.ToLower().Contains(request)
                    );
                    return linqQuery.ToList();
                }
            }
        }
        public static List<LinhVuc> filter(string request)
        {
            var DMLinhVuc = getAllALive();
            return filter(request, DMLinhVuc);
        }
        public static int add(LinhVuc linhvuc)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var lv = new LINHVUC();
                lv.ten = linhvuc.TenLinhVuc;
                db.LINHVUCs.InsertOnSubmit(lv);
                db.SubmitChanges();
                return lv.masolinhvuc;
            }
        }
        public static bool edit(LinhVuc linhvuc)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                //Kiểm tra lĩnh vực có tồn tại không
                LINHVUC lv;
                lv = (from b in db.LINHVUCs
                      where b.masolinhvuc.Equals(linhvuc.MaSoLinhVuc)
                      select b).SingleOrDefault();
                if (lv == null) return false;
                lv.ten = linhvuc.TenLinhVuc;
                lv.trangthai = linhvuc.TrangThai;
                db.SubmitChanges();
                return true;
            }
        }
        public static bool delete(int masolinhvuc)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    //Kiểm tra lĩnh vực có tồn tại không
                    LINHVUC lv;
                    lv = (from b in db.LINHVUCs
                          where b.masolinhvuc.Equals(masolinhvuc)
                          select b).SingleOrDefault();
                    if (lv == null) return false;
                    db.LINHVUCs.DeleteOnSubmit(lv);
                    db.SubmitChanges();
                    return true;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}

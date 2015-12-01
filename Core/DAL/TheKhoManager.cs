using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;
using System.Text.RegularExpressions;

namespace Core.DAL
{
    public class TheKhoManager
    {
        public static partial class Properties
        {
            public const string MaSoSach = "Mã số Sách";
            public const string Sach = "Tên Sách";
            public const string SoLuong = "Số lượng";
            public const string NgayGhi = "Ngày ghi";
        }

        public static List<TheKho> getAll()
        {
            using(EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from tk in db.THEKHOs
                                join s in db.SACHes
                                on tk.masosach equals s.masosach
                                select new TheKho(tk, s);
                return linqQuery.ToList();
            }
        }

        public static List<TheKho> getAllByDate(DateTime date)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = db.THEKHOs
                                .Where(tk => tk.ngayghi <= date) // Duyệt các thẻ kho trước @date
                                .OrderByDescending(tk => tk.ngayghi) //Sắp xếp ngày gần nhất
                                .GroupBy(tk => tk.masosach) // Gọp nhóm các thẻ kho theo mã
                                .Select(group => group.FirstOrDefault()) // Chọn thẻ kho có ngày gần nhất
                                .OrderBy(tk => tk.masosach) // Sắp xếp kết quả theo mã số sách
                                .Join(db.SACHes
                                    , tk => tk.masosach
                                    , s => s.masosach
                                    , (tk, s) => new TheKho(tk,s));
                return linqQuery.ToList();
            }
        }

        public static List<TheKho> find(int masosach)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from tk in db.THEKHOs
                                join s in db.SACHes
                                on tk.masosach equals s.masosach
                                where tk.masosach.Equals(masosach)
                                select new TheKho(tk, s);
                return linqQuery.ToList();
            }
        }

        public static TheKho findByDate(int masosach, DateTime date)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = db.THEKHOs
                                .Where(tk => tk.ngayghi <= date && tk.masosach.Equals(masosach)) // Duyệt các thẻ kho trước @date
                                .OrderByDescending(tk => tk.ngayghi) //Sắp xếp ngày gần nhất
                                .GroupBy(tk => tk.masosach) // Gọp nhóm các thẻ kho theo mã
                                .Select(group => group.FirstOrDefault()) // Chọn thẻ kho có ngày gần nhất
                                .OrderBy(tk => tk.masosach) // Sắp xếp kết quả theo mã số sách
                                .Join(db.SACHes
                                    , tk => tk.masosach
                                    , s => s.masosach
                                    , (tk, s) => new TheKho(tk, s));
                return linqQuery.SingleOrDefault();
            }
        }

        public static List<TheKho> findBy(Dictionary<String, dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;

                var linqQuery = getAll()
                                 .Where(s => s.MaSoSach.Equals(
                                        Params.TryGetValue(Properties.MaSoSach, out value) ? value as int?
                                        : s.MaSoSach
                                 ))
                                 .Where(s => s.Sach.TenSach.Equals(
                                        Params.TryGetValue(Properties.Sach, out value) ? value as string
                                        : s.Sach.TenSach
                                 ))
                                 .Where(s => s.SoLuong.Equals(
                                        Params.TryGetValue(Properties.SoLuong, out value) ? value as decimal?
                                        : s.SoLuong
                                 ))
                                 .Where(s => s.NgayGhi.Equals(
                                        Params.TryGetValue(Properties.NgayGhi, out value) ? value as DateTime?
                                        : s.NgayGhi
                                 ));
                return linqQuery.ToList();
            }
        }

        public static List<TheKho> filter(string request, List<TheKho> DMTheKho)
        {

            if (Regex.IsMatch(request, @"[{=<>!}]"))
            {
                var linqQuery = from s in DMTheKho
                                select s;

                MatchCollection args = Regex.Matches(request, @"({).*?(})");
                foreach (var arg in args)
                {
                    MatchCollection Params = Regex.Matches(arg.ToString(), @"\w+");
                    string method = Regex.Match(arg.ToString(), @"[=<>!]+").ToString();
                    string param = "";
                    int? year = null, month = null, day = null;
                    for (int i = 1; i < Params.Count; i++)
                    {
                        param += " " + Params[i];
                        if (i == 1)
                        {
                            int number;
                            if (Int32.TryParse(Params[i].ToString(), out number))
                            {
                                year = number;
                            }
                        }
                        if (i == 2)
                        {
                            int number;
                            if (Int32.TryParse(Params[i].ToString(), out number))
                            {
                                month = number;
                            }
                        }
                        if (i == 3)
                        {
                            int number;
                            if (Int32.TryParse(Params[i].ToString(), out number))
                            {
                                day = number;
                            }
                        }
                    }
                    param = param.Trim();
                    switch (Params[0].ToString())
                    {
                        case nameof(Properties.MaSoSach):
                            linqQuery = linqQuery.Where(s => FilterHelper.compare(s.MaSoSach, Int32.Parse(param), method, false));
                            break;
                        case nameof(SachManager.Properties.TenSach):
                            linqQuery = linqQuery.Where(s => FilterHelper.compare(s.Sach.TenSach, param, method, true));
                            break;
                        case nameof(SachManager.Properties.TenTacGia):
                            linqQuery = linqQuery.Where(s => FilterHelper.compare(s.Sach.TenTacGia, param, method, true));
                            break;
                        case nameof(Properties.SoLuong):
                            linqQuery = linqQuery.Where(s => FilterHelper.compare(s.SoLuong, Decimal.Parse(param), method, false));
                            break;
                        case nameof(Properties.NgayGhi):
                            linqQuery = linqQuery.Where(s => FilterHelper.compareDate(s.NgayGhi, year, month, day, method));
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
                    var linqQuery = DMTheKho.Where
                    (s => s.MaSoSach.Equals(number)
                    || s.SoLuong.Equals(number)
                    || s.NgayGhi.Year.Equals(number)
                    || s.NgayGhi.Month.Equals(number)
                    || s.NgayGhi.Day.Equals(number)
                    );
                    return linqQuery.ToList();
                }
                else
                {
                    var linqQuery = DMTheKho.Where
                    (s => s.NgayGhi.ToString().ToLower().Contains(request)
                    || s.Sach.TenSach.ToLower().Contains(request)
                    || s.Sach.TenSach.ToLower().Contains(request)
                    || s.NgayGhi.ToString().Contains(request)
                    );
                    return linqQuery.ToList();
                }
            }
        }

        public static List<TheKho> filter(string request)
        {
            var DMTheKho = getAllByDate(DateTime.Now);
            return filter(request, DMTheKho);
        }

        public static bool edit(TheKho thekho)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    THEKHO tk;
                    tk = (from s in db.THEKHOs
                          where s.masosach.Equals(thekho.MaSoSach)
                                && s.ngayghi.Equals(thekho.NgayGhi)
                          select s).SingleOrDefault();
                    if (tk == null) return false; //Nếu không tồn tại thẻ kho
                    tk.soluong = thekho.SoLuong;
                    db.SubmitChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            } 
        }

        public static int add(TheKho thekho)
        {
            try
            {
                using(EntitiesDataContext db = new EntitiesDataContext())
                {
                    THEKHO tk;
                    tk = (from s in db.THEKHOs
                          where s.masosach.Equals(thekho.MaSoSach)
                            && s.ngayghi.Equals(thekho.NgayGhi)
                          select s).SingleOrDefault();
                    if(tk != null)
                    {
                        tk.soluong = thekho.SoLuong;
                        db.SubmitChanges();
                        return 1;
                    }
                    else
                    {
                        tk = new THEKHO()
                        {
                            masosach = thekho.MaSoSach,
                            soluong = thekho.SoLuong,
                            ngayghi = thekho.NgayGhi
                        };
                        db.THEKHOs.InsertOnSubmit(tk);
                        db.SubmitChanges();
                        return 1;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        public static bool delete(TheKho thekho)
        {
            try
            {
                using(EntitiesDataContext db = new EntitiesDataContext())
                {
                    THEKHO tk;
                    tk = (from t in db.THEKHOs
                          where t.masosach.Equals(thekho.MaSoSach)
                          && t.ngayghi.Equals(thekho.NgayGhi)
                          select t).SingleOrDefault();
                    if (tk == null) return false;
                    db.THEKHOs.DeleteOnSubmit(tk);
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

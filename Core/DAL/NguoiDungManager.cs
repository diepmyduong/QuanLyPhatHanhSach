using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;
using System.Text.RegularExpressions;

namespace Core.DAL
{
    public class NguoiDungManager
    {
        public static partial class Properties
        {
            public const string MaSoNguoiDung = "Mã số người dùng";
            public const string TenNguoiDung = "Tên người dùng";
            public const string MatKhau = "Mật khẩu";
            public const string TenDayDu    = "Họ và tên";
            public const string Email = "Email";
            public const string PhanQuyen = "Phân quyền";
            public const string TrangThai = "Trạng Thái";
            public const string MaSoDaiLy = "Mã số đại lý";
            public const string DaiLy = "Đại Lý";
        }

        public static partial class Roles
        {
            public const string daily = "Đại lý";
            public const string admin = "Admin";
        }

        public static List<NguoiDung> getAll()
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from nd in db.NGUOIDUNGs
                                select new NguoiDung(nd);
                return linqQuery.ToList();
            }
        }
        public static List<NguoiDung> getAllAlive()
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from nd in db.NGUOIDUNGs
                                where nd.trangthai != 0
                                select new NguoiDung(nd);
                return linqQuery.ToList();
            }
        }
        public static NguoiDung find(int masonguoidung)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from nd in db.NGUOIDUNGs
                                where nd.masonguoidung == masonguoidung
                                select new NguoiDung(nd);
                return linqQuery.SingleOrDefault();
            }
        }
        public static List<NguoiDung> findBy(Dictionary<String, dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;

                var linqQuery = getAll()
                                 .Where(s => s.MaSoNguoiDung.Equals(
                                        Params.TryGetValue(Properties.MaSoNguoiDung, out value) ? value as int?
                                        : s.MaSoNguoiDung
                                 ))
                                 .Where(s => s.TenNguoiDung.Equals(
                                        Params.TryGetValue(Properties.TenNguoiDung, out value) ? value as string
                                        : s.TenNguoiDung
                                 ))
                                 .Where(s => s.MatKhau.Equals(
                                        Params.TryGetValue(Properties.MatKhau, out value) ? value as string
                                        : s.MatKhau
                                 ))
                                 .Where(s => s.TenDayDu.Equals(
                                        Params.TryGetValue(Properties.TenDayDu, out value) ? value as string
                                        : s.TenDayDu
                                 ))
                                 .Where(s => s.Email.Equals(
                                        Params.TryGetValue(Properties.Email, out value) ? value as string
                                        : s.Email
                                 ))
                                 .Where(s => s.PhanQuyen.Equals(
                                        Params.TryGetValue(Properties.PhanQuyen, out value) ? value as string
                                        : s.PhanQuyen
                                 )).Where(s => s.TrangThai.Equals(
                                        Params.TryGetValue(Properties.TrangThai, out value) ? value as int?
                                        : s.TrangThai
                                 ));
                return linqQuery.ToList();
            }
        }
        public static NguoiDung findByName(string username)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from nd in db.NGUOIDUNGs
                                where nd.tennguoidung == username
                                select new NguoiDung(nd);
                return linqQuery.SingleOrDefault();
            }
        }
        public static List<NguoiDung> filter(string request, List<NguoiDung> DMNguoiDung)
        {

            if (Regex.IsMatch(request, @"[{=<>!}]"))
            {
                var linqQuery = from s in DMNguoiDung
                                select s;

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
                        case nameof(Properties.MaSoNguoiDung):
                            linqQuery = linqQuery.Where(s => FilterHelper.compare(s.MaSoNguoiDung, Int32.Parse(param), method, false));
                            break;
                        case nameof(Properties.TenNguoiDung):
                            linqQuery = linqQuery.Where(s => FilterHelper.compare(s.TenNguoiDung, param, method, true));
                            break;
                        case nameof(Properties.Email):
                            linqQuery = linqQuery.Where(s => FilterHelper.compare(s.Email, param, method, true));
                            break;
                        case nameof(Properties.TenDayDu):
                            linqQuery = linqQuery.Where(s => FilterHelper.compare(s.TenDayDu, param, method, true));
                            break;
                        case nameof(Properties.PhanQuyen):
                            linqQuery = linqQuery.Where(s => FilterHelper.compare(s.isAdmin() ? "Admin" : "Đại lý", param, method, true));
                            break;
                        case nameof(Properties.TrangThai):
                            linqQuery = linqQuery.Where(s => FilterHelper.compare(s.TrangThai, Int32.Parse(param), method, false));
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
                    var linqQuery = DMNguoiDung.Where
                    (s => s.MaSoNguoiDung.Equals(number)
                    );
                    return linqQuery.ToList();
                }
                else
                {
                    var linqQuery = DMNguoiDung.Where
                    (s => s.TenNguoiDung.ToLower().Contains(request)
                    || s.TenDayDu.ToLower().Contains(request)
                    || s.Email.ToLower().Contains(request)
                    || (s.isAdmin() ? "Admin" : "Đại lý").ToLower().Contains(request)
                    );
                    return linqQuery.ToList();
                }
            }
        }
        public static List<NguoiDung> filter(string request)
        {
            var DMNguoiDung = getAllAlive();
            return filter(request, DMNguoiDung);
        }
        public static bool edit(NguoiDung nguoidung)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var s = (from b in db.NGUOIDUNGs
                         where b.masonguoidung == nguoidung.MaSoNguoiDung
                         select b).SingleOrDefault();
                if (s == null) return false;
                s.matkhau = nguoidung.MatKhau;
                s.tendaydu = nguoidung.TenDayDu;
                s.email = nguoidung.Email;
                s.phanquyen = nguoidung.PhanQuyen;
                s.trangthai = nguoidung.TrangThai;
                db.SubmitChanges();
                return true;
            }
        }
        public static int add(NguoiDung nguoidung)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    var s = new NGUOIDUNG();
                    s.tennguoidung = nguoidung.TenNguoiDung;
                    s.matkhau = nguoidung.MatKhau;
                    s.tendaydu = nguoidung.TenDayDu;
                    s.email = nguoidung.Email;
                    s.phanquyen = nguoidung.PhanQuyen;
                    s.trangthai = nguoidung.TrangThai;
                    db.NGUOIDUNGs.InsertOnSubmit(s);
                    db.SubmitChanges();
                    return s.masonguoidung;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public static bool delete(int masonguoidung)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    NGUOIDUNG nguoidung;
                    nguoidung = (from s in db.NGUOIDUNGs
                            where s.masonguoidung.Equals(masonguoidung)
                            select s).SingleOrDefault();
                    if (nguoidung == null) return false;
                    db.NGUOIDUNGs.DeleteOnSubmit(nguoidung);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}

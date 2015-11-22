using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;
using System.Text.RegularExpressions;

namespace Core.DAL
{
    public class NhaXuatBanManager
    {
        public static partial class Properties
        {
            public const string MaSoNXB = "Mã Nhà Xuất Bản";
            public const string TenNXB = "Tên Nhà Xuất Bản";
            public const string DiaChi = "Địa chỉ";
            public const string SoDienThoai = "Số điện thoại";
            public const string SoTaiKhoan = "Số tài khoản";
            public const string Sach = "Sách";
            public const string PhieuNhap = "Phiếu nhập";
            public const string CongNo = "Công nợ";
            public const string HoaDon = "Hóa đơn";
            public const string TongTienNo = "Tổng tiền nợ";
            public const string TongTienNoThang = "Tổng tiền nợ tháng";
        }
        public static List<NhaXuatBan> getAll()
        {
            using(EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from nxb in db.NXBs
                                where nxb.trangthai == null
                                select new NhaXuatBan
                                {
                                    MaSoNXB = nxb.masonxb,
                                    TenNXB = nxb.ten,
                                    DiaChi = nxb.diachi,
                                    SoDienThoai = nxb.sodienthoai,
                                    SoTaiKhoan = nxb.sotaikhoan
                                };
                return linqQuery.ToList();
            }
        }

        public static NhaXuatBan find(int masonxb)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from nxb in db.NXBs
                                where nxb.masonxb.Equals(masonxb)
                                && nxb.trangthai == null
                                select new NhaXuatBan
                                {
                                    MaSoNXB = nxb.masonxb,
                                    TenNXB = nxb.ten,
                                    DiaChi = nxb.diachi,
                                    SoDienThoai = nxb.sodienthoai,
                                    SoTaiKhoan = nxb.sotaikhoan
                                };
                return linqQuery.SingleOrDefault();
            }
        }

        public static List<NhaXuatBan> findBy(Dictionary<string,dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;

                var linqQuery = (from nxb in db.NXBs
                                 where nxb.trangthai == null
                                 select new NhaXuatBan
                                 {
                                     MaSoNXB = nxb.masonxb,
                                     TenNXB = nxb.ten,
                                     DiaChi = nxb.diachi,
                                     SoDienThoai = nxb.sodienthoai,
                                     SoTaiKhoan = nxb.sotaikhoan
                                 })
                                 .Where(nxb => nxb.MaSoNXB.Equals(
                                        Params.TryGetValue(Properties.MaSoNXB, out value) ? value as int?
                                        : nxb.MaSoNXB
                                 )).Where(nxb => nxb.TenNXB.Equals(
                                        Params.TryGetValue(Properties.TenNXB, out value) ? value as string
                                        : nxb.TenNXB
                                 )).Where(nxb => nxb.DiaChi.Equals(
                                        Params.TryGetValue(Properties.DiaChi, out value) ? value as string
                                        : nxb.DiaChi
                                 )).Where(nxb => nxb.SoDienThoai.Equals(
                                        Params.TryGetValue(Properties.SoDienThoai, out value) ? value as string
                                        : nxb.SoDienThoai
                                 )).Where(nxb => nxb.SoTaiKhoan.Equals(
                                        Params.TryGetValue(Properties.SoTaiKhoan, out value) ? value as string
                                        : nxb.SoTaiKhoan
                                 ));
                return linqQuery.ToList<NhaXuatBan>();
            }
        }

        public static List<NhaXuatBan> filter(string request, List<NhaXuatBan> DMNXB)
        {

            if (Regex.IsMatch(request, @"[{=<>!}]"))
            {
                var linqQuery = from nxb in DMNXB
                                select nxb;

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
                        case nameof(Properties.MaSoNXB):
                            linqQuery = linqQuery.Where(nxb => FilterHelper.compare(nxb.MaSoNXB, Int32.Parse(param), method, false));
                            break;
                        case nameof(Properties.TenNXB):
                            linqQuery = linqQuery.Where(nxb => FilterHelper.compare(nxb.TenNXB, param, method, true));
                            break;
                        case nameof(Properties.DiaChi):
                            linqQuery = linqQuery.Where(nxb => FilterHelper.compare(nxb.DiaChi, param, method, true));
                            break;
                        case nameof(Properties.SoDienThoai):
                            linqQuery = linqQuery.Where(nxb => FilterHelper.compare(nxb.SoDienThoai, param, method, true));
                            break;
                        case nameof(Properties.SoTaiKhoan):
                            linqQuery = linqQuery.Where(nxb => FilterHelper.compare(nxb.SoTaiKhoan, param, method, true));
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
                    var linqQuery = DMNXB.Where
                    (nxb => nxb.MaSoNXB.Equals(number)
                    || nxb.SoDienThoai.Contains(number.ToString())
                    || nxb.SoTaiKhoan.Contains(number.ToString())
                    );
                    return linqQuery.ToList<NhaXuatBan>();
                }
                else
                {
                    var linqQuery = DMNXB.Where
                    (nxb => nxb.TenNXB.Contains(request)
                    || nxb.DiaChi.Contains(request)
                    );
                    return linqQuery.ToList<NhaXuatBan>();
                }
            }
        }
        public static List<NhaXuatBan> filter(string request)
        {
            var DMNXB = getAll();
            return filter(request, DMNXB);
        }
        public static int add(NhaXuatBan nhaxuatban)
        {
            try
            {
                using(EntitiesDataContext db = new EntitiesDataContext())
                {
                    var nxb = new NXB
                    {
                        ten = nhaxuatban.TenNXB,
                        diachi = nhaxuatban.DiaChi,
                        sodienthoai = nhaxuatban.SoDienThoai,
                        sotaikhoan = nhaxuatban.SoTaiKhoan
                    }
                    db.NXBs.InsertOnSubmit(nxb);
                    db.SubmitChanges();
                    return nxb.masonxb;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public static bool edit(NhaXuatBan nhaxuatban)
        {
            try
            {
                using(EntitiesDataContext db = new EntitiesDataContext())
                {
                    NXB nxb;
                    nxb = (from n in db.NXBs
                           where n.masonxb.Equals(nhaxuatban.MaSoNXB)
                           select n).SingleOrDefault();
                    if (nxb == null) return false;
                    nxb.ten = nhaxuatban.TenNXB;
                    nxb.diachi = nhaxuatban.DiaChi;
                    nxb.sodienthoai = nhaxuatban.SoDienThoai;
                    nxb.sotaikhoan = nhaxuatban.SoTaiKhoan;
                    db.SubmitChanges();
                    return true;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static bool delete(int masonxb)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    NXB nxb;
                    nxb = (from n in db.NXBs
                           where n.masonxb.Equals(masonxb)
                           select n).SingleOrDefault();
                    if (nxb == null) return false;
                    db.NXBs.DeleteOnSubmit(nxb);
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

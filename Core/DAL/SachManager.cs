using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;

namespace Core.DAL
{
    public class SachManager
    {
        public static partial class Properties
        {
            public const string MaSoSach = "Mã Số Sách";
            public const string TenSach = "Tên Sách";
            public const string MaSoLinhVuc = "Mã số lĩnh vực";
            public const string LinhVucSach = "Lĩnh Vực Sách";
            public const string TenTacGia = "Tên Tác Giả";
            public const string MaSoNXB = "Mã số NXB";
            public const string NXB = "Nhà Xuất Bản";
            public const string Soluong = "Số Lượng";
            public const string GiaNhap = "Giá Nhập";
            public const string GiaBan = "Giá Bán";
            public const string HinhAnh = "Hình Ảnh";
            public const string MoTa = "Mô tả";
            public const string CongNoDaiLy = "Công Nợ Đại Lý";
            public const string CongNoNXB = "Công Nợ NXB";
            public const string HoaDonDaiLy = "Hóa Đơn Đại lý";
            public const string HoaDonNXB = "Hóa Đơn NXB";
            public const string PhieuNhap = "Phiếu Nhập";
            public const string PhieuXuat = "Phiếu Xuất";
            public const string TongSoLuongNhap = "Tổng Số lượng nhập";
            public const string SoLuongNhapTheoThang = "Số lượng nhập";
            public const string TongSoLuongXuat = "Tổng Số lượng xuất";
            public const string SoLuongXuatTheoThang = "Số lượng xuất";
            public const string TongTienNhap = "Tổng tiền nhập";
            public const string TongTienNhapTheoThang = "Tiền nhập";
            public const string TongTienXuat = "Tổng tiền xuất";
            public const string TongTienXuatTheoThang = "Tiền xuất";
            public const string TrangThai = "Trạng thái";
            public const string TongSoLuongBan = "Số lượng bán";
            public const string TongSoLuongBanTheoThang = "Số lượng bán theo tháng";
            public const string TongTienBan = "Tổng tiền bán";
            public const string TongTienBanTheoThang = "Tiền bán theo tháng";
            public const string TongSoLuongNXBNo = "Số lượng Nhà xuất bản nợ";
            public const string TongSoLuongDaiLyNo = "Số lượng Đại lý nợ";
            public const string TongTienNXBNo = "Tổng tiền Nhà xuất bản nợ";
            public const string TongTienDaiLyNo = "Tổng tiền Đại lý nợ";
            public const string TongSoLuongNXBNoTheoThang = "Số lượng Nhà xuất bản nợ theo tháng";
            public const string TongSoLuongDaiLyNoTheoThang = "Số lượng Đại lý nợ theo tháng";
            public const string TongTienNXBNoTheoThang = "Tổng tiền Nhà xuất bản nợ theo tháng";
            public const string TongTienDaiLyNoTheoTang = "Tổng tiền Đại lý nợ theo tháng";

        }
        public static List<Sach> getAll()
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from s in db.SACHes
                                join nxb in db.NXBs on s.masonxb equals nxb.masonxb
                                join lv in db.LINHVUCs on s.masolinhvuc equals lv.masolinhvuc
                                select new Sach(s,nxb,lv);
                return linqQuery.ToList();
            }
        }
        public static List<Sach> getAllAlive()
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from s in db.SACHes
                                join nxb in db.NXBs on s.masonxb equals nxb.masonxb
                                join lv in db.LINHVUCs on s.masolinhvuc equals lv.masolinhvuc
                                where s.trangthai == null
                                select new Sach(s,nxb, lv);
                return linqQuery.ToList();
            }
        }
        public static Sach find(int masosach)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from s in db.SACHes
                                join nxb in db.NXBs on s.masonxb equals nxb.masonxb
                                join lv in db.LINHVUCs on s.masolinhvuc equals lv.masolinhvuc
                                where s.masosach.Equals(masosach)
                                select new Sach(s, nxb, lv);
                return linqQuery.SingleOrDefault();
            }
        }
        public static List<Sach> findBy(Dictionary<String,dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;

                var linqQuery = getAll()
                                 .Where(s => s.MaSoSach.Equals(
                                        Params.TryGetValue(Properties.MaSoSach, out value) ? value as int?
                                        : s.MaSoSach
                                 ))
                                 .Where(s => s.TenSach.Equals(
                                        Params.TryGetValue(Properties.TenSach, out value) ? value as string
                                        : s.TenSach
                                 ))
                                 .Where(s => s.LinhVucSach.MaSoLinhVuc.Equals(
                                        Params.TryGetValue(Properties.LinhVucSach, out value) ? value as int?
                                        : s.LinhVucSach.MaSoLinhVuc
                                 ))
                                 .Where(s => s.TenTacGia.Equals(
                                        Params.TryGetValue(Properties.TenTacGia, out value) ? value as string
                                        : s.TenTacGia
                                 ))
                                 .Where(s => s.NXB.MaSoNXB.Equals(
                                        Params.TryGetValue(Properties.MaSoNXB, out value) ? value as int?
                                        : s.NXB.MaSoNXB
                                 ))
                                 .Where(s => s.Soluong.Equals(
                                        Params.TryGetValue(Properties.Soluong, out value) ? value as decimal?
                                        : s.Soluong
                                 ))
                                 .Where(s => s.GiaBan.Equals(
                                        Params.TryGetValue(Properties.GiaBan, out value) ? value as decimal?
                                        : s.GiaBan
                                 ))
                                 .Where(s => s.GiaNhap.Equals(
                                        Params.TryGetValue(Properties.GiaNhap, out value) ? value as decimal?
                                        : s.GiaNhap
                                 )).Where(s => s.TrangThai.Equals(
                                        Params.TryGetValue(Properties.TrangThai, out value) ? value as int?
                                        : s.TrangThai
                                 ));
                return linqQuery.ToList();
            }
        }
        public static List<Sach> filter(string request, List<Sach> DMSach)
        {

            try
            {
                if (Regex.IsMatch(request, @"[{=<>!}]"))
                {
                    var linqQuery = from s in DMSach
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
                            case nameof(Properties.MaSoSach):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.MaSoSach, Int32.Parse(param), method, false));
                                break;
                            case nameof(Properties.MaSoLinhVuc):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.MaSoLinhVuc, Int32.Parse(param), method, false));
                                break;
                            case nameof(Properties.MaSoNXB):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.MaSoNXB, Int32.Parse(param), method, false));
                                break;
                            case nameof(Properties.TenSach):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.TenSach, param, method, true));
                                break;
                            case nameof(Properties.TenTacGia):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.TenTacGia, param, method, true));
                                break;
                            case nameof(Properties.NXB):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.NXB.TenNXB, param, method, true));
                                break;
                            case nameof(Properties.LinhVucSach):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.LinhVucSach.TenLinhVuc, param, method, true));
                                break;
                            case nameof(Properties.Soluong):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.Soluong, Decimal.Parse(param), method, false));
                                break;
                            case nameof(Properties.GiaBan):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.GiaBan, Decimal.Parse(param), method, false));
                                break;
                            case nameof(Properties.GiaNhap):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.GiaNhap, Decimal.Parse(param), method, false));
                                break;
                            case nameof(Properties.SoLuongNhapTheoThang):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.SoLuongNhapTheoThang, Decimal.Parse(param), method, false));
                                break;
                            case nameof(Properties.TongTienNhapTheoThang):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.TongTienNhapTheoThang, Decimal.Parse(param), method, false));
                                break;
                            case nameof(Properties.TongSoLuongBanTheoThang):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.TongSoLuongBanTheoThang, Decimal.Parse(param), method, false));
                                break;
                            case nameof(Properties.TongTienBanTheoThang):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.TongTienBanTheoThang, Decimal.Parse(param), method, false));
                                break;
                            case nameof(Properties.TongSoLuongDaiLyNoTheoThang):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.TongSoLuongDaiLyNoTheoThang, Decimal.Parse(param), method, false));
                                break;
                            case nameof(Properties.TongTienDaiLyNoTheoTang):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.TongTienDaiLyNoTheoTang, Decimal.Parse(param), method, false));
                                break;
                            case nameof(Properties.TongSoLuongNXBNoTheoThang):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.TongSoLuongNXBNoTheoThang, Decimal.Parse(param), method, false));
                                break;
                            case nameof(Properties.TongTienNXBNoTheoThang):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.TongTienNXBNoTheoThang, Decimal.Parse(param), method, false));
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
                        var linqQuery = DMSach.Where
                        (s => s.MaSoSach.Equals(number)
                        || s.Soluong.Equals(number)
                        || s.GiaNhap.Equals(number)
                        || s.GiaBan.Equals(number)
                        || s.SoLuongNhapTheoThang.Equals((decimal)number)
                        || s.TongTienNhapTheoThang.Equals((decimal)number)
                        || s.TongSoLuongBanTheoThang.Equals((decimal)number)
                        || s.TongTienBanTheoThang.Equals((decimal)number)
                        );
                        return linqQuery.ToList();
                    }
                    else
                    {
                        var linqQuery = DMSach.Where
                        (s => s.TenSach.ToLower().Contains(request)
                        || s.LinhVucSach.TenLinhVuc.ToLower().Contains(request)
                        || s.TenTacGia.ToLower().Contains(request)
                        || s.NXB.TenNXB.ToLower().Contains(request)
                        );
                        return linqQuery.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<Sach> filter(string request)
        {
            var DMSach = getAllAlive();
            return filter(request, DMSach);
        }
        public static bool edit(Sach sach)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var s = (from b in db.SACHes
                         where b.masosach == sach.MaSoSach
                         select b).SingleOrDefault();
                if (s == null) return false;
                s.tensach = sach.TenSach;
                s.masolinhvuc = sach.LinhVucSach.MaSoLinhVuc;
                s.masonxb = sach.NXB.MaSoNXB;
                s.tacgia = sach.TenTacGia;
                s.soluong = sach.Soluong;
                s.giaban = sach.GiaBan;
                s.gianhap = sach.GiaNhap;
                s.hinhanh = sach.HinhAnh;
                s.mota = sach.MoTa;
                s.trangthai = sach.TrangThai;
                db.SubmitChanges();
                return true;
            }
        }
        public static int add(Sach sach)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    var s = new SACH();
                    s.tensach = sach.TenSach;
                    s.masolinhvuc = sach.LinhVucSach.MaSoLinhVuc;
                    s.masonxb = sach.NXB.MaSoNXB;
                    s.tacgia = sach.TenTacGia;
                    s.soluong = sach.Soluong;
                    s.giaban = sach.GiaBan;
                    s.gianhap = sach.GiaNhap;
                    s.hinhanh = sach.HinhAnh;
                    s.mota = sach.MoTa;
                    db.SACHes.InsertOnSubmit(s);
                    db.SubmitChanges();
                    return s.masosach;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public static bool delete(int masosach)
        {
            try
            {
                using(EntitiesDataContext db = new EntitiesDataContext())
                {
                    SACH sach;
                    sach = (from s in db.SACHes
                            where s.masosach.Equals(masosach)
                            select s).SingleOrDefault();
                    if (sach == null) return false;
                    db.SACHes.DeleteOnSubmit(sach);
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

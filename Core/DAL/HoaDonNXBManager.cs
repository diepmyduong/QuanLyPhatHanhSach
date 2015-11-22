using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;
using System.Text.RegularExpressions;

namespace Core.DAL
{
    public class HoaDonNXBManager
    {
        public static partial class Properties
        {
            public const string MaSoHoaDon = "Mã Số Hóa Đơn";
            public const string MaSoNXB = "Mã Số NXB";
            public const string NXB = "Tên NXB";
            public const string NgayLap = "Ngày Lập";
            public const string TongTien = "Tổng tiền";
            public const string ChiTiet = "Chi tiết";
            public const string TrangThai = "Trạng Thái";
        }

        public static List<HoaDonNXB> getAll()
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from hd in db.HOADONNXBs
                                join nxb in db.NXBs
                                on hd.masonxb equals nxb.masonxb
                                select new HoaDonNXB()
                                {
                                    MaSoHoaDon = hd.masohoadon,
                                    MaSoNXB = hd.masonxb,
                                    NXB = new NhaXuatBan
                                    {
                                        MaSoNXB = nxb.masonxb,
                                        TenNXB = nxb.ten,
                                        DiaChi = nxb.diachi,
                                        SoDienThoai = nxb.sodienthoai,
                                        SoTaiKhoan = nxb.sotaikhoan
                                    },
                                    NgayLap = hd.ngaylap,
                                    TongTien = hd.tongtien,
                                    TrangThai = hd.trangthai
                                };
                return linqQuery.ToList();
            }
        }
        public static HoaDonNXB find(int masohoadon)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from hd in db.HOADONNXBs
                                join nxb in db.NXBs
                                on hd.masonxb equals nxb.masonxb
                                where hd.masohoadon.Equals(masohoadon)
                                select new HoaDonNXB()
                                {
                                    MaSoHoaDon = hd.masohoadon,
                                    MaSoNXB = hd.masonxb,
                                    NXB = new NhaXuatBan
                                    {
                                        MaSoNXB = nxb.masonxb,
                                        TenNXB = nxb.ten,
                                        DiaChi = nxb.diachi,
                                        SoDienThoai = nxb.sodienthoai,
                                        SoTaiKhoan = nxb.sotaikhoan
                                    },
                                    NgayLap = hd.ngaylap,
                                    TongTien = hd.tongtien,
                                    TrangThai = hd.trangthai
                                };
                return linqQuery.SingleOrDefault();
            }
        }
        public static List<HoaDonNXB> findBy(Dictionary<string,dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;

                var linqQuery = (from hd in db.HOADONNXBs
                                 join nxb in db.NXBs
                                 on hd.masonxb equals nxb.masonxb
                                 select new HoaDonNXB()
                                 {
                                     MaSoHoaDon = hd.masohoadon,
                                     MaSoNXB = hd.masonxb,
                                     NXB = new NhaXuatBan
                                     {
                                         MaSoNXB = nxb.masonxb,
                                         TenNXB = nxb.ten,
                                         DiaChi = nxb.diachi,
                                         SoDienThoai = nxb.sodienthoai,
                                         SoTaiKhoan = nxb.sotaikhoan
                                     },
                                     NgayLap = hd.ngaylap,
                                     TongTien = hd.tongtien,
                                     TrangThai = hd.trangthai
                                 })
                                 .Where(hd => hd.MaSoHoaDon.Equals(
                                        Params.TryGetValue(Properties.MaSoHoaDon, out value) ? value as int?
                                        : hd.MaSoHoaDon
                                 )).Where(hd => hd.MaSoNXB.Equals(
                                        Params.TryGetValue(Properties.MaSoNXB, out value) ? value as int?
                                        : hd.MaSoNXB
                                 )).Where(hd => hd.NgayLap.Equals(
                                        Params.TryGetValue(Properties.NgayLap, out value) ? value as DateTime?
                                        : hd.NgayLap
                                 )).Where(hd => hd.TongTien.Equals(
                                        Params.TryGetValue(Properties.MaSoHoaDon, out value) ? value as decimal?
                                        : hd.TongTien
                                 ));
                return linqQuery.ToList();
            }
        }
        public static List<HoaDonNXB> filter(string request, List<HoaDonNXB> DMHoaDon)
        {

            if (Regex.IsMatch(request, @"[{=<>!}]"))
            {
                var linqQuery = from s in DMHoaDon
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
                        case nameof(Properties.MaSoHoaDon):
                            linqQuery = linqQuery.Where(s => FilterHelper.compare(s.MaSoHoaDon, Int32.Parse(param), method, false));
                            break;
                        case nameof(NhaXuatBanManager.Properties.TenNXB):
                            linqQuery = linqQuery.Where(s => FilterHelper.compare(s.NXB.TenNXB, param, method, true));
                            break;
                        case nameof(Properties.MaSoNXB):
                            linqQuery = linqQuery.Where(s => FilterHelper.compare(s.MaSoNXB, Int32.Parse(param), method, false));
                            break;
                        case nameof(Properties.NgayLap):
                            linqQuery = linqQuery.Where(s => FilterHelper.compare(s.NgayLap, param, method, true));
                            break;
                        case nameof(Properties.TongTien):
                            linqQuery = linqQuery.Where(s => FilterHelper.compare(s.TongTien, param, method, false));
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
                    var linqQuery = DMHoaDon.Where
                    (s => s.MaSoHoaDon.Equals(number)
                    || s.MaSoNXB.Equals(number)
                    || s.TongTien.Equals(number)
                    );
                    return linqQuery.ToList();
                }
                else
                {
                    var linqQuery = DMHoaDon.Where
                    (s => s.NgayLap.ToString().ToLower().Contains(request)
                    || s.NXB.TenNXB.ToLower().Contains(request)
                    );
                    return linqQuery.ToList();
                }
            }
        }
        public static List<HoaDonNXB> filter(string request)
        {
            var DMHoaDon = getAll();
            return filter(request, DMHoaDon);
        }
        public static bool edit(HoaDonNXB hoadon)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    HOADONNXB hd;
                    hd = (from p in db.HOADONNXBs
                          where p.masohoadon.Equals(hoadon.MaSoHoaDon)
                          select p).SingleOrDefault();
                    if (hd == null) return false;
                    hd.masonxb= hoadon.MaSoNXB;
                    hd.ngaylap = hoadon.NgayLap;
                    hd.trangthai = hoadon.TrangThai;
                    hd.tongtien = hoadon.ChiTiet.Sum(ct => ct.SoLuong * ct.DonGia); // tính tổng tiền các chi tiết
                    foreach (CHITIETHOADONNXB ct in hd.CHITIETHOADONNXBs)
                    {
                        db.CHITIETHOADONNXBs.DeleteOnSubmit(ct);
                    }
                    foreach (ChiTietHoaDonNXB ct in hoadon.ChiTiet)
                    {
                        ChiTiet.add(ct);
                    }
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
        public static int add(HoaDonNXB hoadon)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    var hd = new HOADONNXB
                    {
                        masonxb = hoadon.MaSoNXB,
                        ngaylap = hoadon.NgayLap,
                        tongtien = hoadon.ChiTiet.Sum(ct => ct.SoLuong * ct.DonGia)
                    };
                    db.HOADONNXBs.InsertOnSubmit(hd);
                    db.SubmitChanges();
                    foreach (ChiTietHoaDonNXB ct in hoadon.ChiTiet)
                    {
                        ChiTiet.add(ct, hd.masohoadon);
                    }
                    return hd.masohoadon;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public static bool delete(int masohoadon)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    HOADONNXB hd;
                    hd = (from p in db.HOADONNXBs
                             where p.masohoadon.Equals(masohoadon)
                             select p).SingleOrDefault();
                    if (hd == null) return false;
                    db.HOADONNXBs.DeleteOnSubmit(hd);
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


        public partial class ChiTiet
        {
            public static partial class Properties
            {
                public const string MaSoSach = "Mã Số Sách";
                public const string Sach = "Tên Sách";
                public const string SoLuong = "Số lượng";
                public const string DonGia = "Đơn Giá";
                public const string ThanhTien = "Thành tiền";
                public const string MaSoHoaDon = "Mã Số Hóa Đơn";
                public const string HoaDon = "Hóa đơn";
            }

            public static List<ChiTietHoaDonNXB> getAll()
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    var linqQuery = from ct in db.CHITIETHOADONNXBs
                                    join s in db.SACHes
                                    on ct.masosach equals s.masosach
                                    select new ChiTietHoaDonNXB()
                                    {
                                        MaSoHoaDon = ct.masohoadon,
                                        MaSoSach = ct.masosach,
                                        Sach = new Sach()
                                        {
                                            MaSoSach = s.masosach,
                                            TenSach = s.tensach,
                                            MaSoLinhVuc = s.masolinhvuc,
                                            TenTacGia = s.tacgia,
                                            MaSoNXB = s.masonxb,
                                            Soluong = s.soluong,
                                            GiaBan = s.giaban,
                                            GiaNhap = s.gianhap,
                                            HinhAnh = s.hinhanh
                                        },
                                        SoLuong = ct.soluong,
                                        DonGia = ct.dongia
                                    };
                    return linqQuery.ToList();
                }
            }
            public static List<ChiTietHoaDonNXB> find(int masohoadon)
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    var linqQuery = from ct in db.CHITIETHOADONNXBs
                                    join s in db.SACHes
                                    on ct.masosach equals s.masosach
                                    where ct.masohoadon.Equals(masohoadon)
                                    select new ChiTietHoaDonNXB()
                                    {
                                        MaSoHoaDon = ct.masohoadon,
                                        MaSoSach = ct.masosach,
                                        Sach = new Sach()
                                        {
                                            MaSoSach = s.masosach,
                                            TenSach = s.tensach,
                                            MaSoLinhVuc = s.masolinhvuc,
                                            TenTacGia = s.tacgia,
                                            MaSoNXB = s.masonxb,
                                            Soluong = s.soluong,
                                            GiaBan = s.giaban,
                                            GiaNhap = s.gianhap,
                                            HinhAnh = s.hinhanh
                                        },
                                        SoLuong = ct.soluong,
                                        DonGia = ct.dongia
                                    };
                    return linqQuery.ToList();
                }
            }
            public static List<ChiTietHoaDonNXB> findBy(Dictionary<string, dynamic> Params)
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    dynamic value;

                    var linqQuery = (from ct in db.CHITIETHOADONDAILies
                                     join s in db.SACHes
                                     on ct.masosach equals s.masosach
                                     select new ChiTietHoaDonNXB()
                                     {
                                         MaSoHoaDon = ct.masohoadon,
                                         MaSoSach = ct.masosach,
                                         Sach = new Sach()
                                         {
                                             MaSoSach = s.masosach,
                                             TenSach = s.tensach,
                                             MaSoLinhVuc = s.masolinhvuc,
                                             TenTacGia = s.tacgia,
                                             MaSoNXB = s.masonxb,
                                             Soluong = s.soluong,
                                             GiaBan = s.giaban,
                                             GiaNhap = s.gianhap,
                                             HinhAnh = s.hinhanh
                                         },
                                         SoLuong = ct.soluong,
                                         DonGia = ct.dongia
                                     })
                                     .Where(ct => ct.MaSoHoaDon.Equals(
                                            Params.TryGetValue(Properties.MaSoHoaDon, out value) ? value as int?
                                            : ct.MaSoHoaDon
                                     )).Where(ct => ct.MaSoSach.Equals(
                                            Params.TryGetValue(Properties.MaSoSach, out value) ? value as int?
                                            : ct.MaSoSach
                                     )).Where(ct => ct.SoLuong.Equals(
                                            Params.TryGetValue(Properties.SoLuong, out value) ? value as decimal?
                                            : ct.SoLuong
                                     )).Where(ct => ct.DonGia.Equals(
                                            Params.TryGetValue(Properties.DonGia, out value) ? value as decimal?
                                            : ct.DonGia
                                     ));
                    return linqQuery.ToList();
                }
            }
            public static bool add(ChiTietHoaDonNXB chitiet)
            {
                return add(chitiet, chitiet.MaSoHoaDon);
            }
            public static bool add(ChiTietHoaDonNXB chitiet, int masohoadon)
            {
                try
                {
                    using (EntitiesDataContext db = new EntitiesDataContext())
                    {
                        CHITIETHOADONNXB ct;
                        ct = (from c in db.CHITIETHOADONNXBs
                              where c.masohoadon.Equals(masohoadon)
                              && c.masosach.Equals(chitiet.MaSoSach)
                              select c).SingleOrDefault();
                        if (ct != null) return false;
                        ct = new CHITIETHOADONNXB
                        {
                            masohoadon = masohoadon,
                            masosach = chitiet.MaSoSach,
                            soluong = chitiet.SoLuong,
                            dongia = chitiet.DonGia
                        };
                        db.CHITIETHOADONNXBs.InsertOnSubmit(ct);
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
            public static bool edit(ChiTietHoaDonNXB chitiet)
            {
                try
                {
                    using (EntitiesDataContext db = new EntitiesDataContext())
                    {
                        CHITIETHOADONNXB ct;
                        ct = (from c in db.CHITIETHOADONNXBs
                              where c.masohoadon.Equals(chitiet.MaSoHoaDon)
                              && c.masosach.Equals(chitiet.MaSoSach)
                              select c).SingleOrDefault();
                        if (ct == null) return false;
                        ct.soluong = chitiet.SoLuong;
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
            public static bool delete(ChiTietHoaDonNXB chitiet)
            {
                try
                {
                    using (EntitiesDataContext db = new EntitiesDataContext())
                    {
                        CHITIETHOADONNXB ct;
                        ct = (from c in db.CHITIETHOADONNXBs
                              where c.masohoadon.Equals(chitiet.MaSoHoaDon)
                              && c.masosach.Equals(chitiet.MaSoSach)
                              select c).SingleOrDefault();
                        if (ct == null) return false;
                        db.CHITIETHOADONNXBs.DeleteOnSubmit(ct);
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
}

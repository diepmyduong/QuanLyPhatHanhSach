using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;

namespace Core.DAL
{
    public class HoaDonDaiLyManager
    {
        public static partial class Properties
        {
            public const string MaSoHoaDon = "Mã Số Hóa Đơn";
            public const string MaSoDaiLy = "Mã Số Đại lý";
            public const string DaiLy = "Tên Đại Lý";
            public const string NgayLap = "Ngày Lập";
            public const string TongTien = "Tổng tiền";
            public const string ChiTiet = "Chi tiết";
        }

        public static List<HoaDonDaiLy> getAll()
        {
            using(EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from hd in db.HOADONDAILies
                                join dl in db.DAILies
                                on hd.masodaily equals dl.masodaily
                                select new HoaDonDaiLy()
                                {
                                    MaSoHoaDon = hd.masohoadon,
                                    MaSoDaiLy = hd.masodaily,
                                    DaiLy = new DaiLy()
                                    {
                                        MaSoDaiLy = dl.masodaily,
                                        TenDaiLy = dl.ten,
                                        DiaChi = dl.diachi,
                                        SoDienThoai = dl.sodienthoai,
                                        SoTaiKhoan = dl.sotaikhoan
                                    },
                                    TongTien = hd.tongtien,
                                    NgayLap  = hd.ngaylap
                                };
                return linqQuery.ToList<HoaDonDaiLy>();
            }
        }

        public static HoaDonDaiLy find(int masohoadon)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from hd in db.HOADONDAILies
                                join dl in db.DAILies
                                on hd.masodaily equals dl.masodaily
                                where hd.masodaily.Equals(masohoadon)
                                select new HoaDonDaiLy()
                                {
                                    MaSoHoaDon = hd.masohoadon,
                                    MaSoDaiLy = hd.masodaily,
                                    DaiLy = new DaiLy()
                                    {
                                        MaSoDaiLy = dl.masodaily,
                                        TenDaiLy = dl.ten,
                                        DiaChi = dl.diachi,
                                        SoDienThoai = dl.sodienthoai,
                                        SoTaiKhoan = dl.sotaikhoan
                                    },
                                    TongTien = hd.tongtien,
                                    NgayLap = hd.ngaylap
                                };
                return linqQuery.SingleOrDefault<HoaDonDaiLy>();
            }
        }

        public static List<HoaDonDaiLy> findBy(Dictionary<string, dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;

                var linqQuery = (from hd in db.HOADONDAILies
                                 join dl in db.DAILies
                                 on hd.masodaily equals dl.masodaily
                                 select new HoaDonDaiLy()
                                 {
                                     MaSoHoaDon = hd.masohoadon,
                                     MaSoDaiLy = hd.masodaily,
                                     DaiLy = new DaiLy()
                                     {
                                         MaSoDaiLy = dl.masodaily,
                                         TenDaiLy = dl.ten,
                                         DiaChi = dl.diachi,
                                         SoDienThoai = dl.sodienthoai,
                                         SoTaiKhoan = dl.sotaikhoan
                                     },
                                     TongTien = hd.tongtien,
                                     NgayLap = hd.ngaylap
                                 })
                                 .Where(hd => hd.MaSoHoaDon.Equals(
                                        Params.TryGetValue(Properties.MaSoHoaDon, out value) ? value as int?
                                        : hd.MaSoHoaDon
                                 )).Where(hd => hd.MaSoDaiLy.Equals(
                                        Params.TryGetValue(Properties.MaSoDaiLy, out value) ? value as int?
                                        : hd.MaSoDaiLy
                                 )).Where(hd => hd.NgayLap.Equals(
                                        Params.TryGetValue(Properties.NgayLap, out value) ? value as DateTime?
                                        : hd.NgayLap
                                 )).Where(hd => hd.TongTien.Equals(
                                        Params.TryGetValue(Properties.MaSoHoaDon, out value) ? value as decimal?
                                        : hd.TongTien
                                 ));
                return linqQuery.ToList<HoaDonDaiLy>();
            }
        }



        //Chi tiết hóa đơn
        public partial class ChiTiet
        {
            public partial class Properties
            {
                public const string MaSoSach = "Mã Số Sách";
                public const string Sach = "Tên Sách";
                public const string SoLuong = "Số lượng";
                public const string DonGia = "Đơn Giá";
                public const string ThanhTien = "Thành tiền";
                public const string MaSoHoaDon = "Mã Số Hóa Đơn";
                public const string HoaDon = "Hóa đơn";
            }

            public static List<ChiTietHoaDonDaiLy> getAll()
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    var linqQuery = from ct in db.CHITIETHOADONDAILies
                                    join s in db.SACHes
                                    on ct.masosach equals s.masosach
                                    select new ChiTietHoaDonDaiLy()
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
                    return linqQuery.ToList<ChiTietHoaDonDaiLy>();
                }
            }

            public static List<ChiTietHoaDonDaiLy> find(int masohoadon)
            {
                using(EntitiesDataContext db = new EntitiesDataContext())
                {
                    var linqQuery = from ct in db.CHITIETHOADONDAILies
                                    join s in db.SACHes
                                    on ct.masosach equals s.masosach
                                    where ct.masohoadon.Equals(masohoadon)
                                    select new ChiTietHoaDonDaiLy()
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
                    return linqQuery.ToList<ChiTietHoaDonDaiLy>();
                }
            }

            public static List<ChiTietHoaDonDaiLy> findBy(Dictionary<string, dynamic> Params)
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    dynamic value;

                    var linqQuery = (from ct in db.CHITIETHOADONDAILies
                                     join s in db.SACHes
                                     on ct.masosach equals s.masosach
                                     select new ChiTietHoaDonDaiLy()
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
                    return linqQuery.ToList<ChiTietHoaDonDaiLy>();
                }
            }
        }

        
    }
}

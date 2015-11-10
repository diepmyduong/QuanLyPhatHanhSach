using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;

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
                                    TongTien = hd.tongtien
                                };
                return linqQuery.ToList<HoaDonNXB>();
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
                                    TongTien = hd.tongtien
                                };
                return linqQuery.SingleOrDefault<HoaDonNXB>();
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
                                     TongTien = hd.tongtien
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
                return linqQuery.ToList<HoaDonNXB>();
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
                    return linqQuery.ToList<ChiTietHoaDonNXB>();
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
                    return linqQuery.ToList<ChiTietHoaDonNXB>();
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
                    return linqQuery.ToList<ChiTietHoaDonNXB>();
                }
            }
        }
    }
}

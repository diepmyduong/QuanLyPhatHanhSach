using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;

namespace Core.DAL
{
    public class CongNoNXBManager
    {
        public static partial class Properties
        {
            public const string MaSoSach = "Mã số sách";
            public const string Sach = "Tên Sách";
            public const string MaSoNXB = "Mã Số NXB";
            public const string NXB = "Tên NXB";
            public const string SoLuong = "Só lượng";
            public const string DonGia = "Đơn giá";
            public const string Thang = "Tháng";
            public const string ThanhTien = "Thành tiền";
        }

        public static List<CongNoNXB> getAll()
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from cn in db.CONGNONXBs
                                join s in db.SACHes
                                on cn.masosach equals s.masosach
                                join nxb in db.NXBs
                                on cn.masonxb equals nxb.masonxb
                                select new CongNoNXB()
                                {
                                    MaSoSach = cn.masosach,
                                    Sach = new Sach()
                                    {
                                        MaSoSach = s.masosach,
                                        TenSach = s.tensach,
                                        TenTacGia = s.tacgia,
                                        MaSoLinhVuc = s.masolinhvuc,
                                        MaSoNXB = s.masonxb,
                                        Soluong = s.soluong,
                                        GiaBan = s.giaban,
                                        GiaNhap = s.gianhap,
                                        HinhAnh = s.hinhanh
                                    },
                                    MaSoNXB = cn.masonxb,
                                    NXB = new NhaXuatBan
                                    {
                                        MaSoNXB = nxb.masonxb,
                                        TenNXB = nxb.ten,
                                        DiaChi = nxb.diachi,
                                        SoDienThoai = nxb.sodienthoai,
                                        SoTaiKhoan = nxb.sotaikhoan
                                    },
                                    SoLuong = cn.soluong,
                                    DonGia = cn.dongia,
                                    Thang = cn.thang
                                };
                return linqQuery.ToList<CongNoNXB>();
            }
        }

        public static List<CongNoNXB> find(int masonxb)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from cn in db.CONGNONXBs
                                join s in db.SACHes
                                on cn.masosach equals s.masosach
                                join nxb in db.NXBs
                                on cn.masonxb equals nxb.masonxb
                                where cn.masonxb.Equals(masonxb)
                                select new CongNoNXB()
                                {
                                    MaSoSach = cn.masosach,
                                    Sach = new Sach()
                                    {
                                        MaSoSach = s.masosach,
                                        TenSach = s.tensach,
                                        TenTacGia = s.tacgia,
                                        MaSoLinhVuc = s.masolinhvuc,
                                        MaSoNXB = s.masonxb,
                                        Soluong = s.soluong,
                                        GiaBan = s.giaban,
                                        GiaNhap = s.gianhap,
                                        HinhAnh = s.hinhanh
                                    },
                                    MaSoNXB = cn.masonxb,
                                    NXB = new NhaXuatBan
                                    {
                                        MaSoNXB = nxb.masonxb,
                                        TenNXB = nxb.ten,
                                        DiaChi = nxb.diachi,
                                        SoDienThoai = nxb.sodienthoai,
                                        SoTaiKhoan = nxb.sotaikhoan
                                    },
                                    SoLuong = cn.soluong,
                                    DonGia = cn.dongia,
                                    Thang = cn.thang
                                };
                return linqQuery.ToList<CongNoNXB>();
            }
        }

        public static List<CongNoNXB> findBy(Dictionary<string,dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;

                var linqQuery = (from cn in db.CONGNONXBs
                                 join s in db.SACHes
                                 on cn.masosach equals s.masosach
                                 join nxb in db.NXBs
                                 on cn.masonxb equals nxb.masonxb
                                 select new CongNoNXB()
                                 {
                                     MaSoSach = cn.masosach,
                                     Sach = new Sach()
                                     {
                                         MaSoSach = s.masosach,
                                         TenSach = s.tensach,
                                         TenTacGia = s.tacgia,
                                         MaSoLinhVuc = s.masolinhvuc,
                                         MaSoNXB = s.masonxb,
                                         Soluong = s.soluong,
                                         GiaBan = s.giaban,
                                         GiaNhap = s.gianhap,
                                         HinhAnh = s.hinhanh
                                     },
                                     MaSoNXB = cn.masonxb,
                                     NXB = new NhaXuatBan
                                     {
                                         MaSoNXB = nxb.masonxb,
                                         TenNXB = nxb.ten,
                                         DiaChi = nxb.diachi,
                                         SoDienThoai = nxb.sodienthoai,
                                         SoTaiKhoan = nxb.sotaikhoan
                                     },
                                     SoLuong = cn.soluong,
                                     DonGia = cn.dongia,
                                     Thang = cn.thang
                                 })
                                 .Where(cn => cn.MaSoSach.Equals(
                                        Params.TryGetValue(Properties.MaSoSach, out value) ? value as int?
                                        : cn.MaSoSach
                                 )).Where(cn => cn.MaSoNXB.Equals(
                                        Params.TryGetValue(Properties.MaSoNXB, out value) ? value as int?
                                        : cn.MaSoNXB
                                 )).Where(cn => cn.SoLuong.Equals(
                                        Params.TryGetValue(Properties.SoLuong, out value) ? value as decimal?
                                        : cn.SoLuong
                                 )).Where(cn => cn.DonGia.Equals(
                                        Params.TryGetValue(Properties.DonGia, out value) ? value as decimal?
                                        : cn.DonGia
                                 )).Where(cn => cn.Thang.Equals(
                                        Params.TryGetValue(Properties.Thang, out value) ? value as DateTime?
                                        : cn.Thang
                                 ));
                return linqQuery.ToList<CongNoNXB>();
            }
        }
    }
}

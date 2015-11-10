using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;

namespace Core.DAL
{
    public class CongNoDaiLyManager
    {
        //Thuộc tính của 1 BIZ
        public static partial class Properties
        {
            public const string MaSoSach = "Mã số sách";
            public const string Sach = "Tên Sách";
            public const string MaSoDaiLy = "Mã Số Đại lý";
            public const string DaiLy = "Tên Đại Lý";
            public const string SoLuong = "Só lượng";
            public const string DonGia = "Đơn giá";
            public const string Thang = "Tháng";
            public const string ThanhTien = "Thành tiền";
        }

        public static List<CongNoDaiLy> getAll()
        {
            using(EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from cn in db.CONGNODAILies
                                join s in db.SACHes
                                on cn.masosach equals s.masosach
                                join d in db.DAILies
                                on cn.masodaily equals d.masodaily
                                select new CongNoDaiLy()
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
                                    MaSoDaiLy = cn.masodaily,
                                    DaiLy = new DaiLy()
                                    {
                                        MaSoDaiLy = d.masodaily,
                                        TenDaiLy = d.ten,
                                        DiaChi = d.diachi,
                                        SoDienThoai = d.sodienthoai,
                                        SoTaiKhoan = d.sotaikhoan
                                    },
                                    SoLuong = cn.soluong,
                                    DonGia = cn.dongia,
                                    Thang = cn.thang
                                };
                return linqQuery.ToList<CongNoDaiLy>();
            }
        }

        public static List<CongNoDaiLy> find(int masodaily)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from cn in db.CONGNODAILies
                                join s in db.SACHes
                                on cn.masosach equals s.masosach
                                join d in db.DAILies
                                on cn.masodaily equals d.masodaily
                                where cn.masodaily.Equals(masodaily)
                                select new CongNoDaiLy()
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
                                    MaSoDaiLy = cn.masodaily,
                                    DaiLy = new DaiLy()
                                    {
                                        MaSoDaiLy = d.masodaily,
                                        TenDaiLy = d.ten,
                                        DiaChi = d.diachi,
                                        SoDienThoai = d.sodienthoai,
                                        SoTaiKhoan = d.sotaikhoan
                                    },
                                    SoLuong = cn.soluong,
                                    DonGia = cn.dongia,
                                    Thang = cn.thang
                                };
                return linqQuery.ToList<CongNoDaiLy>();
            }
        }

        public static List<CongNoDaiLy> findBy(Dictionary<string,dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;

                var linqQuery = (from cn in db.CONGNODAILies
                                 join s in db.SACHes
                                 on cn.masosach equals s.masosach
                                 join d in db.DAILies
                                 on cn.masodaily equals d.masodaily
                                 select new CongNoDaiLy()
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
                                     MaSoDaiLy = cn.masodaily,
                                     DaiLy = new DaiLy()
                                     {
                                         MaSoDaiLy = d.masodaily,
                                         TenDaiLy = d.ten,
                                         DiaChi = d.diachi,
                                         SoDienThoai = d.sodienthoai,
                                         SoTaiKhoan = d.sotaikhoan
                                     },
                                     SoLuong = cn.soluong,
                                     DonGia = cn.dongia,
                                     Thang = cn.thang
                                 })
                                 .Where(cn => cn.MaSoSach.Equals(
                                        Params.TryGetValue(Properties.MaSoSach, out value) ? value as int?
                                        : cn.MaSoSach
                                 )).Where(cn => cn.MaSoDaiLy.Equals(
                                        Params.TryGetValue(Properties.MaSoDaiLy, out value) ? value as int?
                                        : cn.MaSoDaiLy
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
                return linqQuery.ToList<CongNoDaiLy>();
            }
        }
    }
}

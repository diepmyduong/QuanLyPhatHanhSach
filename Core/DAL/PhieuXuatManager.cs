using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;

namespace Core.DAL
{
    public class PhieuXuatManager
    {
        public static partial class Properties
        {
            public const string MaSoPhieuXuat = "Mã số phiếu xuất";
            public const string MaSoDaiLy = "Mã số Đại lý";
            public const string DaiLy = "Tên Đại Lý";
            public const string NgayLap = "Ngày lập";
            public const string NguoiNhan = "Người nhận";
            public const string TongTien = "Tổng tiền";
            public const string ChiTiet = "Chi tiết";
        }

        public static List<PhieuXuat> getAll()
        {
            using(EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from phieu in db.PHIEUXUATs
                                join dl in db.DAILies
                                on phieu.masodaily equals dl.masodaily
                                select new PhieuXuat
                                {
                                    MaSoPhieuXuat = phieu.masophieuxuat,
                                    Daily = new DaiLy()
                                    {
                                        MaSoDaiLy = dl.masodaily,
                                        TenDaiLy = dl.ten,
                                        DiaChi = dl.diachi,
                                        SoDienThoai = dl.sodienthoai,
                                        SoTaiKhoan = dl.sotaikhoan
                                    },
                                    NgayLap = phieu.ngaylap,
                                    NguoiNhan = phieu.nguoinhasach,
                                    TongTien = phieu.tongtien
                                };
                return linqQuery.ToList<PhieuXuat>();
            }
        }

        public static List<PhieuXuat> find(int masodaily)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from phieu in db.PHIEUXUATs
                                join dl in db.DAILies
                                on phieu.masodaily equals dl.masodaily
                                select new PhieuXuat
                                {
                                    MaSoPhieuXuat = phieu.masophieuxuat,
                                    Daily = new DaiLy()
                                    {
                                        MaSoDaiLy = dl.masodaily,
                                        TenDaiLy = dl.ten,
                                        DiaChi = dl.diachi,
                                        SoDienThoai = dl.sodienthoai,
                                        SoTaiKhoan = dl.sotaikhoan
                                    },
                                    NgayLap = phieu.ngaylap,
                                    NguoiNhan = phieu.nguoinhasach,
                                    TongTien = phieu.tongtien
                                };
                return linqQuery.ToList<PhieuXuat>();
            }
        }

        public static List<PhieuXuat> findBy(Dictionary<string,dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;

                var linqQuery = (from phieu in db.PHIEUXUATs
                                 join dl in db.DAILies
                                 on phieu.masodaily equals dl.masodaily
                                 select new PhieuXuat
                                 {
                                     MaSoPhieuXuat = phieu.masophieuxuat,
                                     Daily = new DaiLy()
                                     {
                                         MaSoDaiLy = dl.masodaily,
                                         TenDaiLy = dl.ten,
                                         DiaChi = dl.diachi,
                                         SoDienThoai = dl.sodienthoai,
                                         SoTaiKhoan = dl.sotaikhoan
                                     },
                                     NgayLap = phieu.ngaylap,
                                     NguoiNhan = phieu.nguoinhasach,
                                     TongTien = phieu.tongtien
                                 })
                                 .Where(phieu => phieu.MaSoPhieuXuat.Equals(
                                        Params.TryGetValue(Properties.MaSoPhieuXuat, out value) ? value as int?
                                        : phieu.MaSoPhieuXuat
                                 )).Where(phieu => phieu.NguoiNhan.Equals(
                                        Params.TryGetValue(Properties.NguoiNhan, out value) ? value as string
                                        : phieu.NguoiNhan
                                 )).Where(phieu => phieu.NgayLap.Equals(
                                        Params.TryGetValue(Properties.NgayLap, out value) ? value as DateTime?
                                        : phieu.NgayLap
                                 )).Where(phieu => phieu.TongTien.Equals(
                                        Params.TryGetValue(Properties.TongTien, out value) ? value as decimal?
                                        : phieu.TongTien
                                 )).Where(phieu => phieu.Daily.MaSoDaiLy.Equals(
                                        Params.TryGetValue(Properties.DaiLy, out value) ? value as int?
                                        : phieu.Daily.MaSoDaiLy
                                 ));
                return linqQuery.ToList<PhieuXuat>();
            }
        }


        //Chi tiết Phiếu Xuất
        public partial class Chitiet
        {
            public partial class Properties
            {
                public const string MaSoSach = "Mã số sách";
                public const string Sach = "Tên sách";
                public const string SoLuong = "Số lượng";
                public const string DonGia = "Đơn Giá";
                public const string ThanhTien = "Thành tiền";
            }

            public static List<ChiTietPhieuXuat> getAll()
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    var linqQuery = from ct in db.CHITIETPHIEUXUATs
                                    join s in db.SACHes
                                    on ct.masosach equals s.masosach
                                    select new ChiTietPhieuXuat()
                                    {
                                        MaSoSach = ct.masosach,
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
                                        SoLuong = ct.soluong,
                                        DonGia = ct.dongia
                                    };
                    return linqQuery.ToList<ChiTietPhieuXuat>();
                }
            }

            public static List<ChiTietPhieuXuat> find(int masophieuxuat)
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    var linqQuery = from ct in db.CHITIETPHIEUXUATs
                                    join s in db.SACHes
                                    on ct.masosach equals s.masosach
                                    where ct.masophieuxuat.Equals(masophieuxuat)
                                    select new ChiTietPhieuXuat()
                                    {
                                        MaSoSach = ct.masosach,
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
                                        SoLuong = ct.soluong,
                                        DonGia = ct.dongia
                                    };
                    return linqQuery.ToList<ChiTietPhieuXuat>();
                }
            }
        }
    }
}

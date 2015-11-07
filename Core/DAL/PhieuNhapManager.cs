using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;
using Core.DAL;

namespace Core.DAL
{
    public class PhieuNhapManager
    {
        public static partial class Properties
        {
            public const string MaSoPhieuNhap = "Mã phiếu nhập";
            public const string MaSoNXB = "Mã Số NXB";
            public const string NXB = "Nhà Xuất Bản";
            public const string NgayLap = "Ngày lập";
            public const string NguoiGiao = "Người giao";
            public const string TongTien = "Tổng tiền";
            public const string ChiTiet = "Chi Tiết";
            
        }

        public static List<PhieuNhap> getAll()
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from phieu in db.PHIEUNHAPs
                                join nxb in db.NXBs
                                on phieu.masonxb equals nxb.masonxb
                                select new PhieuNhap
                                {
                                    MaSoPhieuNhap = phieu.masophieunhap,
                                    MaSoNXB = phieu.masonxb,
                                    NXB = new NhaXuatBan()
                                    {
                                        MaSoNXB = nxb.masonxb,
                                        TenNXB = nxb.ten,
                                        DiaChi = nxb.diachi,
                                        SoDienThoai = nxb.sodienthoai,
                                        SoTaiKhoan = nxb.sotaikhoan
                                    },
                                    NgayLap = phieu.ngaylap,
                                    NguoiGiao = phieu.nguoigiaosach,
                                    TongTien = phieu.tongtien
                                };
                return linqQuery.ToList<PhieuNhap>();
            }
        }

        public static bool add(PhieuNhap phieunhap)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    var phieu = new PHIEUNHAP()
                    {
                        masonxb = phieunhap.NXB.MaSoNXB,
                        ngaylap = phieunhap.NgayLap,
                        nguoigiaosach = phieunhap.NguoiGiao,
                        tongtien = phieunhap.TongTien,
                    };
                    db.PHIEUNHAPs.InsertOnSubmit(phieu);
                    db.SubmitChanges();
                    PhieuNhapManager.ChiTiet.add(phieunhap.ChiTiet, phieunhap.MaSoPhieuNhap);
                    return true;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }
        public static List<PhieuNhap> find(int masonxb)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from phieu in db.PHIEUNHAPs
                                join nxb in db.NXBs
                                on phieu.masonxb equals nxb.masonxb
                                where phieu.masonxb.Equals(masonxb)
                                select new PhieuNhap
                                {
                                    MaSoPhieuNhap = phieu.masophieunhap,
                                    MaSoNXB = phieu.masonxb,
                                    NXB = new NhaXuatBan()
                                    {
                                        MaSoNXB = nxb.masonxb,
                                        TenNXB = nxb.ten,
                                        DiaChi = nxb.diachi,
                                        SoDienThoai = nxb.sodienthoai,
                                        SoTaiKhoan = nxb.sotaikhoan
                                    },
                                    NgayLap = phieu.ngaylap,
                                    NguoiGiao = phieu.nguoigiaosach,
                                    TongTien = phieu.tongtien
                                };
                return linqQuery.ToList<PhieuNhap>();
            }
        }

        public static List<PhieuNhap> findBy(Dictionary<string,dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;

                var linqQuery = (from phieu in db.PHIEUNHAPs
                                 join nxb in db.NXBs
                                 on phieu.masonxb equals nxb.masonxb
                                 select new PhieuNhap
                                 {
                                     MaSoPhieuNhap = phieu.masophieunhap,
                                     MaSoNXB = phieu.masonxb,
                                     NXB = new NhaXuatBan()
                                     {
                                         MaSoNXB = nxb.masonxb,
                                         TenNXB = nxb.ten,
                                         DiaChi = nxb.diachi,
                                         SoDienThoai = nxb.sodienthoai,
                                         SoTaiKhoan = nxb.sotaikhoan
                                     },
                                     NgayLap = phieu.ngaylap,
                                     NguoiGiao = phieu.nguoigiaosach,
                                     TongTien = phieu.tongtien
                                 })
                                 .Where(phieu => phieu.MaSoPhieuNhap.Equals(
                                        Params.TryGetValue(Properties.MaSoPhieuNhap, out value) ? value as int?
                                        : phieu.MaSoPhieuNhap
                                 )).Where(phieu => phieu.NguoiGiao.Equals(
                                        Params.TryGetValue(Properties.NguoiGiao, out value) ? value as string
                                        : phieu.NguoiGiao
                                 )).Where(phieu => phieu.NgayLap.Equals(
                                        Params.TryGetValue(Properties.NgayLap, out value) ? value as DateTime?
                                        : phieu.NgayLap
                                 )).Where(phieu => phieu.TongTien.Equals(
                                        Params.TryGetValue(Properties.TongTien, out value) ? value as decimal?
                                        : phieu.TongTien
                                 )).Where(phieu => phieu.NXB.MaSoNXB.Equals(
                                        Params.TryGetValue(Properties.NXB, out value) ? value as int?
                                        : phieu.NXB.MaSoNXB
                                 ));
                return linqQuery.ToList<PhieuNhap>();
            }
        }

        //Chi tiết phiếu nhập
        public partial class ChiTiet
        {
            public partial class Properties
            {
                public const string MaSoSach = "Mã Số Sách";
                public const string Sach = "Tên Sách";
                public const string SoLuong = "Số lượng";
                public const string DonGia = "Đơn Giá";
                public const string ThanhTien = "Thành tiền";
            }
            public static List<ChiTietPhieuNhap> getAll()
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    var linqQuery = from chitiet in db.CHITIETPHIEUNHAPs
                                    join s in db.SACHes
                                    on chitiet.masosach equals s.masosach
                                    select new ChiTietPhieuNhap
                                    {
                                        MaSoSach = chitiet.masosach,
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
                                        DonGia = chitiet.dongia,
                                        Soluong = chitiet.soluong
                                    };
                    return linqQuery.ToList<ChiTietPhieuNhap>();
                }
            }

            public static List<ChiTietPhieuNhap> find(int masophieunhap)
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    var linqQuery = from chitiet in db.CHITIETPHIEUNHAPs
                                    join s in db.SACHes
                                    on chitiet.masosach equals s.masosach
                                    where chitiet.masophieunhap.Equals(masophieunhap)
                                    select new ChiTietPhieuNhap
                                    {
                                        MaSoSach = chitiet.masosach,
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
                                        DonGia = chitiet.dongia,
                                        Soluong = chitiet.soluong
                                    };
                    return linqQuery.ToList<ChiTietPhieuNhap>();
                }
            }

            public static bool add(ChiTietPhieuNhap chitiet,int masophieunhap)
            {
                try
                {
                    using (EntitiesDataContext db = new EntitiesDataContext())
                    {
                        CHITIETPHIEUNHAP ct = new CHITIETPHIEUNHAP()
                        {
                            masosach = chitiet.Sach.MaSoSach,
                            dongia = chitiet.DonGia,
                            soluong = chitiet.Soluong,
                            masophieunhap = masophieunhap
                        };
                        db.CHITIETPHIEUNHAPs.InsertOnSubmit(ct);
                        db.SubmitChanges();
                        return true;
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                
            }

            public static bool add(List<ChiTietPhieuNhap> dmchitiet, int masophieunhap)
            {
                try
                {
                    using (EntitiesDataContext db = new EntitiesDataContext())
                    {
                        foreach (ChiTietPhieuNhap chitiet in dmchitiet)
                        {
                            CHITIETPHIEUNHAP ct = new CHITIETPHIEUNHAP()
                            {
                                masosach = chitiet.Sach.MaSoSach,
                                dongia = chitiet.DonGia,
                                soluong = chitiet.Soluong,
                                masophieunhap = masophieunhap
                            };
                            db.CHITIETPHIEUNHAPs.InsertOnSubmit(ct);
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
        }
    }
}

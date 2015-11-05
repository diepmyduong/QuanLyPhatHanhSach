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
            public const string NXB = "Nhà Xuất Bản";
            public const string NgayLap = "Ngày lập";
            public const string NguoiGiao = "Người giao";
            public const string TongTien = "Tổng tiền";
            public const string ThanhTien = "Thành tiền";
            
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

        //Chi tiết phiếu nhập
        public partial class ChiTiet
        {
            public partial class Properties
            {
                public const string SoLuong = "Số lượng";
                public const string DonGia = "Đơn Giá";
                public const string ThanhTien = "Thành tiền";
            }

            public static List<ChiTietPhieuNhap> find(int masophieunhap)
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    var linqQuery = from chitiet in db.CHITIETPHIEUNHAPs
                                    where chitiet.masophieunhap.Equals(masophieunhap)
                                    select new ChiTietPhieuNhap
                                    {
                                        Sach = SachManager.find(chitiet.masosach),
                                        DonGia = chitiet.dongia,
                                        Soluong = chitiet.soluong,
                                        ThanhTien = (chitiet.dongia * chitiet.soluong)
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

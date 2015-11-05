using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;

namespace Core.DAL
{
    public class SachManager
    {
        public static partial class Properties
        {
           
            public const string MaSoSach = "Mã Số Sách";
            public const string TenSach = "Tên Sách";
            public const string LinhVucSach = "Lĩnh Vực Sách";
            public const string TenTacGia = "Tên Tác Giả";
            public const string NXB = "Nhà Xuất Bản";
            public const string Soluong = "Số Lượng";
            public const string GiaNhap = "Giá Nhập";
            public const string GiaBan = "Giá Bán";
            public const string HinhAnh = "Hình Ảnh";
        }
        public static List<Sach> getAll()
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from s in db.SACHes
                                join nxb in db.NXBs on s.masonxb equals nxb.masonxb
                                join lv in db.LINHVUCs on s.masolinhvuc equals lv.masolinhvuc
                                select new Sach
                                {
                                    MaSoSach = s.masosach,
                                    TenSach = s.tensach,
                                    LinhVucSach = new LinhVuc
                                    {
                                        MaSoLinhVuc = s.masolinhvuc,
                                        TenLinhVuc = lv.ten
                                    },
                                    TenTacGia = s.tacgia,
                                    NXB = new NhaXuatBan
                                    {
                                        MaSoNXB = s.masonxb,
                                        TenNXB = nxb.ten,
                                        DiaChi = nxb.diachi,
                                        SoDienThoai = nxb.sodienthoai,
                                        SoTaiKhoan = nxb.sotaikhoan
                                    },
                                    Soluong = s.soluong,
                                    GiaBan = s.giaban,
                                    GiaNhap = s.gianhap,
                                    HinhAnh = s.hinhanh
                                };
                return linqQuery.ToList<Sach>();
            }
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
                db.SubmitChanges();
                return true;
            }
        }

        public static bool add(Sach sach)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                SACH s;
                s = (from b in db.SACHes
                         where b.tensach.Equals(sach.TenSach)
                            && b.masolinhvuc.Equals(sach.LinhVucSach.MaSoLinhVuc)
                            && b.masonxb.Equals(sach.NXB.MaSoNXB)
                         select b).SingleOrDefault();
                if (s != null) return false;
                s = new SACH();
                s.tensach = sach.TenSach;
                s.masolinhvuc = sach.LinhVucSach.MaSoLinhVuc;
                s.masonxb = sach.NXB.MaSoNXB;
                s.tacgia = sach.TenTacGia;
                s.soluong = sach.Soluong;
                s.giaban = sach.GiaBan;
                s.gianhap = sach.GiaNhap;
                s.hinhanh = sach.HinhAnh;
                db.SACHes.InsertOnSubmit(s);
                db.SubmitChanges();
                return true;
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
                                select new Sach
                                {
                                    MaSoSach = s.masosach,
                                    TenSach = s.tensach,
                                    LinhVucSach = new LinhVuc
                                    {
                                        MaSoLinhVuc = s.masolinhvuc,
                                        TenLinhVuc = lv.ten
                                    },
                                    TenTacGia = s.tacgia,
                                    NXB = new NhaXuatBan
                                    {
                                        MaSoNXB = s.masonxb,
                                        TenNXB = nxb.ten,
                                        DiaChi = nxb.diachi,
                                        SoDienThoai = nxb.sodienthoai,
                                        SoTaiKhoan = nxb.sotaikhoan
                                    },
                                    Soluong = s.soluong,
                                    GiaBan = s.giaban,
                                    GiaNhap = s.gianhap,
                                    HinhAnh = s.hinhanh
                                };
                return linqQuery.SingleOrDefault();
            }
        }

        public static List<Sach> findBy(Dictionary<String,dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;

                var linqQuery = (from s in db.SACHes
                                 join nxb in db.NXBs on s.masonxb equals nxb.masonxb
                                 join lv in db.LINHVUCs on s.masolinhvuc equals lv.masolinhvuc
                                 select new Sach
                                 {
                                     MaSoSach = s.masosach,
                                     TenSach = s.tensach,
                                     LinhVucSach = new LinhVuc
                                     {
                                         MaSoLinhVuc = s.masolinhvuc,
                                         TenLinhVuc = lv.ten
                                     },
                                     TenTacGia = s.tacgia,
                                     NXB = new NhaXuatBan
                                     {
                                         MaSoNXB = s.masonxb,
                                         TenNXB = nxb.ten,
                                         DiaChi = nxb.diachi,
                                         SoDienThoai = nxb.sodienthoai,
                                         SoTaiKhoan = nxb.sotaikhoan
                                     },
                                     Soluong = s.soluong,
                                     GiaBan = s.giaban,
                                     GiaNhap = s.gianhap,
                                     HinhAnh = s.hinhanh
                                 })
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
                                        Params.TryGetValue(Properties.NXB, out value) ? value as int?
                                        : s.NXB.MaSoNXB
                                 ))
                                 .Where(s => s.Soluong.Equals(
                                        Params.TryGetValue(Properties.Soluong, out value) ? value as int?
                                        : s.Soluong
                                 ))
                                 .Where(s => s.GiaBan.Equals(
                                        Params.TryGetValue(Properties.GiaBan, out value) ? value as int?
                                        : s.GiaBan
                                 ))
                                 .Where(s => s.GiaNhap.Equals(
                                        Params.TryGetValue(Properties.GiaNhap, out value) ? value as int?
                                        : s.GiaNhap
                                 ));
                return linqQuery.ToList<Sach>();
            }
        }
    }
}

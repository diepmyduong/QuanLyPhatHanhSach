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
    }
}

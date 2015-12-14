using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;
using System.Text.RegularExpressions;

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
            public const string TrangThai = "Trạng thái";
        }

        public static List<PhieuXuat> getAll()
        {
            using(EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from phieu in db.PHIEUXUATs
                                join dl in db.DAILies
                                on phieu.masodaily equals dl.masodaily
                                select new PhieuXuat(phieu, dl);
                return linqQuery.ToList();
            }
        }
        public static PhieuXuat find(int masophieuxuat)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from phieu in db.PHIEUXUATs
                                join dl in db.DAILies
                                on phieu.masodaily equals dl.masodaily
                                where phieu.masophieuxuat.Equals(masophieuxuat)
                                select new PhieuXuat(phieu, dl);
                return linqQuery.SingleOrDefault();
            }
        }

        public static List<PhieuXuat> findBy(Dictionary<string,dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;

                var linqQuery = getAll()
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
                                 )).Where(phieu => phieu.MaSoDaiLy.Equals(
                                        Params.TryGetValue(Properties.MaSoDaiLy, out value) ? value as int?
                                        : phieu.MaSoDaiLy
                                 )).Where(phieu => phieu.TrangThai.Equals(
                                        Params.TryGetValue(Properties.TrangThai, out value) ? value as int?
                                        : phieu.TrangThai
                                 ));
                return linqQuery.ToList();
            }
        }
        public static List<PhieuXuat> getUnaproved()
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from phieu in db.PHIEUXUATs
                                join daily in db.DAILies
                                on phieu.masodaily equals daily.masodaily
                                where phieu.trangthai == 0
                                select new PhieuXuat(phieu, daily);
                return linqQuery.ToList();
            }
        }

        public static List<PhieuXuat> filter(string request, List<PhieuXuat> DMPhieuXuat)
        {
            try
            {
                if (Regex.IsMatch(request, @"[{=<>!}]"))
                {
                    var linqQuery = from s in DMPhieuXuat
                                    select s;

                    MatchCollection args = Regex.Matches(request, @"({).*?(})");
                    foreach (var arg in args)
                    {
                        MatchCollection Params = Regex.Matches(arg.ToString(), @"\w+");
                        string method = Regex.Match(arg.ToString(), @"[=<>!]+").ToString();
                        string param = "";
                        int? year = null, month = null, day = null;
                        for (int i = 1; i < Params.Count; i++)
                        {
                            param += " " + Params[i];
                            if (i == 1)
                            {
                                int number;
                                if (Int32.TryParse(Params[i].ToString(), out number))
                                {
                                    year = number;
                                }
                            }
                            if (i == 2)
                            {
                                int number;
                                if (Int32.TryParse(Params[i].ToString(), out number))
                                {
                                    month = number;
                                }
                            }
                            if (i == 3)
                            {
                                int number;
                                if (Int32.TryParse(Params[i].ToString(), out number))
                                {
                                    day = number;
                                }
                            }
                        }
                        param = param.Trim();
                        switch (Params[0].ToString())
                        {
                            case nameof(Properties.MaSoPhieuXuat):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.MaSoPhieuXuat, Int32.Parse(param), method, false));
                                break;
                            case nameof(Properties.MaSoDaiLy):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.MaSoDaiLy, Int32.Parse(param), method, false));
                                break;
                            case nameof(DaiLyManager.Properties.TenDaiLy):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.Daily.TenDaiLy, param, method, true));
                                break;
                            case nameof(Properties.NguoiNhan):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.NguoiNhan, param, method, true));
                                break;
                            case nameof(Properties.NgayLap):
                                linqQuery = linqQuery.Where(s => FilterHelper.compareDate(s.NgayLap, year, month, day, method));
                                break;
                            case nameof(Properties.TongTien):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.TongTien, Decimal.Parse(param), method, false));
                                break;
                            case nameof(Properties.TrangThai):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.TrangThai == 1 ? "Đã duyệt" : "Chưa duyệt", param, method, true));
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
                        var linqQuery = DMPhieuXuat.Where
                        (s => s.MaSoPhieuXuat.Equals(number)
                        || s.TongTien.Equals(number)
                        || s.NgayLap.Year.Equals(number)
                        || s.NgayLap.Month.Equals(number)
                        || s.NgayLap.Day.Equals(number)
                        );
                        return linqQuery.ToList();
                    }
                    else
                    {
                        var linqQuery = DMPhieuXuat.Where
                        (s => s.NgayLap.ToString().ToLower().Contains(request)
                        || s.Daily.TenDaiLy.ToLower().Contains(request)
                        || s.NguoiNhan.ToLower().Contains(request)
                        || (s.TrangThai == 1 ? "đã duyệt" : "chưa duyệt").Contains(request)
                        );
                        return linqQuery.ToList();
                    }
                }
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }

        public static List<PhieuXuat> filter(string request)
        {
            var DMPhieuXuat = getAll();
            return filter(request, DMPhieuXuat);
        }

        public static bool edit(PhieuXuat phieu)
        {
            try
            {
                using(EntitiesDataContext db = new EntitiesDataContext())
                {
                    PHIEUXUAT px;
                    px = (from p in db.PHIEUXUATs
                          where p.masophieuxuat.Equals(phieu.MaSoPhieuXuat)
                          select p).SingleOrDefault();
                    if (px == null) return false;
                    px.masodaily = phieu.MaSoDaiLy;
                    px.ngaylap = phieu.NgayLap;
                    px.nguoinhasach = phieu.NguoiNhan;
                    px.trangthai = phieu.TrangThai;
                    px.tongtien = phieu.ChiTiet.Sum(ct => ct.SoLuong * ct.DonGia); // tính tổng tiền các chi tiết
                    db.CHITIETPHIEUXUATs.DeleteAllOnSubmit(px.CHITIETPHIEUXUATs);
                    db.SubmitChanges();
                    foreach (ChiTietPhieuXuat ct in phieu.ChiTiet)
                    {
                        ct.MaSoPhieuXuat = phieu.MaSoPhieuXuat;
                        Chitiet.add(ct);
                    }
                    
                    return true;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static int add(PhieuXuat phieuxuat)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    var phieu = new PHIEUXUAT
                    {
                        masodaily = phieuxuat.MaSoDaiLy,
                        ngaylap = phieuxuat.NgayLap,
                        nguoinhasach = phieuxuat.NguoiNhan,
                        tongtien = phieuxuat.ChiTiet.Sum(ct => ct.SoLuong * ct.DonGia),
                        trangthai = 0
                    };
                    db.PHIEUXUATs.InsertOnSubmit(phieu);
                    db.SubmitChanges();
                    foreach(ChiTietPhieuXuat ct in phieuxuat.ChiTiet)
                    {
                        Chitiet.add(ct, phieu.masophieuxuat);
                    }
                    return phieu.masophieuxuat;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public static bool delete(int masophieuxuat)
        {
            try
            {
                using(EntitiesDataContext db = new EntitiesDataContext())
                {
                    PHIEUXUAT phieu;
                    phieu = (from p in db.PHIEUXUATs
                             where p.masophieuxuat.Equals(masophieuxuat)
                             select p).SingleOrDefault();
                    if (phieu == null) return false;
                    db.CHITIETPHIEUXUATs.DeleteAllOnSubmit(phieu.CHITIETPHIEUXUATs);
                    db.PHIEUXUATs.DeleteOnSubmit(phieu);
                    db.SubmitChanges();
                    return true;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
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
                public const string MaSoPhieuXuat = "Mã số phiếu xuất";
                public const string PhieuXuat = "Ngày lập";
                public const string TrangThai = "Trạng thái";
            }

            public static List<ChiTietPhieuXuat> getAll()
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    var linqQuery = from ct in db.CHITIETPHIEUXUATs
                                    //join s in db.SACHes
                                    //on ct.masosach equals s.masosach
                                    select new ChiTietPhieuXuat(ct);
                    return linqQuery.ToList();
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
                                    select new ChiTietPhieuXuat(ct, s);
                    return linqQuery.ToList();
                }
            }

            public static List<ChiTietPhieuXuat> findBy(Dictionary<string,dynamic> Params)
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    dynamic value;

                    var linqQuery = getAll()
                                     .Where(ct => ct.MaSoPhieuXuat.Equals(
                                            Params.TryGetValue(Properties.MaSoPhieuXuat, out value) ? value as int?
                                            : ct.MaSoPhieuXuat
                                     )).Where(ct => ct.MaSoSach.Equals(
                                            Params.TryGetValue(Properties.MaSoSach, out value) ? value as int?
                                            : ct.MaSoSach
                                     )).Where(ct => ct.SoLuong.Equals(
                                            Params.TryGetValue(Properties.SoLuong, out value) ? value as decimal?
                                            : ct.SoLuong
                                     )).Where(ct => ct.DonGia.Equals(
                                            Params.TryGetValue(Properties.DonGia, out value) ? value as decimal?
                                            : ct.DonGia
                                     )).Where(ct => ct.TrangThai.Equals(
                                            Params.TryGetValue(Properties.TrangThai, out value) ? value as int?
                                            : ct.TrangThai
                                     ));
                    return linqQuery.ToList();
                }
            }

            public static bool add(ChiTietPhieuXuat chitiet)
            {
                return add(chitiet, chitiet.MaSoPhieuXuat);
            }

            public static bool add(ChiTietPhieuXuat chitiet, int masophieuxuat)
            {
                try
                {
                    using (EntitiesDataContext db = new EntitiesDataContext())
                    {
                        CHITIETPHIEUXUAT ct;
                        ct = (from c in db.CHITIETPHIEUXUATs
                              where c.masophieuxuat.Equals(masophieuxuat)
                              && c.masosach.Equals(chitiet.MaSoSach)
                              select c).SingleOrDefault();
                        if (ct != null) return false;
                        ct = new CHITIETPHIEUXUAT
                        {
                            masophieuxuat = masophieuxuat,
                            masosach = chitiet.MaSoSach,
                            soluong = chitiet.SoLuong,
                            dongia = chitiet.DonGia,
                            trangthai = chitiet.TrangThai == null ? 0 : 1
                        };
                        db.CHITIETPHIEUXUATs.InsertOnSubmit(ct);
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

            public static bool edit(ChiTietPhieuXuat chitiet)
            {
                try
                {
                    using(EntitiesDataContext db = new EntitiesDataContext())
                    {
                        CHITIETPHIEUXUAT ct;
                        ct = (from c in db.CHITIETPHIEUXUATs
                              where c.masophieuxuat.Equals(chitiet.MaSoPhieuXuat)
                              && c.masosach.Equals(chitiet.MaSoSach)
                              select c).SingleOrDefault();
                        if (ct == null) return false;
                        ct.soluong = chitiet.SoLuong;
                        ct.trangthai = chitiet.TrangThai;
                        db.SubmitChanges();
                        return true;
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }

            public static bool delete(ChiTietPhieuXuat chitiet)
            {
                try
                {
                    using (EntitiesDataContext db = new EntitiesDataContext())
                    {
                        CHITIETPHIEUXUAT ct;
                        ct = (from c in db.CHITIETPHIEUXUATs
                              where c.masophieuxuat.Equals(chitiet.MaSoPhieuXuat)
                              && c.masosach.Equals(chitiet.MaSoSach)
                              select c).SingleOrDefault();
                        if (ct == null) return false;
                        db.CHITIETPHIEUXUATs.DeleteOnSubmit(ct);
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

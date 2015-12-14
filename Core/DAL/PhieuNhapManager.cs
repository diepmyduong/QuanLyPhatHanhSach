using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;
using Core.DAL;
using System.Text.RegularExpressions;

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
            public const string TrangThai = "Trạng Thái";
            
        }

        public static List<PhieuNhap> getAll()
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from phieu in db.PHIEUNHAPs
                                join nxb in db.NXBs
                                on phieu.masonxb equals nxb.masonxb
                                select new PhieuNhap(phieu,nxb);
                return linqQuery.ToList();
            }
        }

        public static List<PhieuNhap> getUnaproved()
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from phieu in db.PHIEUNHAPs
                                join nxb in db.NXBs
                                on phieu.masonxb equals nxb.masonxb
                                where phieu.trangthai == 0
                                select new PhieuNhap(phieu, nxb);
                return linqQuery.ToList();
            }
        }
        public static PhieuNhap find(int masophieunhap)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from phieu in db.PHIEUNHAPs
                                join nxb in db.NXBs
                                on phieu.masonxb equals nxb.masonxb
                                where phieu.masophieunhap.Equals(masophieunhap)
                                select new PhieuNhap(phieu, nxb);
                return linqQuery.SingleOrDefault();
            }
        }
        public static List<PhieuNhap> findBy(Dictionary<string, dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;

                var linqQuery = getAll()
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
                                 )).Where(phieu => phieu.MaSoNXB.Equals(
                                        Params.TryGetValue(Properties.MaSoNXB, out value) ? value as int?
                                        : phieu.MaSoNXB
                                 )).Where(phieu => phieu.TrangThai.Equals(
                                        Params.TryGetValue(Properties.TrangThai, out value) ? value as int?
                                        : phieu.TrangThai
                                 ));
                return linqQuery.ToList();
            }
        }
        public static List<PhieuNhap> filter(string request, List<PhieuNhap> DMPhieuNhap)
        {
            try
            {
                if (Regex.IsMatch(request, @"[{=<>!}]"))
                {
                    var linqQuery = from s in DMPhieuNhap
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
                            case nameof(Properties.MaSoPhieuNhap):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.MaSoPhieuNhap, Int32.Parse(param), method, false));
                                break;
                            case nameof(Properties.MaSoNXB):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.MaSoNXB, Int32.Parse(param), method, false));
                                break;
                            case nameof(NhaXuatBanManager.Properties.TenNXB):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.NXB.TenNXB, param, method, true));
                                break;
                            case nameof(Properties.NguoiGiao):
                                linqQuery = linqQuery.Where(s => FilterHelper.compare(s.NguoiGiao, param, method, true));
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
                        var linqQuery = DMPhieuNhap.Where
                        (s => s.MaSoPhieuNhap.Equals(number)
                        || s.TongTien.Equals(number)
                        || s.NgayLap.Year.Equals(number)
                        || s.NgayLap.Month.Equals(number)
                        || s.NgayLap.Day.Equals(number)
                        );
                        return linqQuery.ToList();
                    }
                    else
                    {
                        var linqQuery = DMPhieuNhap.Where
                        (s => s.NgayLap.ToString().ToLower().Contains(request)
                        || s.NXB.TenNXB.ToLower().Contains(request)
                        || s.NguoiGiao.ToLower().Contains(request)
                        || (s.TrangThai == 1 ? "đã duyệt" : "chưa duyệt").Contains(request)
                        );
                        return linqQuery.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<PhieuNhap> filter(string request)
        {
            var DMPhieuNhap = getAll();
            return filter(request, DMPhieuNhap);
        }
        public static int add(PhieuNhap phieunhap)
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
                        tongtien = phieunhap.ChiTiet.Sum(ct => ct.SoLuong * ct.DonGia),
                        trangthai = 0
                    };
                    db.PHIEUNHAPs.InsertOnSubmit(phieu);
                    db.SubmitChanges();
                    ChiTiet.add(phieunhap.ChiTiet, phieu.masophieunhap);
                    return phieu.masophieunhap;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            
        }
        public static bool edit(PhieuNhap phieu)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    PHIEUNHAP px;
                    px = (from p in db.PHIEUNHAPs
                          where p.masophieunhap.Equals(phieu.MaSoPhieuNhap)
                          select p).SingleOrDefault();
                    if (px == null) return false;
                    px.masonxb = phieu.MaSoNXB;
                    px.ngaylap = phieu.NgayLap;
                    px.nguoigiaosach = phieu.NguoiGiao;
                    px.trangthai = phieu.TrangThai;
                    px.tongtien = phieu.ChiTiet.Sum(ct => ct.SoLuong * ct.DonGia); // tính tổng tiền các chi tiết
                    db.CHITIETPHIEUNHAPs.DeleteAllOnSubmit(px.CHITIETPHIEUNHAPs);
                    db.SubmitChanges();
                    foreach (ChiTietPhieuNhap ct in phieu.ChiTiet)
                    {
                        ct.MaSoPhieuNhap = phieu.MaSoPhieuNhap;
                        ChiTiet.add(ct);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static bool delete(int masophieunhap)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    PHIEUNHAP phieu;
                    phieu = (from p in db.PHIEUNHAPs
                             where p.masophieunhap.Equals(masophieunhap)
                             select p).SingleOrDefault();
                    if (phieu == null) return false;
                    db.CHITIETPHIEUNHAPs.DeleteAllOnSubmit(phieu.CHITIETPHIEUNHAPs);
                    db.PHIEUNHAPs.DeleteOnSubmit(phieu);
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
                public const string MaSoPhieuNhap = "Mã số phiếu";
                public const string PhieuNhap = "Ngày lập";
                public const string TrangThai = "Trạng thái";
            }
            public static List<ChiTietPhieuNhap> getAll()
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    var linqQuery = from chitiet in db.CHITIETPHIEUNHAPs
                                    //join s in db.SACHes
                                    //on chitiet.masosach equals s.masosach
                                    select new ChiTietPhieuNhap(chitiet);
                    return linqQuery.ToList();
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
                                    select new ChiTietPhieuNhap(chitiet, s);
                    return linqQuery.ToList();
                }
            }
            public static List<ChiTietPhieuNhap> findBy(Dictionary<string,dynamic> Params)
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    dynamic value;

                    var linqQuery = getAll()
                                     .Where(chitiet => chitiet.MaSoPhieuNhap.Equals(
                                            Params.TryGetValue(Properties.MaSoPhieuNhap, out value) ? value as int?
                                            : chitiet.MaSoPhieuNhap
                                     )).Where(chitiet => chitiet.MaSoSach.Equals(
                                            Params.TryGetValue(Properties.MaSoSach, out value) ? value as int?
                                            : chitiet.MaSoSach
                                     )).Where(chitiet => chitiet.SoLuong.Equals(
                                            Params.TryGetValue(Properties.SoLuong, out value) ? value as decimal?
                                            : chitiet.SoLuong
                                     )).Where(chitiet => chitiet.DonGia.Equals(
                                            Params.TryGetValue(Properties.DonGia, out value) ? value as decimal?
                                            : chitiet.DonGia
                                     )).Where(chitiet => chitiet.TrangThai.Equals(
                                            Params.TryGetValue(Properties.TrangThai, out value) ? value as int?
                                            : chitiet.TrangThai
                                     ));
                    return linqQuery.ToList();
                }
            }
            public static bool add(ChiTietPhieuNhap chitiet)
            {
                return add(chitiet, chitiet.MaSoPhieuNhap);
            }
            public static bool add(ChiTietPhieuNhap chitiet,int masophieunhap)
            {
                try
                {
                    using (EntitiesDataContext db = new EntitiesDataContext())
                    {
                        CHITIETPHIEUNHAP ct;
                        ct = (from c in db.CHITIETPHIEUNHAPs
                              where c.masophieunhap.Equals(masophieunhap)
                              && c.masosach.Equals(chitiet.MaSoSach)
                              select c).SingleOrDefault();
                        if (ct != null) return false;
                        ct = new CHITIETPHIEUNHAP()
                            {
                                masosach = chitiet.Sach.MaSoSach,
                                dongia = chitiet.DonGia,
                                soluong = chitiet.SoLuong,
                                masophieunhap = masophieunhap,
                                trangthai = chitiet.TrangThai == null ? 0 : 1
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
                                soluong = chitiet.SoLuong,
                                masophieunhap = masophieunhap,
                                trangthai = 0
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
            public static bool edit(ChiTietPhieuNhap chitiet)
            {
                try
                {
                    using (EntitiesDataContext db = new EntitiesDataContext())
                    {
                        CHITIETPHIEUNHAP ct;
                        ct = (from c in db.CHITIETPHIEUNHAPs
                              where c.masophieunhap.Equals(chitiet.MaSoPhieuNhap)
                              && c.masosach.Equals(chitiet.MaSoSach)
                              select c).SingleOrDefault();
                        if (ct == null) return false;
                        ct.soluong = chitiet.SoLuong;
                        ct.trangthai = chitiet.TrangThai;
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
            public static bool delete(ChiTietPhieuNhap chitiet)
            {
                try
                {
                    using (EntitiesDataContext db = new EntitiesDataContext())
                    {
                        CHITIETPHIEUNHAP ct;
                        ct = (from c in db.CHITIETPHIEUNHAPs
                              where c.masophieunhap.Equals(chitiet.MaSoPhieuNhap)
                              && c.masosach.Equals(chitiet.MaSoSach)
                              select c).SingleOrDefault();
                        if (ct == null) return false;
                        db.CHITIETPHIEUNHAPs.DeleteOnSubmit(ct);
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

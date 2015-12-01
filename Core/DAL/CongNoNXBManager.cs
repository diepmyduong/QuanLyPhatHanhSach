using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;
using System.Text.RegularExpressions;

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
            public const string TrangThai = "Trạng thái";
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
                                select new CongNoNXB(cn,nxb, s);
                return linqQuery.ToList();
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
                                select new CongNoNXB(cn,nxb, s);
                return linqQuery.ToList();
            }
        }
        public static List<CongNoNXB> findBy(Dictionary<string,dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;
                var linqQuery = getAll()
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
                return linqQuery.ToList();
            }
        }
        public static List<CongNoNXB> filter(string request, List<CongNoNXB> DMCongNo)
        {

            if (Regex.IsMatch(request, @"[{=<>!}]"))
            {
                var linqQuery = from cn in DMCongNo
                                select cn;

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
                        case nameof(Properties.MaSoSach):
                            linqQuery = linqQuery.Where(cn => FilterHelper.compare(cn.MaSoSach, Int32.Parse(param), method, false));
                            break;
                        case nameof(Properties.MaSoNXB):
                            linqQuery = linqQuery.Where(cn => FilterHelper.compare(cn.MaSoNXB, Int32.Parse(param), method, false));
                            break;
                        case nameof(Properties.SoLuong):
                            linqQuery = linqQuery.Where(cn => FilterHelper.compare(cn.SoLuong, Decimal.Parse(param), method, false));
                            break;
                        case nameof(Properties.DonGia):
                            linqQuery = linqQuery.Where(cn => FilterHelper.compare(cn.DonGia, Decimal.Parse(param), method, false));
                            break;
                        case nameof(Properties.Thang):
                            linqQuery = linqQuery.Where(cn => FilterHelper.compareDate(cn.Thang, year, month, day, method));
                            break;
                        case nameof(Properties.ThanhTien):
                            linqQuery = linqQuery.Where(cn => FilterHelper.compare(cn.ThanhTien, Decimal.Parse(param), method, false));
                            break;
                        case nameof(NhaXuatBanManager.Properties.TenNXB):
                            linqQuery = linqQuery.Where(cn => FilterHelper.compare(cn.NXB.TenNXB, param, method, true));
                            break;
                        case nameof(SachManager.Properties.TenSach):
                            linqQuery = linqQuery.Where(cn => FilterHelper.compare(cn.Sach.TenSach, param, method, true));
                            break;
                        case nameof(SachManager.Properties.TenTacGia):
                            linqQuery = linqQuery.Where(cn => FilterHelper.compare(cn.Sach.TenTacGia, param, method, true));
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
                    var linqQuery = DMCongNo.Where
                    (cn => cn.MaSoSach.Equals(number)
                    || cn.MaSoNXB.Equals(number)
                    || cn.SoLuong.Equals(number)
                    || cn.DonGia.Equals(number)
                    || cn.ThanhTien.Equals(number)
                    || cn.Thang.Year.Equals(number)
                    || cn.Thang.Month.Equals(number)
                    );
                    return linqQuery.ToList();
                }
                else
                {
                    var linqQuery = DMCongNo.Where
                    (cn => cn.NXB.TenNXB.ToLower().Contains(request)
                    || cn.Sach.TenSach.ToLower().Contains(request)
                    || cn.Thang.ToString().ToLower().Contains(request)
                    || cn.Sach.TenTacGia.ToLower().Contains(request)
                    );
                    return linqQuery.ToList();
                }
            }
        }
        public static List<CongNoNXB> filter(string request)
        {
            var DMCongNo = getAll();
            return filter(request, DMCongNo);
        }
        public static int add(CongNoNXB congno)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    CONGNONXB cn;
                    cn = (from c in db.CONGNONXBs
                          where c.masonxb.Equals(congno.MaSoNXB)
                          && c.masosach.Equals(congno.MaSoSach)
                          && c.thang.Month.Equals(congno.Thang.Month)
                          && c.thang.Year.Equals(congno.Thang.Year)
                          select c).SingleOrDefault();
                    if (cn != null)
                    {
                        cn.soluong += congno.SoLuong;
                        db.SubmitChanges();
                        return 1;
                    }
                    else
                    {
                        cn = new CONGNONXB();
                        cn.masonxb = congno.MaSoNXB;
                        cn.masosach = congno.MaSoSach;
                        cn.soluong = congno.SoLuong;
                        cn.dongia = congno.DonGia;
                        cn.thang = congno.Thang;
                        db.CONGNONXBs.InsertOnSubmit(cn);
                        db.SubmitChanges();
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }
        public static bool edit(CongNoNXB congno)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    CONGNONXB cn;
                    cn = (from d in db.CONGNONXBs
                          where d.masonxb.Equals(congno.MaSoNXB)
                          && d.masosach.Equals(congno.MaSoSach)
                          && d.thang.Month.Equals(congno.Thang.Month)
                          && d.thang.Year.Equals(congno.Thang.Year)
                          select d).SingleOrDefault();
                    if (cn == null) return false; //Nếu đại lý không tồn tại
                    cn.soluong = congno.SoLuong;
                    cn.dongia = congno.DonGia;
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
        public static bool delete(CongNoNXB congno)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    CONGNONXB cn;
                    cn = (from d in db.CONGNONXBs
                          where d.masonxb.Equals(congno.MaSoNXB)
                          && d.masosach.Equals(congno.MaSoSach)
                          && d.thang.Month.Equals(congno.Thang.Month)
                          && d.thang.Year.Equals(congno.Thang.Year)
                          select d).SingleOrDefault();
                    if (cn == null) return false; //Nếu đại lý không tồn tại
                    db.CONGNONXBs.DeleteOnSubmit(cn);
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

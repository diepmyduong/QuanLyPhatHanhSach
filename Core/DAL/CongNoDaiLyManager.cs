using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;
using System.Text.RegularExpressions;

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
                                select new CongNoDaiLy(cn,d,s);
                return linqQuery.ToList();
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
                                select new CongNoDaiLy(cn,d, s);
                return linqQuery.ToList();
            }
        }
        public static List<CongNoDaiLy> findBy(Dictionary<string,dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;
                var linqQuery = getAll()
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
                return linqQuery.ToList();
            }
        }
        public static List<CongNoDaiLy> filter(string request, List<CongNoDaiLy> DMCongNo)
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
                        case nameof(Properties.MaSoDaiLy):
                            linqQuery = linqQuery.Where(cn => FilterHelper.compare(cn.MaSoDaiLy, Int32.Parse(param), method, false));
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
                        case nameof(DaiLyManager.Properties.TenDaiLy):
                            linqQuery = linqQuery.Where(cn => FilterHelper.compare(cn.DaiLy.TenDaiLy, param, method, true));
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
                    || cn.MaSoDaiLy.Equals(number)
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
                    (cn => cn.DaiLy.TenDaiLy.Contains(request)
                    || cn.Sach.TenSach.Contains(request)
                    || cn.Thang.ToString().Contains(request)
                    );
                    return linqQuery.ToList();
                }
            }
        }
        public static List<CongNoDaiLy> filter(string request)
        {
            var DMCongNo = getAll();
            return filter(request, DMCongNo);
        }
        public static int add(CongNoDaiLy congno)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    CONGNODAILY cn;
                    cn = (from c in db.CONGNODAILies
                          where c.masodaily.Equals(congno.MaSoDaiLy)
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
                        cn = new CONGNODAILY();
                        cn.masodaily = congno.MaSoDaiLy;
                        cn.masosach = congno.MaSoSach;
                        cn.soluong = congno.SoLuong;
                        cn.dongia = congno.DonGia;
                        cn.thang = congno.Thang;
                        db.CONGNODAILies.InsertOnSubmit(cn);
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
        public static bool edit(CongNoDaiLy congno)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    CONGNODAILY cn;
                    cn = (from d in db.CONGNODAILies
                          where d.masodaily.Equals(congno.MaSoDaiLy)
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
        public static bool delete(CongNoDaiLy congno)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    CONGNODAILY cn;
                    cn = (from d in db.CONGNODAILies
                          where d.masodaily.Equals(congno.MaSoDaiLy)
                          && d.masosach.Equals(congno.MaSoSach)
                          && d.thang.Month.Equals(congno.Thang.Month)
                          && d.thang.Year.Equals(congno.Thang.Year)
                          select d).SingleOrDefault();
                    if (cn == null) return false; //Nếu đại lý không tồn tại
                    db.CONGNODAILies.DeleteOnSubmit(cn);
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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;
using System.Text.RegularExpressions;

namespace Core.DAL
{
    public class DaiLyManager
    {
        public static partial class Properties
        {
            public const string MaSoDaiLy = "Mã số Đại lý";
            public const string TenDaiLy = "Tên Đại Lý";
            public const string DiaChi = "Địa chỉ";
            public const string SoDienThoai = "Số điện thoại";
            public const string SoTaiKhoan = "Số tài khoản";
            public const string PhieuXuat = "Phiếu Xuất";
            public const string CongNo = "Công nợ";
            public const string HoaDon = "Hóa đơn";
        }

        public static List<DaiLy> getAll()
        {
            using(EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from d in db.DAILies
                                where d.trangthai == null
                                select new DaiLy()
                                {
                                    MaSoDaiLy = d.masodaily,
                                    TenDaiLy = d.ten,
                                    DiaChi = d.diachi,
                                    SoDienThoai = d.sodienthoai,
                                    SoTaiKhoan = d.sotaikhoan
                                };
                return linqQuery.ToList();
            }
        }
        public static DaiLy find(int masodaily)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from d in db.DAILies
                                where d.masodaily.Equals(masodaily)
                                && d.trangthai == null
                                select new DaiLy()
                                {
                                    MaSoDaiLy = d.masodaily,
                                    TenDaiLy = d.ten,
                                    DiaChi = d.diachi,
                                    SoDienThoai = d.sodienthoai,
                                    SoTaiKhoan = d.sotaikhoan
                                };
                return linqQuery.SingleOrDefault();
            }
        }
        public static List<DaiLy> findBy(Dictionary<string,dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;
                var linqQuery = (from d in db.DAILies
                                 where d.trangthai == null
                                 select new DaiLy()
                                 {
                                     MaSoDaiLy = d.masodaily,
                                     TenDaiLy = d.ten,
                                     DiaChi = d.diachi,
                                     SoDienThoai = d.sodienthoai,
                                     SoTaiKhoan = d.sotaikhoan
                                 })
                                 .Where(d => d.MaSoDaiLy.Equals(
                                        Params.TryGetValue(Properties.MaSoDaiLy, out value) ? value as int?
                                        : d.MaSoDaiLy
                                 )).Where(d => d.TenDaiLy.Equals(
                                        Params.TryGetValue(Properties.TenDaiLy, out value) ? value as string
                                        : d.TenDaiLy
                                 )).Where(d => d.DiaChi.Equals(
                                        Params.TryGetValue(Properties.DiaChi, out value) ? value as string
                                        : d.DiaChi
                                 )).Where(d => d.SoDienThoai.Equals(
                                        Params.TryGetValue(Properties.SoDienThoai, out value) ? value as string
                                        : d.SoDienThoai
                                 )).Where(d => d.SoTaiKhoan.Equals(
                                        Params.TryGetValue(Properties.SoTaiKhoan, out value) ? value as string
                                        : d.SoTaiKhoan
                                 ));
                return linqQuery.ToList();
            }
        }
        public static List<DaiLy> filter(string request, List<DaiLy> DMDaiLy)
        {

            if (Regex.IsMatch(request, @"[{=<>!}]"))
            {
                var linqQuery = from dl in DMDaiLy
                                select dl;

                MatchCollection args = Regex.Matches(request, @"({).*?(})");
                foreach (var arg in args)
                {
                    MatchCollection Params = Regex.Matches(arg.ToString(), @"\w+");
                    string method = Regex.Match(arg.ToString(), @"[=<>!]+").ToString();
                    string param = "";
                    for (int i = 1; i < Params.Count; i++)
                    {
                        param += " " + Params[i];
                    }
                    param = param.Trim();
                    switch (Params[0].ToString())
                    {
                        case nameof(Properties.MaSoDaiLy):
                            linqQuery = linqQuery.Where(dl => FilterHelper.compare(dl.MaSoDaiLy, Int32.Parse(param), method, false));
                            break;
                        case nameof(Properties.TenDaiLy):
                            linqQuery = linqQuery.Where(dl => FilterHelper.compare(dl.TenDaiLy, param, method, true));
                            break;
                        case nameof(Properties.DiaChi):
                            linqQuery = linqQuery.Where(dl => FilterHelper.compare(dl.DiaChi, param, method, true));
                            break;
                        case nameof(Properties.SoDienThoai):
                            linqQuery = linqQuery.Where(dl => FilterHelper.compare(dl.SoDienThoai, param, method, true));
                            break;
                        case nameof(Properties.SoTaiKhoan):
                            linqQuery = linqQuery.Where(dl => FilterHelper.compare(dl.SoTaiKhoan, param, method, true));
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
                    var linqQuery = DMDaiLy.Where
                    (dl => dl.MaSoDaiLy.Equals(number)
                    || dl.SoDienThoai.Contains(number.ToString())
                    || dl.SoTaiKhoan.Contains(number.ToString())
                    );
                    return linqQuery.ToList();
                }
                else
                {
                    var linqQuery = DMDaiLy.Where
                    (dl => dl.TenDaiLy.Contains(request)
                    || dl.DiaChi.Contains(request)
                    );
                    return linqQuery.ToList();
                }
            }
        }
        public static List<DaiLy> filter(string request)
        {
            var DMDaiLy = getAll();
            return filter(request, DMDaiLy);
        }
        public static int add(DaiLy daily)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    DAILY dl;
                    dl = (from d in db.DAILies
                          where d.masodaily.Equals(daily.MaSoDaiLy)
                          select d).SingleOrDefault();
                    if (dl != null) return 0; //Nếu đại lý đã tồn tại
                    dl = new DAILY()
                    {
                        ten = daily.TenDaiLy,
                        diachi = daily.DiaChi,
                        sodienthoai = daily.SoDienThoai,
                        sotaikhoan = daily.SoTaiKhoan
                    };
                    db.DAILies.InsertOnSubmit(dl);
                    db.SubmitChanges();
                    return dl.masodaily;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            
        }
        public static bool edit(DaiLy daily)
        {
            try
            {
                using(EntitiesDataContext db = new EntitiesDataContext())
                {
                    DAILY dl;
                    dl = (from d in db.DAILies
                          where d.masodaily.Equals(daily.MaSoDaiLy)
                          select d).SingleOrDefault();
                    if (dl == null) return false; //Nếu đại lý không tồn tại
                    dl.ten = daily.TenDaiLy;
                    dl.diachi = daily.DiaChi;
                    dl.sodienthoai = daily.SoDienThoai;
                    dl.sotaikhoan = daily.SoTaiKhoan;
                    db.SubmitChanges();
                    return true;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static bool delete(DaiLy daily)
        {
            try
            {
                using (EntitiesDataContext db = new EntitiesDataContext())
                {
                    DAILY dl;
                    dl = (from d in db.DAILies
                          where d.masodaily.Equals(daily.MaSoDaiLy)
                          select d).SingleOrDefault();
                    if (dl == null) return false; //Nếu đại lý không tồn tại
                    dl.trangthai = 0;
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

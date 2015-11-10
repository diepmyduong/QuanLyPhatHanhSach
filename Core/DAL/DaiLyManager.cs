using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;

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
                                select new DaiLy()
                                {
                                    MaSoDaiLy = d.masodaily,
                                    TenDaiLy = d.ten,
                                    DiaChi = d.diachi,
                                    SoDienThoai = d.sodienthoai,
                                    SoTaiKhoan = d.sotaikhoan
                                };
                return linqQuery.ToList<DaiLy>();
            }
        }

        public static DaiLy find(int masodaily)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from d in db.DAILies
                                where d.masodaily.Equals(masodaily)
                                select new DaiLy()
                                {
                                    MaSoDaiLy = d.masodaily,
                                    TenDaiLy = d.ten,
                                    DiaChi = d.diachi,
                                    SoDienThoai = d.sodienthoai,
                                    SoTaiKhoan = d.sotaikhoan
                                };
                return linqQuery.SingleOrDefault<DaiLy>();
            }
        }

        public static List<DaiLy> findBy(Dictionary<string,dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;

                var linqQuery = (from d in db.DAILies
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
                return linqQuery.ToList<DaiLy>();
            }
        }

        
    }
}

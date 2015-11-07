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
    }
}

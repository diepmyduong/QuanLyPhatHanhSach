using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;

namespace Core.DAL
{
    public class NhaXuatBanManager
    {
        public static List<NhaXuatBan> getAll()
        {
            using(EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from nxb in db.NXBs
                                select new NhaXuatBan
                                {
                                    MaSoNXB = nxb.masonxb,
                                    TenNXB = nxb.ten,
                                    DiaChi = nxb.diachi,
                                    SoDienThoai = nxb.sodienthoai,
                                    SoTaiKhoan = nxb.sotaikhoan
                                };
                return linqQuery.ToList<NhaXuatBan>();
            }
        }
    }
}

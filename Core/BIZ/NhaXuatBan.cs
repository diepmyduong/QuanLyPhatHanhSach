using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BIZ
{
    public class NhaXuatBan
    {
        [System.ComponentModel.DisplayName("Mã Nhà Xuất Bản")]
        public int MaSoNXB { get; set; }
        [System.ComponentModel.DisplayName("Tên Nhà Xuất Bản")]
        public String TenNXB { get; set; }
        [System.ComponentModel.DisplayName("Địa chỉ")]
        public String DiaChi { get; set; }
        [System.ComponentModel.DisplayName("Số điện thoại")]
        public String SoDienThoai { get; set; }
        [System.ComponentModel.DisplayName("Số tài khoản")]
        public String SoTaiKhoan { get; set; }

        public override string ToString()
        {
            return this.TenNXB;
        }
    }
}

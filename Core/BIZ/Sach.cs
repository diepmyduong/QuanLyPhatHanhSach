using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BIZ
{
    public class Sach
    {
        [System.ComponentModel.DisplayName("Mã số sách")]
        public int MaSoSach { get; set; }
        [System.ComponentModel.DisplayName("Tên Sách")]
        public String TenSach { get; set; }
        [System.ComponentModel.DisplayName("Lĩnh Vực")]
        public LinhVuc LinhVucSach { get; set; }
        [System.ComponentModel.DisplayName("Tác Giả")]
        public String TenTacGia { get; set; }
        [System.ComponentModel.DisplayName("Nhà Xuất Bản")]
        public NhaXuatBan NXB { get; set; }
        [System.ComponentModel.DisplayName("Số lượng")]
        public int Soluong { get; set; }
        [System.ComponentModel.DisplayName("Giá Nhập")]
        public int GiaNhap { get; set; }
        [System.ComponentModel.DisplayName("Giá Bán")]
        public int GiaBan { get; set; }
        [System.ComponentModel.DisplayName("Hình Ảnh")]
        public String HinhAnh { get; set; }

        public override string ToString()
        {
            return this.TenSach;
        }

       

    }
}

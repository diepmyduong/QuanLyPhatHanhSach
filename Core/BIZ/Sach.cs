using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DAL;
using System.ComponentModel;

namespace Core.BIZ
{
    public class Sach
    {
        [DisplayName(SachManager.Properties.MaSoSach)]
        public int MaSoSach { get; set; }
        [DisplayName(SachManager.Properties.TenSach)]
        public String TenSach { get; set; }
        [DisplayName(SachManager.Properties.LinhVucSach)]
        public LinhVuc LinhVucSach { get; set; }
        [DisplayName(SachManager.Properties.TenTacGia)]
        public String TenTacGia { get; set; }
        [DisplayName(SachManager.Properties.NXB)]
        public NhaXuatBan NXB { get; set; }
        [DisplayName(SachManager.Properties.Soluong)]
        public int Soluong { get; set; }
        [DisplayName(SachManager.Properties.GiaNhap)]
        public int GiaNhap { get; set; }
        [DisplayName(SachManager.Properties.GiaBan)]
        public int GiaBan { get; set; }
        [DisplayName(SachManager.Properties.HinhAnh)]
        public String HinhAnh { get; set; }

        public bool isExisted()
        {
            Sach sach = SachManager.find(this.MaSoSach);
            if(sach == null)
            {
                return false;
            }
            return true;
        }

        public bool isContentExisted()
        {
            var obj = new { };
            Dictionary<String, dynamic> param = new Dictionary<string, dynamic>();
            param.Add(SachManager.Properties.TenSach, this.TenSach);
            param.Add(SachManager.Properties.TenTacGia, this.TenTacGia);
            param.Add(SachManager.Properties.LinhVucSach, this.LinhVucSach.MaSoLinhVuc);
            param.Add(SachManager.Properties.NXB, this.NXB.MaSoNXB);
            List<Sach> result = SachManager.findBy(param);
            if (result.Count > 0)
                return true;
            return false;
        }

        public override string ToString()
        {
            return this.TenSach;
        }

    }
}

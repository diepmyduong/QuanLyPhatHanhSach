using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DAL;
using System.ComponentModel;

namespace Core.BIZ
{
    public class ChiTietPhieuNhap
    {
        [DisplayName(SachManager.Properties.TenSach)]
        public Sach Sach { get; set; }
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.SoLuong)]
        public int Soluong { get; set; }
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.DonGia)]
        public int DonGia { get; set; }
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.ThanhTien)]
        public int ThanhTien { get; set; }

        public override bool Equals(object obj)
        {
            return this.Sach.MaSoSach.Equals(((ChiTietPhieuNhap)obj).Sach.MaSoSach);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

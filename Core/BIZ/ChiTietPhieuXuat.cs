using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Core.DAL;

namespace Core.BIZ
{
    public class ChiTietPhieuXuat
    {
        private Sach _sach;

        [DisplayName(PhieuXuatManager.Chitiet.Properties.MaSoSach)]
        public int MaSoSach { get; set; }
        [DisplayName(PhieuXuatManager.Chitiet.Properties.Sach)]
        public Sach Sach {
            get
            {
                if(_sach == null)
                {
                    _sach = SachManager.find(this.MaSoSach);
                }
                return _sach;
            }
            set
            {
                _sach = value;
            }
        }
        [DisplayName(PhieuXuatManager.Chitiet.Properties.SoLuong)]
        public decimal SoLuong { get; set; }
        [DisplayName(PhieuXuatManager.Chitiet.Properties.DonGia)]
        public decimal DonGia { get; set; }
        [DisplayName(PhieuXuatManager.Chitiet.Properties.ThanhTien)]
        public decimal ThanhTien {
            get
            {
                return this.SoLuong * this.DonGia;
            }
        }

        public override bool Equals(object obj)
        {
            return this.Sach.MaSoSach.Equals(((ChiTietPhieuXuat)obj).Sach.MaSoSach);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return this.Sach.TenSach;
        }
    }
}

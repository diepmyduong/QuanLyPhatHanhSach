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
        private PhieuXuat _phieuxuat;

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
        [DisplayName(PhieuXuatManager.Chitiet.Properties.MaSoPhieuXuat)]
        public int MaSoPhieuXuat { get; set; }
        [DisplayName(PhieuXuatManager.Chitiet.Properties.PhieuXuat)]
        public PhieuXuat PhieuXuat
        {
            get
            {
                if(_phieuxuat == null)
                {
                    _phieuxuat = PhieuXuatManager.find(this.MaSoPhieuXuat);
                }
                return _phieuxuat;
            }
            set
            {
                _phieuxuat = value;
            }
        }

        public override bool Equals(object obj)
        {
            try
            {
                return this.Sach.MaSoSach.Equals(((ChiTietPhieuXuat)obj).Sach.MaSoSach);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
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

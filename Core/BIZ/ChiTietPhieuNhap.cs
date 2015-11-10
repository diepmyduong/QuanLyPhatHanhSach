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
        private Sach _sach;
        private PhieuNhap _phieunhap;

        [DisplayName(PhieuNhapManager.ChiTiet.Properties.MaSoSach)]
        public int MaSoSach { get; set; }
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.Sach)]
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
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.SoLuong)]
        public decimal SoLuong { get; set; }
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.DonGia)]
        public decimal DonGia { get; set; }
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.ThanhTien)]
        public decimal ThanhTien {
            get
            {
                return this.SoLuong * this.DonGia;
            }
        }
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.MaSoPhieuNhap)]
        public int MaSoPhieuNhap { get; set; }
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.PhieuNhap)]
        public PhieuNhap PhieuNhap
        {
            get
            {
                if(_phieunhap == null)
                {
                    _phieunhap = PhieuNhapManager.find(this.MaSoPhieuNhap);
                }
                return _phieunhap;
            }
            set
            {
                _phieunhap = value;
            }
        }

        public override bool Equals(object obj)
        {
            try
            {
                return this.Sach.MaSoSach.Equals(((ChiTietPhieuNhap)obj).Sach.MaSoSach);
            }
            catch(Exception ex)
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

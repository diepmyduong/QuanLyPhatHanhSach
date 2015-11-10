using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Core.DAL;

namespace Core.BIZ
{
    public class ChiTietHoaDonDaiLy
    {
        private HoaDonDaiLy _hoadon;
        private Sach _sach;

        [DisplayName(HoaDonDaiLyManager.ChiTiet.Properties.MaSoSach)]
        public int MaSoSach { get; set; }
        [DisplayName(HoaDonDaiLyManager.ChiTiet.Properties.Sach)]
        public Sach Sach
        {
            get
            {
                if (_sach == null)
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
        [DisplayName(HoaDonDaiLyManager.ChiTiet.Properties.SoLuong)]
        public decimal SoLuong { get; set; }
        [DisplayName(HoaDonDaiLyManager.ChiTiet.Properties.DonGia)]
        public decimal DonGia { get; set; }
        [DisplayName(HoaDonDaiLyManager.ChiTiet.Properties.ThanhTien)]
        public decimal ThanhTien
        {
            get
            {
                return this.SoLuong * this.DonGia;
            }
        }
        [DisplayName(HoaDonDaiLyManager.ChiTiet.Properties.MaSoHoaDon)]
        public int MaSoHoaDon { get; set; }
        [DisplayName(HoaDonDaiLyManager.ChiTiet.Properties.HoaDon)]
        public HoaDonDaiLy HoaDon
        {
            get
            {
                if(_hoadon == null)
                {
                    _hoadon = HoaDonDaiLyManager.find(this.MaSoHoaDon);
                }
                return _hoadon;
            }
            set
            {
                _hoadon = value;
            }
        }

        public override string ToString()
        {
            return this.Sach.TenSach;
        }
    }
}

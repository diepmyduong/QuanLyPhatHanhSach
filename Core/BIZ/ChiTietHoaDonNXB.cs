using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Core.DAL;

namespace Core.BIZ
{
    public class ChiTietHoaDonNXB
    {
        private Sach _sach;
        private HoaDonNXB _hoadon;

        [DisplayName(HoaDonNXBManager.ChiTiet.Properties.MaSoSach)]
        public int MaSoSach { get; set; }
        [DisplayName(HoaDonNXBManager.ChiTiet.Properties.Sach)]
        public Sach Sach
        {
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
        [DisplayName(HoaDonNXBManager.ChiTiet.Properties.SoLuong)]
        public decimal SoLuong { get; set; }
        [DisplayName(HoaDonNXBManager.ChiTiet.Properties.DonGia)]
        public decimal DonGia { get; set; }
        [DisplayName(HoaDonNXBManager.ChiTiet.Properties.ThanhTien)]
        public decimal ThanhTien
        {
            get
            {
                return this.SoLuong * this.DonGia;
            }
        }
        [DisplayName(HoaDonNXBManager.ChiTiet.Properties.MaSoHoaDon)]
        public int MaSoHoaDon { get; set; }
        [DisplayName(HoaDonNXBManager.ChiTiet.Properties.HoaDon)]
        public HoaDonNXB HoaDon
        {
            get
            {
                if(_hoadon == null)
                {
                    _hoadon = HoaDonNXBManager.find(this.MaSoHoaDon);
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

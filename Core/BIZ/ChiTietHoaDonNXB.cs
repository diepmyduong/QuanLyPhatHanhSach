using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Core.DAL;
using System.ComponentModel.DataAnnotations;

namespace Core.BIZ
{
    public class ChiTietHoaDonNXB
    {
        public ChiTietHoaDonNXB() { }
        public ChiTietHoaDonNXB(CHITIETHOADONNXB chitiet)
        {
            MaSoSach = chitiet.masosach;
            SoLuong = chitiet.soluong;
            DonGia = chitiet.dongia;
            MaSoHoaDon = chitiet.masohoadon;
            TrangThai = chitiet.trangthai;
        }
        public ChiTietHoaDonNXB(CHITIETHOADONNXB chitiet, SACH sach)
            :this(chitiet)
        {
            Sach = new Sach(sach);
        }

        #region Private Properties
        private Sach _sach;
        private HoaDonNXB _hoadon;
        #endregion

        #region Public Properties
        [Required]
        [DisplayName(HoaDonNXBManager.ChiTiet.Properties.MaSoSach)]
        public int MaSoSach { get; set; }
        [DisplayName(HoaDonNXBManager.ChiTiet.Properties.Sach)]
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
        [Required]
        [DisplayName(HoaDonNXBManager.ChiTiet.Properties.SoLuong)]
        public decimal SoLuong { get; set; }
        [Required]
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
        [Required]
        [DisplayName(HoaDonNXBManager.ChiTiet.Properties.MaSoHoaDon)]
        public int MaSoHoaDon { get; set; }
        [DisplayName(HoaDonNXBManager.ChiTiet.Properties.HoaDon)]
        public HoaDonNXB HoaDon
        {
            get
            {
                if (_hoadon == null)
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
        [DisplayName(HoaDonNXBManager.ChiTiet.Properties.TrangThai)]
        public int? TrangThai { get; set; }
        #endregion

        #region Services
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return this.Sach.TenSach;
        }
        public override bool Equals(object obj)
        {
            try
            {
                return this.MaSoSach.Equals(((ChiTietHoaDonNXB)obj).MaSoSach);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion





    }
}

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
    public class ChiTietHoaDonDaiLy
    {
        public ChiTietHoaDonDaiLy() { }
        public ChiTietHoaDonDaiLy(CHITIETHOADONDAILY chitiet)
        {
            MaSoSach = chitiet.masosach;
            MaSoHoaDon = chitiet.masohoadon;
            SoLuong = chitiet.soluong;
            DonGia = chitiet.dongia;
            TrangThai = chitiet.trangthai;
        }
        public ChiTietHoaDonDaiLy(CHITIETHOADONDAILY chitiet, SACH sach)
            :this(chitiet)
        {
            Sach = new Sach(sach);
        }

        #region Private Properties
        private HoaDonDaiLy _hoadon;
        private Sach _sach;
        #endregion

        #region Public Properties
        [Required]
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
        [Required]
        [DisplayName(HoaDonDaiLyManager.ChiTiet.Properties.SoLuong)]
        public decimal SoLuong { get; set; }
        [Required]
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
        [Required]
        [DisplayName(HoaDonDaiLyManager.ChiTiet.Properties.MaSoHoaDon)]
        public int MaSoHoaDon { get; set; }
        [DisplayName(HoaDonDaiLyManager.ChiTiet.Properties.HoaDon)]
        public HoaDonDaiLy HoaDon
        {
            get
            {
                if (_hoadon == null)
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
        [DisplayName(HoaDonDaiLyManager.ChiTiet.Properties.TrangThai)]
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
                return this.MaSoSach.Equals(((ChiTietHoaDonDaiLy)obj).MaSoSach);
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

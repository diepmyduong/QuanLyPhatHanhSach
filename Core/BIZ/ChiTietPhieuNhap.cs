using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DAL;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.BIZ
{
    public class ChiTietPhieuNhap
    {
        public ChiTietPhieuNhap() { }
        public ChiTietPhieuNhap(CHITIETPHIEUNHAP chitiet)
        {
            MaSoPhieuNhap = chitiet.masophieunhap;
            MaSoSach = chitiet.masosach;
            SoLuong = chitiet.soluong;
            DonGia = chitiet.dongia;
            TrangThai = chitiet.trangthai;
        }
        public ChiTietPhieuNhap(CHITIETPHIEUNHAP chitiet, SACH sach)
            : this(chitiet)
        {
            Sach = new Sach(sach);
        }

        #region Private Properties
        private Sach _sach;
        private PhieuNhap _phieunhap;
        #endregion

        #region Public Properties
        [Required]
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.MaSoSach)]
        public int MaSoSach { get; set; }
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.Sach)]
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
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.SoLuong)]
        public decimal SoLuong { get; set; }
        [Required]
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.DonGia)]
        public decimal DonGia { get; set; }
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.ThanhTien)]
        public decimal ThanhTien
        {
            get
            {
                return this.SoLuong * this.DonGia;
            }
        }
        [Required]
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.MaSoPhieuNhap)]
        public int MaSoPhieuNhap { get; set; }
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.PhieuNhap)]
        public PhieuNhap PhieuNhap
        {
            get
            {
                if (_phieunhap == null)
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
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.TrangThai)]
        public int? TrangThai { get; set; }
        #endregion

        #region Services
        #endregion

        #region Override Methods
        public override bool Equals(object obj)
        {
            try
            {
                return this.MaSoSach.Equals(((ChiTietPhieuNhap)obj).MaSoSach);
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

        public override string ToString()
        {
            return this.Sach.TenSach;
        }
        #endregion




    }
}

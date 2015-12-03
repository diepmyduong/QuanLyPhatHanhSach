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
    public class ChiTietPhieuXuat
    {
        public ChiTietPhieuXuat() { }
        public ChiTietPhieuXuat(CHITIETPHIEUXUAT chitiet)
        {
            MaSoPhieuXuat = chitiet.masophieuxuat;
            MaSoSach = chitiet.masosach;
            SoLuong = chitiet.soluong;
            DonGia = chitiet.dongia;
            TrangThai = chitiet.trangthai;
        }
        public ChiTietPhieuXuat(CHITIETPHIEUXUAT chitiet, SACH sach)
            : this(chitiet)
        {
            Sach = new Sach(sach);
        }

        #region Private Properties
        private Sach _sach;
        private PhieuXuat _phieuxuat;
        #endregion

        #region Public Properties
        [Required]
        [DisplayName(PhieuXuatManager.Chitiet.Properties.MaSoSach)]
        public int MaSoSach { get; set; }
        [DisplayName(PhieuXuatManager.Chitiet.Properties.Sach)]
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
        [DisplayName(PhieuXuatManager.Chitiet.Properties.SoLuong)]
        public decimal SoLuong { get; set; }
        [Required]
        [DisplayName(PhieuXuatManager.Chitiet.Properties.DonGia)]
        public decimal DonGia { get; set; }
        [DisplayName(PhieuXuatManager.Chitiet.Properties.ThanhTien)]
        public decimal ThanhTien
        {
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
                if (_phieuxuat == null)
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
        [DisplayName(PhieuXuatManager.Chitiet.Properties.TrangThai)]
        public int? TrangThai { get; set; }
        #endregion

        #region Services
        #endregion

        #region Override Methods
        public override bool Equals(object obj)
        {
            try
            {
                var chitiet = (ChiTietPhieuXuat)obj;
                return this.MaSoSach.Equals(chitiet.MaSoSach);
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

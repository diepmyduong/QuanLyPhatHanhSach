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
    public class PhieuNhap
    {
        #region Private Properties
        private NhaXuatBan _nxb;
        private List<ChiTietPhieuNhap> _chitiet;
        #endregion

        #region Public Properties
        [DisplayName(PhieuNhapManager.Properties.MaSoPhieuNhap)]
        public int MaSoPhieuNhap { get; set; }
        [Required]
        [DisplayName(PhieuNhapManager.Properties.MaSoNXB)]
        public int MaSoNXB { get; set; }
        [DisplayName(PhieuNhapManager.Properties.NXB)]
        public NhaXuatBan NXB
        {
            get
            {
                if (_nxb == null)
                {
                    _nxb = NhaXuatBanManager.find(this.MaSoNXB);
                }
                return _nxb;
            }
            set
            {
                _nxb = value;
            }
        }
        [Required]
        [DisplayName(PhieuNhapManager.Properties.NgayLap)]
        public DateTime NgayLap { get; set; }
        [Required]
        [DisplayName(PhieuNhapManager.Properties.NguoiGiao)]
        public string NguoiGiao { get; set; }
        [DisplayName(PhieuNhapManager.Properties.TongTien)]
        public decimal TongTien { get; set; }
        //Chi tiết phiếu nhập
        [DisplayName(PhieuNhapManager.Properties.ChiTiet)]
        public List<ChiTietPhieuNhap> ChiTiet
        {
            get
            {
                if (_chitiet == null)
                {
                    _chitiet = PhieuNhapManager.ChiTiet.find(this.MaSoPhieuNhap);

                }
                return _chitiet;
            }
            set
            {
                _chitiet = value;
            }
        }
        [DisplayName(PhieuNhapManager.Properties.TrangThai)]
        public int? TrangThai { get; set; }
        #endregion

        #region Services
        public bool isDetailExisted(ChiTietPhieuNhap chitiet)
        {
            return _chitiet.Contains(chitiet);
        }

        public bool addDetail(ChiTietPhieuNhap chitiet)
        {
            if (isDetailExisted(chitiet))
            {
                return false;
            }
            _chitiet.Add(chitiet);
            return true;
        }
        public bool deleteDetail(int masosach)
        {
            foreach(ChiTietPhieuNhap ct in _chitiet)
            {
                if (ct.MaSoSach.Equals(masosach))
                {
                    _chitiet.Remove(ct);
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return this.NgayLap.ToString();
        }
        #endregion
    }
}

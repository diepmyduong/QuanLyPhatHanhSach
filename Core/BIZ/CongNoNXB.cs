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
    public class CongNoNXB
    {
        public CongNoNXB() { }
        public CongNoNXB(CONGNONXB congno)
        {
            MaSoSach = congno.masosach;
            MaSoNXB = congno.masonxb;
            SoLuong = congno.soluong;
            DonGia = congno.dongia;
            Thang = congno.thang;
        }
        public CongNoNXB(CONGNONXB congno, NXB nxb)
            :this(congno)
        {
            NXB = new NhaXuatBan(nxb);
        }
        public CongNoNXB(CONGNONXB congno, SACH sach)
            : this(congno)
        {
            Sach = new Sach(sach);
        }
        public CongNoNXB(CONGNONXB congno, NXB nxb, SACH sach)
            :this(congno,nxb)
        {
            Sach = new Sach(sach);
        }

        #region Private Properties
        private Sach _sach;
        private NhaXuatBan _nxb;
        #endregion

        #region Public Properties
        [Required]
        [DisplayName(CongNoNXBManager.Properties.MaSoSach)]
        public int MaSoSach { get; set; }
        [DisplayName(CongNoNXBManager.Properties.Sach)]
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
        [DisplayName(CongNoNXBManager.Properties.MaSoNXB)]
        public int MaSoNXB { get; set; }
        [DisplayName(CongNoNXBManager.Properties.NXB)]
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
        [DisplayName(CongNoNXBManager.Properties.SoLuong)]
        public decimal SoLuong { get; set; }
        [Required]
        [DisplayName(CongNoNXBManager.Properties.DonGia)]
        public decimal DonGia { get; set; }
        [DisplayName(CongNoNXBManager.Properties.ThanhTien)]
        public decimal ThanhTien
        {
            get
            {
                return this.SoLuong * this.DonGia;
            }
        }
        [Required]
        [DisplayName(CongNoNXBManager.Properties.Thang)]
        public DateTime Thang { get; set; }
        #endregion

        #region Services
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return this.Thang.ToString();
        }
        #endregion




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Core.DAL;

namespace Core.BIZ
{
    public class CongNoNXB
    {
        private Sach _sach;
        private NhaXuatBan _nxb;

        [DisplayName(CongNoNXBManager.Properties.MaSoSach)]
        public int MaSoSach { get; set; }
        [DisplayName(CongNoNXBManager.Properties.Sach)]
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
        [DisplayName(CongNoNXBManager.Properties.MaSoNXB)]
        public int MaSoNXB { get; set; }
        [DisplayName(CongNoNXBManager.Properties.NXB)]
        public NhaXuatBan NXB
        {
            get
            {
                if(_nxb == null)
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
        [DisplayName(CongNoNXBManager.Properties.SoLuong)]
        public decimal SoLuong { get; set; }
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
        [DisplayName(CongNoNXBManager.Properties.Thang)]
        public DateTime Thang { get; set; }

        public override string ToString()
        {
            return this.Thang.ToString();
        }

    }
}

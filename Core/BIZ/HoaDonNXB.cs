using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Core.DAL;

namespace Core.BIZ
{
    public class HoaDonNXB
    {
        #region Private Properties
        private NhaXuatBan _nxb;
        private List<ChiTietHoaDonNXB> _chitiet;
        #endregion

        #region Public Properties
        [DisplayName(HoaDonNXBManager.Properties.MaSoHoaDon)]
        public int MaSoHoaDon { get; set; }
        [DisplayName(HoaDonNXBManager.Properties.MaSoNXB)]
        public int MaSoNXB { get; set; }
        [DisplayName(HoaDonNXBManager.Properties.NXB)]
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
        [DisplayName(HoaDonNXBManager.Properties.NgayLap)]
        public DateTime NgayLap { get; set; }
        [DisplayName(HoaDonNXBManager.Properties.TongTien)]
        public decimal TongTien { get; set; }
        [DisplayName(HoaDonNXBManager.Properties.ChiTiet)]
        public List<ChiTietHoaDonNXB> ChiTiet
        {
            get
            {
                if (_chitiet == null)
                {
                    _chitiet = HoaDonNXBManager.ChiTiet.find(this.MaSoHoaDon);
                }
                return _chitiet;
            }
            set
            {
                _chitiet = value;
            }
        }
        [DisplayName(HoaDonNXBManager.Properties.TrangThai)]
        public int? TrangThai { get; set; }
        #endregion

        #region Services
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return this.NgayLap.ToString();
        }
        #endregion
    }
}

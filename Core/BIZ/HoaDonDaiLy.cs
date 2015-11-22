using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Core.DAL;

namespace Core.BIZ
{
    public class HoaDonDaiLy
    {
        #region Private Properties
        private DaiLy _daily;
        private List<ChiTietHoaDonDaiLy> _chitiet;
        #endregion

        #region Public Properties
        [DisplayName(HoaDonDaiLyManager.Properties.MaSoHoaDon)]
        public int MaSoHoaDon { get; set; }
        [DisplayName(HoaDonDaiLyManager.Properties.MaSoDaiLy)]
        public int MaSoDaiLy { get; set; }
        [DisplayName(HoaDonDaiLyManager.Properties.DaiLy)]
        public DaiLy DaiLy
        {
            get
            {
                if (_daily == null)
                {
                    _daily = DaiLyManager.find(this.MaSoDaiLy);
                }
                return _daily;
            }
            set
            {
                _daily = value;
            }
        }
        [DisplayName(HoaDonDaiLyManager.Properties.NgayLap)]
        public DateTime NgayLap { get; set; }
        [DisplayName(HoaDonDaiLyManager.Properties.TongTien)]
        public decimal TongTien { get; set; }
        [DisplayName(HoaDonDaiLyManager.Properties.ChiTiet)]
        public List<ChiTietHoaDonDaiLy> ChiTiet
        {
            get
            {
                if (_chitiet == null)
                {
                    _chitiet = HoaDonDaiLyManager.ChiTiet.find(this.MaSoHoaDon);
                }
                return _chitiet;
            }
            set
            {
                _chitiet = value;
            }
        }
        [DisplayName(HoaDonDaiLyManager.Properties.TrangThai)]
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

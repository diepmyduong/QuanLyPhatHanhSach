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
    public class HoaDonDaiLy
    {
        public HoaDonDaiLy() { }
        public HoaDonDaiLy(HOADONDAILY hoadon)
        {
            MaSoHoaDon = hoadon.masohoadon;
            MaSoDaiLy = hoadon.masodaily;
            NgayLap = hoadon.ngaylap;
            TongTien = hoadon.tongtien;
            TrangThai = hoadon.trangthai;
        }
        public HoaDonDaiLy(HOADONDAILY hoadon, DAILY daily)
            :this(hoadon)
        {
            DaiLy = new DaiLy(daily);
        }


        #region Private Properties
        private DaiLy _daily;
        private List<ChiTietHoaDonDaiLy> _chitiet;
        #endregion

        #region Public Properties
        [Required]
        [DisplayName(HoaDonDaiLyManager.Properties.MaSoHoaDon)]
        public int MaSoHoaDon { get; set; }
        [Required]
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
        [Required]
        [DisplayName(HoaDonDaiLyManager.Properties.NgayLap)]
        public DateTime NgayLap { get; set; }
        [Required]
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

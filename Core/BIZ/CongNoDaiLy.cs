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
    public class CongNoDaiLy
    {
        public CongNoDaiLy() { }
        public CongNoDaiLy(CONGNODAILY congno)
        {
            MaSoSach = congno.masosach;
            MaSoDaiLy = congno.masodaily;
            SoLuong = congno.soluong;
            DonGia = congno.dongia;
            Thang = congno.thang;
        }
        public CongNoDaiLy(CONGNODAILY congno, DAILY daily)
            :this(congno)
        {
            DaiLy = new DaiLy(daily);
        }
        public CongNoDaiLy(CONGNODAILY congno, SACH sach)
            : this(congno)
        {
            Sach = new Sach(sach);
        }
        public CongNoDaiLy(CONGNODAILY congno, DAILY daily, SACH sach)
            :this(congno,daily)
        {
            Sach = new Sach(sach);
        }

        #region Private Properties
        private Sach _sach;
        private DaiLy _daily;
        private static List<string> _searchKeys;
        #endregion

        #region Public Properties
        [Required]
        [DisplayName(CongNoDaiLyManager.Properties.MaSoSach)]
        public int MaSoSach { get; set; }
        [DisplayName(CongNoDaiLyManager.Properties.Sach)]
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
        [DisplayName(CongNoDaiLyManager.Properties.MaSoDaiLy)]
        public int MaSoDaiLy { get; set; }
        [DisplayName(CongNoDaiLyManager.Properties.DaiLy)]
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
        [DisplayName(CongNoDaiLyManager.Properties.SoLuong)]
        public decimal SoLuong { get; set; }
        [Required]
        [DisplayName(CongNoDaiLyManager.Properties.DonGia)]
        public decimal DonGia { get; set; }
        [DisplayName(CongNoDaiLyManager.Properties.ThanhTien)]
        public decimal ThanhTien
        {
            get
            {
                return this.SoLuong * this.DonGia;
            }
        }
        [Required]
        [DisplayName(CongNoDaiLyManager.Properties.Thang)]
        public DateTime Thang { get; set; }
        #endregion

        #region Services
        public static List<string> searchKeys()
        {
            if (_searchKeys == null)
            {
                _searchKeys = new List<string>();
                _searchKeys.Add(nameof(CongNoDaiLyManager.Properties.MaSoSach));
                _searchKeys.Add(nameof(CongNoDaiLyManager.Properties.MaSoDaiLy));
                _searchKeys.Add(nameof(CongNoDaiLyManager.Properties.SoLuong));
                _searchKeys.Add(nameof(CongNoDaiLyManager.Properties.DonGia));
                _searchKeys.Add(nameof(CongNoDaiLyManager.Properties.Thang));
                _searchKeys.Add(nameof(CongNoDaiLyManager.Properties.ThanhTien));
                _searchKeys.Add(nameof(DaiLyManager.Properties.TenDaiLy));
            }
            return _searchKeys;
        }
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return this.Thang.ToString();
        }
        #endregion





    }
}

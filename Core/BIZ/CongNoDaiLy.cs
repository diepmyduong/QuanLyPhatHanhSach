using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Core.DAL;

namespace Core.BIZ
{
    public class CongNoDaiLy
    {
        private Sach _sach;
        private DaiLy _daily;

        [DisplayName(CongNoDaiLyManager.Properties.MaSoSach)]
        public int MaSoSach { get; set; }
        [DisplayName(CongNoDaiLyManager.Properties.Sach)]
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
        [DisplayName(CongNoDaiLyManager.Properties.MaSoDaiLy)]
        public int MaSoDaiLy { get; set; }
        [DisplayName(CongNoDaiLyManager.Properties.DaiLy)]
        public DaiLy DaiLy
        {
            get
            {
                if(_daily == null)
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
        [DisplayName(CongNoDaiLyManager.Properties.SoLuong)]
        public decimal SoLuong { get; set; }
        [DisplayName(CongNoDaiLyManager.Properties.DonGia)]
        public decimal DonGia { get; set; }
        [DisplayName(CongNoDaiLyManager.Properties.ThanhTien)]
        public decimal ThanhTien {
            get
            {
                return this.SoLuong * this.DonGia;
            }
        }
        [DisplayName(CongNoDaiLyManager.Properties.Thang)]
        public DateTime Thang { get; set;}

        public override string ToString()
        {
            return this.Thang.ToString();
        }
    }
}

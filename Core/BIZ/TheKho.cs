using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Core.DAL;

namespace Core.BIZ
{
    public class TheKho
    {
        private Sach _sach;

        [DisplayName(TheKhoManager.Properties.MaSoSach)]
        public int MaSoSach { get; set; }
        [DisplayName(TheKhoManager.Properties.Sach)]
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
        [DisplayName(TheKhoManager.Properties.SoLuong)]
        public decimal SoLuong { get; set; }
        [DisplayName(TheKhoManager.Properties.NgayGhi)]
        public DateTime NgayGhi { get; set; }

        public override string ToString()
        {
            return this.Sach.TenSach;
        }
    }
}

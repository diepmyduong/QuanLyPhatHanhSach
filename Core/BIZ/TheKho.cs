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
    public class TheKho
    {
        public TheKho() { }
        public TheKho(THEKHO thekho)
        {
            MaSoSach = thekho.masosach;
            SoLuong = thekho.soluong;
            NgayGhi = thekho.ngayghi;
        }
        public TheKho(THEKHO thekho,SACH sach)
            :this(thekho)
        {
            Sach = new Sach(sach);
        }

        #region Private Properties
        private Sach _sach;
        #endregion

        #region Public Properties
        [Required]
        [DisplayName(TheKhoManager.Properties.MaSoSach)]
        public int MaSoSach { get; set; }
        [DisplayName(TheKhoManager.Properties.Sach)]
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
        [DisplayName(TheKhoManager.Properties.SoLuong)]
        public decimal SoLuong { get; set; }
        [Required]
        [DisplayName(TheKhoManager.Properties.NgayGhi)]
        public DateTime NgayGhi { get; set; }
        #endregion

        #region Services
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return this.Sach.TenSach;
        }
        #endregion





    }
}

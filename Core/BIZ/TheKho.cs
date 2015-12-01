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
        private static List<string> _searchKeys;
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
        public static List<string> searchKeys()
        {
            if(_searchKeys == null)
            {
                _searchKeys = new List<string>();
                _searchKeys.Add(nameof(TheKhoManager.Properties.MaSoSach));
                _searchKeys.Add(nameof(SachManager.Properties.TenSach));
                _searchKeys.Add(nameof(SachManager.Properties.TenTacGia));
                _searchKeys.Add(nameof(TheKhoManager.Properties.SoLuong));
                _searchKeys.Add(nameof(TheKhoManager.Properties.NgayGhi));
            }
            return _searchKeys;
        }
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return this.Sach.TenSach;
        }
        #endregion





    }
}

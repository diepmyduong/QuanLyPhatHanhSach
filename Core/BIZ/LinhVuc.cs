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
    public class LinhVuc
    {
        public LinhVuc() { }
        public LinhVuc(LINHVUC linhvuc)
        {
            MaSoLinhVuc = linhvuc.masolinhvuc;
            TenLinhVuc = linhvuc.ten;
            TrangThai = linhvuc.trangthai;
        }

        #region Private Properties
        private List<Sach> _sach;
        private static List<string> _searchKeys;
        #endregion

        #region Public Properties
        [Required]
        [DisplayName(LinhVucManager.Properties.MaSoLinhVuc)]
        public int MaSoLinhVuc { get; set; }
        [Required]
        [DisplayName(LinhVucManager.Properties.TenLinhVuc)]
        public string TenLinhVuc { get; set; }
        //Sách của lĩnh vực
        [DisplayName(LinhVucManager.Properties.Sach)]
        public List<Sach> Sach
        {
            get
            {
                if (_sach == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(SachManager.Properties.LinhVucSach, this.MaSoLinhVuc);
                    param.Add(SachManager.Properties.TrangThai, null);
                    _sach = SachManager.findBy(param);
                }
                return _sach;
            }
            set
            {
                _sach = value;
            }
        }
        [DisplayName(LinhVucManager.Properties.TrangThai)]
        public int? TrangThai { get; set; }
        #endregion

        #region Services
        public bool delete()
        {
            this.TrangThai = 0;
            return LinhVucManager.edit(this);
        }
        public static List<string> searchKeys()
        {
            if (_searchKeys == null)
            {
                _searchKeys = new List<string>();
                _searchKeys.Add(nameof(LinhVucManager.Properties.MaSoLinhVuc));
                _searchKeys.Add(nameof(LinhVucManager.Properties.TenLinhVuc));
            }
            return _searchKeys;
        }
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return this.TenLinhVuc;
        }
        #endregion
    }
}

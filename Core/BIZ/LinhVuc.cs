using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Core.DAL;

namespace Core.BIZ
{
    public class LinhVuc
    {
        private List<Sach> _sach;

        [DisplayName(LinhVucManager.Properties.MaSoLinhVuc)]
        public int MaSoLinhVuc { get; set; }
        [DisplayName(LinhVucManager.Properties.TenLinhVuc)]
        public string TenLinhVuc { get; set; }
        //Sách của lĩnh vực
        [DisplayName(LinhVucManager.Properties.Sach)]
        public List<Sach> Sach
        {
            get
            {
                if(_sach == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(SachManager.Properties.LinhVucSach, this.MaSoLinhVuc);
                    _sach = SachManager.findBy(param);
                }
                return _sach;
            }
            set
            {
                _sach = value;
            }
        }

        public override string ToString()
        {
            return this.TenLinhVuc;
        }
    }
}

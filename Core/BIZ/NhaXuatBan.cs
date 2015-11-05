using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Core.DAL;

namespace Core.BIZ
{
    public class NhaXuatBan
    {
        [DisplayName(NhaXuatBanManager.Properties.MaSoNXB)]
        public int MaSoNXB { get; set; }
        [DisplayName(NhaXuatBanManager.Properties.TenNXB)]
        public String TenNXB { get; set; }
        [DisplayName(NhaXuatBanManager.Properties.DiaChi)]
        public String DiaChi { get; set; }
        [DisplayName(NhaXuatBanManager.Properties.SoDienThoai)]
        public String SoDienThoai { get; set; }
        [DisplayName(NhaXuatBanManager.Properties.SoTaiKhoan)]
        public String SoTaiKhoan { get; set; }

        //Sách của NXB
        private List<Sach> _sach;
        [DisplayName(NhaXuatBanManager.Properties.Sach)]
        public List<Sach> Sach
        {
            get
            {
                if(_sach == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(SachManager.Properties.NXB, this.MaSoNXB);
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
            return this.TenNXB;
        }
        
    }
}

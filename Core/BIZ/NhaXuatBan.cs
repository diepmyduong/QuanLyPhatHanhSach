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
        private List<Sach> _sach;
        private List<PhieuNhap> _phieunhap;
        private List<CongNoNXB> _congno;


        [DisplayName(NhaXuatBanManager.Properties.MaSoNXB)]
        public int MaSoNXB { get; set; }
        [DisplayName(NhaXuatBanManager.Properties.TenNXB)]
        public string TenNXB { get; set; }
        [DisplayName(NhaXuatBanManager.Properties.DiaChi)]
        public string DiaChi { get; set; }
        [DisplayName(NhaXuatBanManager.Properties.SoDienThoai)]
        public string SoDienThoai { get; set; }
        [DisplayName(NhaXuatBanManager.Properties.SoTaiKhoan)]
        public string SoTaiKhoan { get; set; }
        //Sách của NXB
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

        //Phiếu nhập của NXB
        [DisplayName(NhaXuatBanManager.Properties.PhieuNhap)]
        public List<PhieuNhap> PhieuNhap
        {
            get
            {
                if(_phieunhap == null)
                {
                    _phieunhap = PhieuNhapManager.find(this.MaSoNXB);
                }
                return _phieunhap;
            }
            set
            {
                _phieunhap = value;
            }
        }
        //Công nợ của NXB
        [DisplayName(NhaXuatBanManager.Properties.CongNo)]
        public List<CongNoNXB> CongNo
        {
            get
            {
                if(_congno == null)
                {
                    _congno = CongNoNXBManager.find(this.MaSoNXB);
                }
                return _congno;
            }
            set
            {
                _congno = value;
            }
        }

        public override string ToString()
        {
            return this.TenNXB;
        }
        
    }
}

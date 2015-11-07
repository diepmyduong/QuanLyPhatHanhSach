using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Core.DAL;

namespace Core.BIZ
{
    public class DaiLy
    {
        private List<PhieuXuat> _phieuxuat;
        private List<CongNoDaiLy> _congno;

        [DisplayName(DaiLyManager.Properties.MaSoDaiLy)]
        public int MaSoDaiLy { get; set; }
        [DisplayName(DaiLyManager.Properties.TenDaiLy)]
        public string TenDaiLy { get; set; }
        [DisplayName(DaiLyManager.Properties.DiaChi)]
        public string DiaChi { get; set; }
        [DisplayName(DaiLyManager.Properties.SoDienThoai)]
        public string SoDienThoai { get; set; }
        [DisplayName(DaiLyManager.Properties.SoTaiKhoan)]
        public string SoTaiKhoan { get; set; }

        //Phiếu xuất của Đại lý
        
        [DisplayName(DaiLyManager.Properties.PhieuXuat)]
        public List<PhieuXuat> PhieuXuat
        {
            get
            {
                if(_phieuxuat == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(PhieuXuatManager.Properties.DaiLy, this.MaSoDaiLy);
                    _phieuxuat = PhieuXuatManager.findBy(param);
                }
                return _phieuxuat;
            }
            set
            {
                _phieuxuat = value;
            }
        }

        //Công nợ của Đại lý
        [DisplayName(DaiLyManager.Properties.CongNo)]
        public List<CongNoDaiLy> CongNo
        {
            get
            {
                if(_congno == null)
                {
                    _congno = CongNoDaiLyManager.find(this.MaSoDaiLy);
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
            return this.TenDaiLy;
        }
    }
}

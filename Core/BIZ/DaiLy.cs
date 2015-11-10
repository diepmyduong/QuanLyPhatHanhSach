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
        private List<HoaDonDaiLy> _hoadon;

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
        //Hóa đơn của Đại lý
        [DisplayName(DaiLyManager.Properties.HoaDon)]
        public List<HoaDonDaiLy> HoaDon
        {
            get
            {
                if(_hoadon == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(HoaDonDaiLyManager.Properties.MaSoDaiLy, this.MaSoDaiLy);
                    _hoadon = HoaDonDaiLyManager.findBy(param);
                }
                return _hoadon;
            }
            set
            {
                _hoadon = value;
            }
        }

        public override string ToString()
        {
            return this.TenDaiLy;
        }


        public decimal TongTienNo(int startMonth, int startYear, int endMonth, int endYear)
        {
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 31);
            return this.CongNo.Where(cn =>
                        cn.Thang >= startDate
                        && cn.Thang <= endDate).ToList()
                        .Sum(cn => cn.ThanhTien);
        }
    }
}

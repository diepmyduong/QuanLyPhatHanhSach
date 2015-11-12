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
        private List<HoaDonNXB> _hoadon;
        private decimal? _tongtienno;


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
                    param.Add(SachManager.Properties.MaSoNXB, this.MaSoNXB);
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
                    var param = new Dictionary<string, dynamic>();
                    param.Add(PhieuNhapManager.Properties.MaSoNXB, this.MaSoNXB);
                    _phieunhap = PhieuNhapManager.findBy(param);
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

        //Hóa đơn của NXB
        [DisplayName(NhaXuatBanManager.Properties.HoaDon)]
        public List<HoaDonNXB> HoaDon
        {
            get
            {
                if(_hoadon == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(HoaDonNXBManager.Properties.MaSoNXB, this.MaSoNXB);
                    _hoadon = HoaDonNXBManager.findBy(param);
                }
                return _hoadon;
            }
            set
            {
                _hoadon = value;
            }
        }
        [DisplayName(NhaXuatBanManager.Properties.TongTienNo)]
        public decimal? TongTienNo
        {
            get
            {
                if(_tongtienno == null)
                {
                    _tongtienno = this.CongNo.Sum(cn => cn.ThanhTien);
                }
                return _tongtienno;
            }
        }

        #region Services
        public decimal? TongTienNoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
            return this.CongNo.Where(cn =>
                        cn.Thang >= startDate
                        && cn.Thang <= endDate).ToList()
                        .Sum(cn => cn.ThanhTien);
        }

        public decimal? TongTienNhapThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
            return this.PhieuNhap.Where(p =>
                        p.NgayLap >= startDate
                        && p.NgayLap <= endDate).ToList()
                        .Sum(p => p.TongTien);
        }

        public List<CongNoNXB> CongNoTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
            return this.CongNo.Where(cn =>
                        cn.Thang >= startDate
                        && cn.Thang <= endDate).ToList();
        }
        #endregion


        #region Override Methods
        public override string ToString()
        {
            return this.TenNXB;
        }
        #endregion




    }
}

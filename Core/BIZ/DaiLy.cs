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
        public DaiLy() { }
        public DaiLy(DAILY daily)
        {
            MaSoDaiLy = daily.masodaily;
            TenDaiLy = daily.ten;
            DiaChi = daily.diachi;
            SoDienThoai = daily.sodienthoai;
            SoTaiKhoan = daily.sotaikhoan;
            TrangThai = daily.trangthai;
            MaSoNguoiDung = daily.masonguoidung;
            NganHang = daily.nganhang;
        }


        #region Private Properties
        private NguoiDung _nguoidung;
        private List<PhieuXuat> _phieuxuat;
        private List<CongNoDaiLy> _congno;
        private List<HoaDonDaiLy> _hoadon;
        private static List<string> _searchKeys;
        private decimal? _tongtienno;
        private decimal? _tongSoLuongNo;
        private decimal? _tongTienNoTheoThang;
        private decimal? _tongSoLuongNoTheoThang;
        private decimal? _tongTienXuat;
        private decimal? _tongSoLuongXuat;
        private decimal? _tongTienXuatTheoThang;
        private decimal? _tongSoLuongXuatTheoThang;
        private decimal? _tongTienThanhToan;
        private decimal? _tongTienThanhToanTheoThang;
        private static List<string> _searchKeysTheoDoiNo;
        private static List<string> _searchKeysDoanhThu;
        #endregion

        #region Public Properties
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
        [DisplayName(DaiLyManager.Properties.TrangThai)]
        public int? TrangThai { get; set; }
        [DisplayName(DaiLyManager.Properties.NganHang)]
        public string NganHang { get; set; }
        [DisplayName(DaiLyManager.Properties.MaSoNguoiDung)]
        public int MaSoNguoiDung { get; set; }
        [DisplayName(DaiLyManager.Properties.NguoiDung)]
        public NguoiDung NguoiDung {
            get
            {
                if(_nguoidung == null)
                {
                    _nguoidung = NguoiDungManager.find(MaSoNguoiDung);
                }
                return _nguoidung;
            }
            set
            {
                _nguoidung = value;
            }
        }
        //Phiếu xuất của Đại lý

        [DisplayName(DaiLyManager.Properties.PhieuXuat)]
        public List<PhieuXuat> PhieuXuat
        {
            get
            {
                if (_phieuxuat == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(PhieuXuatManager.Properties.MaSoDaiLy, this.MaSoDaiLy);
                    param.Add(PhieuXuatManager.Properties.TrangThai, 1);
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
                if (_congno == null)
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
                if (_hoadon == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(HoaDonDaiLyManager.Properties.MaSoDaiLy, this.MaSoDaiLy);
                    param.Add(HoaDonDaiLyManager.Properties.TrangThai, 1);
                    _hoadon = HoaDonDaiLyManager.findBy(param);
                }
                return _hoadon;
            }
            set
            {
                _hoadon = value;
            }
        }
        [DisplayName(DaiLyManager.Properties.TongTienNo)]
        public decimal? TongTienNo
        {
            get
            {
                if (_tongtienno == null)
                {
                    _tongtienno = this.CongNo.Sum(cn => cn.ThanhTien);
                }
                return _tongtienno;
            }
        }
        [DisplayName(DaiLyManager.Properties.TongTienNoThang)]
        public decimal? TongTienNoThang
        {
            get
            {
                return _tongTienNoTheoThang;
            }
        }
        [DisplayName(DaiLyManager.Properties.TongSoLuongNo)]
        public decimal? TongSoLuongNo
        {
            get
            {
                if (_tongSoLuongNo == null)
                {
                    _tongSoLuongNo = CongNo.Sum(cn => cn.SoLuong);
                }
                return _tongSoLuongNo;
            }
        }
        [DisplayName(DaiLyManager.Properties.TongSoLuongNoTheoThang)]
        public decimal? TongSoLuongNoTheoThang
        {
            get
            {
                return _tongSoLuongNoTheoThang;
            }
        }
        [DisplayName(DaiLyManager.Properties.TongTienXuat)]
        public decimal? TongTienXuat
        {
            get
            {
                if (_tongTienXuat == null)
                {
                    _tongTienXuat = PhieuXuat.Sum(p => p.TongTien);
                }
                return _tongTienXuat;
            }
        }
        [DisplayName(DaiLyManager.Properties.TongTienXuatTheoThang)]
        public decimal? TongTienXuatTheoThang
        {
            get
            {
                return _tongTienXuatTheoThang;
            }
        }
        [DisplayName(DaiLyManager.Properties.TongSoLuongXuat)]
        public decimal? TongSoLuongXuat
        {
            get
            {
                if (_tongSoLuongXuat == null)
                {
                    _tongSoLuongXuat = PhieuXuat.Sum(ph => ph.ChiTiet.Sum(ct => ct.SoLuong));
                }
                return _tongSoLuongXuat;
            }
        }
        [DisplayName(DaiLyManager.Properties.TongSoLuongXuatTheoThang)]
        public decimal? TongSoLuongXuatTheoThang
        {
            get
            {
                return _tongSoLuongXuatTheoThang;
            }
        }
        [DisplayName(DaiLyManager.Properties.TongTienThanhToan)]
        public decimal? TongTienThanhToan
        {
            get
            {
                if (_tongTienThanhToan == null)
                {
                    _tongTienThanhToan = HoaDon.Sum(hd => hd.TongTien);
                }
                return _tongTienThanhToan;
            }
        }
        [DisplayName(DaiLyManager.Properties.TongTienThanhToanTheoThang)]
        public decimal? TongTienThanhToanTheoThang
        {
            get
            {
                return _tongTienThanhToanTheoThang;
            }
        }
        #endregion

        #region Services
        public bool delete()
        {
            this.TrangThai = 0;
            return DaiLyManager.edit(this);
        }
        public static List<string> searchKeys()
        {
            if (_searchKeys == null)
            {
                _searchKeys = new List<string>();
                _searchKeys.Add(nameof(DaiLyManager.Properties.MaSoDaiLy));
                _searchKeys.Add(nameof(DaiLyManager.Properties.TenDaiLy));
                _searchKeys.Add(nameof(DaiLyManager.Properties.DiaChi));
                _searchKeys.Add(nameof(DaiLyManager.Properties.SoDienThoai));
                _searchKeys.Add(nameof(DaiLyManager.Properties.SoTaiKhoan));
            }
            return _searchKeys;
        }
        public static List<string> searchKeysTheoDoiNo()
        {
            if (_searchKeysTheoDoiNo == null)
            {
                _searchKeysTheoDoiNo = new List<string>();
                _searchKeysTheoDoiNo.Add(nameof(DaiLyManager.Properties.MaSoDaiLy));
                _searchKeysTheoDoiNo.Add(nameof(DaiLyManager.Properties.TenDaiLy));
                _searchKeysTheoDoiNo.Add(nameof(DaiLyManager.Properties.SoDienThoai));
                _searchKeysTheoDoiNo.Add(nameof(DaiLyManager.Properties.DiaChi));
                _searchKeysTheoDoiNo.Add(nameof(DaiLyManager.Properties.TongTienNo));
                _searchKeysTheoDoiNo.Add(nameof(DaiLyManager.Properties.TongTienNoThang));
            }
            return _searchKeysTheoDoiNo;
        }
        public static List<string> searchKeysDoanhThu()
        {
            if (_searchKeysDoanhThu == null)
            {
                _searchKeysDoanhThu = new List<string>();
                _searchKeysDoanhThu.Add(nameof(DaiLyManager.Properties.MaSoDaiLy));
                _searchKeysDoanhThu.Add(nameof(DaiLyManager.Properties.TenDaiLy));
                _searchKeysDoanhThu.Add(nameof(DaiLyManager.Properties.TongTienThanhToan));
                _searchKeysDoanhThu.Add(nameof(DaiLyManager.Properties.TongTienThanhToanTheoThang));
            }
            return _searchKeysDoanhThu;
        }
        public List<Sach> getSachNo()
        {
            return CongNo
                    .Where(cn=>cn.SoLuong > 0)
                    .GroupBy(cn => cn.MaSoSach)
                    .Select(group => SachManager.find(group.Key)).ToList();
        }
        public decimal? tongTienNoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongTienNoTheoThang = congNoTheoThang(startMonth, startYear, endMonth, endYear)
                                    .Sum(cn => cn.ThanhTien);
            return _tongTienNoTheoThang;
        }
        public decimal? tongTienXuatThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongTienXuatTheoThang = getPhieuXuatTheoThang(startMonth, startYear, endMonth, endYear)
                        .Sum(p => p.TongTien);
            return _tongTienXuatTheoThang;
        }
        public decimal? tinhTongSoLuongNoTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongSoLuongNoTheoThang = congNoTheoThang(startMonth, startYear, endMonth, endYear)
                                    .Sum(cn => cn.SoLuong);
            return _tongSoLuongNoTheoThang;
        }
        public decimal? tinhTongSoLuongXuatTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongSoLuongXuatTheoThang = getPhieuXuatTheoThang(startMonth, startYear, endMonth, endYear)
                                    .Sum(ph => ph.ChiTiet.Sum(ct => ct.SoLuong));
            return _tongSoLuongXuatTheoThang;
        }
        public decimal? tinhTongTienThanhToanTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongTienThanhToanTheoThang = getHoaDonTheoThang(startMonth, startYear, endMonth, endYear)
                                    .Sum(hd => hd.TongTien);
            return _tongTienThanhToanTheoThang;
        }
        public List<CongNoDaiLy> congNoTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
            return this.CongNo.Where(cn =>
                        cn.Thang >= startDate
                        && cn.Thang <= endDate).ToList();
        }
        public List<PhieuXuat> getPhieuXuatTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
            return this.PhieuXuat.Where(p =>
                        p.NgayLap >= startDate
                        && p.NgayLap <= endDate).ToList();
        }
        public List<HoaDonDaiLy> getHoaDonTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
            return this.HoaDon.Where(p =>
                        p.NgayLap >= startDate
                        && p.NgayLap <= endDate).ToList();
        }

        public List<PhieuXuat> getAllPhieuXuat()
        {
            var param = new Dictionary<string, dynamic>();
            param.Add(PhieuXuatManager.Properties.MaSoDaiLy, this.MaSoDaiLy);
            return PhieuXuatManager.findBy(param);
        }
        public List<HoaDonDaiLy> getAllHoaDon()
        {
            var param = new Dictionary<string, dynamic>();
            param.Add(HoaDonDaiLyManager.Properties.MaSoDaiLy, this.MaSoDaiLy);
            return HoaDonDaiLyManager.findBy(param);
        }
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return this.TenDaiLy;
        }
        #endregion
    }
}

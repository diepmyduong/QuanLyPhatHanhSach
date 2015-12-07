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
    public class NhaXuatBan
    {
        public NhaXuatBan() { }
        public NhaXuatBan(NXB nxb)
        {
            MaSoNXB = nxb.masonxb;
            TenNXB = nxb.ten;
            DiaChi = nxb.diachi;
            SoDienThoai = nxb.sodienthoai;
            SoTaiKhoan = nxb.sotaikhoan;
            TrangThai = nxb.trangthai;
            NganHang = nxb.nganhang;
        }

        #region Private Properties
        private List<Sach> _sach;
        private List<PhieuNhap> _phieunhap;
        private List<CongNoNXB> _congno;
        private List<HoaDonNXB> _hoadon;
        private decimal? _tongtienno;
        private decimal? _tongTienNhap;
        private decimal? _tongSoLuongNo;
        private decimal? _tongSoLuongNhap;
        private decimal? _tongTienNoTheoThang;
        private decimal? _tongTienNhapTheoThang;
        private decimal? _tongSoLuongNoTheoThang;
        private decimal? _tongSoLuongNhapTheoThang;
        private decimal? _tongTienThanhToan;
        private decimal? _tongTienThanhToanTheoThang;
        private static List<string> _searchKeys;
        private static List<string> _searchKeysTheoDoiNo;
        private static List<string> _searchKeysDoanhThu;
        #endregion

        #region Public Properties
        [Required]
        [DisplayName(NhaXuatBanManager.Properties.MaSoNXB)]
        public int MaSoNXB { get; set; }
        [Required]
        [DisplayName(NhaXuatBanManager.Properties.TenNXB)]
        public string TenNXB { get; set; }
        [Required]
        [DisplayName(NhaXuatBanManager.Properties.DiaChi)]
        public string DiaChi { get; set; }
        [Required]
        [DisplayName(NhaXuatBanManager.Properties.SoDienThoai)]
        public string SoDienThoai { get; set; }
        [Required]
        [DisplayName(NhaXuatBanManager.Properties.SoTaiKhoan)]
        public string SoTaiKhoan { get; set; }
        [DisplayName(NhaXuatBanManager.Properties.TrangThai)]
        public int? TrangThai { get; set; }
        [DisplayName(NhaXuatBanManager.Properties.NganHang)]
        public string NganHang { get; set; }
        //Sách của NXB
        [DisplayName(NhaXuatBanManager.Properties.Sach)]
        public List<Sach> Sach
        {
            get
            {
                if (_sach == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(SachManager.Properties.MaSoNXB, this.MaSoNXB);
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

        //Phiếu nhập của NXB
        [DisplayName(NhaXuatBanManager.Properties.PhieuNhap)]
        public List<PhieuNhap> PhieuNhap
        {
            get
            {
                if (_phieunhap == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(PhieuNhapManager.Properties.MaSoNXB, this.MaSoNXB);
                    param.Add(PhieuNhapManager.Properties.TrangThai, 1);
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
                if (_congno == null)
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
                if (_hoadon == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(HoaDonNXBManager.Properties.MaSoNXB, this.MaSoNXB);
                    param.Add(HoaDonNXBManager.Properties.TrangThai, 1);
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
                if (_tongtienno == null)
                {
                    _tongtienno = this.CongNo.Sum(cn => cn.ThanhTien);
                }
                return _tongtienno;
            }
        }
        [DisplayName(NhaXuatBanManager.Properties.TongTienNoThang)]
        public decimal? TongTienNoThang
        {
            get
            {
                return _tongTienNoTheoThang;
            }
        }
        [DisplayName(NhaXuatBanManager.Properties.TongSoLuongNo)]
        public decimal? TongSoLuongNo
        {
            get
            {
                if(_tongSoLuongNo == null)
                {
                    _tongSoLuongNo = CongNo.Sum(cn => cn.SoLuong);
                }
                return _tongSoLuongNo;
            }
        }
        [DisplayName(NhaXuatBanManager.Properties.TongSoLuongNoTheoThang)]
        public decimal? TongSoLuongNoTheoThang
        {
            get
            {
                return _tongSoLuongNoTheoThang;
            }
        }
        [DisplayName(NhaXuatBanManager.Properties.TongTienNhap)]
        public decimal? TongTienNhap
        {
            get
            {
                if (_tongTienNhap == null)
                {
                    _tongTienNhap = PhieuNhap.Sum(p => p.TongTien);
                }
                return _tongTienNhap;
            }
        }
        [DisplayName(NhaXuatBanManager.Properties.TongTienNhapTheoThang)]
        public decimal? TongTienNhapTheoThang
        {
            get
            {
                return _tongTienNhapTheoThang;
            }
        }
        [DisplayName(NhaXuatBanManager.Properties.TongSoLuongNhap)]
        public decimal? TongSoLuongNhap
        {
            get
            {
                if (_tongSoLuongNhap == null)
                {
                    _tongSoLuongNhap = PhieuNhap.Sum(ph => ph.ChiTiet.Sum(ct => ct.SoLuong));
                }
                return _tongSoLuongNhap;
            }
        }
        [DisplayName(NhaXuatBanManager.Properties.TongSoLuongNhapTheoThang)]
        public decimal? TongSoLuongNhapTheoThang
        {
            get
            {
                return _tongSoLuongNhapTheoThang;
            }
        }
        [DisplayName(NhaXuatBanManager.Properties.TongTienThanhToan)]
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
        [DisplayName(NhaXuatBanManager.Properties.TongTienThanhToanTheoThang)]
        public decimal? TongTienThanhToanTheoThang
        {
            get
            {
                return _tongTienThanhToanTheoThang;
            }
        }
        #endregion

        #region Services
        public decimal? tongTienNoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongTienNoTheoThang = congNoTheoThang(startMonth,startYear,endMonth,endYear)
                                    .Sum(cn => cn.ThanhTien);
            return _tongTienNoTheoThang;
        }

        public decimal? tongTienNhapThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongTienNhapTheoThang = getPhieuNhapTheoThang(startMonth,startYear,endMonth,endYear)
                        .Sum(p => p.TongTien);
            return _tongTienNhapTheoThang;
        }

        public decimal? tinhTongSoLuongNoTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongSoLuongNoTheoThang = congNoTheoThang(startMonth, startYear, endMonth, endYear)
                                    .Sum(cn => cn.SoLuong);
            return _tongSoLuongNoTheoThang;
        }
        public decimal? tinhTongSoLuongNhapTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongSoLuongNhapTheoThang = getPhieuNhapTheoThang(startMonth, startYear, endMonth, endYear)
                                    .Sum(ph => ph.ChiTiet.Sum(ct => ct.SoLuong));
            return _tongSoLuongNhapTheoThang;
        }
        public decimal? tinhTongTienThanhToanTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongTienThanhToanTheoThang = getHoaDonTheoThang(startMonth, startYear, endMonth, endYear)
                                    .Sum(hd => hd.TongTien);
            return _tongTienThanhToanTheoThang;
        }
        public List<CongNoNXB> congNoTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
            return this.CongNo.Where(cn =>
                        cn.Thang >= startDate
                        && cn.Thang <= endDate).ToList();
        }

        public List<PhieuNhap> getPhieuNhapTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
            return this.PhieuNhap.Where(p =>
                        p.NgayLap >= startDate
                        && p.NgayLap <= endDate).ToList();
        }
        public List<HoaDonNXB> getHoaDonTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
            return this.HoaDon.Where(p =>
                        p.NgayLap >= startDate
                        && p.NgayLap <= endDate).ToList();
        }
        public bool delete()
        {
            this.TrangThai = 0;
            return NhaXuatBanManager.edit(this);
        }
        public static List<string> searchKeys()
        {
            if (_searchKeys == null)
            {
                _searchKeys = new List<string>();
                _searchKeys.Add(nameof(NhaXuatBanManager.Properties.MaSoNXB));
                _searchKeys.Add(nameof(NhaXuatBanManager.Properties.TenNXB));
                _searchKeys.Add(nameof(NhaXuatBanManager.Properties.SoDienThoai));
                _searchKeys.Add(nameof(NhaXuatBanManager.Properties.DiaChi));
                _searchKeys.Add(nameof(NhaXuatBanManager.Properties.SoTaiKhoan));
            }
            return _searchKeys;
        }
        public static List<string> searchKeysTheoDoiNo()
        {
            if (_searchKeysTheoDoiNo == null)
            {
                _searchKeysTheoDoiNo = new List<string>();
                _searchKeysTheoDoiNo.Add(nameof(NhaXuatBanManager.Properties.MaSoNXB));
                _searchKeysTheoDoiNo.Add(nameof(NhaXuatBanManager.Properties.TenNXB));
                _searchKeysTheoDoiNo.Add(nameof(NhaXuatBanManager.Properties.SoDienThoai));
                _searchKeysTheoDoiNo.Add(nameof(NhaXuatBanManager.Properties.DiaChi));
                _searchKeysTheoDoiNo.Add(nameof(NhaXuatBanManager.Properties.TongTienNo));
                _searchKeysTheoDoiNo.Add(nameof(NhaXuatBanManager.Properties.TongTienNoThang));
            }
            return _searchKeysTheoDoiNo;
        }
        public static List<string> searchKeysDoanhThu()
        {
            if (_searchKeysDoanhThu == null)
            {
                _searchKeysDoanhThu = new List<string>();
                _searchKeysDoanhThu.Add(nameof(NhaXuatBanManager.Properties.MaSoNXB));
                _searchKeysDoanhThu.Add(nameof(NhaXuatBanManager.Properties.TenNXB));
                _searchKeysDoanhThu.Add(nameof(NhaXuatBanManager.Properties.TongTienThanhToan));
                _searchKeysDoanhThu.Add(nameof(NhaXuatBanManager.Properties.TongTienThanhToanTheoThang));
            }
            return _searchKeysDoanhThu;

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

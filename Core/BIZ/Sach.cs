using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DAL;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Data.Linq;
using System.Drawing.Imaging;
using System.ComponentModel.DataAnnotations;

namespace Core.BIZ
{
    public class Sach
    {
        public Sach() { }
        public Sach(SACH sach)
        {
            MaSoSach = sach.masosach;
            MaSoNXB = sach.masonxb;
            MaSoLinhVuc = sach.masolinhvuc;
            TenSach = sach.tensach;
            TenTacGia = sach.tacgia;
            Soluong = sach.soluong;
            GiaNhap = sach.gianhap;
            GiaBan = sach.giaban;
            HinhAnh = sach.hinhanh;
            TrangThai = sach.trangthai;
            MoTa = sach.mota;
        }
        public Sach(SACH sach,NXB nxb)
            : this(sach)
        {
            NXB = new NhaXuatBan(nxb);
        }
        public Sach(SACH sach, LINHVUC linhvuc)
            : this(sach)
        {
            LinhVucSach = new LinhVuc(linhvuc);
        }
        public Sach(SACH sach, NXB nxb, LINHVUC linhvuc)
            : this(sach, nxb)
        {
            LinhVucSach = new LinhVuc(linhvuc);
        }


        #region Private Properties
        private NhaXuatBan _nxb;
        private LinhVuc _linhvuc;
        private List<CongNoDaiLy> _congnodaily;
        private List<CongNoNXB> _congnonxb;
        private List<ChiTietHoaDonDaiLy> _hoadondaily;
        private List<ChiTietHoaDonNXB> _hoadonnxb;
        private List<ChiTietPhieuNhap> _phieunhap;
        private List<ChiTietPhieuXuat> _phieuxuat;
        private decimal? _tongLuongNhap;
        private decimal? _tongLuongXuat;
        private decimal? _tongTienNhap;
        private decimal? _tongTienXuat;
        private Image _image;
        private decimal? _tongTienNhapTheoThang;
        private decimal? _tongTienXuatTheoThang;
        private decimal? _tongSoLuongNhapTheoThang;
        private decimal? _tongSoLuongXuatTheoThang;
        private decimal? _tongSoLuongBan;
        private decimal? _tongSoLuongBanTheoThang;
        private decimal? _tongTienBan;
        private decimal? _tongTienBanTheoThang;
        private static List<string> _searchKeys;
        private static List<string> _searchKeysLoNhap;
        private static List<string> _searchKeysSachBan;
        private static List<string> _searchKeysCongNoNXB;
        private static List<string> _searchKeysCongNoDaiLy;
        private decimal? _tongSoLuongNXBNo;
        private decimal? _tongSoLuongDaiLyNo;
        private decimal? _tongTienNXBNo;
        private decimal? _tongTienDaiLyNo;
        private decimal? _tongSoLuongNXBNoTheoThang;
        private decimal? _tongSoLuongDaiLyNoTheoThang;
        private decimal? _tongTienNXBNoTheoThang;
        private decimal? _tongTienDaiLyNoTheoThang;
        #endregion

        #region Public Properties
        [Required]
        [DisplayName(SachManager.Properties.MaSoSach)]
        public int MaSoSach { get; set; }
        [Required]
        [DisplayName(SachManager.Properties.TenSach)]
        public string TenSach { get; set; }
        [Required]
        [DisplayName(SachManager.Properties.MaSoLinhVuc)]
        public int MaSoLinhVuc { get; set; }
        [DisplayName(SachManager.Properties.LinhVucSach)]
        public LinhVuc LinhVucSach
        {
            get
            {
                if (_linhvuc == null)
                {
                    _linhvuc = LinhVucManager.find(this.MaSoLinhVuc);
                }
                return _linhvuc;
            }
            set
            {
                _linhvuc = value;
            }
        }
        [Required]
        [DisplayName(SachManager.Properties.TenTacGia)]
        public string TenTacGia { get; set; }
        [Required]
        [DisplayName(SachManager.Properties.MaSoNXB)]
        public int MaSoNXB { get; set; }
        [DisplayName(SachManager.Properties.NXB)]
        public NhaXuatBan NXB
        {
            get
            {
                if (_nxb == null)
                {
                    _nxb = NhaXuatBanManager.find(this.MaSoNXB);
                }
                return _nxb;
            }
            set
            {
                _nxb = value;
            }
        }
        [DisplayName(SachManager.Properties.Soluong)]
        public decimal Soluong { get; set; }
        [Required]
        [DisplayName(SachManager.Properties.GiaNhap)]
        public decimal GiaNhap { get; set; }
        [Required]
        [DisplayName(SachManager.Properties.GiaBan)]
        public decimal GiaBan { get; set; }
        [DisplayName(SachManager.Properties.HinhAnh)]
        public Binary HinhAnh { get; set; }
        [DisplayName(SachManager.Properties.MoTa)]
        public string MoTa { get; set; }
        [DisplayName(SachManager.Properties.CongNoDaiLy)]
        public List<CongNoDaiLy> CongNoDaiLy
        {
            get
            {
                if (_congnodaily == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(CongNoDaiLyManager.Properties.MaSoSach, this.MaSoSach);
                    _congnodaily = CongNoDaiLyManager.findBy(param);
                }
                return _congnodaily;
            }
            set
            {
                _congnodaily = value;
            }
        }
        [DisplayName(SachManager.Properties.CongNoNXB)]
        public List<CongNoNXB> CongNoNXB
        {
            get
            {
                if (_congnonxb == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(CongNoNXBManager.Properties.MaSoSach, this.MaSoSach);
                    _congnonxb = CongNoNXBManager.findBy(param)
                        .Where(cn => cn.SoLuong > 0).OrderBy(cn => cn.Thang).ToList();
                }
                return _congnonxb;
            }
            set
            {
                _congnonxb = value;
            }
        }
        [DisplayName(SachManager.Properties.HoaDonDaiLy)]
        public List<ChiTietHoaDonDaiLy> HoaDonDaiLy
        {
            get
            {
                if (_hoadondaily == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(HoaDonDaiLyManager.ChiTiet.Properties.MaSoSach, this.MaSoSach);
                    param.Add(HoaDonDaiLyManager.ChiTiet.Properties.TrangThai, 1);
                    _hoadondaily = HoaDonDaiLyManager.ChiTiet.findBy(param);
                }
                return _hoadondaily;

            }
            set
            {
                _hoadondaily = value;
            }
        }
        [DisplayName(SachManager.Properties.HoaDonNXB)]
        public List<ChiTietHoaDonNXB> HoaDonNXB
        {
            get
            {
                if (_hoadonnxb == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(HoaDonNXBManager.ChiTiet.Properties.MaSoSach, this.MaSoSach);
                    param.Add(HoaDonNXBManager.ChiTiet.Properties.TrangThai, 1);
                    _hoadonnxb = HoaDonNXBManager.ChiTiet.findBy(param);
                }
                return _hoadonnxb;

            }
            set
            {
                _hoadonnxb = value;
            }
        }
        [DisplayName(SachManager.Properties.PhieuNhap)]
        public List<ChiTietPhieuNhap> PhieuNhap
        {
            get
            {
                if (_phieunhap == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(PhieuNhapManager.ChiTiet.Properties.MaSoSach, this.MaSoSach);
                    param.Add(PhieuNhapManager.ChiTiet.Properties.TrangThai, 1);
                    _phieunhap = PhieuNhapManager.ChiTiet.findBy(param);
                }
                return _phieunhap;
            }
            set
            {
                _phieunhap = value;
            }
        }
        [DisplayName(SachManager.Properties.PhieuXuat)]
        public List<ChiTietPhieuXuat> PhieuXuat
        {
            get
            {
                if (_phieuxuat == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(PhieuXuatManager.Chitiet.Properties.MaSoSach, this.MaSoSach);
                    param.Add(PhieuXuatManager.Chitiet.Properties.TrangThai, 1);
                    _phieuxuat = PhieuXuatManager.Chitiet.findBy(param);
                }
                return _phieuxuat;
            }
            set
            {
                _phieuxuat = value;
            }
        }
        [DisplayName(SachManager.Properties.TongSoLuongNhap)]
        public decimal? TongSoLuongNhap
        {
            get
            {
                if(_tongLuongNhap == null)
                {
                    _tongLuongNhap = this.PhieuNhap.Sum(p => p.SoLuong);
                }
                return _tongLuongNhap;
            }
        }
        [DisplayName(SachManager.Properties.TongSoLuongXuat)]
        public decimal? TongSoLuongXuat
        {
            get
            {
                if(_tongLuongXuat == null)
                {
                    _tongLuongXuat = this.PhieuXuat.Sum(p => p.SoLuong);
                }
                return _tongLuongXuat;
            }
        }
        [DisplayName(SachManager.Properties.TongTienNhap)]
        public decimal? TongTienNhap
        {
            get
            {
                if(_tongTienNhap == null)
                {
                    _tongTienNhap = this.PhieuNhap.Sum(p => p.ThanhTien);
                }
                return _tongTienNhap;
            }
        }
        [DisplayName(SachManager.Properties.TongTienXuat)]
        public decimal? TongTienXuat
        {
            get
            {
                if (_tongTienXuat == null)
                {
                    _tongTienXuat = this.PhieuXuat.Sum(p => p.ThanhTien);
                }
                return _tongTienXuat;
            }
        }
        [DisplayName(SachManager.Properties.SoLuongNhapTheoThang)]
        public decimal? SoLuongNhapTheoThang
        {
            get
            {
                return _tongSoLuongNhapTheoThang;
            }
        }
        [DisplayName(SachManager.Properties.TongTienNhapTheoThang)]
        public decimal? TongTienNhapTheoThang
        {
            get
            {
                return _tongTienNhapTheoThang;
            }
        }
        [DisplayName(SachManager.Properties.TrangThai)]
        public int? TrangThai { get; set;}
        public Image HinhAnhTypeImage
        {
            get
            {
                if (_image == null)
                {
                    _image = ImagesHelper.BinaryToImage(this.HinhAnh);
                }
                return _image;
            }
            set
            {
                _image = value;
                if (_image != null)
                {
                    _image = ImagesHelper.ResizeImage(_image, 350, 400);
                    this.HinhAnh = ImagesHelper.ImageToBinary(_image);
                }
            }
        }
        [DisplayName(SachManager.Properties.SoLuongXuatTheoThang)]
        public decimal? SoLuongXuatTheoThang
        {
            get
            {
                return _tongSoLuongXuatTheoThang;
            }
        }
        [DisplayName(SachManager.Properties.TongTienXuatTheoThang)]
        public decimal? TongTienXuatTheoThang
        {
            get
            {
                return _tongTienXuatTheoThang;
            }
        }
        [DisplayName(SachManager.Properties.TongTienBan)]
        public decimal? TongTienBan
        {
            get
            {
                if (_tongTienBan == null)
                {
                    _tongTienBan = HoaDonDaiLy.Sum(ct => ct.ThanhTien);
                }
                return _tongTienBan;
            }
        }
        [DisplayName(SachManager.Properties.TongTienBanTheoThang)]
        public decimal? TongTienBanTheoThang
        {
            get
            {
                return _tongTienBanTheoThang;
            }
        }
        [DisplayName(SachManager.Properties.TongSoLuongBan)]
        public decimal? TongSoLuongBan
        {
            get
            {
                if (_tongSoLuongBan == null)
                {
                    _tongSoLuongBan = HoaDonDaiLy.Sum(ct => ct.SoLuong);
                }
                return _tongSoLuongBan;
            }
        }
        [DisplayName(SachManager.Properties.TongSoLuongBanTheoThang)]
        public decimal? TongSoLuongBanTheoThang
        {
            get
            {
                return _tongSoLuongBanTheoThang;
            }
        }
        [DisplayName(SachManager.Properties.TongSoLuongNXBNo)]
        public decimal? TongSoLuongNXBNo
        {
            get
            {
                if (_tongSoLuongNXBNo == null)
                {
                    _tongSoLuongNXBNo = CongNoNXB.Sum(cn => cn.SoLuong);
                }
                return _tongSoLuongNXBNo;
            }
        }
        [DisplayName(SachManager.Properties.TongSoLuongDaiLyNo)]
        public decimal? TongSoLuongDaiLyNo
        {
            get
            {
                if (_tongSoLuongDaiLyNo == null)
                {
                    _tongSoLuongDaiLyNo = CongNoDaiLy.Sum(cn => cn.SoLuong);
                }
                return _tongSoLuongDaiLyNo;
            }
        }
        [DisplayName(SachManager.Properties.TongTienNXBNo)]
        public decimal? TongTienNXBNo
        {
            get
            {
                if (_tongTienNXBNo == null)
                {
                    _tongTienNXBNo = CongNoNXB.Sum(cn => cn.ThanhTien);
                }
                return _tongTienNXBNo;
            }
        }
        [DisplayName(SachManager.Properties.TongTienDaiLyNo)]
        public decimal? TongTienDaiLyNo
        {
            get
            {
                if (_tongTienDaiLyNo == null)
                {
                    _tongTienDaiLyNo = CongNoDaiLy.Sum(cn => cn.ThanhTien);
                }
                return _tongTienDaiLyNo;
            }
        }
        [DisplayName(SachManager.Properties.TongSoLuongNXBNoTheoThang)]
        public decimal? TongSoLuongNXBNoTheoThang
        {
            get
            {
                return _tongSoLuongNXBNoTheoThang;
            }
        }
        [DisplayName(SachManager.Properties.TongSoLuongDaiLyNoTheoThang)]
        public decimal? TongSoLuongDaiLyNoTheoThang
        {
            get
            {
                return _tongSoLuongDaiLyNoTheoThang;
            }
        }
        [DisplayName(SachManager.Properties.TongTienNXBNoTheoThang)]
        public decimal? TongTienNXBNoTheoThang
        {
            get
            {
                return _tongTienNXBNoTheoThang;
            }
        }
        [DisplayName(SachManager.Properties.TongTienDaiLyNoTheoTang)]
        public decimal? TongTienDaiLyNoTheoTang
        {
            get
            {
                return _tongTienDaiLyNoTheoThang;
            }
        }


        #endregion

        #region Services
        public bool isExisted()
        {
            Sach sach = SachManager.find(this.MaSoSach);
            if (sach == null)
            {
                return false;
            }
            return true;
        }

        public bool isContentExisted()
        {
            var obj = new { };
            Dictionary<String, dynamic> param = new Dictionary<string, dynamic>();
            param.Add(SachManager.Properties.TenSach, this.TenSach);
            param.Add(SachManager.Properties.TenTacGia, this.TenTacGia);
            param.Add(SachManager.Properties.LinhVucSach, this.LinhVucSach.MaSoLinhVuc);
            param.Add(SachManager.Properties.NXB, this.NXB.MaSoNXB);
            List<Sach> result = SachManager.findBy(param);
            if (result.Count > 0)
                return true;
            return false;
        }
        public decimal? tongTienNhapTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongTienNhapTheoThang = getPhieuNhapTheoThang(startMonth, startYear, endMonth, endYear)
                                    .Sum(p => p.ThanhTien);
            return _tongTienNhapTheoThang;
        }
        public decimal? tongTienXuatTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongTienXuatTheoThang = getPhieuXuatTheoThang(startMonth, startYear, endMonth, endYear)
                                    .Sum(p => p.ThanhTien);
            return _tongTienXuatTheoThang;
        }
        public decimal? tongSoLuongNhapTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongSoLuongNhapTheoThang = getPhieuNhapTheoThang(startMonth,startYear,endMonth,endYear)
                                        .Sum(p => p.SoLuong);
            return _tongSoLuongNhapTheoThang;
        }
        public decimal? tongSoLuongXuatTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongSoLuongXuatTheoThang = getPhieuXuatTheoThang(startMonth, startYear, endMonth, endYear)
                                        .Sum(p => p.SoLuong);
            return _tongSoLuongXuatTheoThang;
        }
        public decimal? tongTienBanTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongTienBanTheoThang = getHoaDonDaiLyTheoThang(startMonth, startYear, endMonth, endYear)
                                    .Sum(p => p.ThanhTien);
            return _tongTienBanTheoThang;
        }
        public decimal? tongSoLuongBanTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongSoLuongBanTheoThang = getHoaDonDaiLyTheoThang(startMonth, startYear, endMonth, endYear)
                                        .Sum(p => p.SoLuong);
            return _tongSoLuongBanTheoThang;
        }
        public decimal? tongSoLuongNXBNoTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongSoLuongNXBNoTheoThang = getCongNoNXBTheoThang(startMonth, startYear, endMonth, endYear)
                                        .Sum(p => p.SoLuong);
            return _tongSoLuongNXBNoTheoThang;
        }
        public decimal? tongSoLuongDaiLyNoTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongSoLuongDaiLyNoTheoThang = getCongNoDaiLyTheoThang(startMonth, startYear, endMonth, endYear)
                                        .Sum(p => p.SoLuong);
            return _tongSoLuongDaiLyNoTheoThang;
        }
        public decimal? tongTienNXBNoTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongTienNXBNoTheoThang = getCongNoNXBTheoThang(startMonth, startYear, endMonth, endYear)
                                        .Sum(p => p.ThanhTien);
            return _tongTienNXBNoTheoThang;
        }
        public decimal? tongTienDaiLyNoTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            _tongTienDaiLyNoTheoThang = getCongNoDaiLyTheoThang(startMonth, startYear, endMonth, endYear)
                                        .Sum(p => p.ThanhTien);
            return _tongTienDaiLyNoTheoThang;
        }
        public List<ChiTietPhieuNhap> getPhieuNhapTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
            return this.PhieuNhap.Where(p =>
                        p.PhieuNhap.NgayLap >= startDate
                        && p.PhieuNhap.NgayLap <= endDate).ToList();
        }
        public List<ChiTietPhieuXuat> getPhieuXuatTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
            return PhieuXuat.Where(p =>
                    p.PhieuXuat.NgayLap >= startDate
                    && p.PhieuXuat.NgayLap <= endDate).ToList();
        }
        public List<ChiTietHoaDonDaiLy> getHoaDonDaiLyTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
            return HoaDonDaiLy.Where(ct =>
                    ct.HoaDon.NgayLap >= startDate
                    && ct.HoaDon.NgayLap <= endDate).ToList();
        }
        public List<CongNoDaiLy> getCongNoDaiLyTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
            return CongNoDaiLy.Where(ct => ct.Thang >= startDate
                    && ct.Thang <= endDate).ToList();
        }
        public List<CongNoNXB> getCongNoNXBTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
            return CongNoNXB.Where(ct => ct.Thang >= startDate
                    && ct.Thang <= endDate).ToList();
        }
        public string ImageFolderPath()
        {
            var imageFolderPath = (new Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            imageFolderPath = Regex.Match(imageFolderPath, ".*QuanLyPhatHanhSach/").ToString()
                                + "Core/Images/";
            return imageFolderPath.Replace("%20"," ");
        }
        public bool delete()
        {
            this.TrangThai = 0;
            return SachManager.edit(this);
        }
        public static List<string> searchKeys()
        {
            if (_searchKeys == null)
            {
                _searchKeys = new List<string>();
                _searchKeys.Add(nameof(SachManager.Properties.MaSoSach));
                _searchKeys.Add(nameof(SachManager.Properties.TenSach));
                _searchKeys.Add(nameof(SachManager.Properties.TenTacGia));
                _searchKeys.Add(nameof(SachManager.Properties.NXB));
                _searchKeys.Add(nameof(SachManager.Properties.LinhVucSach));
                _searchKeys.Add(nameof(SachManager.Properties.Soluong));
                _searchKeys.Add(nameof(SachManager.Properties.GiaBan));
                _searchKeys.Add(nameof(SachManager.Properties.GiaNhap));
            }
            return _searchKeys;
        }
        public static List<string> searchKeysLoNhap()
        {
            if (_searchKeysLoNhap == null)
            {
                _searchKeysLoNhap = new List<string>();
                _searchKeysLoNhap.Add(nameof(SachManager.Properties.MaSoSach));
                _searchKeysLoNhap.Add(nameof(SachManager.Properties.TenSach));
                _searchKeysLoNhap.Add(nameof(SachManager.Properties.TenTacGia));
                _searchKeysLoNhap.Add(nameof(SachManager.Properties.NXB));
                _searchKeysLoNhap.Add(nameof(SachManager.Properties.LinhVucSach));
                _searchKeysLoNhap.Add(nameof(SachManager.Properties.SoLuongNhapTheoThang));
                _searchKeysLoNhap.Add(nameof(SachManager.Properties.TongTienNhapTheoThang));
            }
            return _searchKeysLoNhap;
        }
        public static List<string> searchKeysSachBan()
        {
            if (_searchKeysSachBan == null)
            {
                _searchKeysSachBan = new List<string>();
                _searchKeysSachBan.Add(nameof(SachManager.Properties.MaSoSach));
                _searchKeysSachBan.Add(nameof(SachManager.Properties.TenSach));
                _searchKeysSachBan.Add(nameof(SachManager.Properties.TenTacGia));
                _searchKeysSachBan.Add(nameof(SachManager.Properties.NXB));
                _searchKeysSachBan.Add(nameof(SachManager.Properties.LinhVucSach));
                _searchKeysSachBan.Add(nameof(SachManager.Properties.TongSoLuongBanTheoThang));
                _searchKeysSachBan.Add(nameof(SachManager.Properties.TongTienBanTheoThang));
            }
            return _searchKeysSachBan;
        }
        public static List<string> searchKeysCongNoNXB()
        {
            if (_searchKeysCongNoNXB == null)
            {
                _searchKeysCongNoNXB = new List<string>();
                _searchKeysCongNoNXB.Add(nameof(SachManager.Properties.MaSoSach));
                _searchKeysCongNoNXB.Add(nameof(SachManager.Properties.TenSach));
                _searchKeysCongNoNXB.Add(nameof(SachManager.Properties.TenTacGia));
                _searchKeysCongNoNXB.Add(nameof(SachManager.Properties.NXB));
                _searchKeysCongNoNXB.Add(nameof(SachManager.Properties.LinhVucSach));
                _searchKeysCongNoNXB.Add(nameof(SachManager.Properties.TongSoLuongNXBNoTheoThang));
                _searchKeysCongNoNXB.Add(nameof(SachManager.Properties.TongTienNXBNoTheoThang));
            }
            return _searchKeysCongNoNXB;
        }
        public static List<string> searchKeysCongNoDaiLy()
        {
            if (_searchKeysCongNoDaiLy == null)
            {
                _searchKeysCongNoDaiLy = new List<string>();
                _searchKeysCongNoDaiLy.Add(nameof(SachManager.Properties.MaSoSach));
                _searchKeysCongNoDaiLy.Add(nameof(SachManager.Properties.TenSach));
                _searchKeysCongNoDaiLy.Add(nameof(SachManager.Properties.TenTacGia));
                _searchKeysCongNoDaiLy.Add(nameof(SachManager.Properties.NXB));
                _searchKeysCongNoDaiLy.Add(nameof(SachManager.Properties.LinhVucSach));
                _searchKeysCongNoDaiLy.Add(nameof(SachManager.Properties.TongSoLuongDaiLyNoTheoThang));
                _searchKeysCongNoDaiLy.Add(nameof(SachManager.Properties.TongTienDaiLyNoTheoTang));
            }
            return _searchKeysCongNoDaiLy;
        }
        public decimal tinhSoLuongSachDaiLyNo(int masodaily)
        {
            return CongNoDaiLy.Where(cn => cn.MaSoDaiLy == masodaily).Sum(cn => cn.SoLuong);
        }
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return this.TenSach;
        }
        #endregion
    }
}

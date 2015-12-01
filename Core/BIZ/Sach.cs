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
        private static List<string> _searchKeys;
        private static List<string> _searchKeysLoNhap;
        private decimal? _tongSoLuongNXBNo;
        private decimal? _tongSoLuongDaiLyNo;
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
        [DisplayName(SachManager.Properties.TongSoLuongNXBNo)]
        public decimal? TongSoLuongNXBNo {
            get
            {
                if(_tongSoLuongNXBNo == null)
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

        public Image HinhAnhTypeImage
        {
            get
            {
                if(_image == null)
                {
                    _image = ImagesHelper.BinaryToImage(this.HinhAnh);
                }
                return _image;
            }
            set
            {
                _image = value;
                if(_image != null)
                {
                    _image = ImagesHelper.ResizeImage(_image, 350, 400);
                    this.HinhAnh = ImagesHelper.ImageToBinary(_image);
                }
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
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
            _tongTienNhapTheoThang = this.PhieuNhap.Where(p =>
                        p.PhieuNhap.NgayLap >= startDate
                        && p.PhieuNhap.NgayLap <= endDate).ToList()
                        .Sum(p => p.ThanhTien);
            return _tongTienNhapTheoThang;
        }

        public decimal? tongTienXuatTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
            _tongTienXuatTheoThang = this.PhieuXuat.Where(p =>
                        p.PhieuXuat.NgayLap >= startDate
                        && p.PhieuXuat.NgayLap <= endDate).ToList()
                        .Sum(p => p.ThanhTien);
            return _tongTienXuatTheoThang;
        }
        public decimal? tongSoLuongNhapTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
            _tongSoLuongNhapTheoThang = this.PhieuNhap.Where(p =>
                        p.PhieuNhap.NgayLap >= startDate
                        && p.PhieuNhap.NgayLap <= endDate).ToList()
                        .Sum(p => p.SoLuong);
            return _tongSoLuongNhapTheoThang;
        }
        public decimal? tongSoLuongXuatTheoThang(int startMonth, int startYear, int endMonth, int endYear)
        {
            DateTime startDate = new DateTime(startYear, startMonth, 1);
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
            _tongSoLuongXuatTheoThang = this.PhieuXuat.Where(p =>
                        p.PhieuXuat.NgayLap >= startDate
                        && p.PhieuXuat.NgayLap <= endDate).ToList()
                        .Sum(p => p.SoLuong);
            return _tongSoLuongXuatTheoThang;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DAL;
using System.ComponentModel;

namespace Core.BIZ
{
    public class Sach
    {
        private NhaXuatBan _nxb;
        private LinhVuc _linhvuc;
        private List<CongNoDaiLy> _congnodaily;
        private List<CongNoNXB> _congnonxb;
        private List<ChiTietHoaDonDaiLy> _hoadondaily;
        private List<ChiTietHoaDonNXB> _hoadonnxb;
        private List<ChiTietPhieuNhap> _phieunhap;
        private List<ChiTietPhieuXuat> _phieuxuat;

        [DisplayName(SachManager.Properties.MaSoSach)]
        public int MaSoSach { get; set; }
        [DisplayName(SachManager.Properties.TenSach)]
        public string TenSach { get; set; }
        [DisplayName(SachManager.Properties.MaSoLinhVuc)]
        public int MaSoLinhVuc { get; set; }
        [DisplayName(SachManager.Properties.LinhVucSach)]
        public LinhVuc LinhVucSach {
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
        [DisplayName(SachManager.Properties.TenTacGia)]
        public string TenTacGia { get; set; }
        [DisplayName(SachManager.Properties.MaSoNXB)]
        public int MaSoNXB { get; set; }
        [DisplayName(SachManager.Properties.NXB)]
        public NhaXuatBan NXB {
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
        [DisplayName(SachManager.Properties.GiaNhap)]
        public decimal GiaNhap { get; set; }
        [DisplayName(SachManager.Properties.GiaBan)]
        public decimal GiaBan { get; set; }
        [DisplayName(SachManager.Properties.HinhAnh)]
        public string HinhAnh { get; set; }
        [DisplayName(SachManager.Properties.CongNoDaiLy)]
        public List<CongNoDaiLy> CongNoDaiLy
        {
            get
            {
                if(_congnodaily == null)
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
                if(_congnonxb == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(CongNoNXBManager.Properties.MaSoSach, this.MaSoSach);
                    _congnonxb = CongNoNXBManager.findBy(param);
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
                if(_hoadondaily == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(HoaDonDaiLyManager.ChiTiet.Properties.MaSoSach, this.MaSoSach);
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
                if(_phieunhap == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(PhieuNhapManager.ChiTiet.Properties.MaSoSach, this.MaSoSach);
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
                if(_phieuxuat == null)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(PhieuXuatManager.Chitiet.Properties.MaSoSach, this.MaSoSach);
                    _phieuxuat = PhieuXuatManager.Chitiet.findBy(param);
                }
                return _phieuxuat;
            }
            set
            {
                _phieuxuat = value;
            }
        }

        public bool isExisted()
        {
            Sach sach = SachManager.find(this.MaSoSach);
            if(sach == null)
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

        public override string ToString()
        {
            return this.TenSach;
        }

    }
}

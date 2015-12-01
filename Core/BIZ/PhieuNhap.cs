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
    public class PhieuNhap
    {
        public PhieuNhap() { }
        public PhieuNhap(PHIEUNHAP phieu)
        {
            MaSoPhieuNhap = phieu.masophieunhap;
            MaSoNXB = phieu.masonxb;
            NgayLap = phieu.ngaylap;
            NguoiGiao = phieu.nguoigiaosach;
            TongTien = phieu.tongtien;
            TrangThai = phieu.trangthai;
        }
        public PhieuNhap(PHIEUNHAP phieu, NXB nxb)
            : this(phieu)
        {
            NXB = new NhaXuatBan(nxb);
        }

        #region Private Properties
        private NhaXuatBan _nxb;
        private List<ChiTietPhieuNhap> _chitiet;
        private static List<string> _searchKeys;
        #endregion

        #region Public Properties
        [DisplayName(PhieuNhapManager.Properties.MaSoPhieuNhap)]
        public int MaSoPhieuNhap { get; set; }
        [Required]
        [DisplayName(PhieuNhapManager.Properties.MaSoNXB)]
        public int MaSoNXB { get; set; }
        [DisplayName(PhieuNhapManager.Properties.NXB)]
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
        [Required]
        [DisplayName(PhieuNhapManager.Properties.NgayLap)]
        public DateTime NgayLap { get; set; }
        [Required]
        [DisplayName(PhieuNhapManager.Properties.NguoiGiao)]
        public string NguoiGiao { get; set; }
        [DisplayName(PhieuNhapManager.Properties.TongTien)]
        public decimal TongTien { get; set; }
        //Chi tiết phiếu nhập
        [DisplayName(PhieuNhapManager.Properties.ChiTiet)]
        public List<ChiTietPhieuNhap> ChiTiet
        {
            get
            {
                if (_chitiet == null)
                {
                    _chitiet = PhieuNhapManager.ChiTiet.find(this.MaSoPhieuNhap);

                }
                return _chitiet;
            }
            set
            {
                _chitiet = value;
            }
        }
        [DisplayName(PhieuNhapManager.Properties.TrangThai)]
        public int? TrangThai { get; set; }
        #endregion

        #region Services
        public bool isDetailExisted(ChiTietPhieuNhap chitiet)
        {
            return _chitiet.Contains(chitiet);
        }

        public bool addDetail(ChiTietPhieuNhap chitiet)
        {
            if (isDetailExisted(chitiet))
            {
                return false;
            }
            _chitiet.Add(chitiet);
            return true;
        }
        public bool deleteDetail(int masosach)
        {
            foreach(ChiTietPhieuNhap ct in _chitiet)
            {
                if (ct.MaSoSach.Equals(masosach))
                {
                    _chitiet.Remove(ct);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Duyệt phiếu nhập
        /// </summary>
        /// <returns></returns>
        public bool accept()
        {
            //Duyệt từng chi tiết
            foreach (ChiTietPhieuNhap ct in this.ChiTiet)
            {
                //Cập nhật thông tin sách
                ct.Sach.Soluong += ct.SoLuong;
                if (!SachManager.edit(ct.Sach)) return false;
                //Ghi thẻ kho
                var tk = new TheKho
                {
                    MaSoSach = ct.MaSoSach,
                    SoLuong = ct.Sach.Soluong,
                    NgayGhi = DateTime.Now
                };
                if (TheKhoManager.add(tk) == 0) return false;
                //Cập nhật công nợ
                var congno = new CongNoNXB
                {
                    MaSoNXB = this.MaSoNXB,
                    MaSoSach = ct.MaSoSach,
                    SoLuong = ct.SoLuong,
                    DonGia = ct.DonGia,
                    Thang = DateTime.Now
                };
                if (CongNoNXBManager.add(congno) == 0) return false;
                ct.TrangThai = 1;
            }
            //Thay đổi trang thái phiếu nhập
            this.TrangThai = 1;
            if (PhieuNhapManager.edit(this))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static List<string> searchKeys()
        {
            if (_searchKeys == null)
            {
                _searchKeys = new List<string>();
                _searchKeys.Add(nameof(PhieuNhapManager.Properties.MaSoPhieuNhap));
                _searchKeys.Add(nameof(PhieuNhapManager.Properties.NgayLap));
                _searchKeys.Add(nameof(PhieuNhapManager.Properties.NguoiGiao));
                _searchKeys.Add(nameof(PhieuNhapManager.Properties.TongTien));
                _searchKeys.Add(nameof(PhieuNhapManager.Properties.TrangThai));
                _searchKeys.Add(nameof(NhaXuatBanManager.Properties.TenNXB));
            }
            return _searchKeys;
        }
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return this.NgayLap.ToString();
        }
        #endregion
    }
}

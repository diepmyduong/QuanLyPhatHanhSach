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
    public class PhieuXuat
    {
        public PhieuXuat() { }
        public PhieuXuat(PHIEUXUAT phieu)
        {
            MaSoPhieuXuat = phieu.masophieuxuat;
            MaSoDaiLy = phieu.masodaily;
            NgayLap = phieu.ngaylap;
            NguoiNhan = phieu.nguoinhasach;
            TongTien = phieu.tongtien;
            TrangThai = phieu.trangthai;
        }
        public PhieuXuat(PHIEUXUAT phieu, DAILY daily)
            :this(phieu)
        {
            Daily = new DaiLy(daily);
        }

        #region Private Properties
        private DaiLy _daily;
        private List<ChiTietPhieuXuat> _chitiet;
        private static List<string> _searchKeys;
        #endregion

        #region Public Properties
        [Required]
        [DisplayName(PhieuXuatManager.Properties.MaSoPhieuXuat)]
        public int MaSoPhieuXuat { get; set; }
        [Required]
        [DisplayName(PhieuXuatManager.Properties.MaSoDaiLy)]
        public int MaSoDaiLy { get; set; }
        [DisplayName(PhieuXuatManager.Properties.DaiLy)]
        public DaiLy Daily
        {
            get
            {
                if (_daily == null)
                {
                    _daily = DaiLyManager.find(this.MaSoDaiLy);
                }
                return _daily;
            }
            set
            {
                _daily = value;
            }
        }
        [Required]
        [DisplayName(PhieuXuatManager.Properties.NgayLap)]
        public DateTime NgayLap { get; set; }
        [Required]
        [DisplayName(PhieuXuatManager.Properties.NguoiNhan)]
        public string NguoiNhan { get; set; }
        [Required]
        [DisplayName(PhieuXuatManager.Properties.TongTien)]
        public decimal TongTien { get; set; }

        //Chi tiết phiếu xuát
        [DisplayName(PhieuXuatManager.Properties.ChiTiet)]
        public List<ChiTietPhieuXuat> ChiTiet
        {
            get
            {
                if (_chitiet == null)
                {
                    _chitiet = PhieuXuatManager.Chitiet.find(this.MaSoPhieuXuat);
                }
                return _chitiet;
            }
            set
            {
                _chitiet = value;
            }
        }
        [DisplayName(PhieuXuatManager.Properties.TrangThai)]
        public int? TrangThai { get; set; }
        #endregion

        #region Services
        /// <summary>
        /// Kiểm tra chi tiết đã có trong danh sách chưa
        /// </summary>
        /// <param name="chitiet"></param>
        /// <returns></returns>
        public bool isDetailExisted(ChiTietPhieuXuat chitiet)
        {
            return _chitiet.Contains(chitiet);
        }
        /// <summary>
        /// Thêm chi tiết vào danh sách chi tiết của phiếu
        /// </summary>
        /// <param name="chitiet"></param>
        /// <returns></returns>
        public bool addDetail(ChiTietPhieuXuat chitiet)
        {
            if (isDetailExisted(chitiet))
            {
                return false;
            }
            _chitiet.Add(chitiet);
            return true;
        }
        public bool addDetail(Sach sach, decimal soluong)
        {
            var chitiet = new ChiTietPhieuXuat
            {
                MaSoSach = sach.MaSoSach,
                Sach = sach,
                SoLuong = soluong,
                DonGia = sach.GiaBan
            };
            foreach(var ct in ChiTiet)
            {
                if (ct.Equals(chitiet))
                {
                    ct.SoLuong += chitiet.SoLuong;
                    return true;
                }
            }
            ChiTiet.Add(chitiet);
            return true;
        }
        public static List<string> searchKeys()
        {
            if (_searchKeys == null)
            {
                _searchKeys = new List<string>();
                _searchKeys.Add(nameof(PhieuXuatManager.Properties.MaSoPhieuXuat));
                _searchKeys.Add(nameof(PhieuXuatManager.Properties.NguoiNhan));
                _searchKeys.Add(nameof(PhieuXuatManager.Properties.NgayLap));
                _searchKeys.Add(nameof(PhieuXuatManager.Properties.TongTien));
                _searchKeys.Add(nameof(PhieuXuatManager.Properties.TrangThai));
                _searchKeys.Add(nameof(DaiLyManager.Properties.TenDaiLy));
            }
            return _searchKeys;
        }
        public bool deleteDetail(int masosach)
        {
            foreach (ChiTietPhieuXuat ct in _chitiet)
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
        public AcceptStatus accept()
        {
            if(Daily.TongTienNo > 10000000)
            {
                return AcceptStatus.Limited;
            }
            //Kiểm tra số lượng có thể duyệt không
            foreach (ChiTietPhieuXuat ct in this.ChiTiet)
            {
                if(ct.Sach.Soluong < ct.SoLuong)
                {
                    return AcceptStatus.ProductNotEnought;
                }
            }
            
                //Duyệt từng chi tiết
            foreach (ChiTietPhieuXuat ct in this.ChiTiet)
            {
                //Cập nhật thông tin sách
                ct.Sach.Soluong -= ct.SoLuong;
                if (!SachManager.edit(ct.Sach)) return AcceptStatus.UpdateProductFail;
                
                //Ghi thẻ kho
                var tk = new TheKho
                {
                    MaSoSach = ct.MaSoSach,
                    SoLuong = ct.Sach.Soluong,
                    NgayGhi = DateTime.Now
                };
                if (TheKhoManager.add(tk) == 0) return AcceptStatus.UpdateStoreFail;
                //Cập nhật công nợ
                var congno = new CongNoDaiLy
                {
                    MaSoDaiLy = this.MaSoDaiLy,
                    MaSoSach = ct.MaSoSach,
                    SoLuong = ct.SoLuong,
                    DonGia = ct.DonGia,
                    Thang = DateTime.Now
                };
                if (CongNoDaiLyManager.add(congno) == 0) return AcceptStatus.UpdateLiabilitiesFail;
                ct.TrangThai = 1;
            }
            //Thay đổi trang thái phiếu nhập
            this.TrangThai = 1;
            if (PhieuXuatManager.edit(this))
            {
                return AcceptStatus.Success;
            }
            else
            {
                return AcceptStatus.Error;
            }
        }

        public enum AcceptStatus
        {
            Success,
            ProductNotEnought,
            UpdateProductFail,
            UpdateStoreFail,
            UpdateLiabilitiesFail,
            Error,
            Limited
        }
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return this.NgayLap.ToString();
        }
        public override bool Equals(object obj)
        {
            return MaSoPhieuXuat.Equals(((PhieuXuat)obj).MaSoPhieuXuat);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion




    }
}

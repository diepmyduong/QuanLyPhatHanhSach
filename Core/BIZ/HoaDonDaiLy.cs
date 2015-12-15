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
    public class HoaDonDaiLy
    {
        public HoaDonDaiLy() { }
        public HoaDonDaiLy(HOADONDAILY hoadon)
        {
            MaSoHoaDon = hoadon.masohoadon;
            MaSoDaiLy = hoadon.masodaily;
            NgayLap = hoadon.ngaylap;
            TongTien = hoadon.tongtien;
            TrangThai = hoadon.trangthai;
        }
        public HoaDonDaiLy(HOADONDAILY hoadon, DAILY daily)
            :this(hoadon)
        {
            DaiLy = new DaiLy(daily);
        }


        #region Private Properties
        private DaiLy _daily;
        private List<ChiTietHoaDonDaiLy> _chitiet;
        private static List<string> _searchKeys;
        #endregion

        #region Public Properties
        [Required]
        [DisplayName(HoaDonDaiLyManager.Properties.MaSoHoaDon)]
        public int MaSoHoaDon { get; set; }
        [Required]
        [DisplayName(HoaDonDaiLyManager.Properties.MaSoDaiLy)]
        public int MaSoDaiLy { get; set; }
        [DisplayName(HoaDonDaiLyManager.Properties.DaiLy)]
        public DaiLy DaiLy
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
        [DisplayName(HoaDonDaiLyManager.Properties.NgayLap)]
        public DateTime NgayLap { get; set; }
        [Required]
        [DisplayName(HoaDonDaiLyManager.Properties.TongTien)]
        public decimal TongTien { get; set; }
        [DisplayName(HoaDonDaiLyManager.Properties.ChiTiet)]
        public List<ChiTietHoaDonDaiLy> ChiTiet
        {
            get
            {
                if (_chitiet == null)
                {
                    _chitiet = HoaDonDaiLyManager.ChiTiet.find(this.MaSoHoaDon);
                }
                return _chitiet;
            }
            set
            {
                _chitiet = value;
            }
        }
        [DisplayName(HoaDonDaiLyManager.Properties.TrangThai)]
        public int? TrangThai { get; set; }
        #endregion

        #region Services
        public static List<string> searchKeys()
        {
            if (_searchKeys == null)
            {
                _searchKeys = new List<string>();
                _searchKeys.Add(nameof(HoaDonDaiLyManager.Properties.MaSoHoaDon));
                _searchKeys.Add(nameof(HoaDonDaiLyManager.Properties.MaSoDaiLy));
                _searchKeys.Add(nameof(HoaDonDaiLyManager.Properties.NgayLap));
                _searchKeys.Add(nameof(HoaDonDaiLyManager.Properties.TongTien));
                _searchKeys.Add(nameof(HoaDonDaiLyManager.Properties.TrangThai));
                _searchKeys.Add(nameof(DaiLyManager.Properties.TenDaiLy));
            }
            return _searchKeys;
        }
        public bool isDetailExisted(ChiTietHoaDonDaiLy chitiet)
        {
            return _chitiet.Contains(chitiet);
        }
        public bool addDetail(ChiTietHoaDonDaiLy chitiet)
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
            foreach (ChiTietHoaDonDaiLy ct in _chitiet)
            {
                if (ct.MaSoSach.Equals(masosach))
                {
                    _chitiet.Remove(ct);
                    return true;
                }
            }
            return false;
        }
        public bool accept()
        {
            //Kiểm tra số lượng có thể duyệt không
            foreach (ChiTietHoaDonDaiLy ct in this.ChiTiet)
            {
                //Nếu tổng số lượng sách mà đại lý nợ ít hơn só sách sẽ thanh toán thì hủy duyệt
                if (ct.Sach.TongSoLuongDaiLyNo < ct.SoLuong)
                {
                    return false;
                }
            }
            //Duyệt từng chi tiết
            foreach (ChiTietHoaDonDaiLy ct in this.ChiTiet)
            {
                //
                //Cập nhật công nợ
                var sl = ct.SoLuong; // Số lượng sách thanh toán
                var count = ct.Sach.CongNoDaiLy.Count; // Số lượng các công nợ 
                var index = 0;
                while (sl > 0)
                {
                    if (index == count)
                    {
                        //Đã duyệt hết các công nợ
                        break;
                    }
                    if (ct.Sach.CongNoDaiLy[index].SoLuong >= sl) // Nếu công nợ nhiều hơn hoặc bằng số lượng thanh toán
                    {
                        //Giảm số lượng công nợ
                        ct.Sach.CongNoDaiLy[index].SoLuong -= sl;
                        sl -= sl;
                        //Cập nhật
                        if (!CongNoDaiLyManager.edit(ct.Sach.CongNoDaiLy[index]))
                            return false;
                    }
                    else // Nếu công nợ ít hơn số lượng thanh toán
                    {
                        sl -= ct.Sach.CongNoDaiLy[index].SoLuong;
                        ct.Sach.CongNoDaiLy[index].SoLuong = 0;
                        //Cập nhật
                        if (!CongNoDaiLyManager.edit(ct.Sach.CongNoDaiLy[index]))
                            return false;
                    }
                    index++;
                }
                ct.TrangThai = 1;
            }
            //Thay đổi trang thái phiếu nhập
            this.TrangThai = 1;
            if (HoaDonDaiLyManager.edit(this))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return this.NgayLap.ToString();
        }
        public override bool Equals(object obj)
        {
            return MaSoHoaDon.Equals(((HoaDonDaiLy)obj).MaSoHoaDon);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}

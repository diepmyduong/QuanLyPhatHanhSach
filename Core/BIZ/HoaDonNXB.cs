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
    public class HoaDonNXB
    {
        public HoaDonNXB() { }
        public HoaDonNXB(HOADONNXB hoadon)
        {
            MaSoHoaDon = hoadon.masohoadon;
            MaSoNXB = hoadon.masonxb;
            NgayLap = hoadon.ngaylap;
            TongTien = hoadon.tongtien;
            TrangThai = hoadon.trangthai;
        }
        public HoaDonNXB(HOADONNXB hoadon, NXB nxb)
            :this(hoadon)
        {
            NXB = new NhaXuatBan(nxb);
        }

        #region Private Properties
        private NhaXuatBan _nxb;
        private List<ChiTietHoaDonNXB> _chitiet;
        private static List<string> _searchKeys;
        #endregion

        #region Public Properties
        [Required]
        [DisplayName(HoaDonNXBManager.Properties.MaSoHoaDon)]
        public int MaSoHoaDon { get; set; }
        [Required]
        [DisplayName(HoaDonNXBManager.Properties.MaSoNXB)]
        public int MaSoNXB { get; set; }
        [DisplayName(HoaDonNXBManager.Properties.NXB)]
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
        [DisplayName(HoaDonNXBManager.Properties.NgayLap)]
        public DateTime NgayLap { get; set; }
        [Required]
        [DisplayName(HoaDonNXBManager.Properties.TongTien)]
        public decimal TongTien { get; set; }
        [DisplayName(HoaDonNXBManager.Properties.ChiTiet)]
        public List<ChiTietHoaDonNXB> ChiTiet
        {
            get
            {
                if (_chitiet == null)
                {
                    _chitiet = HoaDonNXBManager.ChiTiet.find(this.MaSoHoaDon);
                }
                return _chitiet;
            }
            set
            {
                _chitiet = value;
            }
        }
        [DisplayName(HoaDonNXBManager.Properties.TrangThai)]
        public int? TrangThai { get; set; }
        #endregion

        #region Services
        public static List<string> searchKeys()
        {
            if (_searchKeys == null)
            {
                _searchKeys = new List<string>();
                _searchKeys.Add(nameof(HoaDonNXBManager.Properties.MaSoHoaDon));
                _searchKeys.Add(nameof(HoaDonNXBManager.Properties.MaSoNXB));
                _searchKeys.Add(nameof(HoaDonNXBManager.Properties.NgayLap));
                _searchKeys.Add(nameof(HoaDonNXBManager.Properties.TongTien));
                _searchKeys.Add(nameof(HoaDonNXBManager.Properties.TrangThai));
                _searchKeys.Add(nameof(NhaXuatBanManager.Properties.TenNXB));
            }
            return _searchKeys;
        }
        public bool isDetailExisted(ChiTietHoaDonNXB chitiet)
        {
            return _chitiet.Contains(chitiet);
        }
        public bool addDetail(ChiTietHoaDonNXB chitiet)
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
            foreach (ChiTietHoaDonNXB ct in _chitiet)
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
            foreach (ChiTietHoaDonNXB ct in this.ChiTiet)
            {
                //Nếu tổng số lượng sách mà nhà xuất bản nợ ít hơn só sách sẽ thanh toán thì hủy duyệt
                if(ct.Sach.TongSoLuongNXBNo < ct.SoLuong)
                {
                    return false;
                }
            }
            //Duyệt từng chi tiết
            foreach (ChiTietHoaDonNXB ct in this.ChiTiet)
            {
                //
                //Cập nhật công nợ
                var sl = ct.SoLuong; // Số lượng sách thanh toán
                var count = ct.Sach.CongNoNXB.Count; // Số lượng các công nợ 
                var index = 0;
                while(sl > 0)
                {
                    if(index == count)
                    {
                        //Đã duyệt hết các công nợ
                        break;
                    }
                    if(ct.Sach.CongNoNXB[index].SoLuong >= sl) // Nếu công nợ nhiều hơn hoặc bằng số lượng thanh toán
                    {
                        //Giảm số lượng công nợ
                        ct.Sach.CongNoNXB[index].SoLuong -= sl;
                        sl -= sl;
                        //Cập nhật
                        if (!CongNoNXBManager.edit(ct.Sach.CongNoNXB[index]))
                            return false;
                    }
                    else // Nếu công nợ ít hơn số lượng thanh toán
                    {
                        sl -= ct.Sach.CongNoNXB[index].SoLuong;
                        ct.Sach.CongNoNXB[index].SoLuong = 0;
                        //Cập nhật
                        if (!CongNoNXBManager.edit(ct.Sach.CongNoNXB[index]))
                            return false;
                    }
                    index++;
                }
                ct.TrangThai = 1;
            }
            //Thay đổi trang thái phiếu nhập
            this.TrangThai = 1;
            if (HoaDonNXBManager.edit(this))
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
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Core.DAL;

namespace Core.BIZ
{
    public class PhieuNhap
    {

        private NhaXuatBan _nxb;

        [DisplayName(PhieuNhapManager.Properties.MaSoPhieuNhap)]
        public int MaSoPhieuNhap { get; set; }
        [DisplayName(PhieuNhapManager.Properties.MaSoNXB)]
        public int MaSoNXB { get; set; }
        [DisplayName(PhieuNhapManager.Properties.NXB)]
        public NhaXuatBan NXB {
            get
            {
                if(_nxb == null)
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
        [DisplayName(PhieuNhapManager.Properties.NgayLap)]
        public DateTime NgayLap { get; set; }
        [DisplayName(PhieuNhapManager.Properties.NguoiGiao)]
        public string NguoiGiao { get; set; }
        [DisplayName(PhieuNhapManager.Properties.TongTien)]
        public decimal TongTien { get; set; }
        //Chi tiết phiếu nhập
        private List<ChiTietPhieuNhap> _chitiet;
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

        public override string ToString()
        {
            return this.NgayLap.ToString();
        }
    }
}

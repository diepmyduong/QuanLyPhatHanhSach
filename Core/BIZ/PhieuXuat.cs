using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Core.DAL;

namespace Core.BIZ
{
    public class PhieuXuat
    {
        private DaiLy _daily;
        private List<ChiTietPhieuXuat> _chitiet;

        [DisplayName(PhieuXuatManager.Properties.MaSoPhieuXuat)]
        public int MaSoPhieuXuat { get; set; }
        [DisplayName(PhieuXuatManager.Properties.MaSoDaiLy)]
        public int MaSoDaiLy { get; set; }
        [DisplayName(PhieuXuatManager.Properties.DaiLy)]
        public DaiLy Daily {
            get
            {
                if(_daily == null)
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
        [DisplayName(PhieuXuatManager.Properties.NgayLap)]
        public DateTime NgayLap { get; set; }
        [DisplayName(PhieuXuatManager.Properties.NguoiNhan)]
        public string NguoiNhan { get; set; }
        [DisplayName(PhieuXuatManager.Properties.TongTien)]
        public decimal TongTien { get; set; }

        //Chi tiết phiếu xuát
        [DisplayName(PhieuXuatManager.Properties.ChiTiet)]
        public List<ChiTietPhieuXuat> ChiTiet
        {
            get
            {
                if(_chitiet == null)
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

        public override string ToString()
        {
            return this.NgayLap.ToString();
        }
    }
}

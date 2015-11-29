﻿using System;
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
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return this.NgayLap.ToString();
        }
        #endregion




    }
}

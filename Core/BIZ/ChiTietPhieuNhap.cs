﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DAL;
using System.ComponentModel;

namespace Core.BIZ
{
    public class ChiTietPhieuNhap
    {
        private Sach _sach;

        [DisplayName(PhieuNhapManager.ChiTiet.Properties.MaSoSach)]
        public int MaSoSach { get; set; }
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.Sach)]
        public Sach Sach {
            get
            {
                if(_sach == null)
                {
                    _sach = SachManager.find(this.MaSoSach);
                }
                return _sach;
            }
            set
            {
                _sach = value;
            }
        }
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.SoLuong)]
        public decimal Soluong { get; set; }
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.DonGia)]
        public decimal DonGia { get; set; }
        [DisplayName(PhieuNhapManager.ChiTiet.Properties.ThanhTien)]
        public decimal ThanhTien {
            get
            {
                return this.Soluong * this.DonGia;
            }
        }

        public override bool Equals(object obj)
        {
            return this.Sach.MaSoSach.Equals(((ChiTietPhieuNhap)obj).Sach.MaSoSach);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return this.Sach.TenSach;
        }
    }
}

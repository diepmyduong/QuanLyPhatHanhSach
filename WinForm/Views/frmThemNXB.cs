﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.BIZ;
using Core.DAL;

namespace WinForm.Views
{
    public partial class frmThemNXB : Form
    {
        public frmThemNXB(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Properties
        Form _frmParent;
        #endregion

        #region Form Control Listener
        //Khi Load Form
        private void frmThemNXB_Load(object sender, EventArgs e)
        {

        }
        //Khi Chọn Thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!txbTenNXB.Text.Equals("") && !txbDiaChi.Text.Equals("") && !txbSoDienThoai.Text.Equals("") && !txbSoTaiKhoan.Text.Equals(""))
            {
                NhaXuatBan nxb = new NhaXuatBan();
                nxb.TenNXB = txbTenNXB.Text.ToString();
                nxb.DiaChi = txbDiaChi.Text.ToString();
                nxb.SoDienThoai = txbSoDienThoai.Text.ToString();
                nxb.SoTaiKhoan = txbSoTaiKhoan.Text.ToString();
               
                    if (NhaXuatBanManager.add(nxb) != 0)
                        MessageBox.Show("Đã thêm nhà xuất bản thành công");
                    else
                        MessageBox.Show("Không thêm được");


               
            }
            else
                MessageBox.Show("Bạn cần nhập đầy đủ thuông tin");
        }
        //Khi Hủy Thêm
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            if (_frmParent.GetType().Name == nameof(frmDanhMucNXB))
            {
                (_frmParent as frmDanhMucNXB).loadNXB();
            }
        }
        #endregion

    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm.Views
{
    public partial class frmLapPhieuXuat : Form
    {
        public frmLapPhieuXuat(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }
        #region Private Properties
        private Form _frmParent;
        #endregion

        #region Form Control Listener
        //Khi form load
        private void frmLapPhieuXuat_Load(object sender, EventArgs e)
        {

        }
        //Khi chọn thêm mới đại lý
        private void btnThemDaiLy_Click(object sender, EventArgs e)
        {

        }
        //Khi chọn lưu lại đơn xuất
        private void btnLuu_Click(object sender, EventArgs e)
        {

        }
        //Khi chọn thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {

        }
        #endregion

    }
}
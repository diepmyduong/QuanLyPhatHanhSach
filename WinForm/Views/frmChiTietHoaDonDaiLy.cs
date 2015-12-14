using System;
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
    public partial class frmChiTietHoaDonDaiLy : Form
    {
        public frmChiTietHoaDonDaiLy(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }
        public frmChiTietHoaDonDaiLy(Form parent, HoaDonDaiLy hd)
            : this(parent)
        {
            _CurrentHD = hd;
        }

        #region Private Properties
        private Form _frmParent;
        private HoaDonDaiLy _CurrentHD;
        #endregion

        #region Form Control Listener
        private void frmChiTietHoaDonDaiLy_Load(object sender, EventArgs e)
        {
            if (_CurrentHD != null)
                gdvChiTiet.DataSource = _CurrentHD.ChiTiet;
            lbMaSoHoaDon.Text = _CurrentHD.MaSoHoaDon + "";
            lbMaSoDaiLy.Text = _CurrentHD.MaSoDaiLy + "";
            lbTenDaiLy.Text = _CurrentHD.DaiLy.TenDaiLy;
            lbNgayLap.Text = _CurrentHD.NgayLap.ToString();
            lbTongTien.Text = _CurrentHD.TongTien.ToString();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}

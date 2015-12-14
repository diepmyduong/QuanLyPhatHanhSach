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
    public partial class frmChiTietHoaDonNXB : Form
    {
        public frmChiTietHoaDonNXB(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }
        public frmChiTietHoaDonNXB(Form parent, HoaDonNXB hd)
            : this(parent)
        {
            _CurrentHD = hd;
        }

        #region Private Properties
        private Form _frmParent;
        private HoaDonNXB _CurrentHD;
        #endregion

        #region Form Control Listener
        private void frmChiTietHoaDonNXB_Load(object sender, EventArgs e)
        {
            if(_CurrentHD!=null)
                 gdvChiTiet.DataSource = _CurrentHD.ChiTiet;
            lbMaSoHoaDon.Text = _CurrentHD.MaSoHoaDon + "";
            lbMaSoNXB.Text = _CurrentHD.MaSoNXB + "";
            lbTenNXB.Text = _CurrentHD.NXB.TenNXB;
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

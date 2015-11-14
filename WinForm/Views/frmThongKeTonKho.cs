using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.DAL;
using Core.BIZ;

namespace WinForm.Views
{
    public partial class frmThongKeTonKho : Form
    {
        public frmThongKeTonKho(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }
        #region Private Properties
        private Form _frmParent;
        private List<TheKho> _DMTheKho;
        private decimal _tongLuongSach;
        #endregion
        #region Form Control Listen
        private void frmThongKeTonKho_Load(object sender, EventArgs e)
        {
            //Load thẻ kho
            loadTheKho();
        }

        private void dtpNgayGhi_ValueChanged(object sender, EventArgs e)
        {
            loadTheKho();
        }

        private void btnLoNhap_Click(object sender, EventArgs e)
        {
            frmThongKeLoNhap form = new frmThongKeLoNhap(this);
            form.ShowDialog(this);
        }

        private void btnLoXuat_Click(object sender, EventArgs e)
        {
            frmThongKeLoXuat form  = new frmThongKeLoXuat(this);
            form.ShowDialog(this);
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {

        }

        private void txbLoc_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txbLoc_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Form Services
        public void loadTheKho()
        {
            _DMTheKho = TheKhoManager.getAllByDate(dtpNgayGhi.Value);
            gdvTheKho.DataSource = _DMTheKho;
            _tongLuongSach = _DMTheKho.Sum(tk => tk.SoLuong);
            lbTongLuongSach.Text = _tongLuongSach.ToString();
        }
        #endregion

    }
}

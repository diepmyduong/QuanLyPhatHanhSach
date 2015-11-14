using System;
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
    public partial class frmThongKeDoanhThu : Form
    {
        public frmThongKeDoanhThu(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Properties
        private Form _frmParent;
        #endregion

        #region Form Control Listener
        private void frmThongKeDoanhThu_Load(object sender, EventArgs e)
        {

        }

        private void cmbStartMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbEndMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbEndYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnNguonThu_Click(object sender, EventArgs e)
        {
            frmThongKeNguonThu form = new frmThongKeNguonThu(this);
            form.ShowDialog(this);
        }

        private void btnNguonChi_Click(object sender, EventArgs e)
        {
            frmThongKeNguonChi form = new frmThongKeNguonChi(this);
            form.ShowDialog(this);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbLocDaiLy_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txbLocDaiLy_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txbLocNXB_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txbLocNXB_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        #endregion


    }
}

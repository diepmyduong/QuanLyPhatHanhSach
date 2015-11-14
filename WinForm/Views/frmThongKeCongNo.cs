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
    public partial class frmThongKeCongNo : Form
    {
        public frmThongKeCongNo(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Properties
        private Form _frmParent;
        #endregion

        #region Form Control Listener
        private void frmThongKeCongNo_Load(object sender, EventArgs e)
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

        private void rabtnNXB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rabtnDaiLy_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btLoc_Click(object sender, EventArgs e)
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

    }
}

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
    public partial class frmThemDaiLy : Form
    {
        public frmThemDaiLy(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Propertites
        private Form _frmParent;
        #endregion

        #region Form Control Listener
        //Khi form Load
        private void frmThemDaiLy_Load(object sender, EventArgs e)
        {

        }
        //Khi thêm Đại lý
        private void btnThem_Click(object sender, EventArgs e)
        {

        }
        //Khi hủy thêm
        private void btnThoat_Click(object sender, EventArgs e)
        {

        }
        #endregion

    }
}

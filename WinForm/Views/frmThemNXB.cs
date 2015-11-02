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

        }
        //Khi Hủy Thêm
        private void btnThoat_Click(object sender, EventArgs e)
        {

        }
        #endregion

    }
}

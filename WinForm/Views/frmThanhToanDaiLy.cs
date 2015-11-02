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
    public partial class frmThanhToanDaiLy : Form
    {
        public frmThanhToanDaiLy(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Properties
        private Form _frmParent;
        #endregion

        #region Form Control Listener
        //Khi form load
        private void frmThanhToanDaiLy_Load(object sender, EventArgs e)
        {

        }
        //Khi chọn đại lý
        private void cmbDaiLy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //khi Lưu lại hóa đơn
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

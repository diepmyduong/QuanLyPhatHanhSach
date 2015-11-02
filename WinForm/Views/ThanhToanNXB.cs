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
    public partial class ThanhToanNXB : Form
    {
        public ThanhToanNXB(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }
        #region Private Properties
        private Form _frmParent;
        #endregion

        #region Form Control Listenner
        //khi form load
        private void ThanhToanNXB_Load(object sender, EventArgs e)
        {

        }
        //Khi chọn lưu lại hóa đơn
        private void btnLuu_Click(object sender, EventArgs e)
        {

        }
        //Khi chọn thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {

        }
        //Khi chọn NXB
        private void cmbNXB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

    }
}

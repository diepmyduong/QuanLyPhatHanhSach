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
    public partial class frmCongNoNXB : Form
    {
        public frmCongNoNXB(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Properties
        private Form _frmParent;
        #endregion

        #region Form Control Listener
        //Khi load form
        private void frmCongNoNXB_Load(object sender, EventArgs e)
        {

        }
        //Khi chọn xem chi tiết
        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {

        }
        //Khi chọn thanh toán
        private void btnThanhToan_Click(object sender, EventArgs e)
        {

        }
        //Khi chọn nút lọc
        private void btnLoc_Click(object sender, EventArgs e)
        {

        }
        //Khi chọn nút thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {

        }
        //Khi chọn thắng bắt đầu
        private void cmbStartMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //Khi chọn năm bắt đầu
        private void cmbStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //Khi chọn tháng kết thúc
        private void cmbEndMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //Khi chọn năm kết thúc
        private void cmbEndYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //Khi nhập từ khóa lọc
        private void txbLoc_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        #endregion


    }
}

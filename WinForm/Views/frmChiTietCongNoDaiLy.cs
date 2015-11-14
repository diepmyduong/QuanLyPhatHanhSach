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
    public partial class frmChiTietCongNoDaiLy : Form
    {
        public frmChiTietCongNoDaiLy(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Properties
        private Form _frmParent;
        #endregion

        #region Form Control Listener
        //Khi form load
        private void frmChiTietCongNoDaiLy_Load(object sender, EventArgs e)
        {

        }
        //Khi chọn thanh toán
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            frmThanhToanDaiLy form = new frmThanhToanDaiLy(this);
            form.ShowDialog(this);
        }
        //Khi chọn thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Khi chọn tháng bắt đầu
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
        #endregion


    }
}

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
    public partial class frmCongNoDaiLy : Form
    {
        public frmCongNoDaiLy(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Properties
        private Form _frmParent;
        #endregion

        #region Form Control Listener
        //Khi load form
        private void frmCongNoDaiLy_Load(object sender, EventArgs e)
        {

        }
        //Khi chọn xem chi tiết CÔng nợ
        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            frmChiTietCongNoDaiLy form = new frmChiTietCongNoDaiLy(this);
            form.ShowDialog(this);
        }
        //Khi chọn thanh toán
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            frmThanhToanDaiLy form = new frmThanhToanDaiLy(this);
            form.ShowDialog(this);
        }
        //Khi chọn lọc
        private void btnLoc_Click(object sender, EventArgs e)
        {

        }
        //Khi chọn Thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Khi chọn công nợ của 1 đại lý
        private void gdvDMCongNo_SelectionChanged(object sender, EventArgs e)
        {

        }
        //Khi gõ từ khóa lọc
        private void txbLoc_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        #endregion


    }
}

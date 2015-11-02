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
    public partial class frmDanhMucDaiLy : Form
    {
        public frmDanhMucDaiLy(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Propertites
        private Form _frmParent;
        #endregion

        #region Form Control Listener
        //Khi Load Form
        private void frmDanhMucDaiLy_Load(object sender, EventArgs e)
        {

        }
        //Khi Cập nhật thông tin Đại lý
        private void btnCapNhat_Click(object sender, EventArgs e)
        {

        }
        //Khi Xóa 1 Đại lý
        private void btnXoa_Click(object sender, EventArgs e)
        {

        }
        //Khi Thêm Mới Đại lý
        private void btnThemDaiLy_Click(object sender, EventArgs e)
        {

        }
        //Khi Chọn Xem Công Nợ Đại lý
        private void btnXemCongNo_Click(object sender, EventArgs e)
        {

        }
        //Khi Chọn thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {

        }
        //Khi Chọn Đại lý từ Danh Mục Đại lý
        private void gdvDMDaiLy_SelectionChanged(object sender, EventArgs e)
        {

        }
        #endregion


    }
}

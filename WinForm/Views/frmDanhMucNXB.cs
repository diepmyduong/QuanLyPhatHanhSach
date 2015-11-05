using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.BIZ;
using Core.DAL;

namespace WinForm.Views
{
    public partial class frmDanhMucNXB : Form
    {
        public frmDanhMucNXB(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;

        }
        #region Private Propertites
        private Form _frmParent;
        private List<NhaXuatBan> _DMNXB;
        #endregion

        #region Form Control Listener
        //Khi Load Form
        private void frmDanhMucNXB_Load(object sender, EventArgs e)
        {
            //Load Danh mục NXB
            loadNXB();
        }
        //Khi Cập nhật thông tin NXB
        private void btnCapNhat_Click(object sender, EventArgs e)
        {

        }
        //Khi CHọn xóa NXB
        private void btnXoa_Click(object sender, EventArgs e)
        {

        }
        //Khi chọn Thêm mới 1 NXB
        private void btnThemNXB_Click(object sender, EventArgs e)
        {
            frmThemNXB form = new frmThemNXB(this);
            form.ShowDialog(this);
        }
        //Khi Chọn Xem Công nợ NXB
        private void btnXemCongNo_Click(object sender, EventArgs e)
        {

        }
        //Khi Chọn Thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            if (_frmParent.GetType().Name == nameof(frmMain))
            {
                (_frmParent as frmMain).loadNXB();
            }

        }
        //Khi chọn 1 NXB từ Danh mục NXB
        private void gdvDMNXB_SelectionChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Form Services
        /// <summary>
        /// Load danh sách NXB và đưa lên giao diện
        /// </summary>
        public void loadNXB()
        {
            _DMNXB = NhaXuatBanManager.getAll();
            gdvDMNXB.DataSource = _DMNXB;
        }
        #endregion

    }
}

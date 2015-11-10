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
using System.Reflection;

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
        private NhaXuatBan _currentNXB;
        #endregion

        #region Form Control Listener
        //Khi Load Form
        private void frmDanhMucNXB_Load(object sender, EventArgs e)
        {
            //Load Danh mục NXB
            loadNXB();
            txbLoc.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txbLoc.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txbLoc.AutoCompleteCustomSource = null;
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
            frmCongNoNXB form = new frmCongNoNXB(this, _currentNXB);
            form.ShowDialog(this);
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
            int index = ((DataGridView)sender).CurrentRow.Index;
            _currentNXB = (((DataGridView)sender).DataSource as List<NhaXuatBan>)[index];
            selectNXB(_currentNXB);
        }
        private void txbLoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 123)
            {
                txbLoc.Text = txbLoc.Text + "{";
                string request = txbLoc.Text;
                var pros = typeof(NhaXuatBanManager.Properties).GetFields();
                AutoCompleteStringCollection source = new AutoCompleteStringCollection();
                foreach (FieldInfo info in pros)
                {
                    source.Add(request + info.Name);
                }
                txbLoc.AutoCompleteCustomSource = source;
            }
        }

        private void txbLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gdvDMNXB.DataSource = NhaXuatBanManager.filter(txbLoc.Text, _DMNXB);
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txbLoc.Text))
            {
                gdvDMNXB.DataSource = NhaXuatBanManager.filter(txbLoc.Text, _DMNXB);
            }
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

        public void selectNXB(NhaXuatBan nxb)
        {
            if(nxb != null)
            {
                txbMaSoNXB.Text = nxb.MaSoNXB.ToString();
                txbTenNXB.Text = nxb.TenNXB;
                txbDiaChi.Text = nxb.DiaChi;
                txbSoDienThoai.Text = nxb.SoDienThoai;
                txbSoTaiKhoan.Text = nxb.SoTaiKhoan;
            }
        }
        #endregion

        
    }
}

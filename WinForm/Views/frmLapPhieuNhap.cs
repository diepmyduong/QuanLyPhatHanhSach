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
    public partial class frmLapPhieuNhap : Form
    {
        public frmLapPhieuNhap(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Properties
        private List<NhaXuatBan> _DMNXB;
        private List<Sach> _DMSach;
        private Form _frmParent;
        private PhieuNhap _phieunhap;
        #endregion

        #region Form Control Listen
        //Khi load Form
        private void frmLapPhieuNhap_Load(object sender, EventArgs e)
        {
            //Load danh sách NXB
            loadNXB();
            //Load danh sách các sách;
            loadSach();

        }
        //Khi Nhấn Lưu lại phiếu nhập
        private void btnLuu_Click(object sender, EventArgs e)
        {

        }
        //Khi chọn thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            if (_frmParent.GetType().Name == nameof(frmMain))
            {
                (_frmParent as frmMain).loadSach();
            }
        }
        //Khi chọn Nhà Xuất Bản
        private void cmbNhaXuatBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void gdvChiTiet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var a = gdvChiTiet.Columns[e.ColumnIndex].Name;
        }
        #endregion

        #region Form Services
        public void loadNXB()
        {
            _DMNXB = NhaXuatBanManager.getAll();
            cmbNhaXuatBan.DataSource = _DMNXB;
            cmbNhaXuatBan.DisplayMember = nameof(NhaXuatBanManager.Properties.TenNXB);
            cmbNhaXuatBan.ValueMember = nameof(NhaXuatBanManager.Properties.MaSoNXB);
        }

        public void loadSach()
        {
            _DMSach = SachManager.getAll();
        }
        private void gdvChiTiet_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var currentRow = (sender as DataGridView).CurrentRow;
            var cellMaSoSach = (DataGridViewComboBoxCell)currentRow.Cells[nameof(SachManager.Properties.MaSoSach)];
            var cellTenSach = (DataGridViewComboBoxCell)currentRow.Cells[nameof(SachManager.Properties.TenSach)];
            cellMaSoSach.DataSource = cellTenSach.DataSource = _DMSach;
            cellMaSoSach.DisplayMember = nameof(SachManager.Properties.MaSoSach);
            cellTenSach.DisplayMember = nameof(SachManager.Properties.TenSach);
            cellMaSoSach.ValueMember = nameof(SachManager.Properties.MaSoSach);
            cellTenSach.ValueMember = nameof(SachManager.Properties.MaSoSach);
            ComboBox cmbBx = e.Control as ComboBox;
            if (cmbBx != null)
            {
                e.CellStyle.BackColor = gdvChiTiet.DefaultCellStyle.BackColor;
            }

        }
        #endregion


    }
}

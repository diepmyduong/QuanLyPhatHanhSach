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
            createGridViewColumns();
            loadNXB();
            txbLoc.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txbLoc.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txbLoc.AutoCompleteCustomSource = null;
        }
        //Khi Cập nhật thông tin NXB
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn cập nhật nhà xuất bản", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (!txbMaSoNXB.Text.Equals("") && !txbTenNXB.Text.Equals("") && !txbSoTaiKhoan.Text.Equals("") && !txbSoDienThoai.Text.Equals(""))
                {
                    _currentNXB.TenNXB = txbTenNXB.Text.ToString();
                    _currentNXB.DiaChi = txbDiaChi.Text.ToString();
                    _currentNXB.SoDienThoai = txbSoDienThoai.Text.ToString();
                    _currentNXB.SoTaiKhoan = txbSoTaiKhoan.Text.ToString();
                    _currentNXB.NganHang = txbNganHang.Text.ToString();

                    if (NhaXuatBanManager.edit(_currentNXB))
                    {
                        MessageBox.Show("Đã sửa thành công");
                        loadNXB();
                    }
                    else
                        MessageBox.Show("Không sửa được");



                }
                else
                    MessageBox.Show("Bạn phải nhập đủ thông tin nhà xuất bản");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
         
        }
        //Khi CHọn xóa NXB
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa nhà xuất bản", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (!txbMaSoNXB.Text.Equals(""))
                {
                    if (NhaXuatBanManager.delete(int.Parse(txbMaSoNXB.Text.ToString())))
                    {
                        MessageBox.Show("Đã xóa thành công");
                        loadNXB();
                    }
                    else
                        MessageBox.Show("Không xóa được");
                }
                else
                    MessageBox.Show("Chưa nhập mã nhà xuất bản cần xóa");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
         
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
            frmChiTietCongNoNXB form = new frmChiTietCongNoNXB(this,_currentNXB);
            form.ShowDialog(this);
        }
        //Khi Chọn Thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
                if (_frmParent.GetType().Name == nameof(frmMain))
                {
                    (_frmParent as frmMain).loadNXB();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
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
                var pros = NhaXuatBan.searchKeys();
                AutoCompleteStringCollection source = new AutoCompleteStringCollection();
                foreach (var info in pros)
                {
                    source.Add(request + info);
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
            _DMNXB = NhaXuatBanManager.getAllAlive();
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
                txbNganHang.Text = nxb.NganHang;
            }
        }

        private void createGridViewColumns()
        {
            gdvDMNXB.AutoGenerateColumns = false; // Bỏ auto generate Columns
            gdvDMNXB.ColumnCount = 6; // Xác định số columns có
            setColumn(gdvDMNXB.Columns[0]
                , nameof(NhaXuatBanManager.Properties.MaSoNXB)
                , NhaXuatBanManager.Properties.MaSoNXB);
            setColumn(gdvDMNXB.Columns[1]
                , nameof(NhaXuatBanManager.Properties.TenNXB)
                , NhaXuatBanManager.Properties.TenNXB);
            setColumn(gdvDMNXB.Columns[2]
                , nameof(NhaXuatBanManager.Properties.DiaChi)
                , NhaXuatBanManager.Properties.DiaChi);
            setColumn(gdvDMNXB.Columns[3]
                , nameof(NhaXuatBanManager.Properties.SoDienThoai)
                , NhaXuatBanManager.Properties.SoDienThoai);
            setColumn(gdvDMNXB.Columns[4]
                , nameof(NhaXuatBanManager.Properties.SoDienThoai)
                , NhaXuatBanManager.Properties.SoTaiKhoan);
            setColumn(gdvDMNXB.Columns[5]
                , nameof(NhaXuatBanManager.Properties.NganHang)
                , NhaXuatBanManager.Properties.NganHang);

        }

        private void setColumn(DataGridViewColumn column, string propertyName, string name)
        {
            column.Name = propertyName;
            column.DataPropertyName = propertyName;
            column.HeaderText = name;
        }
        #endregion


    }
}

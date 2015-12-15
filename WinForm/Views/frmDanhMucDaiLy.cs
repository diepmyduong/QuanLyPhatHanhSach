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
    public partial class frmDanhMucDaiLy : Form
    {
        public frmDanhMucDaiLy(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Propertites
        private Form _frmParent;
        private List<DaiLy> _DMDaiLy;
        private DaiLy _currentDaiLy;
        #endregion

        #region Form Control Listener
        //Khi Load Form
        private void frmDanhMucDaiLy_Load(object sender, EventArgs e)
        {
            createGridViewColumns();
            loadDaiLy();
        }
        //Khi Cập nhật thông tin Đại lý
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn cập nhật đại lý", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (!txbMaSoDaiLy.Text.Equals("") && !txbTenDaiLy.Text.Equals("") && !txbSoDienThoai.Text.Equals("") && !txbSoTaiKhoan.Text.Equals("") && !txbDiaChi.Text.Equals(""))
                {
                    //DaiLy dl = new DaiLy();
                    //dl.MaSoDaiLy = int.Parse(txbMaSoDaiLy.Text);
                    _currentDaiLy.TenDaiLy = txbTenDaiLy.Text.ToString();
                    _currentDaiLy.DiaChi = txbDiaChi.Text.ToString();
                    _currentDaiLy.SoDienThoai = txbSoDienThoai.Text.ToString();
                    _currentDaiLy.SoTaiKhoan = txbSoTaiKhoan.Text.ToString();
                    _currentDaiLy.NganHang = txbNganHang.Text.ToString();


                    if (DaiLyManager.edit(_currentDaiLy))
                    {
                        MessageBox.Show("Đã sửa đại lý thành công");
                        loadDaiLy();
                    }
                    else
                        MessageBox.Show("Không sửa được");



                }
                else
                    MessageBox.Show("Nhập đầy đủ thông tin cần sửa");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
          
        }
        //Khi Xóa 1 Đại lý
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa đại lý", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (!txbMaSoDaiLy.Text.Equals(""))
                {

                    // dl.MaSoDaiLy = int.Parse(txbMaSoDaiLy.Text.ToString());
                    if (DaiLyManager.delete(_currentDaiLy.MaSoDaiLy))
                    {
                        MessageBox.Show("Xóa đại lý thành công");
                        loadDaiLy();
                    }
                    else
                        MessageBox.Show("Không xóa được đại lý");
                }
                else
                    MessageBox.Show("Chọn đại lý cần xóa");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
          
        }
        //Khi Thêm Mới Đại lý
        private void btnThemDaiLy_Click(object sender, EventArgs e)
        {
            frmThemDaiLy form = new frmThemDaiLy(this);
            form.ShowDialog(this);
        }
        //Khi Chọn Xem Công Nợ Đại lý
        private void btnXemCongNo_Click(object sender, EventArgs e)
        {
            frmCongNoDaiLy form = new frmCongNoDaiLy(this);
            form.ShowDialog(this);
        }
        //Khi Chọn thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }
        //Khi Chọn Đại lý từ Danh Mục Đại lý
        private void gdvDMDaiLy_SelectionChanged(object sender, EventArgs e)
        {
            int index = ((DataGridView)sender).CurrentRow.Index;
            _currentDaiLy = (((DataGridView)sender).DataSource as List<DaiLy>)[index];
            selectDaiLy(_currentDaiLy);
        }
        #endregion

        #region
        public void loadDaiLy()
        {
            _DMDaiLy = DaiLyManager.getAllAlive();
            gdvDMDaiLy.DataSource = _DMDaiLy;
        }
        public void selectDaiLy(DaiLy daily)
        {
            if (daily != null)
            {
                txbMaSoDaiLy.Text = daily.MaSoDaiLy.ToString();
                txbTenDaiLy.Text = daily.TenDaiLy;
                txbDiaChi.Text = daily.DiaChi;
                txbSoDienThoai.Text = daily.SoDienThoai;
                txbSoTaiKhoan.Text = daily.SoTaiKhoan;
                txbNganHang.Text = daily.NganHang;
            }
        }

        private void createGridViewColumns()
        {
            gdvDMDaiLy.AutoGenerateColumns = false; // Bỏ auto generate Columns
            gdvDMDaiLy.ColumnCount = 6; // Xác định số columns có
            setColumn(gdvDMDaiLy.Columns[0]
                , nameof(DaiLyManager.Properties.MaSoDaiLy)
                , DaiLyManager.Properties.MaSoDaiLy);
            setColumn(gdvDMDaiLy.Columns[1]
                , nameof(DaiLyManager.Properties.TenDaiLy)
                , DaiLyManager.Properties.TenDaiLy);
            setColumn(gdvDMDaiLy.Columns[2]
                , nameof(DaiLyManager.Properties.DiaChi)
                , DaiLyManager.Properties.DiaChi);
            setColumn(gdvDMDaiLy.Columns[3]
                , nameof(DaiLyManager.Properties.SoDienThoai)
                , DaiLyManager.Properties.SoDienThoai);
            setColumn(gdvDMDaiLy.Columns[4]
                , nameof(DaiLyManager.Properties.SoDienThoai)
                , DaiLyManager.Properties.SoTaiKhoan);
            setColumn(gdvDMDaiLy.Columns[5]
                , nameof(DaiLyManager.Properties.NganHang)
                , DaiLyManager.Properties.NganHang);

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

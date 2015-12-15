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
using System.Globalization;

namespace WinForm.Views
{
    public partial class frmChiTietLoNhap : Form
    {
        public frmChiTietLoNhap(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        public frmChiTietLoNhap(Form parent, Sach sach)
            : this(parent)
        {
            _currentSach = sach;
        }

        #region Private Properties
        private Form _frmParent;
        private Sach _currentSach;
        private int _startMonth;
        private int _startYear;
        private int _endMonth;
        private int _endYear;
        private CultureInfo _cultureInfo;
        #endregion

        #region Form Control Listener
        private void frmChiTietLoNhap_Load(object sender, EventArgs e)
        {

            _cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
            _cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
            createGridViewColumns();
            //Load combobox tháng và năm
            cmbStartMonth.DataSource = new BindingSource(FilterHelper.Monthes, null);
            cmbEndMonth.DataSource = new BindingSource(FilterHelper.Monthes, null);
            cmbStartMonth.DisplayMember = cmbEndMonth.DisplayMember = "Key";
            cmbStartMonth.ValueMember = cmbEndMonth.ValueMember = "Value";
            cmbStartYear.DataSource = new BindingSource(FilterHelper.Years, null);
            cmbEndYear.DataSource = new BindingSource(FilterHelper.Years, null);
            cmbStartYear.DisplayMember = cmbEndYear.DisplayMember = "Key";
            cmbStartYear.ValueMember = cmbEndYear.ValueMember = "Value";

            //Set Tháng năm mặc định
            cmbStartMonth.SelectedValue = 1;
            cmbStartYear.SelectedValue = DateTime.Now.Year;
            cmbEndMonth.SelectedValue = 12;
            cmbEndYear.SelectedValue = DateTime.Now.Year;
            if (_currentSach != null)
            {
                lbMaSach.Text = _currentSach.MaSoSach + "";
                lbTenSach.Text = _currentSach.TenSach;
                loadChiTiet();
            }

        }

        private void cmbStartMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            reloadGridView();
        }

        private void cmbStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            reloadGridView();
        }

        private void cmbEndMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            reloadGridView();
        }

        private void cmbEndYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            reloadGridView();
        }

        private void btLoc_Click(object sender, EventArgs e)
        {

        }

        private void txbLoc_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txbLoc_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

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
        #endregion

        #region Services
        private void createGridViewColumns()
        {
            gdvChiTiet.AutoGenerateColumns = false;
            gdvChiTiet.ColumnCount = 6;
            setColumn(gdvChiTiet.Columns[0]
                , nameof(PhieuNhapManager.ChiTiet.Properties.MaSoPhieuNhap)
                , PhieuNhapManager.ChiTiet.Properties.MaSoPhieuNhap);
            setColumn(gdvChiTiet.Columns[1]
                , nameof(PhieuNhapManager.Properties.NguoiGiao)
                , PhieuNhapManager.Properties.NguoiGiao);
            setColumn(gdvChiTiet.Columns[2]
                , nameof(PhieuNhapManager.ChiTiet.Properties.SoLuong)
                , PhieuNhapManager.ChiTiet.Properties.SoLuong);
            setColumn(gdvChiTiet.Columns[3]
                , nameof(PhieuNhapManager.ChiTiet.Properties.DonGia)
                , PhieuNhapManager.ChiTiet.Properties.DonGia);
            setColumn(gdvChiTiet.Columns[4]
                , nameof(PhieuNhapManager.ChiTiet.Properties.ThanhTien)
                , PhieuNhapManager.ChiTiet.Properties.ThanhTien);
            setColumn(gdvChiTiet.Columns[5]
                , nameof(PhieuNhapManager.Properties.NgayLap)
                , PhieuNhapManager.Properties.NgayLap);

        }

        private void setColumn(DataGridViewColumn column, string propertyName, string name)
        {
            column.Name = propertyName;
            column.DataPropertyName = propertyName;
            column.HeaderText = name;
        }

        public void loadChiTiet()
        {
            if (_currentSach != null)
            {
                gdvChiTiet.DataSource = _currentSach.PhieuNhap;
                reloadGridView();
            }
        }
        private void reloadGridView()
        {
            if (gdvChiTiet.DataSource != null && _startYear != 0 && _startYear != 0 && _endMonth != 0 && _endYear != 0)
            {
                foreach (DataGridViewRow row in gdvChiTiet.Rows)
                {
                    ChiTietPhieuNhap chitiet = row.DataBoundItem as ChiTietPhieuNhap;
                    gdvChiTiet.Rows[row.Index].Cells[nameof(PhieuNhapManager.Properties.NguoiGiao)].Value
                        = chitiet.PhieuNhap.NguoiGiao;
                    gdvChiTiet.Rows[row.Index].Cells[nameof(PhieuNhapManager.Properties.NgayLap)].Value
                        = chitiet.PhieuNhap.NgayLap.ToString();
                }
                lbSoLuongNhap.Text = _currentSach.tongSoLuongNhapTheoThang(_startMonth, _startYear, _endMonth, _endYear).ToString();
                lbTongTien.Text = money(_currentSach.tongTienNhapTheoThang(_startMonth, _startYear, _endMonth, _endYear));
            }

        }
        private string money(decimal? m)
        {
            return String.Format(_cultureInfo, "{0:c}", m);
        }
        #endregion


    }
}

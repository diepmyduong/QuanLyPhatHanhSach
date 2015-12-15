using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.DAL;
using Core.BIZ;
using System.Globalization;

namespace WinForm.Views
{
    public partial class frmChiTietLoXuat : Form
    {
        public frmChiTietLoXuat(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }
        public frmChiTietLoXuat(Form parent, Sach sach)
            : this(parent)
        {
            _currentSach = sach;
        }
        private Form _frmParent;
        private Sach _currentSach;
        private int _startMonth;
        private int _startYear;
        private int _endMonth;
        private int _endYear;
        private CultureInfo _cultureInfo;

        private void frmChiTietLoXuat_Load(object sender, EventArgs e)
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
        private void createGridViewColumns()
        {
            gdvChiTiet.AutoGenerateColumns = false;
            gdvChiTiet.ColumnCount = 6;
            setColumn(gdvChiTiet.Columns[0]
                , nameof(PhieuXuatManager.Chitiet.Properties.MaSoPhieuXuat)
                , PhieuXuatManager.Chitiet.Properties.MaSoPhieuXuat);
            setColumn(gdvChiTiet.Columns[1]
                , nameof(PhieuXuatManager.Properties.NguoiNhan)
                , PhieuXuatManager.Properties.NguoiNhan);
            setColumn(gdvChiTiet.Columns[2]
                , nameof(PhieuXuatManager.Chitiet.Properties.SoLuong)
                , PhieuXuatManager.Chitiet.Properties.SoLuong);
            setColumn(gdvChiTiet.Columns[3]
                , nameof(PhieuXuatManager.Chitiet.Properties.DonGia)
                , PhieuXuatManager.Chitiet.Properties.DonGia);
            setColumn(gdvChiTiet.Columns[4]
                , nameof(PhieuXuatManager.Chitiet.Properties.ThanhTien)
                , PhieuXuatManager.Chitiet.Properties.ThanhTien);
            setColumn(gdvChiTiet.Columns[5]
                , nameof(PhieuXuatManager.Properties.NgayLap)
                , PhieuXuatManager.Properties.NgayLap);

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
                gdvChiTiet.DataSource = _currentSach.PhieuXuat;
                reloadGridView();
            }
        }
        private void reloadGridView()
        {
            if (gdvChiTiet.DataSource != null && _startYear != 0 && _startYear != 0 && _endMonth != 0 && _endYear != 0)
            {
                foreach (DataGridViewRow row in gdvChiTiet.Rows)
                {
                    ChiTietPhieuXuat chitiet = row.DataBoundItem as ChiTietPhieuXuat;
                    gdvChiTiet.Rows[row.Index].Cells[nameof(PhieuXuatManager.Properties.NguoiNhan)].Value
                        = chitiet.PhieuXuat.NguoiNhan;
                    gdvChiTiet.Rows[row.Index].Cells[nameof(PhieuXuatManager.Properties.NgayLap)].Value
                        = chitiet.PhieuXuat.NgayLap.ToString();
                }
                lbSoLuongXuat.Text = _currentSach.tongSoLuongNhapTheoThang(_startMonth, _startYear, _endMonth, _endYear).ToString();
                lbTongTien.Text = money(_currentSach.tongTienNhapTheoThang(_startMonth, _startYear, _endMonth, _endYear));
            }

        }
        private string money(decimal? m)
        {
            return String.Format(_cultureInfo, "{0:c}", m);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

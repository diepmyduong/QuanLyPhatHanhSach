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
    public partial class frmThongKeLoNhap : Form
    {
        public frmThongKeLoNhap(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Properties
        private Form _frmParent;
        private List<Sach> _DMSach;
        private int _startMonth;
        private int _startYear;
        private int _endMonth;
        private int _endYear;
        private Sach _currentSach;
        private decimal? _tongLuongNhap;
        private CultureInfo _cultureInfo;
        #endregion

        #region Form Control Listenner
        private void frmThongKeLoNhap_Load(object sender, EventArgs e)
        {
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
            loadSach();
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbLoc_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txbLoc_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            frmChiTietLoNhap form = new frmChiTietLoNhap(this, _currentSach);
            form.ShowDialog(this);
        }
        private void gdvLoNhap_DataSourceChanged(object sender, EventArgs e)
        {
            reloadGridView();
        }

        private void gdvLoNhap_SelectionChanged(object sender, EventArgs e)
        {
            int index = gdvLoNhap.CurrentRow.Index;
            _currentSach = (gdvLoNhap.DataSource as List<Sach>)[index];
        }
        #endregion

        #region Form Services
        public void loadSach()
        {
            _DMSach = SachManager.getAll();
            
            gdvLoNhap.DataSource = _DMSach;
        }
        private void createGridViewColumns()
        {
            gdvLoNhap.AutoGenerateColumns = false;
            gdvLoNhap.ColumnCount = 6;
            setColumn(gdvLoNhap.Columns[0]
                , nameof(SachManager.Properties.MaSoSach)
                , SachManager.Properties.MaSoSach);
            setColumn(gdvLoNhap.Columns[1]
                , nameof(SachManager.Properties.TenSach)
                , SachManager.Properties.TenSach);
            setColumn(gdvLoNhap.Columns[2]
                , nameof(SachManager.Properties.TenTacGia)
                , SachManager.Properties.TenTacGia);
            setColumn(gdvLoNhap.Columns[3]
                , nameof(SachManager.Properties.NXB)
                , SachManager.Properties.NXB);
            setColumn(gdvLoNhap.Columns[4]
                , nameof(SachManager.Properties.SoLuongNhapTheoThang)
                , SachManager.Properties.SoLuongNhapTheoThang);
            setColumn(gdvLoNhap.Columns[5]
                , nameof(SachManager.Properties.TongTienNhapTheoThang)
                , SachManager.Properties.TongTienNhapTheoThang);

        }

        private void setColumn(DataGridViewColumn column, string propertyName, string name)
        {
            column.Name = propertyName;
            column.DataPropertyName = propertyName;
            column.HeaderText = name;
        }

        private void reloadGridView()
        {
            if (gdvLoNhap.DataSource!= null && _startYear != 0 && _startYear != 0 && _endMonth != 0 && _endYear != 0)
            {
                _tongLuongNhap = 0;
                foreach (DataGridViewRow row in gdvLoNhap.Rows)
                {
                    Sach sach = row.DataBoundItem as Sach;
                    decimal? soLuongNhap = sach.tongSoLuongNhapTheoThang(_startMonth, _startYear, _endMonth, _endYear);
                    decimal? tienNhap = sach.tongTienNhapTheoThang(_startMonth, _startYear, _endMonth, _endYear);
                    gdvLoNhap.Rows[row.Index].Cells[nameof(SachManager.Properties.SoLuongNhapTheoThang)].Value
                        = soLuongNhap.ToString();
                    gdvLoNhap.Rows[row.Index].Cells[nameof(SachManager.Properties.TongTienNhapTheoThang)].Value
                        = tienNhap.ToString();
                    _tongLuongNhap += soLuongNhap;
                }
                lbTongLuongNhap.Text = _tongLuongNhap.ToString();
            }
                
        }
        private string money(decimal m)
        {
            return String.Format(_cultureInfo, "{0:c}", m);
        }
        #endregion

        
    }
}

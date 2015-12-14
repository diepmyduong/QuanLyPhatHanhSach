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
    public partial class frmThongKeLoXuat : Form
    {
        public frmThongKeLoXuat(Form parent)
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
        private decimal? _tongLuongXuat;
        private CultureInfo _cultureInfo;
        #endregion

        #region Form Control Listener
        private void frmThongKeLoXuat_Load(object sender, EventArgs e)
        {
            _cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
            createGridViewColumns();
            _startMonth = 1;
            _startYear = DateTime.Now.Year;
            _endMonth = 12;
            _endYear = DateTime.Now.Year;
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
            cmbStartYear.SelectedValue = _startYear;
            cmbEndMonth.SelectedValue = 12;
            cmbEndYear.SelectedValue = _endYear;
            loadSach();
        }

        private void cmbStartMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadSach();
            reloadGridView();
        }

        private void cmbStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadSach();
            reloadGridView();
        }

        private void cmbEndMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadSach();
            reloadGridView();
        }

        private void cmbEndYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadSach();
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
        #endregion
        public void loadSach()
        {
            _DMSach = SachManager.getAllAlive().Where(s => s.tongSoLuongXuatTheoThang(_startMonth,_startYear,_endMonth,_endYear)  > 0).ToList();

            gdvLoXuat.DataSource = _DMSach;
        }
        private void createGridViewColumns()
        {
            gdvLoXuat.AutoGenerateColumns = false;
            gdvLoXuat.ColumnCount = 6;
            setColumn(gdvLoXuat.Columns[0]
                , nameof(SachManager.Properties.MaSoSach)
                , SachManager.Properties.MaSoSach);
            setColumn(gdvLoXuat.Columns[1]
                , nameof(SachManager.Properties.TenSach)
                , SachManager.Properties.TenSach);
            setColumn(gdvLoXuat.Columns[2]
                , nameof(SachManager.Properties.TenTacGia)
                , SachManager.Properties.TenTacGia);
            setColumn(gdvLoXuat.Columns[3]
                , nameof(SachManager.Properties.NXB)
                , SachManager.Properties.NXB);
            setColumn(gdvLoXuat.Columns[4]
                , nameof(SachManager.Properties.SoLuongXuatTheoThang)
                , SachManager.Properties.SoLuongXuatTheoThang);
            setColumn(gdvLoXuat.Columns[5]
                , nameof(SachManager.Properties.TongTienXuatTheoThang)
                , SachManager.Properties.TongTienXuatTheoThang);

        }

        private void setColumn(DataGridViewColumn column, string propertyName, string name)
        {
            column.Name = propertyName;
            column.DataPropertyName = propertyName;
            column.HeaderText = name;
        }
        private void reloadGridView()
        {
            if (gdvLoXuat.DataSource != null && _startYear != 0 && _startYear != 0 && _endMonth != 0 && _endYear != 0)
            {
                _tongLuongXuat = 0;
                foreach (DataGridViewRow row in gdvLoXuat.Rows)
                {
                    Sach sach = row.DataBoundItem as Sach;
                    decimal? soLuongXuat = sach.tongSoLuongXuatTheoThang(_startMonth, _startYear, _endMonth, _endYear);
                    decimal? tienXuat = sach.tongTienXuatTheoThang(_startMonth, _startYear, _endMonth, _endYear);
                    gdvLoXuat.Rows[row.Index].Cells[nameof(SachManager.Properties.SoLuongXuatTheoThang)].Value
                        = soLuongXuat.ToString();
                    gdvLoXuat.Rows[row.Index].Cells[nameof(SachManager.Properties.TongTienXuatTheoThang)].Value
                        = tienXuat.ToString();
                    _tongLuongXuat += soLuongXuat;
                }
                lbTongLuongXuat.Text = _tongLuongXuat.ToString();
            }

        }
        private string money(decimal m)
        {
            return String.Format(_cultureInfo, "{0:c}", m);
        }

        private void gdvLoXuat_SelectionChanged(object sender, EventArgs e)
        {
            int index = gdvLoXuat.CurrentRow.Index;
            _currentSach = (gdvLoXuat.DataSource as List<Sach>)[index];
        }

        private void gdvLoXuat_DataSourceChanged(object sender, EventArgs e)
        {
            reloadGridView();
        }
    }
}

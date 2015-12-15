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
    public partial class frmCongNoDaiLy : Form
    {
        public frmCongNoDaiLy(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Properties
        private Form _frmParent;
        private DaiLy _currentDaiLy;
        private List<DaiLy> _DMDaiLy;
        private int _startMonth;
        private int _startYear;
        private int _endMonth;
        private int _endYear;
        private decimal? _tongTienNo;
        private decimal? _tongTienXuat;
        private CultureInfo _cultureInfo;
        #endregion

        #region Form Control Listener
        //Khi load form
        private void frmCongNoDaiLy_Load(object sender, EventArgs e)
        {
            createGridViewColumns();
            _startMonth = 1;
            _startYear = DateTime.Now.Year;
            _endMonth = 12;
            _endYear = DateTime.Now.Year;
            _cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
            //Load combobox tháng và năm
            cmbStartMonth.DataSource = new BindingSource(FilterHelper.Monthes, null);
            cmbEndMonth.DataSource = new BindingSource(FilterHelper.Monthes, null);
            cmbStartMonth.DisplayMember = "Key";
            cmbEndMonth.DisplayMember = "Key";
            cmbStartMonth.ValueMember = "Value";
            cmbEndMonth.ValueMember = "Value";
            cmbStartYear.DataSource = new BindingSource(FilterHelper.Years, null);
            cmbEndYear.DataSource = new BindingSource(FilterHelper.Years, null);
            cmbStartYear.DisplayMember = "Key";
            cmbEndYear.DisplayMember = "Key";
            cmbStartYear.ValueMember = "Value";
            cmbEndYear.ValueMember = "Value";

            //Set Tháng năm mặc định
            cmbStartMonth.SelectedValue = _startMonth;
            cmbStartYear.SelectedValue = _startYear;
            cmbEndMonth.SelectedValue = _endMonth;
            cmbEndYear.SelectedValue = _endYear;

            //Set Event Listener
            cmbStartMonth.SelectedIndexChanged += cmbStartMonth_SelectedIndexChanged;
            cmbStartYear.SelectedIndexChanged += cmbStartYear_SelectedIndexChanged;
            cmbEndMonth.SelectedIndexChanged += cmbEndMonth_SelectedIndexChanged;
            cmbEndYear.SelectedIndexChanged += cmbEndYear_SelectedIndexChanged;
            LoadDaiLy();

            //Set textbox goi y search
            txbLoc.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txbLoc.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txbLoc.AutoCompleteCustomSource = null;
        }
        private void LoadDaiLy()
        {
            _DMDaiLy = DaiLyManager.getAllAlive()
                    .Where(dl => dl.tinhTongSoLuongNoTheoThang(_startMonth, _startYear, _endMonth, _endYear) > 0
                            && dl.tongTienNoThang(_startMonth, _startYear, _endMonth, _endYear) > 0
                            && dl.tongTienXuatThang(_startMonth, _startYear, _endMonth, _endYear) > 0).ToList();
                            
            gdvDMCongNo.DataSource = _DMDaiLy;
            _tongTienNo = _DMDaiLy.Sum(dl => dl.TongTienNoThang);
            _tongTienXuat = _DMDaiLy.Sum(dl => dl.TongTienXuatTheoThang);
            lbTongConNo.Text = DataDisplayHelper.displayMoney((decimal)_tongTienNo);
            lbTongTienSach.Text = DataDisplayHelper.displayMoney((decimal)_tongTienXuat);
            lbTongDaThu.Text = DataDisplayHelper.displayMoney((decimal)(_tongTienXuat - _tongTienNo));
        }
        //Khi chọn xem chi tiết CÔng nợ
        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            frmChiTietCongNoDaiLy form = new frmChiTietCongNoDaiLy(this,_currentDaiLy);
            form.ShowDialog(this);
        }
        //Khi chọn thanh toán
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            frmThanhToanDaiLy form = new frmThanhToanDaiLy(this,_currentDaiLy);
            form.ShowDialog(this);
        }
        //Khi chọn lọc
        private void btnLoc_Click(object sender, EventArgs e)
        {
            gdvDMCongNo.DataSource = DaiLyManager.filter(txbLoc.Text, _DMDaiLy);
        }
        //Khi chọn Thoát
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
        //Khi chọn công nợ của 1 đại lý
        private void gdvDMCongNo_SelectionChanged(object sender, EventArgs e)
        {
            int index = ((DataGridView)sender).CurrentRow.Index;
            _currentDaiLy = (((DataGridView)sender).DataSource as List<DaiLy>)[index];
            selectDaiLy(_currentDaiLy);
        }
        //Khi gõ từ khóa lọc
        private void txbLoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 123)
            {
                txbLoc.Text = txbLoc.Text + "{";
                string request = txbLoc.Text;
                var pros = DaiLy.searchKeysTheoDoiNo();
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
                gdvDMCongNo.DataSource = DaiLyManager.filter(txbLoc.Text, _DMDaiLy);
            }
        }
        #endregion

        private void cmbStartMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            LoadDaiLy();
        }

        private void cmbStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            LoadDaiLy();
        }

        private void cmbEndMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            LoadDaiLy();
        }

        private void cmbEndYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            LoadDaiLy();
        }
        public void selectDaiLy(DaiLy daily)
        {
            if (daily != null)
            {
                lbMaSoDaiLy.Text = daily.MaSoDaiLy.ToString();
                lbTenDaiLy.Text = daily.TenDaiLy;
                lbTienSachDaiLy.Text = String.Format(_cultureInfo, "{0:c}", daily.TongTienXuatTheoThang);
                lbConNoDaiLy.Text = String.Format(_cultureInfo, "{0:c}", daily.TongTienNoThang);
                lbDaThuDaiLy.Text = String.Format(_cultureInfo, "{0:c}", daily.TongTienXuatTheoThang - daily.TongTienNoThang);
            }
        }

        private void createGridViewColumns()
        {
            gdvDMCongNo.AutoGenerateColumns = false; // Bỏ auto generate Columns
            gdvDMCongNo.ColumnCount = 6; // Xác định số columns có
            setColumn(gdvDMCongNo.Columns[0]
                , nameof(DaiLyManager.Properties.MaSoDaiLy)
                , DaiLyManager.Properties.MaSoDaiLy);
            setColumn(gdvDMCongNo.Columns[1]
                , nameof(DaiLyManager.Properties.TenDaiLy)
                , DaiLyManager.Properties.TenDaiLy);
            setColumn(gdvDMCongNo.Columns[2]
                , nameof(DaiLyManager.Properties.SoDienThoai)
                , DaiLyManager.Properties.SoDienThoai);
            setColumn(gdvDMCongNo.Columns[3]
                , nameof(DaiLyManager.Properties.DiaChi)
                , DaiLyManager.Properties.DiaChi);
            setColumn(gdvDMCongNo.Columns[4]
                , nameof(DaiLyManager.Properties.TongTienNo)
                , DaiLyManager.Properties.TongTienNo);
            setColumn(gdvDMCongNo.Columns[5]
                , nameof(DaiLyManager.Properties.TongTienNoThang)
                , DaiLyManager.Properties.TongTienNoThang);
            
        }

        private void setColumn(DataGridViewColumn column, string propertyName, string name)
        {
            column.Name = propertyName;
            column.DataPropertyName = propertyName;
            column.HeaderText = name;
        }

        
    }
}

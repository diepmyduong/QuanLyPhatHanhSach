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
    public partial class frmChiTietCongNoDaiLy : Form
    {
        public frmChiTietCongNoDaiLy(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }
        public frmChiTietCongNoDaiLy(Form parent,DaiLy daily)
         : this(parent)
        {
            this._daily = daily;
        }

        #region Private Properties
        private Form _frmParent;
        private DaiLy _daily;
        private CultureInfo _cultureInfo;
        private int _startMonth;
        private int _startYear;
        private int _endMonth;
        private int _endYear;
        private decimal? _tongTienNoThang;
        private decimal? _tongTienXuatThang;
        #endregion

        #region Form Control Listener
        //Khi form load
        private void frmChiTietCongNoDaiLy_Load(object sender, EventArgs e)
       {
            _cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
            lbMaSoDaiLy.Text = _daily.MaSoDaiLy.ToString();
            lbTenDaiLy.Text = _daily.TenDaiLy;
            lbDiaChi.Text = _daily.DiaChi;
            lbSoDienThoai.Text = _daily.SoDienThoai;
            lbTaiKhoan.Text = _daily.SoTaiKhoan;
            lbTongTienNo.Text = String.Format(_cultureInfo, "{0:c}", _daily.TongTienNo);

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
        }
        //Khi chọn thanh toán
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            frmThanhToanDaiLy form = new frmThanhToanDaiLy(this,_daily);
            form.ShowDialog(this);
        }
        //Khi chọn thoát
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
        //Khi chọn tháng bắt đầu
        private void cmbStartMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadChiTietCongNo();
        }
        //Khi chọn năm bắt đầu
        private void cmbStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadChiTietCongNo();
        }
        //Khi chọn tháng kết thúc
        private void cmbEndMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadChiTietCongNo();
        }
        //Khi chọn năm kết thúc
        private void cmbEndYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadChiTietCongNo();
        }
        public void loadChiTietCongNo()
        {
            if (_startYear != 0 && _startYear != 0 && _endMonth != 0 && _endYear != 0)
            {
                gdvChiTiet.DataSource = _daily.congNoTheoThang(_startMonth, _startYear, _endMonth, _endYear);
                gdvChiTiet.Columns[nameof(CongNoDaiLyManager.Properties.MaSoDaiLy)].Visible = false;
                gdvChiTiet.Columns[nameof(CongNoDaiLyManager.Properties.DaiLy)].Visible = false;
                _tongTienNoThang = _daily.tongTienNoThang(_startMonth, _startYear, _endMonth, _endYear);
                _tongTienXuatThang = _daily.tongTienXuatThang(_startMonth, _startYear, _endMonth, _endYear);
                lbConNo.Text = String.Format(_cultureInfo, "{0:c}", _tongTienNoThang);
                lbTienSach.Text = String.Format(_cultureInfo, "{0:c}", _tongTienXuatThang);
                lbDaThu.Text = String.Format(_cultureInfo, "{0:c}", _tongTienXuatThang - _tongTienNoThang);
                gdvChiTiet.Columns[0].Width = 122;
                gdvChiTiet.Columns[1].Width = 122;
                gdvChiTiet.Columns[2].Width = 122;
                gdvChiTiet.Columns[3].Width = 122;
                gdvChiTiet.Columns[4].Width = 130;
                gdvChiTiet.Columns[5].Width = 140;

            }
        }
        private void createGridViewColumns()
        {
            gdvChiTiet.AutoGenerateColumns = false; // Bỏ auto generate Columns
            gdvChiTiet.ColumnCount = 6; // Xác định số columns có
            setColumn(gdvChiTiet.Columns[0]
                , nameof(CongNoDaiLyManager.Properties.MaSoSach)
                , CongNoDaiLyManager.Properties.MaSoDaiLy);
            setColumn(gdvChiTiet.Columns[1]
                , nameof(CongNoDaiLyManager.Properties.Sach)
                , CongNoDaiLyManager.Properties.Sach);
            setColumn(gdvChiTiet.Columns[2]
                , nameof(CongNoDaiLyManager.Properties.SoLuong)
                , CongNoDaiLyManager.Properties.SoLuong);
            setColumn(gdvChiTiet.Columns[3]
                , nameof(CongNoDaiLyManager.Properties.DonGia)
                , CongNoDaiLyManager.Properties.DonGia);
            setColumn(gdvChiTiet.Columns[4]
                , nameof(CongNoDaiLyManager.Properties.ThanhTien)
                , CongNoDaiLyManager.Properties.ThanhTien);
            setColumn(gdvChiTiet.Columns[5]
                , nameof(CongNoDaiLyManager.Properties.Thang)
                , CongNoDaiLyManager.Properties.Thang);
            gdvChiTiet.Columns[0].Width = 122;
            gdvChiTiet.Columns[1].Width = 122;
            gdvChiTiet.Columns[2].Width = 122;
            gdvChiTiet.Columns[3].Width = 122;
            gdvChiTiet.Columns[4].Width = 122;
            gdvChiTiet.Columns[5].Width = 122;

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

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
    public partial class frmCongNoNXB : Form
    {
        public frmCongNoNXB(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Properties
        private Form _frmParent;
        private NhaXuatBan _currentNXB;
        private List<NhaXuatBan> _DMNXB;
        private int _startMonth;
        private int _startYear;
        private int _endMonth;
        private int _endYear;
        private decimal? _tongTienNo;
        private decimal? _tongTienNhap;
        private CultureInfo _cultureInfo;
        #endregion

        #region Form Control Listener
        //Khi load form
        private void frmCongNoNXB_Load(object sender, EventArgs e)
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
            cmbStartMonth.ValueMember =  "Value";
            cmbEndMonth.ValueMember = "Value";
            cmbStartYear.DataSource = new BindingSource(FilterHelper.Years, null);
            cmbEndYear.DataSource = new BindingSource(FilterHelper.Years, null);
            cmbStartYear.DisplayMember ="Key";
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
            loadNXB();

            //Set textbox goi y search
            txbLoc.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txbLoc.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txbLoc.AutoCompleteCustomSource = null;
        }
        //Khi chọn xem chi tiết
        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            frmChiTietCongNoNXB form = new frmChiTietCongNoNXB(this, _currentNXB);
            form.ShowDialog(this);
        }
        //Khi chọn thanh toán
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            frmThanhToanNXB form = new frmThanhToanNXB(this, _currentNXB);
            form.ShowDialog(this);
        }
        //Khi chọn nút lọc
        private void btnLoc_Click(object sender, EventArgs e)
        {

        }
        //Khi chọn nút thoát
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
        //Khi chọn thắng bắt đầu
        private void cmbStartMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadNXB();
        }
        //Khi chọn năm bắt đầu
        private void cmbStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadNXB();
        }
        //Khi chọn tháng kết thúc
        private void cmbEndMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadNXB();
        }
        //Khi chọn năm kết thúc
        private void cmbEndYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadNXB();
        }
        //Khi nhập từ khóa lọc
        private void txbLoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 123)
            {
                txbLoc.Text = txbLoc.Text + "{";
                string request = txbLoc.Text;
                var pros = NhaXuatBan.searchKeysTheoDoiNo();
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
        private void gdvDMNXB_SelectionChanged(object sender, EventArgs e)
        {
            int index = ((DataGridView)sender).CurrentRow.Index;
            _currentNXB = (((DataGridView)sender).DataSource as List<NhaXuatBan>)[index];
            selectNXB(_currentNXB);
        }
        #endregion

        #region Form Services
        public void loadNXB()
        {
            _DMNXB = NhaXuatBanManager.getAllAlive()
                        .Where(nxb => nxb.tongTienNoThang(_startMonth, _startYear, _endMonth, _endYear) > 0
                        && nxb.tinhTongSoLuongNoTheoThang(_startMonth, _startYear, _endMonth, _endYear) > 0
                        && nxb.tongTienNhapThang(_startMonth, _startYear, _endMonth, _endYear) > 0).ToList();
            gdvDMNXB.DataSource = _DMNXB;

            _tongTienNo = _DMNXB.Sum(nxb => nxb.TongTienNoThang);
            _tongTienNhap = _DMNXB.Sum(nxb => nxb.TongTienNhapTheoThang);
            lbTongConNo.Text = String.Format(_cultureInfo, "{0:c}", _tongTienNo);
            lbTongTienSach.Text = String.Format(_cultureInfo, "{0:c}", _tongTienNhap);
            lbTongDaChi.Text = String.Format(_cultureInfo, "{0:c}", _tongTienNhap - _tongTienNo);
        }
        public void loadNXB(List<NhaXuatBan> DMNXB)
        {
            _DMNXB = DMNXB.Where(nxb => nxb.tongTienNoThang(_startMonth, _startYear, _endMonth, _endYear) > 0
                        && nxb.tinhTongSoLuongNoTheoThang(_startMonth, _startYear, _endMonth, _endYear) > 0
                        && nxb.tongTienNhapThang(_startMonth, _startYear, _endMonth, _endYear) > 0).ToList();
            gdvDMNXB.DataSource = _DMNXB;

            _tongTienNo = _DMNXB.Sum(nxb => nxb.TongTienNoThang);
            _tongTienNhap = _DMNXB.Sum(nxb => nxb.TongTienNhapTheoThang);
            lbTongConNo.Text = String.Format(_cultureInfo, "{0:c}", _tongTienNo);
            lbTongTienSach.Text = String.Format(_cultureInfo, "{0:c}", _tongTienNhap);
            lbTongDaChi.Text = String.Format(_cultureInfo, "{0:c}", _tongTienNhap - _tongTienNo);
        }
        public void selectNXB(NhaXuatBan nxb)
        {
            if(nxb != null)
            {
                lbMaSoNXB.Text = nxb.MaSoNXB.ToString();
                lbTenNXB.Text = nxb.TenNXB;
                lbTienSachNXB.Text = String.Format(_cultureInfo, "{0:c}", nxb.TongTienNhapTheoThang);
                lbConNoNXB.Text = String.Format(_cultureInfo, "{0:c}", nxb.TongTienNoThang);
                lbDaChiNXB.Text = String.Format(_cultureInfo, "{0:c}", nxb.TongTienNhapTheoThang - nxb.TongTienNoThang);
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
                , nameof(NhaXuatBanManager.Properties.TongTienNo)
                , NhaXuatBanManager.Properties.TongTienNo);
            setColumn(gdvDMNXB.Columns[5]
                , nameof(NhaXuatBanManager.Properties.TongTienNoThang)
                , NhaXuatBanManager.Properties.TongTienNoThang);

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

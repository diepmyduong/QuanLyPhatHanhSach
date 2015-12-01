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
            _cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
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
            //Thêm cột Tổng tiền nợ
            DataGridViewColumn colTongTienNo = new DataGridViewColumn();
            colTongTienNo.Name = nameof(NhaXuatBanManager.Properties.TongTienNoThang);
            colTongTienNo.HeaderText = NhaXuatBanManager.Properties.TongTienNoThang;
            colTongTienNo.CellTemplate = new DataGridViewTextBoxCell();
            gdvDMNXB.Columns.Add(colTongTienNo);
            loadNXB();
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
            this.Close();
        }
        //Khi chọn thắng bắt đầu
        private void cmbStartMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            gdvDMNXB.DataSource = null;
            gdvDMNXB.DataSource = _DMNXB;
        }
        //Khi chọn năm bắt đầu
        private void cmbStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            gdvDMNXB.DataSource = null;
            gdvDMNXB.DataSource = _DMNXB;
        }
        //Khi chọn tháng kết thúc
        private void cmbEndMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            gdvDMNXB.DataSource = null;
            gdvDMNXB.DataSource = _DMNXB;
        }
        //Khi chọn năm kết thúc
        private void cmbEndYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            gdvDMNXB.DataSource = null;
            gdvDMNXB.DataSource = _DMNXB;
        }
        //Khi nhập từ khóa lọc
        private void txbLoc_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void gdvDMNXB_SelectionChanged(object sender, EventArgs e)
        {
            int index = ((DataGridView)sender).CurrentRow.Index;
            _currentNXB = (((DataGridView)sender).DataSource as List<NhaXuatBan>)[index];
            selectNXB(_currentNXB);
        }
        private void gdvDMNXB_DataSourceChanged(object sender, EventArgs e)
        {
            _tongTienNo = 0;
            _tongTienNhap = 0;
            foreach (DataGridViewRow row in ((DataGridView)sender).Rows)
            {
                DataGridViewCell cellTongTienNo = row.Cells[nameof(NhaXuatBanManager.Properties.TongTienNoThang)];
                var nxb = (((DataGridView)sender).DataSource as List<NhaXuatBan>)[row.Index];
                decimal? tienNoThang = nxb.tongTienNoThang(_startMonth, _startYear, _endMonth, _endYear);
                cellTongTienNo.Value = tienNoThang;
                decimal? tienNhapThang = nxb.tongTienNhapThang(_startMonth, _startYear, _endMonth, _endYear);
                _tongTienNo += tienNoThang;
                _tongTienNhap += tienNhapThang;
            }
            gdvDMNXB.Columns[nameof(NhaXuatBanManager.Properties.TongTienNoThang)].DisplayIndex = gdvDMNXB.Columns.Count - 1;
            lbTongConNo.Text = String.Format(_cultureInfo, "{0:c}", _tongTienNo);
            lbTongTienSach.Text = String.Format(_cultureInfo, "{0:c}", _tongTienNhap);
            lbTongDaChi.Text = String.Format(_cultureInfo, "{0:c}", _tongTienNhap - _tongTienNo);
        }
        #endregion

        #region Form Services
        public void loadNXB()
        {
            _DMNXB = NhaXuatBanManager.getAll();
            gdvDMNXB.DataSource = _DMNXB;
        }
        public void selectNXB(NhaXuatBan nxb)
        {
            if(nxb != null)
            {
                decimal? tienNhapThang = nxb.tongTienNhapThang(_startMonth, _startYear, _endMonth, _endYear);
                decimal? tienNoThang = nxb.tongTienNoThang(_startMonth, _startYear, _endMonth, _endYear);
                lbMaSoNXB.Text = nxb.MaSoNXB.ToString();
                lbTenNXB.Text = nxb.TenNXB;
                lbTienSachNXB.Text = String.Format(_cultureInfo, "{0:c}", tienNhapThang);
                lbConNoNXB.Text = String.Format(_cultureInfo, "{0:c}", tienNoThang);
                lbDaChiNXB.Text = String.Format(_cultureInfo, "{0:c}", tienNhapThang - tienNoThang);


            }
        }
        #endregion

        
    }
}

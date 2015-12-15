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
    public partial class frmChiTietCongNoNXB : Form
    {
        public frmChiTietCongNoNXB(Form parent)
        {
            InitializeComponent();
            this._frmParent = parent;
        }

        public frmChiTietCongNoNXB(Form parent,NhaXuatBan nxb)
            :this(parent)
        {
            this._nxb = nxb;
        }

        #region Private Properties
        private Form _frmParent;
        private NhaXuatBan _nxb;
        private CultureInfo _cultureInfo;
        private int _startMonth;
        private int _startYear;
        private int _endMonth;
        private int _endYear;
        private decimal? _tongTienNoThang;
        private decimal? _tongTienNhapThang;
        #endregion

        #region Form Control Listener
        //Khi load Form
        private void frmChiTietCongNoNXB_Load(object sender, EventArgs e)
        {
            _cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
            lbMaSoNXB.Text = _nxb.MaSoNXB.ToString();
            lbTenNXB.Text = _nxb.TenNXB;
            lbDiaChi.Text = _nxb.DiaChi;
            lbSoDienThoai.Text = _nxb.SoDienThoai;
            lbTaiKhoan.Text = _nxb.SoTaiKhoan;
            lbTongTienNo.Text = String.Format(_cultureInfo, "{0:c}",_nxb.TongTienNo);

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
            frmThanhToanNXB form = new frmThanhToanNXB(this, _nxb);
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
        #endregion

        #region Form Services
        public void loadChiTietCongNo()
        {
            if (_startYear != 0 && _startYear != 0 && _endMonth != 0 && _endYear != 0)
            {
                gdvChiTiet.DataSource = _nxb.congNoTheoThang(_startMonth, _startYear, _endMonth, _endYear);
                gdvChiTiet.Columns[nameof(CongNoNXBManager.Properties.MaSoNXB)].Visible = false;
                gdvChiTiet.Columns[nameof(CongNoNXBManager.Properties.NXB)].Visible = false;
                _tongTienNoThang = _nxb.tongTienNoThang(_startMonth, _startYear, _endMonth, _endYear);
                _tongTienNhapThang = _nxb.tongTienNhapThang(_startMonth, _startYear, _endMonth, _endYear);
                lbConNo.Text = String.Format(_cultureInfo, "{0:c}", _tongTienNoThang);
                lbTienSach.Text = String.Format(_cultureInfo, "{0:c}", _tongTienNhapThang);
                lbDaChi.Text = String.Format(_cultureInfo, "{0:c}", _tongTienNhapThang - _tongTienNoThang);

            }
        }
        #endregion

    }
}

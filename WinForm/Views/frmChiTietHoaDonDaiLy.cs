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
    public partial class frmChiTietHoaDonDaiLy : Form
    {
        public frmChiTietHoaDonDaiLy(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }
        public frmChiTietHoaDonDaiLy(Form parent, HoaDonDaiLy hd)
            : this(parent)
        {
            _CurrentHD = hd;
        }

        #region Private Properties
        private Form _frmParent;
        private HoaDonDaiLy _CurrentHD;
        #endregion

        #region Form Control Listener
        private void frmChiTietHoaDonDaiLy_Load(object sender, EventArgs e)
        {
            createGridViewColumns();
            if (_CurrentHD != null)
            {
                gdvChiTiet.DataSource = _CurrentHD.ChiTiet;
                lbMaSoHoaDon.Text = _CurrentHD.MaSoHoaDon + "";
                lbMaSoDaiLy.Text = _CurrentHD.MaSoDaiLy + "";
                lbTenDaiLy.Text = _CurrentHD.DaiLy.TenDaiLy;
                lbNgayLap.Text = _CurrentHD.NgayLap.ToString();
                lbTongTien.Text = _CurrentHD.TongTien.ToString();
            }
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

        private void createGridViewColumns()
        {
            gdvChiTiet.AutoGenerateColumns = false; // Bỏ auto generate Columns
            gdvChiTiet.ColumnCount = 5; // Xác định số columns có
            setColumn(gdvChiTiet.Columns[0]
                , nameof(HoaDonDaiLyManager.ChiTiet.Properties.MaSoSach)
                , HoaDonDaiLyManager.ChiTiet.Properties.MaSoSach);
            setColumn(gdvChiTiet.Columns[1]
                , nameof(HoaDonDaiLyManager.ChiTiet.Properties.Sach)
                , HoaDonDaiLyManager.ChiTiet.Properties.Sach);
            setColumn(gdvChiTiet.Columns[2]
                , nameof(HoaDonDaiLyManager.ChiTiet.Properties.SoLuong)
                , HoaDonDaiLyManager.ChiTiet.Properties.SoLuong);
            setColumn(gdvChiTiet.Columns[3]
                , nameof(HoaDonDaiLyManager.ChiTiet.Properties.DonGia)
                , HoaDonDaiLyManager.ChiTiet.Properties.DonGia);
            setColumn(gdvChiTiet.Columns[4]
                , nameof(HoaDonDaiLyManager.ChiTiet.Properties.ThanhTien)
                , HoaDonDaiLyManager.ChiTiet.Properties.ThanhTien);
            gdvChiTiet.Columns[0].Width = 150;
            gdvChiTiet.Columns[1].Width = 150;
            gdvChiTiet.Columns[2].Width = 150;
            gdvChiTiet.Columns[3].Width = 150;
            gdvChiTiet.Columns[4].Width = 150;

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

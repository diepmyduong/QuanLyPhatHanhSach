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
    public partial class frmDanhMucHoaDonDaiLy : Form
    {
        public frmDanhMucHoaDonDaiLy()
        {
            InitializeComponent();
        }
        private List<DaiLy> _DMDaiLy;
        private HoaDonDaiLy _CurrentHD;

        private void LoadDaiLy()
        {
            _DMDaiLy = DaiLyManager.getAll();
            cmbDaiLy.DataSource = _DMDaiLy;
            cmbDaiLy.DisplayMember = nameof(DaiLyManager.Properties.TenDaiLy);
            cmbDaiLy.ValueMember = nameof(DaiLyManager.Properties.MaSoDaiLy);
        }
        private void frmDanhMucHoaDonDaiLy_Load(object sender, EventArgs e)
        {
            createGridViewColumns();
            LoadDaiLy();
            btDuyet.Enabled = false;
            btXoa.Enabled = false;
        }

        private void btChuaDuyet_Click(object sender, EventArgs e)
        {
            gdvHoaDon.DataSource = HoaDonDaiLyManager.getUnaproved();
        }

        private void btLoadAll_Click(object sender, EventArgs e)
        {
            gdvHoaDon.DataSource = HoaDonDaiLyManager.getAll();
        }

        private void gdvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gdvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            int index = ((DataGridView)sender).CurrentRow.Index;
            _CurrentHD = (((DataGridView)sender).DataSource as List<HoaDonDaiLy>)[index];
            select(_CurrentHD);
        }
        public void select(HoaDonDaiLy hd)
        {
            if (hd != null)
            {
                txbMaHD.Text = hd.MaSoHoaDon + "";
                cmbDaiLy.SelectedValue = hd.MaSoDaiLy;
                string day = hd.NgayLap.ToString();
                DateTime thedate = DateTime.Parse(day);
                String dateString = thedate.ToString("yyyy/MM/dd");
                day = dateString;

                char[] cut = day.ToCharArray();
                string nam = "";
                for (int i = 0; i < 4; i++)
                {
                    nam += cut[i];
                }
                string thang = "";
                for (int i = 5; i < 7; i++)
                {
                    thang += cut[i];
                }
                string ngay = "";
                for (int i = 8; i < 10; i++)
                {
                    ngay += cut[i];
                }
                dtpNgay.Value = new DateTime(int.Parse(nam), int.Parse(thang), int.Parse(ngay));
                int trangthai = (int)hd.TrangThai;
                if (trangthai == 1)
                {
                    btDuyet.Text = "Đã duyệt";
                    btDuyet.Enabled = false;
                    btXoa.Enabled = false;

                }
                if (trangthai == 0)
                {
                    btDuyet.Text = "Duyệt hóa đơn";
                    btDuyet.Enabled = true;
                    btXoa.Enabled = true;
                }
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

        private void btXoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (HoaDonDaiLyManager.delete(_CurrentHD.MaSoHoaDon))
                    MessageBox.Show("Đã xóa thành công hóa đơn");
                else
                    MessageBox.Show("Không xóa được");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
          
        }

        private void btDuyet_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn duyệt hóa đơn", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (_CurrentHD != null && _CurrentHD.TrangThai == 0)
                {
                    if (_CurrentHD.accept())
                    {
                        MessageBox.Show("Duyệt hóa đơn thành công");
                        gdvHoaDon.DataSource = HoaDonDaiLyManager.getAll();
                    }
                    else
                        MessageBox.Show("Duyệt không thành công, vui lòng kiểm tra lại Công nợ");
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
          
        }

        private void btXemCT_Click(object sender, EventArgs e)
        {
            frmChiTietHoaDonDaiLy fr = new frmChiTietHoaDonDaiLy(this, _CurrentHD);
            fr.Show();
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }
        private void createGridViewColumns()
        {
            gdvHoaDon.AutoGenerateColumns = false; // Bỏ auto generate Columns
            gdvHoaDon.ColumnCount = 6; // Xác định số columns có
            setColumn(gdvHoaDon.Columns[0]
                , nameof(HoaDonDaiLyManager.Properties.MaSoHoaDon)
                , HoaDonDaiLyManager.Properties.MaSoHoaDon);
            setColumn(gdvHoaDon.Columns[1]
                , nameof(HoaDonDaiLyManager.Properties.MaSoDaiLy)
                , HoaDonDaiLyManager.Properties.MaSoDaiLy);
            setColumn(gdvHoaDon.Columns[2]
                , nameof(HoaDonDaiLyManager.Properties.DaiLy)
                , HoaDonDaiLyManager.Properties.DaiLy);
            setColumn(gdvHoaDon.Columns[3]
                , nameof(HoaDonDaiLyManager.Properties.NgayLap)
                , HoaDonDaiLyManager.Properties.NgayLap);
            setColumn(gdvHoaDon.Columns[4]
                , nameof(HoaDonDaiLyManager.Properties.TongTien)
                , HoaDonDaiLyManager.Properties.TongTien);
            setColumn(gdvHoaDon.Columns[5]
                , nameof(HoaDonDaiLyManager.Properties.TrangThai)
                , HoaDonDaiLyManager.Properties.TrangThai);
            gdvHoaDon.Columns[0].Width = 125;
            gdvHoaDon.Columns[1].Width = 125;
            gdvHoaDon.Columns[2].Width = 125;
            gdvHoaDon.Columns[3].Width = 125;
            gdvHoaDon.Columns[4].Width = 125;
            gdvHoaDon.Columns[5].Width = 100;
        }

        private void setColumn(DataGridViewColumn column, string propertyName, string name)
        {
            column.Name = propertyName;
            column.DataPropertyName = propertyName;
            column.HeaderText = name;
        }
    }
}

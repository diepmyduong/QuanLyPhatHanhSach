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
                    btDuyet.Text = "Duyệt phiếu nhập";
                    btDuyet.Enabled = true;
                    btXoa.Enabled = true;
                }
            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            if (HoaDonDaiLyManager.delete(_CurrentHD.MaSoHoaDon))
                MessageBox.Show("Đã xóa thành công hóa đơn");
            else
                MessageBox.Show("Không xóa được");
        }

        private void btDuyet_Click(object sender, EventArgs e)
        {

            if (_CurrentHD != null && _CurrentHD.TrangThai == 0)
            {
                if (_CurrentHD.accept())
                {
                    MessageBox.Show("Duyệt hóa đơn thành công");
                    gdvHoaDon.DataSource = HoaDonDaiLyManager.getAll();
                }
                else
                    MessageBox.Show("Duyệt không thành công");
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
    }
}

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
    public partial class frmDanhMucHoaDonNXB : Form
    {
        public frmDanhMucHoaDonNXB()
        {
            InitializeComponent();
        }

        private List<NhaXuatBan> _DMNXB;
        private HoaDonNXB _CurrentHD;

        private void LoadNXB()
        {
            _DMNXB = NhaXuatBanManager.getAll();
            cmbNXB.DataSource = _DMNXB;
            cmbNXB.DisplayMember = nameof(NhaXuatBanManager.Properties.TenNXB);
            cmbNXB.ValueMember = nameof(NhaXuatBanManager.Properties.MaSoNXB);
        }

        private void btLoadAll_Click(object sender, EventArgs e)
        {
            gdvHoaDon.DataSource = HoaDonNXBManager.getAll();
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmDanhMucHoaDonNXB_Load(object sender, EventArgs e)
        {
            LoadNXB();
            btDuyet.Enabled = false;
            btXoa.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btChuaDuyet_Click(object sender, EventArgs e)
        {
            gdvHoaDon.DataSource = HoaDonNXBManager.getUnaproved();
        }

        private void gdvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            int index = ((DataGridView)sender).CurrentRow.Index;
            _CurrentHD = (((DataGridView)sender).DataSource as List<HoaDonNXB>)[index];
            select(_CurrentHD);

        }
        public void select(HoaDonNXB hd)
        {
          if (hd!=null)
            {
                txbMaHD.Text = hd.MaSoHoaDon + "";
                cmbNXB.SelectedValue = hd.MaSoNXB;
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
                int trangthai =(int) hd.TrangThai;
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

        private void btDuyet_Click(object sender, EventArgs e)
        {
            if (_CurrentHD != null && _CurrentHD.TrangThai == 0)
            {
                if (_CurrentHD.accept())
                {
                    MessageBox.Show("Duyệt hóa đơn thành công");
                    gdvHoaDon.DataSource = HoaDonNXBManager.getAll();
                }
                else
                    MessageBox.Show("Duyệt không thành công");
            }
           
        }

        private void btXemCT_Click(object sender, EventArgs e)
        {
            
            frmChiTietHoaDonNXB fr = new frmChiTietHoaDonNXB(this, _CurrentHD);
            fr.Show();
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            if (HoaDonNXBManager.delete(_CurrentHD.MaSoHoaDon))
                MessageBox.Show("Đã xóa thành công");
            else
                MessageBox.Show("Không xóa được");
        }
    }
}

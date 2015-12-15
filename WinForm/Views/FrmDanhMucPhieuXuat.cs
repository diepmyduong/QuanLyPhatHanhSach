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
    public partial class FrmDanhMucPhieuXuat : Form
    {
        public FrmDanhMucPhieuXuat()
        {
            InitializeComponent();
        }
        private List<PhieuXuat> _DSPX;
        private List<DaiLy> _DSDLy;
        private void LoadPX()
        {
            _DSPX = PhieuXuatManager.getAll();
            gdvXuat.DataSource = _DSPX;

        }
        private void LoadDaiLy()
        {
            _DSDLy = DaiLyManager.getAll();
            cmbDaiLy.DataSource = _DSDLy;
            cmbDaiLy.DisplayMember = "TenDaiLy";
            cmbDaiLy.ValueMember = "MaSoDaiLy";
        }
        private void LoadPXChuaDuyet()
        {
           _DSPX = PhieuXuatManager.getUnaproved();
           gdvXuat.DataSource = _DSPX;
        }
        private void FrmDanhMucPhieuXuat_Load(object sender, EventArgs e)
        {
            LoadDaiLy();
            btDuyet.Enabled = false;
            btXoa.Enabled = false;
        }

        private void btLoadAll_Click(object sender, EventArgs e)
        {
            LoadPX();
        }

        private void btChuaDuyet_Click(object sender, EventArgs e)
        {
            LoadPXChuaDuyet();
        }

        private void gdvXuat_SelectionChanged(object sender, EventArgs e)
        {
            txbMaPhieuXuat.Text = gdvXuat.CurrentRow.Cells[0].Value.ToString();
            cmbDaiLy.SelectedValue = int.Parse(gdvXuat.CurrentRow.Cells[1].Value.ToString());
            txbNguoiNhan.Text = gdvXuat.CurrentRow.Cells[4].Value.ToString();
            string day = gdvXuat.CurrentRow.Cells[3].Value.ToString();
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
            dtpNgayXuat.Value = new DateTime(int.Parse(nam), int.Parse(thang), int.Parse(ngay));
            lbTongTien.Text = gdvXuat.CurrentRow.Cells[5].Value.ToString();
            int trangthai = int.Parse(gdvXuat.CurrentRow.Cells[6].Value.ToString());
            if (trangthai == 1)
            {
                btDuyet.Text = "Đã duyệt";
                btDuyet.Enabled = false;
                btXoa.Enabled = false;

            }
            if (trangthai == 0)
            {
                btDuyet.Text = "Duyệt phiếu xuất";
                btDuyet.Enabled = true;
                btXoa.Enabled = true;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            if (!txbMaPhieuXuat.Text.Equals(""))
            {
                PhieuXuat pn = new PhieuXuat();
                int x = int.Parse(txbMaPhieuXuat.Text.ToString());
                var phieu = PhieuXuatManager.find(x);

                if (phieu.TrangThai != 1)
                {
                    if (PhieuXuatManager.delete(phieu.MaSoPhieuXuat))
                    {
                        MessageBox.Show("Đã xóa thành công");
                        LoadPX();
                    }
                    else
                        MessageBox.Show("Không xóa được");
                }
                else
                    MessageBox.Show("...");
            }
            else
                MessageBox.Show("Chọn phiếu xuất cần xóa");
        }

        private void btDuyet_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn duyệt phiếu này", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (!txbMaPhieuXuat.Text.Equals(""))
                {
                   
                   
     
                    int maso = int.Parse(txbMaPhieuXuat.Text.ToString());
                    var phieu = PhieuXuatManager.find(maso);
                    if (phieu != null)
                    {
                        var result = phieu.accept();
                        switch (result)
                        {
                            case PhieuXuat.AcceptStatus.Success:
                                MessageBox.Show("Đã duyệt thành công");
                                LoadPX();
                                return;
                            case PhieuXuat.AcceptStatus.Error:
                                MessageBox.Show("Sách tồn không đủ để duyệt! Phiếu xuất yêu cầu được hủy!");
                                return;
                            case PhieuXuat.AcceptStatus.Limited:
                                MessageBox.Show("Tiền nợ đã vượt quá mức cho phép, vui lòng thanh toán trước khi đặt tiếp");
                                return;
                            default:
                                MessageBox.Show("Duyệt không thành công");
                                return;
                        }
                    }

                }
                else
                    MessageBox.Show("Chưa chọn phiếu xuất cần duyệt");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
          
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gdvXuat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

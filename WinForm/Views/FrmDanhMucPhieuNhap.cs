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
    public partial class FrmDanhMucPhieuNhap : Form
    {
        public FrmDanhMucPhieuNhap()
        {
            InitializeComponent();
        }
        private List<PhieuNhap> __DSPN;
        private List<NhaXuatBan> _DSNXB;
        private void LoadDSPM()
        {
            __DSPN = PhieuNhapManager.getAll();
            gdvPhieuNhap.DataSource = __DSPN;
        }
        private void loadDSChuaDuyet()
        {
            __DSPN = PhieuNhapManager.getUnaproved();
            gdvPhieuNhap.DataSource = __DSPN;
        }
        private void LoadDSNXB()
        {
            _DSNXB = NhaXuatBanManager.getAll();
            cmbNhaXuatBan.DataSource = _DSNXB;
            cmbNhaXuatBan.DisplayMember = "TenNXB";
            cmbNhaXuatBan.ValueMember = "MaSoNXB";
        }
        private void btLoadAll_Click(object sender, EventArgs e)
        {
            LoadDSPM();
        }

        private void gdvPhieuNhap_SelectionChanged(object sender, EventArgs e)
        {
            txbMaPhieuNhap.Text = gdvPhieuNhap.CurrentRow.Cells[0].Value.ToString();
            cmbNhaXuatBan.SelectedValue = int.Parse(gdvPhieuNhap.CurrentRow.Cells[1].Value.ToString());
            txbNguoiGiao.Text = gdvPhieuNhap.CurrentRow.Cells[4].Value.ToString();
            string day = gdvPhieuNhap.CurrentRow.Cells[3].Value.ToString();
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
            dtpNgayLap.Value = new DateTime(int.Parse(nam), int.Parse(thang), int.Parse(ngay));
            lbTongTien.Text = gdvPhieuNhap.CurrentRow.Cells[5].Value.ToString();
            int trangthai = int.Parse(gdvPhieuNhap.CurrentRow.Cells[6].Value.ToString());
            if (trangthai == 1)
            {
                btDuyet.Text = "Đã duyệt";
                btDuyet.Enabled = false;
                btXoa.Enabled = false;
                
            }
            if (trangthai ==0)
            {
                btDuyet.Text = "Duyệt phiếu nhập";
                btDuyet.Enabled = true;
                btXoa.Enabled = true;
            }

        }

        private void FrmDanhMucPhieuNhap_Load(object sender, EventArgs e)
        {
            LoadDSNXB();
            btDuyet.Enabled = false;
            btXoa.Enabled = false;
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            ///  List<ChiTietPhieuNhap> _DSCTPN = PhieuNhapManager.ChiTiet.getAll();
            /// kiểm trạng trạng thái
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (!txbMaPhieuNhap.Text.Equals(""))
                {
                    PhieuNhap pn = new PhieuNhap();
                    int x = int.Parse(txbMaPhieuNhap.Text.ToString());
                    var phieu = PhieuNhapManager.find(x);

                    if (phieu.TrangThai != 1)
                    {
                        if (PhieuNhapManager.delete(phieu.MaSoPhieuNhap))
                        {
                            MessageBox.Show("Đã xóa thành công");
                            LoadDSPM();
                        }
                        else
                            MessageBox.Show("Không xóa được");
                    }
                    else
                        MessageBox.Show("Không tìm thấy");
                }
                else
                    MessageBox.Show("Chọn phiếu nhập cần xóa");

            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
          
        }

        private void btChuaDuyet_Click(object sender, EventArgs e)
        {
            loadDSChuaDuyet();
        }

        private void btDuyet_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Bạn có muốn duyệt phiếu nhập", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (!txbMaPhieuNhap.Text.Equals(""))
                {

                    int ma = int.Parse(txbMaPhieuNhap.Text.ToString());
                    var Phieu = PhieuNhapManager.find(ma);

                    // thêm kiểm tra trạng thái phiếu nhập
                    if (Phieu != null)
                    {
                        if (Phieu.accept())
                        {
                            MessageBox.Show("Đã duyệt thành công");
                            LoadDSPM();
                        }
                        else
                            MessageBox.Show("Chưa duyệt được");
                    }
                    else
                        MessageBox.Show("Không tìm thấy phiếu nhập");


                }
                else
                    MessageBox.Show("Chưa chọn phiếu nhập cần duyệt");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
       
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            frmLapPhieuNhap fr = new frmLapPhieuNhap(this);
            fr.ShowDialog(this);
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

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
    }
}

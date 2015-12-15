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
    public partial class frmLapPhieuXuat : Form
    {
        public frmLapPhieuXuat(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }
        #region Private Properties
        private Form _frmParent;
        private List<DaiLy> _DMDAILY;
        private List<Sach> _DMSach;
        private PhieuXuat _PhieuXuat;
        #endregion

        #region Form Control Listener
        //Khi form load
        private void frmLapPhieuXuat_Load(object sender, EventArgs e)
        {
            loadDaiLy();
            loadSach();
        }
        //Khi chọn thêm mới đại lý
        private void btnThemDaiLy_Click(object sender, EventArgs e)
        {
            frmDanhMucDaiLy form = new frmDanhMucDaiLy(this);
            form.ShowDialog(this);
        }
        //Khi chọn lưu lại đơn xuất
        private void btnLuu_Click(object sender, EventArgs e)
        {
            //decimal tongtien = 0;
            //List<ChiTietPhieuXuat> list = new List<ChiTietPhieuXuat>();
            //if (!txbNguoiNhan.Text.Equals(""))
            //{
            //    PhieuXuat px = new PhieuXuat();
            //    px.MaSoDaiLy = int.Parse(cmbDaiLy.SelectedValue.ToString());
            //    px.NgayLap = DateTime.Parse(dtpNgayLap.Value.ToString("yyyy-MM-dd"));
            //    px.NguoiNhan = txbNguoiNhan.Text.ToString();

            //    for (int i = 0; i < gdvChiTiet.RowCount - 1; i++)
            //    {
            //        if (!String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[i].Cells[0].Value)) && !String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[i].Cells[1].Value)) && !String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[i].Cells[2].Value)))
            //        {
            //            ChiTietPhieuXuat ctpx = new ChiTietPhieuXuat();
            //            ctpx.MaSoSach = int.Parse(gdvChiTiet.Rows[i].Cells[0].Value.ToString());
            //            ctpx.SoLuong = int.Parse(gdvChiTiet.Rows[i].Cells[1].Value.ToString());
            //            ctpx.DonGia = int.Parse(gdvChiTiet.Rows[i].Cells[2].Value.ToString());
            //            tongtien = tongtien + ctpx.SoLuong * ctpx.DonGia;
            //            list.Add(ctpx);
            //        }
            //        else
            //        {
            //            if (String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[i].Cells[0].Value)) || String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[i].Cells[1].Value)) || String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[i].Cells[2].Value)))
            //            {
            //                MessageBox.Show("Chưa nhập đủ thông tin vào bảng");
            //                return;
            //            }
            //        }

            //    }
            //    px.TongTien = tongtien;
            //    if (px.AddInvoiceAndDetail(list))
            //        MessageBox.Show("đã thêm thành công");
            //    else
            //        MessageBox.Show("Không thêm được");

            //}
            //else
            //    MessageBox.Show("Bạn chưa nhập người nhận hàng");
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
        public void loadDaiLy()
        {
            _DMDAILY = DaiLyManager.getAll();
            cmbDaiLy.DataSource = _DMDAILY;
            cmbDaiLy.DisplayMember = nameof(DaiLyManager.Properties.TenDaiLy);
            cmbDaiLy.ValueMember = nameof(DaiLyManager.Properties.MaSoDaiLy);
        }

        public void loadSach()
        {
            _DMSach = SachManager.getAll();
        }
        #endregion

        private void gdvChiTiet_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var currentRow = (sender as DataGridView).CurrentRow;
            var cellTenSach = (DataGridViewComboBoxCell)currentRow.Cells[nameof(SachManager.Properties.TenSach)];
            cellTenSach.DataSource = _DMSach;
            cellTenSach.DisplayMember = nameof(SachManager.Properties.TenSach);
            cellTenSach.ValueMember = nameof(SachManager.Properties.MaSoSach);
            ComboBox cmbBx = e.Control as ComboBox;
            if (cmbBx != null)
            {
                e.CellStyle.BackColor = gdvChiTiet.DefaultCellStyle.BackColor;
            }
        }

        private void gdvChiTiet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var a = gdvChiTiet.Columns[e.ColumnIndex].Name;
        }

        private int Tongtien(int rows)
        {
            int tongtien = 0;
            int tongtienrow = 0;
            for (int row = 0; row <= rows; row++)
            {
                int Soluong = int.Parse(gdvChiTiet.Rows[row].Cells[1].Value.ToString());
                int DonGia = int.Parse(gdvChiTiet.Rows[row].Cells[2].Value.ToString());

                tongtien = tongtien + Soluong * DonGia;
                tongtienrow = Soluong * DonGia;
                gdvChiTiet.Rows[row].Cells[3].Value = tongtienrow;
            }
            return tongtien;
        }

        private void gdvChiTiet_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            if (dong != 0)
            {
                if (!TenSach.Equals("") && !SoLuong.Equals("") && !DonGia.Equals(""))
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[dong - 1].Cells[1].Value)) && !String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[dong - 1].Cells[2].Value)))
                    {
                        lbTongTien.Text = Tongtien(dong - 1) + "";
                    }
                    else
                        MessageBox.Show("số lượng và đơn giá không được để trống");
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin trên dòng");
                }

            }
        }

        private void gdvChiTiet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

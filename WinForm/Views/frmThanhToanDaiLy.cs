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
using System.Text.RegularExpressions;

namespace WinForm.Views
{
    public partial class frmThanhToanDaiLy : Form
    {
        public frmThanhToanDaiLy(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }
        public frmThanhToanDaiLy(Form parent, DaiLy daily)
            : this(parent)
        {
            _currentDaiLy = daily;
        }
        #region Private Properties
        private Form _frmParent;
        private DaiLy _currentDaiLy;
        private List<DaiLy> _DMDaiLy;
        private List<Sach> _DMSachNXB;
        #endregion

        #region Form Control Listener
        //Khi form load
        private void frmThanhToanDaiLy_Load(object sender, EventArgs e)
        {
            loadDaiLy();
            int parentDaily = -1;
            if (_currentDaiLy != null)
            {
                parentDaily = _currentDaiLy.MaSoDaiLy;
                cmbDaiLy.SelectedIndex = _DMDaiLy.FindIndex(n => n.MaSoDaiLy == parentDaily);
                txbMaSoDaiLy.Text = parentDaily + "";
                loadSach();
                cmbDaiLy.Enabled = false;
            }
            else
            {
                cmbDaiLy.Enabled = true;
            }
        }
        #region Form Services
        public void loadDaiLy()
        {
            _DMDaiLy = DaiLyManager.getAll();
            cmbDaiLy.DataSource = _DMDaiLy;
            cmbDaiLy.DisplayMember = nameof(DaiLyManager.Properties.TenDaiLy);
            cmbDaiLy.ValueMember = nameof(DaiLyManager.Properties.MaSoDaiLy);
        }
        public void loadSach()
        {
            _DMSachNXB = _currentDaiLy.getSachNo();
        }
        #endregion
        //Khi chọn đại lý
        private int Tongtien(int rows)
        {
            int tongtien = 0;
            int tongtienrow = 0;
            for (int row = 0; row <= rows; row++)
            {
                if (CheckNumber(gdvChiTiet.Rows[row].Cells[1].Value.ToString()))
                {
                    int Soluong = int.Parse(gdvChiTiet.Rows[row].Cells[1].Value.ToString());
                    int DonGia = int.Parse(gdvChiTiet.Rows[row].Cells[2].Value.ToString());

                    tongtien = tongtien + Soluong * DonGia;
                    tongtienrow = Soluong * DonGia;
                    gdvChiTiet.Rows[row].Cells[3].Value = tongtienrow;
                }
                else
                {
                    MessageBox.Show("Số lượng phải nhập số");
                    return 0;
                }
            }
            return tongtien;
        }
        private bool CheckNumber(string input)
        {
            return Regex.IsMatch(input, @"^\d+$");
        }
        private void cmbDaiLy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //khi Lưu lại hóa đơn
        private void btnLuu_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn lưu", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                List<ChiTietHoaDonDaiLy> ListChiTiet = new List<ChiTietHoaDonDaiLy>();
                HoaDonDaiLy hd = new HoaDonDaiLy();
                hd.MaSoDaiLy = int.Parse(txbMaSoDaiLy.Text.ToString());
                hd.NgayLap = DateTime.Parse(dtpNgayLap.Value.ToString("yyyy-MM-dd"));

                for (int i = 0; i < gdvChiTiet.RowCount - 1; i++)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[i].Cells[0].Value)) && !String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[i].Cells[1].Value)) && !String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[i].Cells[2].Value)))
                    {
                        ChiTietHoaDonDaiLy ct = new ChiTietHoaDonDaiLy();
                        ct.MaSoSach = int.Parse(gdvChiTiet.Rows[i].Cells[0].Value.ToString());
                        ct.SoLuong = int.Parse(gdvChiTiet.Rows[i].Cells[1].Value.ToString());
                        ct.DonGia = int.Parse(gdvChiTiet.Rows[i].Cells[2].Value.ToString());
                        ListChiTiet.Add(ct);
                    }
                }
                if (ListChiTiet.Count > 1)
                {
                    for (int i = 0; i < ListChiTiet.Count - 1; i++)
                    {
                        for (int j = i + 1; j < ListChiTiet.Count; j++)
                            if (ListChiTiet[i].MaSoSach.Equals(ListChiTiet[j].MaSoSach))
                            {
                                MessageBox.Show("Không nhập trùng sách");
                                return;
                            }
                    }
                }

                hd.ChiTiet = ListChiTiet;

                int x = HoaDonDaiLyManager.add(hd);
                if (x != 0)
                {
                    MessageBox.Show("Đã thêm thành công hóa đơn đại lý");
                    txbMaHoaDon.Text = x + "";
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
          
        
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
        #endregion

        private void gdvChiTiet_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var currentRow = (sender as DataGridView).CurrentRow;
            var cellTenSach = (DataGridViewComboBoxCell)currentRow.Cells[nameof(SachManager.Properties.TenSach)];
            cellTenSach.DataSource = _DMSachNXB;
            cellTenSach.DisplayMember = nameof(SachManager.Properties.TenSach);
            cellTenSach.ValueMember = nameof(SachManager.Properties.MaSoSach);
            ComboBox cmbBx = e.Control as ComboBox;
            if (cmbBx != null)
            {
                e.CellStyle.BackColor = gdvChiTiet.DefaultCellStyle.BackColor;
            }
        }

        private void gdvChiTiet_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            if (dong != 0)
            {
                if (!TenSach.Equals("") && !SoLuong.Equals(""))
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[dong - 1].Cells[1].Value)))
                    {
                        int x = int.Parse(gdvChiTiet.Rows[dong - 1].Cells[0].Value.ToString());
                        Sach s = SachManager.find(x);
                        gdvChiTiet.Rows[dong - 1].Cells[2].Value = s.GiaNhap;
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

        private void cmbDaiLy_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _currentDaiLy = DaiLyManager.find(int.Parse(cmbDaiLy.SelectedValue.ToString()));
            txbMaSoDaiLy.Text = _currentDaiLy.MaSoDaiLy + "";
            loadSach();
            cmbDaiLy.Enabled = false;
        }
    }
}

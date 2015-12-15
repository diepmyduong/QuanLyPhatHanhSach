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
    public partial class frmThanhToanNXB : Form
    {
        public frmThanhToanNXB(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }
        public frmThanhToanNXB(Form parent, NhaXuatBan nxb)
            : this(parent)
        {
            _currentNXB = nxb;
        }
        #region Private Properties
        private Form _frmParent;
        private NhaXuatBan _currentNXB;
        private List<NhaXuatBan> _DMNXB;
        private List<Sach> _DMSachNXB;
        #endregion

        private void frmThanhToanNXB_Load(object sender, EventArgs e)
        {
            loadNXB();
            int parentMaNXB = -1;
            if (_currentNXB != null)
            {
                parentMaNXB = _currentNXB.MaSoNXB;
                cmbNXB.SelectedIndex = _DMNXB.FindIndex(n => n.MaSoNXB == parentMaNXB);
                txbMaSoNXB.Text = parentMaNXB + "";
                cmbNXB.Enabled = true;
                loadSach();
                cmbNXB.Enabled = false;
            }
            else
            {
                cmbNXB.Enabled = true;
            }
        }
       
        #region Form Services
        public void loadNXB()
        {
            _DMNXB = NhaXuatBanManager.getAll();
            cmbNXB.DataSource = _DMNXB;
            cmbNXB.DisplayMember = nameof(NhaXuatBanManager.Properties.TenNXB);
            cmbNXB.ValueMember = nameof(NhaXuatBanManager.Properties.MaSoNXB);
        }
        public void loadSach()
        {
            _DMSachNXB = _currentNXB.Sach;
        }
        #endregion
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

        private void cmbNXB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _currentNXB = NhaXuatBanManager.find(int.Parse(cmbNXB.SelectedValue.ToString()));
            txbMaSoNXB.Text = _currentNXB.MaSoNXB + "";
            loadSach();
            cmbNXB.Enabled = false;
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn lưu", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                List<ChiTietHoaDonNXB> ListChiTiet = new List<ChiTietHoaDonNXB>();
                HoaDonNXB hd = new HoaDonNXB();
                hd.MaSoNXB = int.Parse(txbMaSoNXB.Text.ToString());
                hd.NgayLap = DateTime.Parse(dtpNgayLap.Value.ToString("yyyy-MM-dd"));
                //Lay list chi tiet
                for (int i = 0; i < gdvChiTiet.RowCount - 1; i++)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[i].Cells[0].Value)) && !String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[i].Cells[1].Value)) && !String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[i].Cells[2].Value)))
                    {
                        ChiTietHoaDonNXB ct = new ChiTietHoaDonNXB();
                        ct.MaSoSach = int.Parse(gdvChiTiet.Rows[i].Cells[0].Value.ToString());
                        ct.SoLuong = int.Parse(gdvChiTiet.Rows[i].Cells[1].Value.ToString());
                        ct.DonGia = int.Parse(gdvChiTiet.Rows[i].Cells[2].Value.ToString());
                        ListChiTiet.Add(ct);
                    }
                }
                //Kiem tra chi tiet . Chi ong code cai nay nhin no gon hon ne
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


                //for (int i = 0; i < ListChiTiet.Count; i++)
                //{

                //    hd.addDetail(ListChiTiet[i]);
                //}

                int x = HoaDonNXBManager.add(hd);
                if (x != 0)
                {
                    MessageBox.Show("Đã thêm thành công hóa đơn nhà xuất bản");
                    txbMaHoaDon.Text = x + "";
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }
        }
    
}

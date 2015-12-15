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
    public partial class frmLapPhieuNhap : Form
    {
        public frmLapPhieuNhap(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Properties
        private List<NhaXuatBan> _DMNXB;
        private List<Sach> _DMSach;
        private Form _frmParent;
        private PhieuNhap _phieunhap;
        #endregion

        #region Form Control Listen
        //Khi load Form
        private void frmLapPhieuNhap_Load(object sender, EventArgs e)
        {
            //Load danh sách NXB
            loadNXB();
            //Load danh sách các sách;
            // loadSach();
            cmbNhaXuatBan.SelectedIndex = -1;
        }
        //Khi Nhấn Lưu lại phiếu nhập
        private void btnLuu_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn lưu phiếu nhập này", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                decimal tongtien = 0;
                List<ChiTietPhieuNhap> list = new List<ChiTietPhieuNhap>();
                if (!txbNguoiGiao.Text.Equals(""))
                {
                    PhieuNhap pn = new PhieuNhap();
                    pn.MaSoNXB = int.Parse(cmbNhaXuatBan.SelectedValue.ToString());
                    pn.NgayLap = DateTime.Parse(dtpNgayLap.Value.ToString("yyyy-MM-dd"));
                    pn.NguoiGiao = txbNguoiGiao.Text.ToString();

                    for (int i = 0; i < gdvChiTiet.RowCount - 1; i++)
                    {
                        if (!String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[i].Cells[0].Value)) && !String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[i].Cells[1].Value)) && !String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[i].Cells[2].Value)))
                        {
                            ChiTietPhieuNhap ctpn = new ChiTietPhieuNhap();
                            ctpn.MaSoSach = int.Parse(gdvChiTiet.Rows[i].Cells[0].Value.ToString());
                            ctpn.SoLuong = int.Parse(gdvChiTiet.Rows[i].Cells[1].Value.ToString());
                            ctpn.DonGia = int.Parse(gdvChiTiet.Rows[i].Cells[2].Value.ToString());
                            tongtien = tongtien + ctpn.SoLuong * ctpn.DonGia;
                            if(list.Any(s=>s.MaSoSach == ctpn.MaSoSach))
                            {
                                MessageBox.Show("Không được nhập trùng chi tiết");
                                return;
                            }
                            list.Add(ctpn);
                    
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[i].Cells[0].Value)) || String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[i].Cells[1].Value)) || String.IsNullOrEmpty(Convert.ToString(gdvChiTiet.Rows[i].Cells[2].Value)))
                            {
                                MessageBox.Show("Chưa nhập đủ thông tin vào bảng");
                                return;
                            }
                        }

                    }

                    pn.ChiTiet = list;

                        pn.TongTien = tongtien;
                        int x = PhieuNhapManager.add(pn);
                        if (x != 0)
                        {
                            MessageBox.Show("đã thêm thành công");
                            txbMaPhieuNhap.Text = x + "";
                        }
                        else
                            MessageBox.Show("Không thêm được");
                   

                }
                else
                    MessageBox.Show("Bạn chưa nhập người giao hàng");
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
                if (_frmParent.GetType().Name == nameof(frmMain))
                {
                    (_frmParent as frmMain).loadSach();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
          
        }
        //Khi chọn Nhà Xuất Bản
        private void cmbNhaXuatBan_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        private void gdvChiTiet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var a = gdvChiTiet.Columns[e.ColumnIndex].Name;
        }
        #endregion

        #region Form Services
        public void loadNXB()
        {
            _DMNXB = NhaXuatBanManager.getAll();
            cmbNhaXuatBan.DataSource = _DMNXB;
            cmbNhaXuatBan.DisplayMember = nameof(NhaXuatBanManager.Properties.TenNXB);
            cmbNhaXuatBan.ValueMember = nameof(NhaXuatBanManager.Properties.MaSoNXB);
        }

        public void loadSach()
        {

            _DMSach = SachManager.getAll();
        }
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
      


        #endregion

        #region service
        //private bool CheckChiTiet()
        //{
        //    if (gdvChiTiet.RowCount != 0)
        //    {
        //        for (int i = 0; i < gdvChiTiet.RowCount -1; i++)
        //        {
        //            for (int j = 0; j < gdvChiTiet.RowCount; j++)

        //        }
        //    }
        //}
        #endregion

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

        private void cmbNhaXuatBan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbNhaXuatBan.SelectedIndex != -1)
            {
                NhaXuatBan nxb = NhaXuatBanManager.find(int.Parse(cmbNhaXuatBan.SelectedValue.ToString()));
                _DMSach = nxb.Sach;
                cmbNhaXuatBan.Enabled = false;
            }
        }
        private bool CheckNumber(string input)
        {
            return Regex.IsMatch(input, @"^\d+$");
        }
    }
}

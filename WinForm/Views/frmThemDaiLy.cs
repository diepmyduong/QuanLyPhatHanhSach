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
    public partial class frmThemDaiLy : Form
    {
        public frmThemDaiLy(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Propertites
        private Form _frmParent;
        #endregion

        #region Form Control Listener
        //Khi form Load
        private void frmThemDaiLy_Load(object sender, EventArgs e)
        {

        }
        //Khi thêm Đại lý
        private void btnThem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thêm đại lý", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (!txbTenDaiLy.Text.Equals("") && !txbDiaChi.Text.Equals("") && !txbSoDienThoai.Text.Equals("") && !txbSoTaiKhoan.Text.Equals(""))
                {
                    DaiLy dl = new DaiLy();
                    dl.TenDaiLy = txbTenDaiLy.Text;
                    dl.DiaChi = txbDiaChi.Text;
                    dl.SoDienThoai = txbSoDienThoai.Text;
                    dl.SoTaiKhoan = txbSoTaiKhoan.Text;
                    dl.NganHang = txbNganHang.Text.ToString();

                    if (DaiLyManager.add(dl) != 0)
                        MessageBox.Show("Đã thêm đại lý thành công");
                    else
                        MessageBox.Show("Không thêm được, đại lý đã tồn tại ");

                }
                else
                    MessageBox.Show("Bạn chưa nhập đầy đủ thông tin");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
           
        }
        //Khi hủy thêm
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
                if (_frmParent.GetType().Name == nameof(frmDanhMucDaiLy))
                {
                    (_frmParent as frmDanhMucDaiLy).loadDaiLy();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
           
        }
        #endregion

    }
}

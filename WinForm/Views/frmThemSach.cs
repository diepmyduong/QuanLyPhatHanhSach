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
    public partial class frmThemSach : Form
    {
        public frmThemSach(Form frmParent)
        {
            InitializeComponent();
            this._frmParent = frmParent as frmMain;
        }

        #region Private Properties

        private frmMain _frmParent;
        private Sach _sach;
        private List<LinhVuc> _DMLinhVuc;
        private List<NhaXuatBan> _DMNXB;
        #endregion

        #region Form Control Listen
        //Khi Load Form
        private void frmThemSach_Load(object sender, EventArgs e)
        {
            //Load tất cả lĩnh vực
            _DMLinhVuc = LinhVucManager.getAll();
            cmbLinhVuc.DataSource = _DMLinhVuc;
            //Load tất cả nhà xuất bản
            _DMNXB = NhaXuatBanManager.getAll();
            cmbNXB.DataSource = _DMNXB;
        }
        //Khi chọn thêm mới 1 lĩnh vực
        private void btnThemLinhVuc_Click(object sender, EventArgs e)
        {

        }
        //Khi chọn tải ảnh lên
        private void btnTaiAnh_Click(object sender, EventArgs e)
        {

        }
        //Khi chọn tạo sách
        private void btnTao_Click(object sender, EventArgs e)
        {
            //Kiểm tra điều kiện
            if (String.IsNullOrEmpty(txbTenSach.Text))
            {
                MessageBox.Show("Tên Sách chưa hợp lệ");
                return;
            }
            if (String.IsNullOrEmpty(txbTacGia.Text))
            {
                MessageBox.Show("Tên Tác giả chưa hợp lệ");
                return;
            }
            if (String.IsNullOrEmpty(txbGiaBan.Text))
            {
                MessageBox.Show("Giá bán chưa hợp lệ");
                return;
            }
            if (String.IsNullOrEmpty(txbGiaNhap.Text))
            {
                MessageBox.Show("Giá nhập chưa hợp lệ");
                return;
            }
            if (cmbLinhVuc.SelectedIndex == -1)
            {
                MessageBox.Show("Lĩnh vực chưa hợp lệ");
                return;
            }
            if (cmbNXB.SelectedIndex == -1)
            {
                MessageBox.Show("Nhà Xuất Bản chưa hợp lệ");
                return;
            }
            //Tạo đối tượng
            _sach = new Sach()
            {
                TenSach = txbTenSach.Text,
                LinhVucSach = _DMLinhVuc[cmbLinhVuc.SelectedIndex],
                TenTacGia = txbTacGia.Text,
                NXB = _DMNXB[cmbNXB.SelectedIndex],
                Soluong = Decimal.ToInt32(nmbSoLuong.Value),
                GiaBan = Int32.Parse(txbGiaBan.Text),
                GiaNhap = Int32.Parse(txbGiaNhap.Text),
                HinhAnh = picHinhAnh.ImageLocation,
            };
            //Thêm vào database
            if (SachManager.add(_sach))
            {
                MessageBox.Show("Tạo mới thành công");
                reset();
                return;
            }
            MessageBox.Show("Thông tin đã tồn tại");
        }
        //Khi chọn thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            _frmParent.reload();
        }

        #endregion

        #region Form Services
        /// <summary>
        /// Reset lại các textbox 
        /// </summary>
        private void reset()
        {
            txbTenSach.Text ="";
            cmbLinhVuc.SelectedIndex = -1;
            txbTacGia.Text = "";
            cmbNXB.SelectedIndex = -1;
            txbGiaBan.Text = "";
            txbGiaNhap.Text = "";
            picHinhAnh.ImageLocation = "";
        }

        #endregion


    }
}

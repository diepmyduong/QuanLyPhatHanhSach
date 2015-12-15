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
            frmThemLinhVuc form = new frmThemLinhVuc(this);
            form.ShowDialog(this);
        }
        //Khi chọn tải ảnh lên
        private void btnTaiAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofile = new OpenFileDialog();
            ofile.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
            if (ofile.ShowDialog() == DialogResult.OK)
            {
                picHinhAnh.ImageLocation = ofile.FileName;
                picHinhAnh.Size = new System.Drawing.Size(250, 200);
                picHinhAnh.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        //Khi chọn tạo sách
        private void btnTao_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thêm sách", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
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
                    HinhAnhTypeImage = picHinhAnh.Image,
                    MoTa = rtxbMoTa.Text.ToString(),

                };
                //Thêm vào database
                if (_sach.isExisted() == true)
                    MessageBox.Show("Tên sách đã tồn tại");
                else
                {
                    var result = SachManager.add(_sach);
                    if (result != 0)
                    {
                        MessageBox.Show("Tạo mới thành công");
                        reset();
                        return;
                    }
                    else
                        MessageBox.Show("Không thêm được sách");
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
            //Kiểm tra điều kiện
           

        }
        //Khi chọn thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
                _frmParent.reload();
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
           
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

        public void reloadLinhVuc()
        {
            _DMLinhVuc = LinhVucManager.getAll();
            cmbLinhVuc.DataSource = _DMLinhVuc;
        }

        #endregion


    }
}

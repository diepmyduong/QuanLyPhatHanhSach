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
using System.Reflection;

namespace WinForm.Views
{
    public partial class frmMain : Form
    {

        public frmMain()
        {
            InitializeComponent();
        }

        #region Private Properties

        private List<Sach> _DMSach;
        private List<LinhVuc> _DMLinhVuc;
        private List<NhaXuatBan> _DMNXB;
        private Sach _currentSach;

        #endregion

        #region Form Control Listen
        //Khi Load Form
        private void frmMain_Load(object sender, EventArgs e)
        {
            createGridViewColumns();
            //Load các lĩnh vực
            loadLinhVuc();
            //Load các NXB
            loadNXB();
            //Load tất cả sách
            loadSach();
            txbLoc.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txbLoc.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txbLoc.AutoCompleteCustomSource = null;
        }
        //Chọn Xem Danh mục Sách
        private void menuItemXemDanhMucSach_Click(object sender, EventArgs e)
        {

        }
        //Chọn Xem Danh mục NXB
        private void menuItemXemDanhMucNXB_Click(object sender, EventArgs e)
        {
            frmDanhMucNXB form = new frmDanhMucNXB(this);
            form.ShowDialog(this);
        }
        //Chọn Tạo Phiếu Nhập
        private void menuItemTaoPhieuNhap_Click(object sender, EventArgs e)
        {
            frmLapPhieuNhap form = new frmLapPhieuNhap(this);
            form.ShowDialog(this);
        }
        //Chọn Xem Danh Mục Đại lý
        private void menuItemXemDanhMucDaiLy_Click(object sender, EventArgs e)
        {
            frmDanhMucDaiLy form = new frmDanhMucDaiLy(this);
            form.ShowDialog(this);
        }
        //Chọn tạo phiếu xuất
        private void menuItemTaoPhieuXuat_Click(object sender, EventArgs e)
        {
            FrmDanhMucPhieuXuat fr = new FrmDanhMucPhieuXuat();
            fr.Show();
        }
        //Chọn Thanh toán với Đại lý
        private void menuItemThanhToanDaiLy_Click(object sender, EventArgs e)
        {
            frmThanhToanDaiLy form = new frmThanhToanDaiLy(this);
            form.ShowDialog(this);
        }
        //Chọn thanh toán với NXB
        private void menuItemThanhToanNXB_Click(object sender, EventArgs e)
        {
            frmThanhToanNXB form = new frmThanhToanNXB(this);
            form.ShowDialog(this);
        }
        //Chọn theo dõi nỡ với Đại lý
        private void menuItemNoDaiLy_Click(object sender, EventArgs e)
        {
            frmCongNoDaiLy form = new frmCongNoDaiLy(this);
            form.ShowDialog(this);
        }
        //Chọn theo dõi nợ với NXB
        private void menuItemNoNXB_Click(object sender, EventArgs e)
        {
            frmCongNoNXB form = new frmCongNoNXB(this);
           form.ShowDialog(this);
        }
        //Chọn xem thống kê tồn kho
        private void menuItemTonKho_Click(object sender, EventArgs e)
        {
            frmThongKeTonKho form = new frmThongKeTonKho(this);
            form.ShowDialog(this);
        }
        //Chọn xem thống ke Doanh Thu
        private void menuItemThongKeDoanhThu_Click(object sender, EventArgs e)
        {
            frmThongKeDoanhThu form = new frmThongKeDoanhThu(this);
            form.ShowDialog(this);
        }
        //Chọn xem thống kê Sách bán
        private void menuItemThongKeSach_Click(object sender, EventArgs e)
        {
            frmThongKeSachBan form = new frmThongKeSachBan(this);
            form.ShowDialog(this);
        }
        //Chọn xem Thống kê Công nợ
        private void menuItemThongKeCongNo_Click(object sender, EventArgs e)
        {
            frmThongKeCongNo form = new frmThongKeCongNo(this);
            form.ShowDialog(this);
        }
        private void menuItemThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Chọn cập nhật thông tin Sách
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn cập nhật sản phẩm", "Thông báo", MessageBoxButtons.YesNo);
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
                if (String.IsNullOrEmpty(txbGiaMua.Text))
                {
                    MessageBox.Show("Giá mua chưa hợp lệ");
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
                //Kiểm tra thông tin cập nhật trùng
                //if(_DMSach.Find(sach => !sach.MaSoSach.Equals(Int32.Parse(txbMaSach.Text))
                //                        && sach.TenSach.Equals(txbTenSach.Text)
                //                        && sach.LinhVucSach.MaSoLinhVuc.Equals(cmbLinhVuc.SelectedValue)
                //                        && sach.NXB.MaSoNXB.Equals(cmbNXB.SelectedValue)) != null)
                //{
                //    MessageBox.Show("Thông tin bị trùng");
                //}
                //Tạo mới đối tượng
                Sach s = new Sach();
                s.MaSoSach = Int32.Parse(txbMaSach.Text);
                s.TenSach = txbTenSach.Text;
                s.LinhVucSach = _DMLinhVuc[cmbLinhVuc.SelectedIndex];
                s.TenTacGia = txbTacGia.Text;
                s.NXB = _DMNXB[cmbNXB.SelectedIndex];
                s.Soluong = Decimal.Parse(txbSoLuong.Text);
                s.GiaBan = Decimal.Parse(txbGiaBan.Text);
                s.GiaNhap = Decimal.Parse(txbGiaMua.Text);
                s.HinhAnhTypeImage = picHinhAnh.Image;
                s.MoTa = rtxbMoTa.Text.ToString();
                //Cập nhật


                if (SachManager.edit(s))
                {
                    MessageBox.Show("Thay đổi thông tin sách thành công");
                    reload();
                }
                else
                    MessageBox.Show("Không thay đổi được, kiểm tra lại");

            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
            //Kiểm tra điều kiện
           
           
            
        }
        //Xóa sách đã chọn
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa sản phẩm", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (!txbMaSach.Text.Equals(""))
                {
                    if (SachManager.delete(int.Parse(txbMaSach.Text)))
                    {
                        MessageBox.Show("Xóa sách thành công");
                        reload();
                    }
                    else
                        MessageBox.Show("Không xóa được, vui lòng kiểm tra lại");
                }
                else
                {
                    MessageBox.Show("Nhập vào mã sách cần xóa");
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }


        }
        //Tải ảnh lên
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
        //Chọn thêm sách 
        private void btnThemSach_Click(object sender, EventArgs e)
        {
            frmThemSach form = new frmThemSach(this);
            form.ShowDialog(this);
        }
        //Chọn thêm lĩnh vực
        private void btnThemLinhVuc_Click(object sender, EventArgs e)
        {
            frmThemLinhVuc form = new frmThemLinhVuc(this);
            form.ShowDialog(this);
        }
        //Chọn thêm NXB
        private void btnThemNXB_Click(object sender, EventArgs e)
        {
            frmDanhMucNXB form = new frmDanhMucNXB(this);
            form.ShowDialog(this);
        }
        //Chọn Lập Phiếu Nhập
        private void btnLapPhieuNhap_Click(object sender, EventArgs e)
        {
            frmLapPhieuNhap form = new frmLapPhieuNhap(this);
            form.ShowDialog(this);
        }
        //Chọn Lọc danh sách sách theo từ khóa
        private void btnLoc_Click(object sender, EventArgs e)
        {
            gdvDanhMucSach.DataSource = SachManager.filter(txbLoc.Text,_DMSach);
        }
        //Khi gõ vào thanh lọc
        private void txbLoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 123)
            {
                txbLoc.Text = txbLoc.Text + "{";
                string request = txbLoc.Text;
                var pros = Sach.searchKeys();
                AutoCompleteStringCollection source = new AutoCompleteStringCollection();
                foreach (var info in pros)
                {
                    source.Add(request + info);
                }
                txbLoc.AutoCompleteCustomSource = source;
            }

        }
        private void txbLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gdvDanhMucSach.DataSource = SachManager.filter(txbLoc.Text, _DMSach);
            }
        }
        //Khi chọn một sách thì duyệt lên chi tiết
        private void gdvDanhMucSach_SelectionChanged(object sender, EventArgs e)
        {
            //int index = Int32.Parse((sender as DataGridView).CurrentRow.Cells["MaSoSach"].Value.ToString());
            //selectBook(_DMSach.Find(s => s.MaSoSach.Equals(index)));
            int index = ((DataGridView)sender).CurrentRow.Index;
            _currentSach = (((DataGridView)sender).DataSource as List<Sach>)[index];
            selectBook(_currentSach);
        }
        #endregion
        #region Form Services
        /// <summary>
        /// Hiển thị chi tiết sách
        /// </summary>
        /// 
        public void selectBook(Sach s)
        {
            if(s != null)
            {
                txbMaSach.Text = s.MaSoSach.ToString();
                txbTenSach.Text = s.TenSach;
                cmbLinhVuc.SelectedIndex = _DMLinhVuc.FindIndex(lv => lv.MaSoLinhVuc == s.LinhVucSach.MaSoLinhVuc);
                cmbNXB.SelectedIndex = _DMNXB.FindIndex(nxb => nxb.MaSoNXB == s.NXB.MaSoNXB);
                txbTacGia.Text = s.TenTacGia;
                txbSoLuong.Text = s.Soluong.ToString();
                txbGiaBan.Text = s.GiaBan.ToString();
                txbGiaMua.Text = s.GiaNhap.ToString();
                rtxbMoTa.Text = s.MoTa.ToString();
                if(s.HinhAnh != null)
                {
                    picHinhAnh.Image = s.HinhAnhTypeImage;
                }
                else
                {
                    picHinhAnh.Image = Properties.Resources.DefaultImage;
                }
                
            }

        }

        public void addSach(Sach sach)
        {
            _DMSach.Add(sach);
        }
        /// <summary>
        /// Load lại form
        /// </summary>
        public void reload()
        {
            loadSach();
        }
        /// <summary>
        /// Load danh sách NXB và add vào giao diện
        /// </summary>
        public void loadNXB()
        {
            _DMNXB = NhaXuatBanManager.getAll();
            cmbNXB.DataSource = _DMNXB;
            cmbNXB.DisplayMember = nameof(NhaXuatBanManager.Properties.TenNXB);
            cmbNXB.ValueMember = nameof(NhaXuatBanManager.Properties.MaSoNXB);
        }
        /// <summary>
        /// Load danh sách Lĩnh vực và add vào giao diện
        /// </summary>
        public void loadLinhVuc()
        {
            _DMLinhVuc = LinhVucManager.getAll();
            cmbLinhVuc.DataSource = _DMLinhVuc;
            cmbLinhVuc.DisplayMember = nameof(LinhVucManager.Properties.TenLinhVuc);
            cmbLinhVuc.ValueMember = nameof(LinhVucManager.Properties.MaSoLinhVuc);
        }
        /// <summary>
        /// Load danh sách Sách và add vào giao diện
        /// </summary>
        public void loadSach()
        {
            _DMSach = SachManager.getAllAlive();
            gdvDanhMucSach.DataSource = _DMSach;
        }
        private void createGridViewColumns()
        {
            gdvDanhMucSach.AutoGenerateColumns = false; // Bỏ auto generate Columns
            gdvDanhMucSach.ColumnCount = 9; // Xác định số columns có
            setColumn(gdvDanhMucSach.Columns[0]
                , nameof(SachManager.Properties.MaSoSach)
                , SachManager.Properties.MaSoSach);
            setColumn(gdvDanhMucSach.Columns[1]
                , nameof(SachManager.Properties.TenSach)
                , SachManager.Properties.TenSach);
            setColumn(gdvDanhMucSach.Columns[2]
                , nameof(SachManager.Properties.LinhVucSach)
                , SachManager.Properties.LinhVucSach);
            setColumn(gdvDanhMucSach.Columns[3]
                , nameof(SachManager.Properties.TenTacGia)
                , SachManager.Properties.TenTacGia);
            setColumn(gdvDanhMucSach.Columns[4]
                , nameof(SachManager.Properties.NXB)
                , SachManager.Properties.NXB);
            setColumn(gdvDanhMucSach.Columns[5]
                , nameof(SachManager.Properties.Soluong)
                , SachManager.Properties.Soluong);
            setColumn(gdvDanhMucSach.Columns[6]
                , nameof(SachManager.Properties.GiaBan)
                , SachManager.Properties.GiaBan);
            setColumn(gdvDanhMucSach.Columns[7]
                , nameof(SachManager.Properties.GiaNhap)
                , SachManager.Properties.GiaNhap);
            setColumn(gdvDanhMucSach.Columns[8]
               , nameof(SachManager.Properties.MoTa)
               , SachManager.Properties.MoTa);

        }

        private void setColumn(DataGridViewColumn column, string propertyName, string name)
        {
            column.Name = propertyName;
            column.DataPropertyName = propertyName;
            column.HeaderText = name;
        }




        #endregion

        private void xemPhiếuNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDanhMucPhieuNhap fr = new FrmDanhMucPhieuNhap();
            fr.ShowDialog();
        }

        private void gdvDanhMucSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbLinhVuc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void xemDanhMụcHóaĐơnNXBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDanhMucHoaDonNXB fr = new frmDanhMucHoaDonNXB();
            fr.Show();
        }

        private void xemDanhMụcHóaĐơnĐạiLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDanhMucHoaDonDaiLy fr = new frmDanhMucHoaDonDaiLy();
            fr.Show();
        }
    }
}

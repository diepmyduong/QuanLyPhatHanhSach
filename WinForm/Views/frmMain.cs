﻿using System;
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

        #endregion

        #region Form Control Listen
        //Khi Load Form
        private void frmMain_Load(object sender, EventArgs e)
        {
            
            //Load các lĩnh vực
            _DMLinhVuc = LinhVucManager.getAll();
            cmbLinhVuc.DataSource = _DMLinhVuc;
            cmbLinhVuc.DisplayMember = "TenLinhVuc";
            cmbLinhVuc.ValueMember = "MaSoLinhVuc";
            //Load các NXB
            _DMNXB = NhaXuatBanManager.getAll();
            cmbNXB.DataSource = _DMNXB;
            cmbNXB.DisplayMember = "TenNXB";
            cmbNXB.ValueMember = "MaSoNXB";
            //Load tất cả sách
            _DMSach = SachManager.getAll();
            gdvDanhMucSach.DataSource = _DMSach;
            gdvDanhMucSach.Columns["HinhAnh"].Visible = false;
        }
        //Chọn Xem Danh mục Sách
        private void menuItemXemDanhMucSach_Click(object sender, EventArgs e)
        {

        }
        //Chọn Xem Danh mục NXB
        private void menuItemXemDanhMucNXB_Click(object sender, EventArgs e)
        {

        }
        //Chọn Tạo Phiếu Nhập
        private void menuItemTaoPhieuNhap_Click(object sender, EventArgs e)
        {

        }
        //Chọn Xem Danh Mục Đại lý
        private void menuItemXemDanhMucDaiLy_Click(object sender, EventArgs e)
        {

        }
        //Chọn tạo phiếu xuất
        private void menuItemTaoPhieuXuat_Click(object sender, EventArgs e)
        {

        }
        //Chọn Thanh toán với Đại lý
        private void menuItemThanhToanDaiLy_Click(object sender, EventArgs e)
        {

        }
        //Chọn thanh toán với NXB
        private void menuItemThanhToanNXB_Click(object sender, EventArgs e)
        {

        }
        //Chọn theo dõi nỡ với Đại lý
        private void menuItemNoDaiLy_Click(object sender, EventArgs e)
        {

        }
        //Chọn theo dõi nợ với NXB
        private void menuItemNoNXB_Click(object sender, EventArgs e)
        {

        }
        //Chọn xem thống kê tồn kho
        private void menuItemTonKho_Click(object sender, EventArgs e)
        {

        }
        //Chọn xem thống ke Doanh Thu
        private void menuItemThongKeDoanhThu_Click(object sender, EventArgs e)
        {

        }
        //Chọn xem thống kê Sách bán
        private void menuItemThongKeSach_Click(object sender, EventArgs e)
        {

        }
        //Chọn xem Thống kê Công nợ
        private void menuItemThongKeCongNo_Click(object sender, EventArgs e)
        {

        }
        //Chọn cập nhật thông tin Sách
        private void btnCapNhat_Click(object sender, EventArgs e)
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
            //Tạo mới đối tượng
            Sach s = new Sach();
            s.MaSoSach = Int32.Parse(txbMaSach.Text);
            s.TenSach = txbTenSach.Text;
            s.LinhVucSach = _DMLinhVuc[cmbLinhVuc.SelectedIndex];
            s.TenTacGia = txbTacGia.Text;
            s.NXB = _DMNXB[cmbNXB.SelectedIndex];
            s.Soluong = Int32.Parse(txbSoLuong.Text);
            s.GiaBan = Int32.Parse(txbGiaBan.Text);
            s.GiaNhap = Int32.Parse(txbGiaMua.Text);
            s.HinhAnh = picHinhAnh.ImageLocation;
            //Cập nhật
            SachManager.edit(s);
            //Load lại form
            reload();
            
        }
        //Xóa sách đã chọn
        private void btnXoa_Click(object sender, EventArgs e)
        {

        }
        //Tải ảnh lên
        private void btnTaiAnh_Click(object sender, EventArgs e)
        {

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

        }
        //Chọn thêm NXB
        private void btnThemNXB_Click(object sender, EventArgs e)
        {

        }
        //Chọn Lập Phiếu Nhập
        private void btnLapPhieuNhap_Click(object sender, EventArgs e)
        {

        }
        //Chọn Lọc danh sách sách theo từ khóa
        private void btnLoc_Click(object sender, EventArgs e)
        {
            gdvDanhMucSach.DataSource = filter(txbLoc.Text);
        }
        //Khi gõ vào thanh lọc
        private void txbLoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Return)
            {
                gdvDanhMucSach.DataSource = filter(txbLoc.Text);
            }
        }
        //Khi chọn một sách thì duyệt lên chi tiết
        private void gdvDanhMucSach_SelectionChanged(object sender, EventArgs e)
        {
            int index = Int32.Parse((sender as DataGridView).CurrentRow.Cells["MaSoSach"].Value.ToString());
            selectBook(_DMSach.Find(s => s.MaSoSach.Equals(index)));
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
            }
            
        }
        /// <summary>
        /// Thực hiện lọc dữ liệu với tham số cho trước
        /// </summary>
        private List<Sach> filter(string request)
        {
            request = txbLoc.Text.ToLower();
            int number;
            bool isNumber = Int32.TryParse(request, out number);
            if (isNumber)
            {
                var linqQuery = _DMSach.Where
                (s => s.MaSoSach.Equals(number)
                || s.Soluong.Equals(number)
                || s.GiaNhap.Equals(number)
                || s.GiaBan.Equals(number)
                );
                return linqQuery.ToList<Sach>();
            }
            else
            {
                var linqQuery = _DMSach.Where
                (s => s.TenSach.ToLower().Contains(request)
                || s.LinhVucSach.TenLinhVuc.ToLower().Contains(request)
                || s.TenTacGia.ToLower().Contains(request)
                || s.NXB.TenNXB.ToLower().Contains(request)
                );
                return linqQuery.ToList<Sach>();
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
            this.OnLoad(new EventArgs());
        }
        #endregion



    }
}
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
    public partial class frmThemLinhVuc : Form
    {
        public frmThemLinhVuc(Form frmParent)
        {
            InitializeComponent();
            _frmParent = frmParent;
        }

        #region Private Propertites

        private List<LinhVuc> _DMLinhVuc;
        private Form _frmParent;

        #endregion

        #region Form Control Listen
        //Khi load form
        private void frmThemLinhVuc_Load(object sender, EventArgs e)
        {
            createGridViewColumns();
            //Load danh mục Lĩnh vực
            loadLinhVuc();

        }
        //Khi chọn Thêm Lĩnh vực
        private void btnThem_Click(object sender, EventArgs e)
        {
            string tensach = Prompt.showDialog("Tên Lĩnh vực :", "Nhập tên lĩnh vực muốn thêm.",this);
            if (!String.IsNullOrEmpty(tensach))
            {
                LinhVuc lv = new LinhVuc() { TenLinhVuc = tensach };
               
                    var result = LinhVucManager.add(lv);
                    if (result != 0)
                    {
                        MessageBox.Show("Đã thêm.");
                        reload();
                        return;
                    }
                    MessageBox.Show("Không thêm được");
               
            }
        }
        //Khi chọn xóa 1 lĩnh vực
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa lĩnh vực", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (!txbMaLinhVuc.Text.Equals(""))
                {
                    if (LinhVucManager.delete(int.Parse(txbMaLinhVuc.Text.ToString())))
                    {
                        MessageBox.Show("Đã xóa lĩnh vực thành công");
                        loadLinhVuc();
                    }
                    else
                        MessageBox.Show("Không xóa được !");
                }
                else
                    MessageBox.Show("Chọn lĩnh vực cần xóa");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
            
        }
        //Khi chọn thoát khỏi form
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
                if (_frmParent.GetType().Name == nameof(frmThemSach))
                {
                    (_frmParent as frmThemSach).reloadLinhVuc();
                    return;
                }
                if (_frmParent.GetType().Name == nameof(frmMain))
                {
                    (_frmParent as frmMain).loadLinhVuc();
                    return;
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
          
        }
        //Khi cập nhật lĩnh vực sửa
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn cập nhật", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (String.IsNullOrEmpty(txbTenLinhVuc.Text))
                {
                    MessageBox.Show("Tên lĩnh vực chưa hợp lệ");
                    return;
                }
                //Kiểm tra thông tin sửa có tồn tại không
                if (_DMLinhVuc.Find(l => l.TenLinhVuc.Equals(txbTenLinhVuc.Text)) != null)
                {
                    MessageBox.Show("Lĩnh vực đã tồn tại.");
                    return;
                }
                LinhVuc lv = new LinhVuc()
                {
                    MaSoLinhVuc = Int32.Parse(txbMaLinhVuc.Text),
                    TenLinhVuc = txbTenLinhVuc.Text
                };
                if (LinhVucManager.edit(lv))
                {
                    MessageBox.Show("Đã cập nhật.");
                    reload();
                    return;
                }
                MessageBox.Show("Cập nhật thất bại.");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        
        }
        //Khi chọn 1 lĩnh vực từ Danh mục lĩnh vực
        private void gdvDMLinhVuc_SelectionChanged(object sender, EventArgs e)
        {
            int index = Int32.Parse((sender as DataGridView).CurrentRow.Cells["MaSoLinhVuc"].Value.ToString());
            selectLinhVuc(_DMLinhVuc.Find(s => s.MaSoLinhVuc.Equals(index)));
        }
        #endregion

        #region Form Services

        private void selectLinhVuc(LinhVuc lv)
        {
            if (lv != null)
            {
                txbMaLinhVuc.Text = lv.MaSoLinhVuc.ToString();
                txbTenLinhVuc.Text = lv.TenLinhVuc;
            }
        }

        private void reload()
        {
            loadLinhVuc();
        }

        private void loadLinhVuc()
        {
            _DMLinhVuc = LinhVucManager.getAllALive();
            gdvDMLinhVuc.DataSource = _DMLinhVuc;
        }

        private void createGridViewColumns()
        {
            gdvDMLinhVuc.AutoGenerateColumns = false; // Bỏ auto generate Columns
            gdvDMLinhVuc.ColumnCount = 2; // Xác định số columns có
            setColumn(gdvDMLinhVuc.Columns[0]
                , nameof(LinhVucManager.Properties.MaSoLinhVuc)
                , LinhVucManager.Properties.MaSoLinhVuc);
            setColumn(gdvDMLinhVuc.Columns[1]
                , nameof(LinhVucManager.Properties.TenLinhVuc)
                , LinhVucManager.Properties.TenLinhVuc);

        }

        private void setColumn(DataGridViewColumn column, string propertyName, string name)
        {
            column.Name = propertyName;
            column.DataPropertyName = propertyName;
            column.HeaderText = name;
        }
        #endregion

    }
}

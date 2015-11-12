using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.DAL;
using Core.BIZ;

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

        #region Form Control Listenner
        //khi form load
        private void ThanhToanNXB_Load(object sender, EventArgs e)
        {
            int parentMaNXB = -1;
            if (_currentNXB != null)
            {
                parentMaNXB = _currentNXB.MaSoNXB;
            }
            loadNXB();
            if(parentMaNXB != -1)
            {
                cmbNXB.SelectedValue = parentMaNXB;
            }
            
        }
        //Khi chọn lưu lại hóa đơn
        private void btnLuu_Click(object sender, EventArgs e)
        {

        }
        //Khi chọn thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Khi chọn NXB
        private void cmbNXB_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentNXB = cmbNXB.SelectedItem as NhaXuatBan;
            _DMSachNXB = _currentNXB.Sach;
            txbMaSoNXB.Text = _currentNXB.MaSoNXB.ToString();
        }
        #endregion

        #region Form Services
        public void loadNXB()
        {
            _DMNXB = NhaXuatBanManager.getAll();
            cmbNXB.DataSource = _DMNXB;
            cmbNXB.DisplayMember = nameof(NhaXuatBanManager.Properties.TenNXB);
            cmbNXB.ValueMember = nameof(NhaXuatBanManager.Properties.MaSoNXB);
        }
        #endregion

    }
}

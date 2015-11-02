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
    public partial class frmLapPhieuNhap : Form
    {
        public frmLapPhieuNhap(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Properties
        private List<NhaXuatBan> _DMNXB;
        private Form _frmParent;
        #endregion

        #region Form Control Listen
        //Khi load Form
        private void frmLapPhieuNhap_Load(object sender, EventArgs e)
        {

        }
        //Khi Nhấn Lưu lại phiếu nhập
        private void btnLuu_Click(object sender, EventArgs e)
        {

        }
        //Khi chọn thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {

        }
        //Khi chọn Nhà Xuất Bản
        private void cmbNhaXuatBan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion



    }
}

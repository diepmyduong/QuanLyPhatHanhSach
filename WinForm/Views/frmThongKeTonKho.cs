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
using System.IO;

namespace WinForm.Views
{
    public partial class frmThongKeTonKho : Form
    {
        public frmThongKeTonKho(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }
        #region Private Properties
        private Form _frmParent;
        private List<TheKho> _DMTheKho;
        private decimal _tongLuongSach;
        #endregion
        #region Form Control Listen
        private void frmThongKeTonKho_Load(object sender, EventArgs e)
        {
            //Load thẻ kho
            loadTheKho();
        }

        private void dtpNgayGhi_ValueChanged(object sender, EventArgs e)
        {
            loadTheKho();
        }

        private void btnLoNhap_Click(object sender, EventArgs e)
        {
            frmThongKeLoNhap form = new frmThongKeLoNhap(this);
            form.ShowDialog(this);
        }

        private void btnLoXuat_Click(object sender, EventArgs e)
        {
            frmThongKeLoXuat form  = new frmThongKeLoXuat(this);
            form.ShowDialog(this);
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {

        }

        private void txbLoc_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txbLoc_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Form Services
        public void loadTheKho()
        {
            _DMTheKho = TheKhoManager.getAllByDate(dtpNgayGhi.Value);
            gdvTheKho.DataSource = _DMTheKho;
            _tongLuongSach = _DMTheKho.Sum(tk => tk.SoLuong);
            lbTongLuongSach.Text = _tongLuongSach.ToString();
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            //PdfPTable pdfTable = new PdfPTable(gdvTheKho.ColumnCount);
            //pdfTable.DefaultCell.Padding = 3;
            //pdfTable.WidthPercentage = 30;
            //pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.DefaultCell.BorderWidth = 1;
            //pdfTable.TotalWidth = 350f;
            //pdfTable.LockedWidth = true;
            //float[] widths = new float[] { 50f, 100f, 100f, 100f };
            //pdfTable.SetWidths(widths);


            ////Adding Header row
            //foreach (DataGridViewColumn column in gdvTheKho.Columns)
            //{
            //    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
            //    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
            //    pdfTable.AddCell(cell);
            //}

            ////Adding DataRow
            //foreach (DataGridViewRow row in gdvTheKho.Rows)
            //{
            //    foreach (DataGridViewCell cell in row.Cells)
            //    {
            //        if (!String.IsNullOrEmpty(Convert.ToString(cell.Value)))
            //            pdfTable.AddCell(cell.Value.ToString());
            //    }
            //}

            ////Exporting to PDF
            //string folderPath = @"C:\Users\huy\Desktop\Web\QuanLyPhatHanhSach\Report\";
            //if (!Directory.Exists(folderPath))
            //{
            //    Directory.CreateDirectory(folderPath);
            //}
            //using (FileStream stream = new FileStream(folderPath + "data.pdf", FileMode.Create))
            //{
            //    Document pdfDoc = new Document(PageSize.A3,100f,100f,100f,0);
            //    PdfWriter.GetInstance(pdfDoc, stream);
            //    pdfDoc.Open();
            //    var FontColour = new BaseColor(255, 0, 0);
            //    var redListTextFont = FontFactory.GetFont("Arial", 28, FontColour);
            //    Paragraph docTitle = new Paragraph("Thong ke ton kho ngay " + dtpNgayGhi.Value.Day + "-" + dtpNgayGhi.Value.Month + "-" + dtpNgayGhi.Value.Year + "\n", redListTextFont);
            //    Paragraph docTitle1 = new Paragraph("Tong so luong sach  :" + lbTongLuongSach.Text + "\n", redListTextFont);
            //    docTitle.Alignment = Element.ALIGN_CENTER;
            //    docTitle1.Alignment = Element.ALIGN_CENTER;
            //    pdfDoc.Add(docTitle);
            //    pdfDoc.Add(docTitle1);
            //    pdfDoc.Add(new Paragraph("\n"));
            //    pdfDoc.Add(new Paragraph("\n"));
            //    pdfDoc.Add(pdfTable);
            //    pdfDoc.Close();
            //    stream.Close();
            //}
        }
    }
}

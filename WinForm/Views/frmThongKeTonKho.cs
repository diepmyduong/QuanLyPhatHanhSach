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
using iTextSharp.text;
using iTextSharp.text.pdf;
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
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xuất tạo file báo cáo", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var printer = new PrintHelper();
                string x = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "";

                string tenfile = x + "ReportTonKho.pdf";
                printer.FileName = tenfile;
                printer.FolderPath = "D://Report";
                printer.Title = "Báo cáo tồn kho";
                printer.printTonKho(_DMTheKho, dtpNgayGhi.Value);
                MessageBox.Show("Đã tạo file thành công , Tên file là : " + tenfile);
                //var redListTextFont = FontFactory.RegisterDirectory(Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts");
                //var _bold = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 10f,iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                //var _bold1 = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 10f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
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
                //    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText,_bold));
                //    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);

                //    pdfTable.AddCell(cell);
                //}

                ////Adding DataRow
                //foreach (DataGridViewRow row in gdvTheKho.Rows)
                //{
                //    foreach (DataGridViewCell cell in row.Cells)
                //    {
                //        if (!String.IsNullOrEmpty(Convert.ToString(cell.Value)))
                //            pdfTable.AddCell(new Phrase(cell.Value.ToString(),_bold1));
                //    }
                //}

                ////Exporting to PDF
                //string folderPath = @"D:\Report\";
                //string x = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-"+ DateTime.Now.Hour + "-"+ DateTime.Now.Minute +"-"+ DateTime.Now.Second + "";

                //string tenfile = x + "ReportTonKho.pdf";
                //if (!Directory.Exists(folderPath))
                //{
                //    Directory.CreateDirectory(folderPath);
                //}
                //using (FileStream stream = new FileStream(folderPath + tenfile, FileMode.Create))
                //{
                //    Document pdfDoc = new Document(PageSize.A3, 100f, 100f, 100f, 0);
                //    PdfWriter.GetInstance(pdfDoc, stream);
                //    pdfDoc.Open();
                //    var FontColour = new BaseColor(255, 0, 0);
                //    var _bold2 = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 20f, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);
                //    Paragraph docTitle = new Paragraph("Thống kê tồn kho ngày : " + dtpNgayGhi.Value.Day + "-" + dtpNgayGhi.Value.Month + "-" + dtpNgayGhi.Value.Year + "\n", _bold2);
                //    Paragraph docTitle1 = new Paragraph("Tổng số lượng sách   : " + lbTongLuongSach.Text + "\n", _bold2);
                //    docTitle.Alignment = Element.ALIGN_LEFT;
                //    docTitle1.Alignment = Element.ALIGN_LEFT;
                //    pdfDoc.Add(docTitle);
                //    pdfDoc.Add(docTitle1);
                //    pdfDoc.Add(new Paragraph("\n"));
                //    pdfDoc.Add(new Paragraph("\n"));
                //    pdfDoc.Add(pdfTable);
                //    pdfDoc.Close();
                //    stream.Close();

                //    MessageBox.Show("Đã tạo file thành công , Tên file là : " + tenfile);
                //}
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
            
        }
    }
}

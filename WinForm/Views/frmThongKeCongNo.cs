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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace WinForm.Views
{
    public partial class frmThongKeCongNo : Form
    {
        public frmThongKeCongNo(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Properties
        private Form _frmParent;
        private int _startMonth;
        private int _startYear;
        private int _endMonth;
        private int _endYear;
        private List<Sach> _DMSachDLy;
        private List<Sach> _DMSachNXB;
        private  int x = 0;
        #endregion

        #region Form Control Listener
        private void frmThongKeCongNo_Load(object sender, EventArgs e)
        {
            _startMonth = 1;
            _startYear = DateTime.Now.Year;
            _endMonth = 12;
            _endYear = DateTime.Now.Year;
            //Load combobox tháng và năm
            cmbStartMonth.DataSource = new BindingSource(FilterHelper.Monthes, null);
            cmbEndMonth.DataSource = new BindingSource(FilterHelper.Monthes, null);
            cmbStartMonth.DisplayMember = "Key";
            cmbEndMonth.DisplayMember = "Key";
            cmbStartMonth.ValueMember = "Value";
            cmbEndMonth.ValueMember = "Value";
            cmbStartYear.DataSource = new BindingSource(FilterHelper.Years, null);
            cmbEndYear.DataSource = new BindingSource(FilterHelper.Years, null);
            cmbStartYear.DisplayMember = "Key";
            cmbEndYear.DisplayMember = "Key";
            cmbStartYear.ValueMember = "Value";
            cmbEndYear.ValueMember = "Value";


            //Set Tháng năm mặc định

            cmbStartMonth.SelectedValue = 1;
            cmbStartYear.SelectedValue = _startYear;
            cmbEndMonth.SelectedValue = 12;
            cmbEndYear.SelectedValue = _endYear;
            loadTT();
        }
        private void loadTT()
        {
            if (x == 1)
            {
                _DMSachNXB = SachManager.getAll()
                    .Where(s => s.tongSoLuongNXBNoTheoThang(_startMonth, _startYear, _endMonth, _endYear) > 0 &&
                    s.tongTienNXBNoTheoThang(_startMonth, _startYear, _endMonth, _endYear) > 0).ToList();
                gdvDMSach.DataSource = _DMSachNXB;
                lbSoLuongNo.Text = _DMSachNXB.Sum(s => s.TongSoLuongNXBNoTheoThang) + "";
                lbTongTien.Text = _DMSachNXB.Sum(s => s.TongTienNXBNoTheoThang) + "";
            }
            if (x == 2)
            {
                _DMSachDLy = SachManager.getAll()
                    .Where(s => s.tongSoLuongDaiLyNoTheoThang(_startMonth, _startYear, _endMonth, _endYear) > 0 && s.tongTienDaiLyNoTheoThang(_startMonth, _startYear, _endMonth, _endYear) > 0).ToList();
                gdvDMSach.DataSource = _DMSachDLy;
                lbSoLuongNo.Text = _DMSachDLy.Sum(s => s.TongSoLuongDaiLyNoTheoThang) + "";
                lbTongTien.Text = _DMSachDLy.Sum(s => s.TongTienDaiLyNoTheoTang) + "";
            }
          
        }
        private void cmbStartMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadTT();
        }

        private void cmbStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadTT();
        }

        private void cmbEndMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadTT();
        }

        private void cmbEndYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadTT();
        }

        private void rabtnNXB_CheckedChanged(object sender, EventArgs e)
        {
            x = 1;
            gdvDMSach.DataSource = null;
            createGridViewColumns();
            loadTT();
        }

        private void rabtnDaiLy_CheckedChanged(object sender, EventArgs e)
        {
            x = 2;
            gdvDMSach.DataSource = null;
            createGridViewColumns();
            loadTT();
        }

        private void btLoc_Click(object sender, EventArgs e)
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
        private void createGridViewColumns()
        {
            if (x == 1)
            {
                gdvDMSach.AutoGenerateColumns = false;
                gdvDMSach.ColumnCount = 7;
                setColumn(gdvDMSach.Columns[0]
                    , nameof(SachManager.Properties.MaSoSach)
                    , SachManager.Properties.MaSoSach);
                setColumn(gdvDMSach.Columns[1]
                    , nameof(SachManager.Properties.TenSach)
                    , SachManager.Properties.TenSach);
                setColumn(gdvDMSach.Columns[2]
                    , nameof(SachManager.Properties.TenTacGia)
                    , SachManager.Properties.TenTacGia);
                setColumn(gdvDMSach.Columns[3]
                    , nameof(SachManager.Properties.LinhVucSach)
                    , SachManager.Properties.LinhVucSach);
                setColumn(gdvDMSach.Columns[4]
                    , nameof(SachManager.Properties.NXB)
                    , SachManager.Properties.NXB);
                setColumn(gdvDMSach.Columns[5]
                    , nameof(SachManager.Properties.TongSoLuongNXBNoTheoThang)
                    , SachManager.Properties.TongSoLuongNXBNoTheoThang);
                setColumn(gdvDMSach.Columns[6]
                    , nameof(SachManager.Properties.TongTienNXBNoTheoThang)
                    , SachManager.Properties.TongTienNXBNoTheoThang);
            }
            if (x == 2)
            {
                gdvDMSach.AutoGenerateColumns = false;
                gdvDMSach.ColumnCount = 7;
                setColumn(gdvDMSach.Columns[0]
                    , nameof(SachManager.Properties.MaSoSach)
                    , SachManager.Properties.MaSoSach);
                setColumn(gdvDMSach.Columns[1]
                    , nameof(SachManager.Properties.TenSach)
                    , SachManager.Properties.TenSach);
                setColumn(gdvDMSach.Columns[2]
                    , nameof(SachManager.Properties.TenTacGia)
                    , SachManager.Properties.TenTacGia);
                setColumn(gdvDMSach.Columns[3]
                    , nameof(SachManager.Properties.LinhVucSach)
                    , SachManager.Properties.LinhVucSach);
                setColumn(gdvDMSach.Columns[4]
                    , nameof(SachManager.Properties.NXB)
                    , SachManager.Properties.NXB);
                setColumn(gdvDMSach.Columns[5]
                    , nameof(SachManager.Properties.TongSoLuongDaiLyNoTheoThang)
                    , SachManager.Properties.TongSoLuongDaiLyNoTheoThang);
                setColumn(gdvDMSach.Columns[6]
                    , nameof(SachManager.Properties.TongTienDaiLyNoTheoTang)
                    , SachManager.Properties.TongTienDaiLyNoTheoTang);
            }

        }

        private void setColumn(DataGridViewColumn column, string propertyName, string name)
        {
            column.Name = propertyName;
            column.DataPropertyName = propertyName;
            column.HeaderText = name;
        }
        #endregion

        private void btIn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xuất tạo file báo cáo", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (x == 1)
                {
                    var printer = new PrintHelper();
                    string x = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "";

                    string tenfile = x + "ReportCongNoNXB.pdf";
                    printer.FileName = tenfile;
                    printer.FolderPath = "D://Report";
                    printer.Title = "Báo cáo Công nợ Nhà Xuất Bản";
                    var startDate = new DateTime(_startYear, _startMonth, 1);
                    var endDate = new DateTime(_endYear, _endMonth, 1);
                    endDate.AddMonths(1).AddDays(-1);
                    printer.printCongNoNXB(_DMSachNXB, startDate, endDate);
                    MessageBox.Show("Đã tạo file thành công , Tên file là : " + tenfile);
                }
                if (x == 2)
                {
                    var printer = new PrintHelper();
                    string x = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "";

                    string tenfile = x + "ReportCongNoDaiLy.pdf";
                    printer.FileName = tenfile;
                    printer.FolderPath = "D://Report";
                    printer.Title = "Báo cáo Công nợ Đại lý";
                    var startDate = new DateTime(_startYear, _startMonth, 1);
                    var endDate = new DateTime(_endYear, _endMonth, 1);
                    endDate.AddMonths(1).AddDays(-1);
                    printer.printCongNoDaiLy(_DMSachDLy, startDate, endDate);
                    MessageBox.Show("Đã tạo file thành công , Tên file là : " + tenfile);
                }
                //var redListTextFont = FontFactory.RegisterDirectory(Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts");
                //var _bold = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 10f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                //var _bold1 = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 10f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                //PdfPTable pdfTable = new PdfPTable(gdvDMSach.ColumnCount);
                //pdfTable.DefaultCell.Padding = 3;
                //pdfTable.WidthPercentage = 30;
                //pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
                //pdfTable.DefaultCell.BorderWidth = 1;
                //pdfTable.TotalWidth = 630f;
                //pdfTable.LockedWidth = true;
                //float[] widths = new float[] { 30f, 100f,100f,100f,100f,100f,100f };
                //pdfTable.SetWidths(widths);


                ////Adding Header row
                //foreach (DataGridViewColumn column in gdvDMSach.Columns)
                //{
                //    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, _bold));
                //    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);

                //    pdfTable.AddCell(cell);
                //}

                ////Adding DataRow
                //foreach (DataGridViewRow row in gdvDMSach.Rows)
                //{
                //    foreach (DataGridViewCell cell in row.Cells)
                //    {
                //        if (!String.IsNullOrEmpty(Convert.ToString(cell.Value)))
                //            pdfTable.AddCell(new Phrase(cell.Value.ToString(), _bold1));
                //    }
                //}

                ////Exporting to PDF
                //string folderPath = @"C:\Users\huy\Desktop\Report\";
                //string x = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "";

                //string tenfile = x + "ReportThongKeCongNo.pdf";
                //if (!Directory.Exists(folderPath))
                //{
                //    Directory.CreateDirectory(folderPath);
                //}
                //using (FileStream stream = new FileStream(folderPath + tenfile, FileMode.Create))
                //{
                //    string loai = ""; 
                //    if (rabtnDaiLy.Checked == true)
                //        loai = "Đại lý";
                //    if (rabtnNXB.Checked == true)
                //        loai = "Nhà xuất bản";
                //    Document pdfDoc = new Document(PageSize.A3, 100f, 100f, 100f, 0);
                //    PdfWriter.GetInstance(pdfDoc, stream);
                //    pdfDoc.Open();
                //    var FontColour = new BaseColor(255, 0, 0);
                //    var _bold2 = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 20f, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);
                //    Paragraph docTitle = new Paragraph("Thống kê công nợ " + loai + "\n", _bold2);
                //    Paragraph docTitle1 = new Paragraph("Từ tháng : " + cmbStartMonth.Text + " Năm  " + cmbStartYear.Text +  "\n", _bold2);
                //    Paragraph docTitle2 = new Paragraph("đến tháng : " + cmbEndMonth.Text + " Năm  " + cmbEndYear.Text + "\n", _bold2);
                //    docTitle.Alignment = Element.ALIGN_LEFT;
                //    docTitle1.Alignment = Element.ALIGN_LEFT;
                //    docTitle2.Alignment = Element.ALIGN_LEFT;
                //    pdfDoc.Add(docTitle);
                //    pdfDoc.Add(docTitle1);
                //    pdfDoc.Add(docTitle2);
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

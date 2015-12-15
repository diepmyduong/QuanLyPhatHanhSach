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
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace WinForm.Views
{
    public partial class frmThongKeLoNhap : Form
    {
        public frmThongKeLoNhap(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }

        #region Private Properties
        private Form _frmParent;
        private List<Sach> _DMSach;
        private int _startMonth;
        private int _startYear;
        private int _endMonth;
        private int _endYear;
        private Sach _currentSach;
        private decimal? _tongLuongNhap;
        private CultureInfo _cultureInfo;
        #endregion

        #region Form Control Listenner
        private void frmThongKeLoNhap_Load(object sender, EventArgs e)
        {
            _cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
            createGridViewColumns();
            _startMonth = 1;
            _startYear = DateTime.Now.Year;
            _endMonth = 12;
            _endYear = DateTime.Now.Year;
            //Load combobox tháng và năm
            cmbStartMonth.DataSource = new BindingSource(FilterHelper.Monthes, null);
            cbEndMonth.DataSource = new BindingSource(FilterHelper.Monthes, null);
            cmbStartMonth.DisplayMember = "Key";
            cbEndMonth.DisplayMember = "Key";
            cmbStartMonth.ValueMember = "Value";
            cbEndMonth.ValueMember = "Value";
            cmbStartYear.DataSource = new BindingSource(FilterHelper.Years, null);
            cmbEndYear.DataSource = new BindingSource(FilterHelper.Years, null);
            cmbStartYear.DisplayMember = "Key";
            cmbEndYear.DisplayMember = "Key";
            cmbStartYear.ValueMember = "Value";
            cmbEndYear.ValueMember = "Value";


            //Set Tháng năm mặc định

            cmbStartMonth.SelectedValue = 1;
            cmbStartYear.SelectedValue = _startYear;
            cbEndMonth.SelectedValue = 12;
            cmbEndYear.SelectedValue = _endYear;
            loadSach();
        }

        private void cmbStartMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadSach();
        }

        private void cmbStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadSach();
        }

        private void cbEndMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadSach();
        }
        private void cmbEndYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadSach();
        }

        private void btLoc_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbLoc_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txbLoc_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
      

        private void gdvLoNhap_SelectionChanged(object sender, EventArgs e)
        {
            int index = gdvLoNhap.CurrentRow.Index;
            _currentSach = (gdvLoNhap.DataSource as List<Sach>)[index];
        }
        #endregion

        #region Form Services
        public void loadSach()
        {
            _DMSach = SachManager.getAllAlive()
                .Where(p => p.tongSoLuongNhapTheoThang(_startMonth, _startYear, _endMonth, _endYear) > 0
                && p.tongTienNhapTheoThang(_startMonth, _startYear, _endMonth, _endYear) > 0).ToList() ;
            lbTongLuongNhap.Text = _DMSach.Sum(s => s.SoLuongNhapTheoThang).ToString();
            gdvLoNhap.DataSource = _DMSach;
        }
        private void createGridViewColumns()
        {
            gdvLoNhap.AutoGenerateColumns = false;
            gdvLoNhap.ColumnCount = 6;
            setColumn(gdvLoNhap.Columns[0]
                , nameof(SachManager.Properties.MaSoSach)
                , SachManager.Properties.MaSoSach);
            setColumn(gdvLoNhap.Columns[1]
                , nameof(SachManager.Properties.TenSach)
                , SachManager.Properties.TenSach);
            setColumn(gdvLoNhap.Columns[2]
                , nameof(SachManager.Properties.TenTacGia)
                , SachManager.Properties.TenTacGia);
            setColumn(gdvLoNhap.Columns[3]
                , nameof(SachManager.Properties.NXB)
                , SachManager.Properties.NXB);
            setColumn(gdvLoNhap.Columns[4]
                , nameof(SachManager.Properties.SoLuongNhapTheoThang)
                , SachManager.Properties.SoLuongNhapTheoThang);
            setColumn(gdvLoNhap.Columns[5]
                , nameof(SachManager.Properties.TongTienNhapTheoThang)
                , SachManager.Properties.TongTienNhapTheoThang);

        }

        private void setColumn(DataGridViewColumn column, string propertyName, string name)
        {
            column.Name = propertyName;
            column.DataPropertyName = propertyName;
            column.HeaderText = name;
        }

        //private void reloadGridView()
        //{
        //    if (gdvLoNhap.DataSource!= null && _startYear != 0 && _startYear != 0 && _endMonth != 0 && _endYear != 0)
        //    {
        //        _tongLuongNhap = 0;
        //        foreach (DataGridViewRow row in gdvLoNhap.Rows)
        //        {
        //            Sach sach = row.DataBoundItem as Sach;
        //            decimal? soLuongNhap = sach.tongSoLuongNhapTheoThang(_startMonth, _startYear, _endMonth, _endYear);
        //            decimal? tienNhap = sach.tongTienNhapTheoThang(_startMonth, _startYear, _endMonth, _endYear);
        //            gdvLoNhap.Rows[row.Index].Cells[nameof(SachManager.Properties.SoLuongNhapTheoThang)].Value
        //                = soLuongNhap.ToString();
        //            gdvLoNhap.Rows[row.Index].Cells[nameof(SachManager.Properties.TongTienNhapTheoThang)].Value
        //                = tienNhap.ToString();
        //            _tongLuongNhap += soLuongNhap;
        //        }
        //        lbTongLuongNhap.Text = _tongLuongNhap.ToString();
        //    }
                
        //}
        private string money(decimal m)
        {
            return String.Format(_cultureInfo, "{0:c}", m);
        }


        #endregion

        private void btIn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xuất tạo file báo cáo", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var printer = new PrintHelper();
                string x = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "";

                string tenfile = x + "ReportLoNhap.pdf";
                printer.FileName = tenfile;
                printer.FolderPath = "D://Report";
                printer.Title = "Báo cáo Lô nhập";
                var startDate = new DateTime(_startYear, _startMonth, 1);
                var endDate = new DateTime(_endYear, _endMonth, 1);
                endDate.AddMonths(1).AddDays(-1);
                printer.printLoNhap(_DMSach, startDate, endDate);
                MessageBox.Show("Đã tạo file thành công , Tên file là : " + tenfile);
                //var redListTextFont = FontFactory.RegisterDirectory(Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts");
                //var _bold = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 10f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                //var _bold1 = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 10f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                //PdfPTable pdfTable = new PdfPTable(gdvLoNhap.ColumnCount);
                //pdfTable.DefaultCell.Padding = 3;
                //pdfTable.WidthPercentage = 30;
                //pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
                //pdfTable.DefaultCell.BorderWidth = 1;
                //pdfTable.TotalWidth = 550f;
                //pdfTable.LockedWidth = true;
                //float[] widths = new float[] { 50f, 100f, 100f, 100f, 100f, 100f };
                //pdfTable.SetWidths(widths);


                ////Adding Header row
                //foreach (DataGridViewColumn column in gdvLoNhap.Columns)
                //{
                //    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, _bold));
                //    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);

                //    pdfTable.AddCell(cell);
                //}

                ////Adding DataRow
                //foreach (DataGridViewRow row in gdvLoNhap.Rows)
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

                //string tenfile = x + "ReportThongKeLoNhap.pdf";
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
                //    Paragraph docTitle = new Paragraph("Thống kê lô nhập " +  "\n", _bold2);
                //    Paragraph docTitle1 = new Paragraph("Từ tháng  : " + cmbStartMonth.Text + " Năm  " + cmbStartYear.Text + "\n", _bold2);
                //    Paragraph docTitle2 = new Paragraph("đến tháng : " + cbEndMonth.Text + " Năm  " + cmbEndYear.Text + "\n", _bold2);
                //    Paragraph docTitle3 = new Paragraph("Tổng số lượng : " + lbTongLuongNhap.Text+ "\n", _bold2);
                //    docTitle.Alignment = Element.ALIGN_LEFT;
                //    docTitle1.Alignment = Element.ALIGN_LEFT;
                //    docTitle2.Alignment = Element.ALIGN_LEFT;
                //    docTitle3.Alignment = Element.ALIGN_LEFT;
                //    pdfDoc.Add(docTitle);
                //    pdfDoc.Add(docTitle1);
                //    pdfDoc.Add(docTitle2);
                //    pdfDoc.Add(docTitle3);
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (_currentSach != null)
            {
                frmChiTietLoNhap form = new frmChiTietLoNhap(this, _currentSach);
                form.ShowDialog(this);
            }
            else
                MessageBox.Show("Chọn sách cần xem");
        }
    }
}

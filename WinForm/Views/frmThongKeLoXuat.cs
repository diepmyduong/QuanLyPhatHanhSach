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
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace WinForm.Views
{
    public partial class frmThongKeLoXuat : Form
    {
        public frmThongKeLoXuat(Form parent)
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
        private decimal? _tongLuongXuat;
        private CultureInfo _cultureInfo;
        #endregion

        #region Form Control Listener
        private void frmThongKeLoXuat_Load(object sender, EventArgs e)
        {
            _cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
            createGridViewColumns();
            _startMonth = 1;
            _startYear = DateTime.Now.Year;
            _endMonth = 12;
            _endYear = DateTime.Now.Year;
            //Load combobox tháng và năm
            cmbStartMonth.DataSource = new BindingSource(FilterHelper.Monthes, null);
            cmbEndMonth.DataSource = new BindingSource(FilterHelper.Monthes, null);
            cmbStartMonth.DisplayMember = cmbEndMonth.DisplayMember = "Key";
            cmbStartMonth.ValueMember = cmbEndMonth.ValueMember = "Value";
            cmbStartYear.DataSource = new BindingSource(FilterHelper.Years, null);
            cmbEndYear.DataSource = new BindingSource(FilterHelper.Years, null);
            cmbStartYear.DisplayMember = cmbEndYear.DisplayMember = "Key";
            cmbStartYear.ValueMember = cmbEndYear.ValueMember = "Value";

            //Set Tháng năm mặc định
            cmbStartMonth.SelectedValue = 1;
            cmbStartYear.SelectedValue = _startYear;
            cmbEndMonth.SelectedValue = 12;
            cmbEndYear.SelectedValue = _endYear;
            loadSach();
        }

        private void cmbStartMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadSach();
            reloadGridView();
        }

        private void cmbStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadSach();
            reloadGridView();
        }

        private void cmbEndMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadSach();
            reloadGridView();
        }

        private void cmbEndYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            loadSach();
            reloadGridView();
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
        #endregion
        public void loadSach()
        {
            _DMSach = SachManager.getAllAlive().Where(s => s.tongSoLuongXuatTheoThang(_startMonth,_startYear,_endMonth,_endYear)  > 0).ToList();

            gdvLoXuat.DataSource = _DMSach;
        }
        private void createGridViewColumns()
        {
            gdvLoXuat.AutoGenerateColumns = false;
            gdvLoXuat.ColumnCount = 6;
            setColumn(gdvLoXuat.Columns[0]
                , nameof(SachManager.Properties.MaSoSach)
                , SachManager.Properties.MaSoSach);
            setColumn(gdvLoXuat.Columns[1]
                , nameof(SachManager.Properties.TenSach)
                , SachManager.Properties.TenSach);
            setColumn(gdvLoXuat.Columns[2]
                , nameof(SachManager.Properties.TenTacGia)
                , SachManager.Properties.TenTacGia);
            setColumn(gdvLoXuat.Columns[3]
                , nameof(SachManager.Properties.NXB)
                , SachManager.Properties.NXB);
            setColumn(gdvLoXuat.Columns[4]
                , nameof(SachManager.Properties.SoLuongXuatTheoThang)
                , SachManager.Properties.SoLuongXuatTheoThang);
            setColumn(gdvLoXuat.Columns[5]
                , nameof(SachManager.Properties.TongTienXuatTheoThang)
                , SachManager.Properties.TongTienXuatTheoThang);
            gdvLoXuat.Columns[0].Width = 120;
            gdvLoXuat.Columns[1].Width = 120;
            gdvLoXuat.Columns[2].Width = 120;
            gdvLoXuat.Columns[3].Width = 120;
            gdvLoXuat.Columns[4].Width = 120;
            gdvLoXuat.Columns[5].Width = 120;

        }

        private void setColumn(DataGridViewColumn column, string propertyName, string name)
        {
            column.Name = propertyName;
            column.DataPropertyName = propertyName;
            column.HeaderText = name;
        }
        private void reloadGridView()
        {
            if (gdvLoXuat.DataSource != null && _startYear != 0 && _startYear != 0 && _endMonth != 0 && _endYear != 0)
            {
                _tongLuongXuat = 0;
                foreach (DataGridViewRow row in gdvLoXuat.Rows)
                {
                    Sach sach = row.DataBoundItem as Sach;
                    decimal? soLuongXuat = sach.tongSoLuongXuatTheoThang(_startMonth, _startYear, _endMonth, _endYear);
                    decimal? tienXuat = sach.tongTienXuatTheoThang(_startMonth, _startYear, _endMonth, _endYear);
                    gdvLoXuat.Rows[row.Index].Cells[nameof(SachManager.Properties.SoLuongXuatTheoThang)].Value
                        = soLuongXuat.ToString();
                    gdvLoXuat.Rows[row.Index].Cells[nameof(SachManager.Properties.TongTienXuatTheoThang)].Value
                        = tienXuat.ToString();
                    _tongLuongXuat += soLuongXuat;
                }
                lbTongLuongXuat.Text = _tongLuongXuat.ToString();
            }

        }
        private string money(decimal m)
        {
            return String.Format(_cultureInfo, "{0:c}", m);
        }

        private void gdvLoXuat_SelectionChanged(object sender, EventArgs e)
        {
            int index = gdvLoXuat.CurrentRow.Index;
            _currentSach = (gdvLoXuat.DataSource as List<Sach>)[index];
        }

        private void gdvLoXuat_DataSourceChanged(object sender, EventArgs e)
        {
            reloadGridView();
        }

        private void btIn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xuất tạo file báo cáo", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var printer = new PrintHelper();
                string x = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "";

                string tenfile = x + "ReportLoXuat.pdf";
                printer.FileName = tenfile;
                printer.FolderPath = "D://Report";
                printer.Title = "Báo cáo Lô xuất";
                var startDate = new DateTime(_startYear, _startMonth, 1);
                var endDate = new DateTime(_endYear, _endMonth, 1);
                endDate.AddMonths(1).AddDays(-1);
                printer.printLoXuat(_DMSach, startDate, endDate);
                MessageBox.Show("Đã tạo file thành công , Tên file là : " + tenfile);
                //var redListTextFont = FontFactory.RegisterDirectory(Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts");
                //var _bold = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 10f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                //var _bold1 = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 10f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                //PdfPTable pdfTable = new PdfPTable(gdvLoXuat.ColumnCount);
                //pdfTable.DefaultCell.Padding = 3;
                //pdfTable.WidthPercentage = 30;
                //pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
                //pdfTable.DefaultCell.BorderWidth = 1;
                //pdfTable.TotalWidth = 550f;
                //pdfTable.LockedWidth = true;
                //float[] widths = new float[] { 50f, 100f, 100f, 100f, 100f, 100f };
                //pdfTable.SetWidths(widths);


                ////Adding Header row
                //foreach (DataGridViewColumn column in gdvLoXuat.Columns)
                //{
                //    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, _bold));
                //    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);

                //    pdfTable.AddCell(cell);
                //}

                ////Adding DataRow
                //foreach (DataGridViewRow row in gdvLoXuat.Rows)
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

                //string tenfile = x + "ReportThongKeLoXuat.pdf";
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
                //    Paragraph docTitle = new Paragraph("Thống kê lô xuất " + "\n", _bold2);
                //    Paragraph docTitle1 = new Paragraph("Từ tháng  : " + cmbStartMonth.Text + " Năm  " + cmbStartYear.Text + "\n", _bold2);
                //    Paragraph docTitle2 = new Paragraph("đến tháng : " + cmbEndMonth.Text + " Năm  " + cmbEndYear.Text + "\n", _bold2);
                //    Paragraph docTitle3 = new Paragraph("Tổng số lượng : " + lbTongLuongXuat.Text + "\n", _bold2);
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

        private void btXem_Click(object sender, EventArgs e)
        {
            if (_currentSach != null)
            {
                frmChiTietLoXuat form = new frmChiTietLoXuat(this, _currentSach);
                form.ShowDialog(this);
            }
            else
                MessageBox.Show("Chọn sách cần xem");
        }
    }
}

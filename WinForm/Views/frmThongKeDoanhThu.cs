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
    public partial class frmThongKeDoanhThu : Form
    {
        public frmThongKeDoanhThu(Form parent)
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
        private List<DaiLy> _DSDaiLy;
        private List<NhaXuatBan> _DSNXB;
        private DaiLy _CurrentDaiLy;
        private NhaXuatBan _CurrentNXB;

        #endregion

        #region Form Control Listener
        private void frmThongKeDoanhThu_Load(object sender, EventArgs e)
        {

            _startMonth = 1;
            _startYear = DateTime.Now.Year;
            _endMonth = 12;
            _endYear = DateTime.Now.Year;
            createGridViewColumns();
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
            LoadTT();
        }

        private void LoadTT()
        {
            _DSDaiLy = DaiLyManager.getAllAlive()
                                    .Where(dl => dl.tinhTongTienThanhToanTheoThang(_startMonth,_startYear,_endMonth, _endYear) > 0).ToList();
            gdvDaiLy.DataSource = _DSDaiLy;
            lbTongThu.Text = _DSDaiLy.Sum(s => s.TongTienThanhToanTheoThang) + "";
            int tongthu = int.Parse(lbTongThu.Text.ToString());

            _DSNXB = NhaXuatBanManager.getAllAlive()
                .Where(dl => dl.tinhTongTienThanhToanTheoThang(_startMonth, _startYear, _endMonth, _endYear) > 0).ToList();
            gdvNXB.DataSource = _DSNXB;
            lbTongChi.Text = _DSNXB.Sum(s => s.TongTienThanhToanTheoThang) + "";
            int tongchi = int.Parse(lbTongChi.Text.ToString());
            lbLoiNhuan.Text= tongthu - tongchi + "";

        }


        private void cmbStartMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            LoadTT();
        }

        private void cmbStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            LoadTT();
        }

        private void cmbEndMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endMonth = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            LoadTT();
        }

        private void cmbEndYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endYear = ((KeyValuePair<string, int>)((ComboBox)sender).SelectedItem).Value;
            LoadTT();
        }

        private void btnNguonThu_Click(object sender, EventArgs e)
        {
            frmThongKeNguonThu form = new frmThongKeNguonThu(this,_CurrentDaiLy);
            form.ShowDialog(this);
        }

        private void btnNguonChi_Click(object sender, EventArgs e)
        {
            frmThongKeNguonChi form = new frmThongKeNguonChi(this,_CurrentNXB);
            form.ShowDialog(this);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbLocDaiLy_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txbLocDaiLy_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txbLocNXB_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txbLocNXB_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        #endregion

        private void gdvDaiLy_SelectionChanged(object sender, EventArgs e)
        {
            int index = gdvDaiLy.CurrentRow.Index;
            _CurrentDaiLy = (gdvDaiLy.DataSource as List<DaiLy>)[index];
        }

        private void gdvNXB_SelectionChanged(object sender, EventArgs e)
        {
            int index = gdvNXB.CurrentRow.Index;
            _CurrentNXB = (gdvNXB.DataSource as List<NhaXuatBan>)[index];
        }
        private void createGridViewColumns()
        {
            gdvDaiLy.AutoGenerateColumns = false;
            gdvDaiLy.ColumnCount = 4;
            setColumn(gdvDaiLy.Columns[0]
                , nameof(DaiLyManager.Properties.MaSoDaiLy)
                , DaiLyManager.Properties.MaSoDaiLy);
            setColumn(gdvDaiLy.Columns[1]
                , nameof(DaiLyManager.Properties.TenDaiLy)
                , DaiLyManager.Properties.TenDaiLy);
            setColumn(gdvDaiLy.Columns[2]
                , nameof(DaiLyManager.Properties.TongTienThanhToan)
                , DaiLyManager.Properties.TongTienThanhToan);
            setColumn(gdvDaiLy.Columns[3]
                , nameof(DaiLyManager.Properties.TongTienThanhToanTheoThang)
                , DaiLyManager.Properties.TongTienThanhToanTheoThang);
            gdvDaiLy.Columns[0].Width = 50;
            gdvDaiLy.Columns[1].Width = 90;
            gdvDaiLy.Columns[2].Width = 90;
            gdvDaiLy.Columns[3].Width = 100;

            gdvNXB.AutoGenerateColumns = false;
            gdvNXB.ColumnCount = 4;
            setColumn(gdvNXB.Columns[0]
                , nameof(NhaXuatBanManager.Properties.MaSoNXB)
                , NhaXuatBanManager.Properties.MaSoNXB);
            setColumn(gdvNXB.Columns[1]
                , nameof(NhaXuatBanManager.Properties.TenNXB)
                , NhaXuatBanManager.Properties.TenNXB);
            setColumn(gdvNXB.Columns[2]
                , nameof(NhaXuatBanManager.Properties.TongTienThanhToan)
                , NhaXuatBanManager.Properties.TongTienThanhToan);
            setColumn(gdvNXB.Columns[3]
                , nameof(NhaXuatBanManager.Properties.TongTienThanhToanTheoThang)
                , NhaXuatBanManager.Properties.TongTienThanhToanTheoThang);
            gdvNXB.Columns[0].Width = 50;
            gdvNXB.Columns[1].Width = 90;
            gdvNXB.Columns[2].Width = 90;
            gdvNXB.Columns[3].Width = 100;
        }

        private void setColumn(DataGridViewColumn column, string propertyName, string name)
        {
            column.Name = propertyName;
            column.DataPropertyName = propertyName;
            column.HeaderText = name;
        }

        private void btIn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xuất tạo file báo cáo", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var printer = new PrintHelper();
                string x = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "";

                string tenfile = x + "ReportDoanhThu.pdf";
                printer.FileName = tenfile;
                printer.FolderPath = "D://Report";
                printer.Title = "Báo cáo Doanh Thu";
                var startDate = new DateTime(_startYear, _startMonth, 1);
                var endDate = new DateTime(_endYear, _endMonth, 1);
                endDate.AddMonths(1).AddDays(-1);
                printer.printDoanhThu(_DSDaiLy,_DSNXB, startDate, endDate);
                MessageBox.Show("Đã tạo file thành công , Tên file là : " + tenfile);
                //var redListTextFont = FontFactory.RegisterDirectory(Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts");
                //var _bold = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 10f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                //var _bold1 = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 10f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                //PdfPTable pdfTable = new PdfPTable(gdvDaiLy.ColumnCount);
                //pdfTable.DefaultCell.Padding = 3;
                //pdfTable.WidthPercentage = 30;
                //pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
                //pdfTable.DefaultCell.BorderWidth = 1;
                //pdfTable.TotalWidth = 350f;
                //pdfTable.LockedWidth = true;
                //float[] widths = new float[] { 50f, 100f, 100f, 100f };
                //pdfTable.SetWidths(widths);

                //PdfPTable pdfTable1 = new PdfPTable(gdvNXB.ColumnCount);
                //pdfTable1.DefaultCell.Padding = 3;
                //pdfTable1.WidthPercentage = 30;
                //pdfTable1.HorizontalAlignment = Element.ALIGN_CENTER;
                //pdfTable1.DefaultCell.BorderWidth = 1;
                //pdfTable1.TotalWidth = 350f;
                //pdfTable1.LockedWidth = true;
                //pdfTable1.SetWidths(widths);


                ////Adding Header row
                //foreach (DataGridViewColumn column in gdvDaiLy.Columns)
                //{
                //    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, _bold));
                //    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);

                //    pdfTable.AddCell(cell);
                //}
                //foreach (DataGridViewColumn column in gdvNXB.Columns)
                //{
                //    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, _bold));
                //    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);

                //    pdfTable1.AddCell(cell);
                //}

                ////Adding DataRow
                //foreach (DataGridViewRow row in gdvDaiLy.Rows)
                //{
                //    foreach (DataGridViewCell cell in row.Cells)
                //    {
                //        if (!String.IsNullOrEmpty(Convert.ToString(cell.Value)))
                //            pdfTable.AddCell(new Phrase(cell.Value.ToString(), _bold1));
                //    }
                //}
                //foreach (DataGridViewRow row in gdvNXB.Rows)
                //{
                //    foreach (DataGridViewCell cell in row.Cells)
                //    {
                //        if (!String.IsNullOrEmpty(Convert.ToString(cell.Value)))
                //            pdfTable1.AddCell(new Phrase(cell.Value.ToString(), _bold1));
                //    }
                //}

                ////Exporting to PDF
                //string folderPath = @"C:\Users\huy\Desktop\Report\";
                //string x = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "";

                //string tenfile = x + "ReportThongKeDoanhThu.pdf";
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
                //    Paragraph docTitle = new Paragraph("Thống kê doanh thu " + "\n", _bold2);
                //    Paragraph docTitle1 = new Paragraph("Từ tháng        : " + cmbStartMonth.Text + " Năm  " + cmbStartYear.Text + "\n", _bold2);
                //    Paragraph docTitle2 = new Paragraph("đến tháng       : " + cmbEndMonth.Text + " Năm  " + cmbEndYear.Text + "\n", _bold2);
                //    Paragraph docTitle3 = new Paragraph("Tổng thu        : " + lbTongThu.Text + "\n", _bold2);
                //    Paragraph docTitle4 = new Paragraph("Tổng chi        : " + lbTongChi.Text + "\n", _bold2);
                //    Paragraph docTitle5 = new Paragraph("Tổng lợi nhuận  : " + lbLoiNhuan.Text + "\n", _bold2);
                //    docTitle.Alignment = Element.ALIGN_LEFT;
                //    docTitle1.Alignment = Element.ALIGN_LEFT;
                //    docTitle2.Alignment = Element.ALIGN_LEFT;
                //    docTitle3.Alignment = Element.ALIGN_LEFT;
                //    docTitle4.Alignment = Element.ALIGN_LEFT;
                //    docTitle5.Alignment = Element.ALIGN_LEFT;
                //    pdfDoc.Add(docTitle);
                //    pdfDoc.Add(docTitle1);
                //    pdfDoc.Add(docTitle2);
                //    pdfDoc.Add(docTitle3);
                //    pdfDoc.Add(docTitle4);
                //    pdfDoc.Add(docTitle5);
                //    pdfDoc.Add(new Paragraph("\n"));
                //    pdfDoc.Add(new Paragraph("\n"));
                //    Paragraph docTitle6 = new Paragraph("Nguồn thu : " + "\n", _bold2);
                //    docTitle6.Alignment = Element.ALIGN_CENTER;
                //    pdfDoc.Add(docTitle6);
                //    pdfDoc.Add(new Paragraph("\n"));

                //    pdfDoc.Add(pdfTable);

                //    Paragraph docTitle7 = new Paragraph("Nguồn chi : " + "\n", _bold2);
                //    docTitle7.Alignment = Element.ALIGN_CENTER;
                //    pdfDoc.Add(docTitle7);
                //    pdfDoc.Add(new Paragraph("\n"));

                //    pdfDoc.Add(pdfTable1);

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

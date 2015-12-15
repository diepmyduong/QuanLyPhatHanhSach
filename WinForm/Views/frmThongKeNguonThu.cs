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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace WinForm.Views
{
    public partial class frmThongKeNguonThu : Form
    {
        public frmThongKeNguonThu(Form parent)
        {
            InitializeComponent();
            _frmParent = parent;
        }
        public frmThongKeNguonThu(Form parent, DaiLy dl)
            :this(parent)
        {
            _CurrentDaiLy = dl;
        }

        #region Private Properties
        private Form _frmParent;
        private int _startMonth;
        private int _startYear;
        private int _endMonth;
        private int _endYear;
        private List<HoaDonDaiLy> _HDDaiLy;
        private DaiLy _CurrentDaiLy;
        #endregion

        #region Form Control Listener
        private void frmThongKeNguonThu_Load(object sender, EventArgs e)
        {
            _startMonth = 1;
            _startYear = DateTime.Now.Year;
            _endMonth = 12;
            _endYear = DateTime.Now.Year;
            //Load combobox tháng và năm
            createGridViewColumns();
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
            if (_CurrentDaiLy != null)
            {
                lbMa.Text = _CurrentDaiLy.MaSoDaiLy + "";
                lbTen.Text = _CurrentDaiLy.TenDaiLy;
            }
        }
        private void LoadTT()
        {
            if (_CurrentDaiLy != null)
            {
                _HDDaiLy = _CurrentDaiLy.getHoaDonTheoThang(_startMonth, _startYear, _endMonth, _endYear).ToList();
                gdvHoaDon.DataSource = _HDDaiLy;
                lbTongTienThu.Text = _HDDaiLy.Sum(p => p.TongTien) + "";
            }
            
           
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

        private void btLoc_Click(object sender, EventArgs e)
        {

        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            frmChiTietHoaDonDaiLy form = new frmChiTietHoaDonDaiLy(this);
            form.ShowDialog(this);
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
        private void createGridViewColumns()
        {
            gdvHoaDon.AutoGenerateColumns = false; // Bỏ auto generate Columns
            gdvHoaDon.ColumnCount = 5; // Xác định số columns có
            setColumn(gdvHoaDon.Columns[0]
                , nameof(HoaDonDaiLyManager.Properties.MaSoHoaDon)
                , HoaDonDaiLyManager.Properties.MaSoHoaDon);
            gdvHoaDon.Columns[0].Width = 130;
            gdvHoaDon.Columns[1].Width = 130;
            gdvHoaDon.Columns[2].Width = 150;
            gdvHoaDon.Columns[3].Width = 150;
            gdvHoaDon.Columns[4].Width = 150;
            setColumn(gdvHoaDon.Columns[1]
                , nameof(HoaDonDaiLyManager.Properties.MaSoDaiLy)
                , HoaDonDaiLyManager.Properties.MaSoDaiLy);
            setColumn(gdvHoaDon.Columns[2]
                , nameof(HoaDonDaiLyManager.Properties.DaiLy)
                , HoaDonDaiLyManager.Properties.DaiLy);
            setColumn(gdvHoaDon.Columns[3]
                , nameof(HoaDonDaiLyManager.Properties.NgayLap)
                ,HoaDonDaiLyManager.Properties.NgayLap);
            setColumn(gdvHoaDon.Columns[4]
                , nameof(HoaDonDaiLyManager.Properties.TongTien)
                , HoaDonDaiLyManager.Properties.TongTien);
            

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
                var redListTextFont = FontFactory.RegisterDirectory(Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts");
                var _bold = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 10f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                var _bold1 = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 10f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                PdfPTable pdfTable = new PdfPTable(gdvHoaDon.ColumnCount);
                pdfTable.DefaultCell.Padding = 3;
                pdfTable.WidthPercentage = 30;
                pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable.DefaultCell.BorderWidth = 1;
                pdfTable.TotalWidth = 500f;
                pdfTable.LockedWidth = true;
                float[] widths = new float[] { 100f, 100f, 100f, 100f, 100f };
                pdfTable.SetWidths(widths);


                //Adding Header row
                foreach (DataGridViewColumn column in gdvHoaDon.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, _bold));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);

                    pdfTable.AddCell(cell);
                }

                //Adding DataRow
                foreach (DataGridViewRow row in gdvHoaDon.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (!String.IsNullOrEmpty(Convert.ToString(cell.Value)))
                            pdfTable.AddCell(new Phrase(cell.Value.ToString(), _bold1));
                    }
                }

                //Exporting to PDF
                string folderPath = @"C:\Users\huy\Desktop\Report\";
                string x = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "";

                string tenfile = x + "ReportThongNguonThu.pdf";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                using (FileStream stream = new FileStream(folderPath + tenfile, FileMode.Create))
                {

                    Document pdfDoc = new Document(PageSize.A3, 100f, 100f, 100f, 0);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    var FontColour = new BaseColor(255, 0, 0);
                    var _bold2 = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 20f, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);
                    Paragraph docTitle = new Paragraph("Thống kê nguồn chi " + "\n", _bold2);
                    Paragraph docTitle4 = new Paragraph("Đại lý : " + lbTen.Text + "\n" + "Mã ĐL :" + lbMa.Text + "\n", _bold2);
                    Paragraph docTitle1 = new Paragraph("Từ tháng  : " + cmbStartMonth.Text + " Năm  " + cmbStartYear.Text + "\n", _bold2);
                    Paragraph docTitle2 = new Paragraph("đến tháng : " + cmbEndMonth.Text + " Năm  " + cmbEndYear.Text + "\n", _bold2);
                    Paragraph docTitle3 = new Paragraph("Tổng tiền thu : " + lbTongTienThu.Text + "\n", _bold2);
                    docTitle.Alignment = Element.ALIGN_LEFT;
                    docTitle4.Alignment = Element.ALIGN_LEFT;
                    docTitle1.Alignment = Element.ALIGN_LEFT;
                    docTitle2.Alignment = Element.ALIGN_LEFT;
                    docTitle3.Alignment = Element.ALIGN_LEFT;
                    pdfDoc.Add(docTitle);
                    pdfDoc.Add(docTitle4);
                    pdfDoc.Add(docTitle1);
                    pdfDoc.Add(docTitle2);
                    pdfDoc.Add(docTitle3);
                    pdfDoc.Add(new Paragraph("\n"));
                    pdfDoc.Add(new Paragraph("\n"));
                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();
                    stream.Close();

                    MessageBox.Show("Đã tạo file thành công , Tên file là : " + tenfile);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }
    }
}

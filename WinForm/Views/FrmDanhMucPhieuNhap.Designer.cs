namespace WinForm.Views
{
    partial class FrmDanhMucPhieuNhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelContainer = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbTongTien = new System.Windows.Forms.Label();
            this.gdvPhieuNhap = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btXoa = new System.Windows.Forms.Button();
            this.btLoadAll = new System.Windows.Forms.Button();
            this.btChuaDuyet = new System.Windows.Forms.Button();
            this.btThem = new System.Windows.Forms.Button();
            this.txbMaPhieuNhap = new System.Windows.Forms.TextBox();
            this.cmbNhaXuatBan = new System.Windows.Forms.ComboBox();
            this.dtpNgayLap = new System.Windows.Forms.DateTimePicker();
            this.txbNguoiGiao = new System.Windows.Forms.TextBox();
            this.btnNXB = new System.Windows.Forms.Button();
            this.btDuyet = new System.Windows.Forms.Button();
            this.panelContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdvPhieuNhap)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.tableLayoutPanel1);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Padding = new System.Windows.Forms.Padding(10);
            this.panelContainer.Size = new System.Drawing.Size(782, 557);
            this.panelContainer.TabIndex = 2;
            this.panelContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContainer_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbTongTien, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.gdvPhieuNhap, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txbMaPhieuNhap, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbNhaXuatBan, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtpNgayLap, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbNguoiGiao, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnNXB, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btDuyet, 3, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(764, 541);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã phiếu xuất :";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nhà xuất bản :";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ngày lập :";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Người Giao :";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(563, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 30);
            this.label5.TabIndex = 8;
            this.label5.Text = "Tổng tiền";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTongTien
            // 
            this.lbTongTien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lbTongTien.AutoSize = true;
            this.lbTongTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTongTien.ForeColor = System.Drawing.Color.Red;
            this.lbTongTien.Location = new System.Drawing.Point(595, 30);
            this.lbTongTien.Name = "lbTongTien";
            this.tableLayoutPanel1.SetRowSpan(this.lbTongTien, 2);
            this.lbTongTien.Size = new System.Drawing.Size(30, 60);
            this.lbTongTien.TabIndex = 9;
            this.lbTongTien.Text = "0";
            this.lbTongTien.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gdvPhieuNhap
            // 
            this.gdvPhieuNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gdvPhieuNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.gdvPhieuNhap, 4);
            this.gdvPhieuNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdvPhieuNhap.Location = new System.Drawing.Point(3, 123);
            this.gdvPhieuNhap.Name = "gdvPhieuNhap";
            this.gdvPhieuNhap.Size = new System.Drawing.Size(758, 365);
            this.gdvPhieuNhap.TabIndex = 10;
            this.gdvPhieuNhap.SelectionChanged += new System.EventHandler(this.gdvPhieuNhap_SelectionChanged);
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 4);
            this.flowLayoutPanel1.Controls.Add(this.btnThoat);
            this.flowLayoutPanel1.Controls.Add(this.btXoa);
            this.flowLayoutPanel1.Controls.Add(this.btLoadAll);
            this.flowLayoutPanel1.Controls.Add(this.btChuaDuyet);
            this.flowLayoutPanel1.Controls.Add(this.btThem);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 494);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(758, 44);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(648, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(107, 41);
            this.btnThoat.TabIndex = 0;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btXoa
            // 
            this.btXoa.Location = new System.Drawing.Point(535, 3);
            this.btXoa.Name = "btXoa";
            this.btXoa.Size = new System.Drawing.Size(107, 41);
            this.btXoa.TabIndex = 1;
            this.btXoa.Text = "Xóa";
            this.btXoa.UseVisualStyleBackColor = true;
            this.btXoa.Click += new System.EventHandler(this.btXoa_Click);
            // 
            // btLoadAll
            // 
            this.btLoadAll.Location = new System.Drawing.Point(422, 3);
            this.btLoadAll.Name = "btLoadAll";
            this.btLoadAll.Size = new System.Drawing.Size(107, 41);
            this.btLoadAll.TabIndex = 2;
            this.btLoadAll.Text = "Tất cả PN";
            this.btLoadAll.UseVisualStyleBackColor = true;
            this.btLoadAll.Click += new System.EventHandler(this.btLoadAll_Click);
            // 
            // btChuaDuyet
            // 
            this.btChuaDuyet.Location = new System.Drawing.Point(309, 3);
            this.btChuaDuyet.Name = "btChuaDuyet";
            this.btChuaDuyet.Size = new System.Drawing.Size(107, 41);
            this.btChuaDuyet.TabIndex = 3;
            this.btChuaDuyet.Text = "PN Chưa duyệt";
            this.btChuaDuyet.UseVisualStyleBackColor = true;
            this.btChuaDuyet.Click += new System.EventHandler(this.btChuaDuyet_Click);
            // 
            // btThem
            // 
            this.btThem.Location = new System.Drawing.Point(196, 3);
            this.btThem.Name = "btThem";
            this.btThem.Size = new System.Drawing.Size(107, 41);
            this.btThem.TabIndex = 4;
            this.btThem.Text = "Thêm phiếu";
            this.btThem.UseVisualStyleBackColor = true;
            this.btThem.Click += new System.EventHandler(this.btThem_Click);
            // 
            // txbMaPhieuNhap
            // 
            this.txbMaPhieuNhap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbMaPhieuNhap, 2);
            this.txbMaPhieuNhap.Enabled = false;
            this.txbMaPhieuNhap.Location = new System.Drawing.Point(117, 5);
            this.txbMaPhieuNhap.Name = "txbMaPhieuNhap";
            this.txbMaPhieuNhap.Size = new System.Drawing.Size(337, 20);
            this.txbMaPhieuNhap.TabIndex = 4;
            // 
            // cmbNhaXuatBan
            // 
            this.cmbNhaXuatBan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbNhaXuatBan.FormattingEnabled = true;
            this.cmbNhaXuatBan.Location = new System.Drawing.Point(117, 34);
            this.cmbNhaXuatBan.Name = "cmbNhaXuatBan";
            this.cmbNhaXuatBan.Size = new System.Drawing.Size(185, 21);
            this.cmbNhaXuatBan.TabIndex = 5;
            // 
            // dtpNgayLap
            // 
            this.dtpNgayLap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.dtpNgayLap, 2);
            this.dtpNgayLap.Location = new System.Drawing.Point(117, 65);
            this.dtpNgayLap.Name = "dtpNgayLap";
            this.dtpNgayLap.Size = new System.Drawing.Size(337, 20);
            this.dtpNgayLap.TabIndex = 6;
            // 
            // txbNguoiGiao
            // 
            this.txbNguoiGiao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbNguoiGiao, 2);
            this.txbNguoiGiao.Location = new System.Drawing.Point(117, 95);
            this.txbNguoiGiao.Name = "txbNguoiGiao";
            this.txbNguoiGiao.Size = new System.Drawing.Size(337, 20);
            this.txbNguoiGiao.TabIndex = 13;
            // 
            // btnNXB
            // 
            this.btnNXB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNXB.Location = new System.Drawing.Point(308, 33);
            this.btnNXB.Name = "btnNXB";
            this.btnNXB.Size = new System.Drawing.Size(146, 23);
            this.btnNXB.TabIndex = 12;
            this.btnNXB.Text = "Thêm NXB";
            this.btnNXB.UseVisualStyleBackColor = true;
            // 
            // btDuyet
            // 
            this.btDuyet.Location = new System.Drawing.Point(460, 93);
            this.btDuyet.Name = "btDuyet";
            this.btDuyet.Size = new System.Drawing.Size(301, 24);
            this.btDuyet.TabIndex = 4;
            this.btDuyet.Text = "Duyệt Phiếu nhập";
            this.btDuyet.UseVisualStyleBackColor = true;
            this.btDuyet.Click += new System.EventHandler(this.btDuyet_Click);
            // 
            // FrmDanhMucPhieuNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 557);
            this.Controls.Add(this.panelContainer);
            this.Name = "FrmDanhMucPhieuNhap";
            this.Text = "FrmDanhMucPhieuNhap";
            this.Load += new System.EventHandler(this.FrmDanhMucPhieuNhap_Load);
            this.panelContainer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdvPhieuNhap)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbTongTien;
        private System.Windows.Forms.DataGridView gdvPhieuNhap;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btXoa;
        private System.Windows.Forms.Button btLoadAll;
        private System.Windows.Forms.Button btChuaDuyet;
        private System.Windows.Forms.TextBox txbMaPhieuNhap;
        private System.Windows.Forms.ComboBox cmbNhaXuatBan;
        private System.Windows.Forms.DateTimePicker dtpNgayLap;
        private System.Windows.Forms.TextBox txbNguoiGiao;
        private System.Windows.Forms.Button btnNXB;
        private System.Windows.Forms.Button btDuyet;
        private System.Windows.Forms.Button btThem;
    }
}
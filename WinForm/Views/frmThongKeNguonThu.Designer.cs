﻿namespace WinForm.Views
{
    partial class frmThongKeNguonThu
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
            this.cmbEndMonth = new System.Windows.Forms.ComboBox();
            this.cmbEndYear = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbTongTienThu = new System.Windows.Forms.Label();
            this.txbLoc = new System.Windows.Forms.TextBox();
            this.btLoc = new System.Windows.Forms.Button();
            this.gdvHoaDon = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.cmbStartYear = new System.Windows.Forms.ComboBox();
            this.cmbStartMonth = new System.Windows.Forms.ComboBox();
            this.btnChiTiet = new System.Windows.Forms.Button();
            this.panelContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdvHoaDon)).BeginInit();
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
            this.panelContainer.Size = new System.Drawing.Size(784, 561);
            this.panelContainer.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmbEndMonth, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmbEndYear, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbTongTienThu, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbLoc, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.gdvHoaDon, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.cmbStartYear, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbStartMonth, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btLoc, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnChiTiet, 5, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
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
            this.label1.Location = new System.Drawing.Point(27, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Xem từ :";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Đến :";
            // 
            // cmbEndMonth
            // 
            this.cmbEndMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEndMonth.FormattingEnabled = true;
            this.cmbEndMonth.Location = new System.Drawing.Point(79, 64);
            this.cmbEndMonth.Name = "cmbEndMonth";
            this.cmbEndMonth.Size = new System.Drawing.Size(146, 21);
            this.cmbEndMonth.TabIndex = 12;
            this.cmbEndMonth.SelectedIndexChanged += new System.EventHandler(this.cmbEndMonth_SelectedIndexChanged);
            // 
            // cmbEndYear
            // 
            this.cmbEndYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEndYear.FormattingEnabled = true;
            this.cmbEndYear.Location = new System.Drawing.Point(231, 64);
            this.cmbEndYear.Name = "cmbEndYear";
            this.cmbEndYear.Size = new System.Drawing.Size(146, 21);
            this.cmbEndYear.TabIndex = 13;
            this.cmbEndYear.SelectedIndexChanged += new System.EventHandler(this.cmbEndYear_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label3.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label3, 3);
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(489, 0);
            this.label3.Name = "label3";
            this.tableLayoutPanel1.SetRowSpan(this.label3, 2);
            this.label3.Size = new System.Drawing.Size(166, 60);
            this.label3.TabIndex = 14;
            this.label3.Text = "TỔNG TIỀN THU";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTongTienThu
            // 
            this.lbTongTienThu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lbTongTienThu.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lbTongTienThu, 3);
            this.lbTongTienThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTongTienThu.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbTongTienThu.Location = new System.Drawing.Point(560, 60);
            this.lbTongTienThu.Name = "lbTongTienThu";
            this.tableLayoutPanel1.SetRowSpan(this.lbTongTienThu, 2);
            this.lbTongTienThu.Size = new System.Drawing.Size(23, 60);
            this.lbTongTienThu.TabIndex = 15;
            this.lbTongTienThu.Text = "0";
            this.lbTongTienThu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txbLoc
            // 
            this.txbLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbLoc, 4);
            this.txbLoc.Location = new System.Drawing.Point(3, 125);
            this.txbLoc.Name = "txbLoc";
            this.txbLoc.Size = new System.Drawing.Size(526, 20);
            this.txbLoc.TabIndex = 16;
            this.txbLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbLoc_KeyDown);
            this.txbLoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbLoc_KeyPress);
            // 
            // btLoc
            // 
            this.btLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btLoc.Location = new System.Drawing.Point(535, 123);
            this.btLoc.Name = "btLoc";
            this.btLoc.Size = new System.Drawing.Size(108, 23);
            this.btLoc.TabIndex = 17;
            this.btLoc.Text = "Lọc";
            this.btLoc.UseVisualStyleBackColor = true;
            this.btLoc.UseWaitCursor = true;
            this.btLoc.Click += new System.EventHandler(this.btLoc_Click);
            // 
            // gdvHoaDon
            // 
            this.gdvHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.gdvHoaDon, 6);
            this.gdvHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdvHoaDon.Location = new System.Drawing.Point(3, 153);
            this.gdvHoaDon.Name = "gdvHoaDon";
            this.gdvHoaDon.Size = new System.Drawing.Size(758, 335);
            this.gdvHoaDon.TabIndex = 18;
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 6);
            this.flowLayoutPanel1.Controls.Add(this.btnThoat);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 494);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(758, 44);
            this.flowLayoutPanel1.TabIndex = 19;
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(647, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(108, 41);
            this.btnThoat.TabIndex = 0;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // cmbStartYear
            // 
            this.cmbStartYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStartYear.FormattingEnabled = true;
            this.cmbStartYear.Location = new System.Drawing.Point(231, 34);
            this.cmbStartYear.Name = "cmbStartYear";
            this.cmbStartYear.Size = new System.Drawing.Size(146, 21);
            this.cmbStartYear.TabIndex = 11;
            this.cmbStartYear.SelectedIndexChanged += new System.EventHandler(this.cmbStartYear_SelectedIndexChanged);
            // 
            // cmbStartMonth
            // 
            this.cmbStartMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStartMonth.FormattingEnabled = true;
            this.cmbStartMonth.Location = new System.Drawing.Point(79, 34);
            this.cmbStartMonth.Name = "cmbStartMonth";
            this.cmbStartMonth.Size = new System.Drawing.Size(146, 21);
            this.cmbStartMonth.TabIndex = 10;
            this.cmbStartMonth.SelectedIndexChanged += new System.EventHandler(this.cmbStartMonth_SelectedIndexChanged);
            // 
            // btnChiTiet
            // 
            this.btnChiTiet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChiTiet.Location = new System.Drawing.Point(649, 123);
            this.btnChiTiet.Name = "btnChiTiet";
            this.btnChiTiet.Size = new System.Drawing.Size(112, 23);
            this.btnChiTiet.TabIndex = 20;
            this.btnChiTiet.Text = "Xem chi tiết";
            this.btnChiTiet.UseVisualStyleBackColor = true;
            this.btnChiTiet.Click += new System.EventHandler(this.btnChiTiet_Click);
            // 
            // frmThongKeNguonThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panelContainer);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmThongKeNguonThu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thống kê nguồn thu";
            this.Load += new System.EventHandler(this.frmThongKeNguonThu_Load);
            this.panelContainer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdvHoaDon)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbEndMonth;
        private System.Windows.Forms.ComboBox cmbEndYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbTongTienThu;
        private System.Windows.Forms.TextBox txbLoc;
        private System.Windows.Forms.Button btLoc;
        private System.Windows.Forms.DataGridView gdvHoaDon;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.ComboBox cmbStartYear;
        private System.Windows.Forms.ComboBox cmbStartMonth;
        private System.Windows.Forms.Button btnChiTiet;
    }
}
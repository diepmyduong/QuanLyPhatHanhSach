﻿namespace WinForm.Views
{
    partial class frmChiTietLoNhap
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
            this.lbSoLuongNhap = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbEndMonth = new System.Windows.Forms.ComboBox();
            this.cmbEndYear = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txbLoc = new System.Windows.Forms.TextBox();
            this.btLoc = new System.Windows.Forms.Button();
            this.gdvChiTiet = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.cmbStartYear = new System.Windows.Forms.ComboBox();
            this.cmbStartMonth = new System.Windows.Forms.ComboBox();
            this.lbTongTien = new System.Windows.Forms.Label();
            this.panelContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdvChiTiet)).BeginInit();
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.lbSoLuongNhap, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmbEndMonth, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmbEndYear, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbLoc, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btLoc, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.gdvChiTiet, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.cmbStartYear, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbStartMonth, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbTongTien, 4, 2);
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(764, 541);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbSoLuongNhap
            // 
            this.lbSoLuongNhap.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbSoLuongNhap.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lbSoLuongNhap, 2);
            this.lbSoLuongNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSoLuongNhap.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbSoLuongNhap.Location = new System.Drawing.Point(536, 17);
            this.lbSoLuongNhap.Name = "lbSoLuongNhap";
            this.tableLayoutPanel1.SetRowSpan(this.lbSoLuongNhap, 2);
            this.lbSoLuongNhap.Size = new System.Drawing.Size(23, 25);
            this.lbSoLuongNhap.TabIndex = 21;
            this.lbSoLuongNhap.Text = "0";
            this.lbSoLuongNhap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MediumBlue;
            this.label4.Location = new System.Drawing.Point(399, 77);
            this.label4.Name = "label4";
            this.tableLayoutPanel1.SetRowSpan(this.label4, 2);
            this.label4.Size = new System.Drawing.Size(131, 25);
            this.label4.TabIndex = 20;
            this.label4.Text = "TỔNG TIỀN :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.cmbEndMonth.Size = new System.Drawing.Size(108, 21);
            this.cmbEndMonth.TabIndex = 12;
            this.cmbEndMonth.SelectedIndexChanged += new System.EventHandler(this.cmbEndMonth_SelectedIndexChanged);
            // 
            // cmbEndYear
            // 
            this.cmbEndYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEndYear.FormattingEnabled = true;
            this.cmbEndYear.Location = new System.Drawing.Point(193, 64);
            this.cmbEndYear.Name = "cmbEndYear";
            this.cmbEndYear.Size = new System.Drawing.Size(108, 21);
            this.cmbEndYear.TabIndex = 13;
            this.cmbEndYear.SelectedIndexChanged += new System.EventHandler(this.cmbEndYear_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(342, 17);
            this.label3.Name = "label3";
            this.tableLayoutPanel1.SetRowSpan(this.label3, 2);
            this.label3.Size = new System.Drawing.Size(188, 25);
            this.label3.TabIndex = 14;
            this.label3.Text = "SỐ LƯỢNG NHẬP :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txbLoc
            // 
            this.txbLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbLoc, 5);
            this.txbLoc.Location = new System.Drawing.Point(3, 125);
            this.txbLoc.Name = "txbLoc";
            this.txbLoc.Size = new System.Drawing.Size(641, 20);
            this.txbLoc.TabIndex = 16;
            this.txbLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbLoc_KeyDown);
            this.txbLoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbLoc_KeyPress);
            // 
            // btLoc
            // 
            this.btLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btLoc.Location = new System.Drawing.Point(650, 123);
            this.btLoc.Name = "btLoc";
            this.btLoc.Size = new System.Drawing.Size(111, 23);
            this.btLoc.TabIndex = 17;
            this.btLoc.Text = "Lọc";
            this.btLoc.UseVisualStyleBackColor = true;
            this.btLoc.UseWaitCursor = true;
            this.btLoc.Click += new System.EventHandler(this.btLoc_Click);
            // 
            // gdvChiTiet
            // 
            this.gdvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gdvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.gdvChiTiet, 6);
            this.gdvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdvChiTiet.Location = new System.Drawing.Point(3, 153);
            this.gdvChiTiet.Name = "gdvChiTiet";
            this.gdvChiTiet.Size = new System.Drawing.Size(758, 335);
            this.gdvChiTiet.TabIndex = 18;
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
            this.cmbStartYear.Location = new System.Drawing.Point(193, 34);
            this.cmbStartYear.Name = "cmbStartYear";
            this.cmbStartYear.Size = new System.Drawing.Size(108, 21);
            this.cmbStartYear.TabIndex = 11;
            this.cmbStartYear.SelectedIndexChanged += new System.EventHandler(this.cmbStartYear_SelectedIndexChanged);
            // 
            // cmbStartMonth
            // 
            this.cmbStartMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStartMonth.FormattingEnabled = true;
            this.cmbStartMonth.Location = new System.Drawing.Point(79, 34);
            this.cmbStartMonth.Name = "cmbStartMonth";
            this.cmbStartMonth.Size = new System.Drawing.Size(108, 21);
            this.cmbStartMonth.TabIndex = 10;
            this.cmbStartMonth.SelectedIndexChanged += new System.EventHandler(this.cmbStartMonth_SelectedIndexChanged);
            // 
            // lbTongTien
            // 
            this.lbTongTien.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbTongTien.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lbTongTien, 2);
            this.lbTongTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTongTien.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbTongTien.Location = new System.Drawing.Point(536, 77);
            this.lbTongTien.Name = "lbTongTien";
            this.tableLayoutPanel1.SetRowSpan(this.lbTongTien, 2);
            this.lbTongTien.Size = new System.Drawing.Size(23, 25);
            this.lbTongTien.TabIndex = 21;
            this.lbTongTien.Text = "0";
            this.lbTongTien.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmChiTietLoNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panelContainer);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmChiTietLoNhap";
            this.Text = "Chi tiết lô nhập";
            this.Load += new System.EventHandler(this.frmChiTietLoNhap_Load);
            this.panelContainer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdvChiTiet)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbSoLuongNhap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbEndMonth;
        private System.Windows.Forms.ComboBox cmbEndYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbLoc;
        private System.Windows.Forms.Button btLoc;
        private System.Windows.Forms.DataGridView gdvChiTiet;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.ComboBox cmbStartYear;
        private System.Windows.Forms.ComboBox cmbStartMonth;
        private System.Windows.Forms.Label lbTongTien;
    }
}
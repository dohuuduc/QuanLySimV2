namespace QuanLyData {
  partial class frmMain {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.cấuHìnhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cấuHìnhToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.groupMain = new System.Windows.Forms.GroupBox();
      this.GridViewMain = new System.Windows.Forms.DataGridView();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.button1 = new System.Windows.Forms.Button();
      this.btntim = new System.Windows.Forms.Button();
      this.txtTim = new System.Windows.Forms.TextBox();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.panel1 = new System.Windows.Forms.Panel();
      this.panel2 = new System.Windows.Forms.Panel();
      this.tabControl2 = new System.Windows.Forms.TabControl();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.palSIPLogs = new System.Windows.Forms.TableLayoutPanel();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.cbb_TypeDataSource = new System.Windows.Forms.ComboBox();
      this.button3 = new System.Windows.Forms.Button();
      this.btn_View = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.button6 = new System.Windows.Forms.Button();
      this.txt_FileName = new System.Windows.Forms.TextBox();
      this.btn_Import = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.menuStrip1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.groupMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.GridViewMain)).BeginInit();
      this.groupBox1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.tabControl2.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cấuHìnhToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(847, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // cấuHìnhToolStripMenuItem
      // 
      this.cấuHìnhToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cấuHìnhToolStripMenuItem1});
      this.cấuHìnhToolStripMenuItem.Name = "cấuHìnhToolStripMenuItem";
      this.cấuHìnhToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
      this.cấuHìnhToolStripMenuItem.Text = "Cấu Hình";
      // 
      // cấuHìnhToolStripMenuItem1
      // 
      this.cấuHìnhToolStripMenuItem1.Name = "cấuHìnhToolStripMenuItem1";
      this.cấuHìnhToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
      this.cấuHìnhToolStripMenuItem1.Text = "Cấu Hình";
      this.cấuHìnhToolStripMenuItem1.Click += new System.EventHandler(this.cấuHìnhToolStripMenuItem1_Click);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 24);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(847, 426);
      this.tabControl1.TabIndex = 1;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.groupMain);
      this.tabPage1.Controls.Add(this.groupBox1);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(839, 400);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Main";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // groupMain
      // 
      this.groupMain.Controls.Add(this.GridViewMain);
      this.groupMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupMain.Location = new System.Drawing.Point(3, 51);
      this.groupMain.Name = "groupMain";
      this.groupMain.Size = new System.Drawing.Size(833, 346);
      this.groupMain.TabIndex = 2;
      this.groupMain.TabStop = false;
      this.groupMain.Text = "Xem Tất Cả Khách Hàng";
      // 
      // GridViewMain
      // 
      this.GridViewMain.AllowUserToAddRows = false;
      this.GridViewMain.AllowUserToResizeRows = false;
      this.GridViewMain.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      this.GridViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.GridViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.GridViewMain.Location = new System.Drawing.Point(3, 16);
      this.GridViewMain.Name = "GridViewMain";
      this.GridViewMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.GridViewMain.Size = new System.Drawing.Size(827, 327);
      this.GridViewMain.TabIndex = 1;
      this.GridViewMain.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.GridViewMain_CellValueNeeded);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.button1);
      this.groupBox1.Controls.Add(this.btntim);
      this.groupBox1.Controls.Add(this.txtTim);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox1.Location = new System.Drawing.Point(3, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(833, 48);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Tìm Kiếm";
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button1.Location = new System.Drawing.Point(723, 16);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(104, 23);
      this.button1.TabIndex = 3;
      this.button1.Text = "Tìm Nâng Cao";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // btntim
      // 
      this.btntim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btntim.Location = new System.Drawing.Point(642, 17);
      this.btntim.Name = "btntim";
      this.btntim.Size = new System.Drawing.Size(75, 23);
      this.btntim.TabIndex = 2;
      this.btntim.Text = "Tìm";
      this.btntim.UseVisualStyleBackColor = true;
      this.btntim.Click += new System.EventHandler(this.btntim_Click);
      // 
      // txtTim
      // 
      this.txtTim.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtTim.Location = new System.Drawing.Point(6, 19);
      this.txtTim.Name = "txtTim";
      this.txtTim.Size = new System.Drawing.Size(630, 20);
      this.txtTim.TabIndex = 1;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.panel1);
      this.tabPage2.Controls.Add(this.groupBox3);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(839, 400);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Import";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.panel2);
      this.panel1.Controls.Add(this.groupBox2);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 50);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(833, 347);
      this.panel1.TabIndex = 5;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.tabControl2);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 177);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(833, 170);
      this.panel2.TabIndex = 5;
      // 
      // tabControl2
      // 
      this.tabControl2.Controls.Add(this.tabPage3);
      this.tabControl2.Controls.Add(this.tabPage4);
      this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl2.Location = new System.Drawing.Point(0, 0);
      this.tabControl2.Name = "tabControl2";
      this.tabControl2.SelectedIndex = 0;
      this.tabControl2.Size = new System.Drawing.Size(833, 170);
      this.tabControl2.TabIndex = 0;
      // 
      // tabPage3
      // 
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(825, 144);
      this.tabPage3.TabIndex = 0;
      this.tabPage3.Text = "tabPage3";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // tabPage4
      // 
      this.tabPage4.Location = new System.Drawing.Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage4.Size = new System.Drawing.Size(825, 144);
      this.tabPage4.TabIndex = 1;
      this.tabPage4.Text = "tabPage4";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.palSIPLogs);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox2.Location = new System.Drawing.Point(0, 0);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(833, 177);
      this.groupBox2.TabIndex = 4;
      this.groupBox2.TabStop = false;
      // 
      // palSIPLogs
      // 
      this.palSIPLogs.AutoScroll = true;
      this.palSIPLogs.ColumnCount = 10;
      this.palSIPLogs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
      this.palSIPLogs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
      this.palSIPLogs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
      this.palSIPLogs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
      this.palSIPLogs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
      this.palSIPLogs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
      this.palSIPLogs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
      this.palSIPLogs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
      this.palSIPLogs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
      this.palSIPLogs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
      this.palSIPLogs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.palSIPLogs.Location = new System.Drawing.Point(3, 16);
      this.palSIPLogs.Name = "palSIPLogs";
      this.palSIPLogs.RowCount = 2;
      this.palSIPLogs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.palSIPLogs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.palSIPLogs.Size = new System.Drawing.Size(827, 158);
      this.palSIPLogs.TabIndex = 7;
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.cbb_TypeDataSource);
      this.groupBox3.Controls.Add(this.button3);
      this.groupBox3.Controls.Add(this.btn_View);
      this.groupBox3.Controls.Add(this.button2);
      this.groupBox3.Controls.Add(this.button6);
      this.groupBox3.Controls.Add(this.txt_FileName);
      this.groupBox3.Controls.Add(this.btn_Import);
      this.groupBox3.Controls.Add(this.label3);
      this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox3.Location = new System.Drawing.Point(3, 3);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(833, 47);
      this.groupBox3.TabIndex = 3;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Dữ Liệu Nguồn (TXT: UTF-16)";
      // 
      // cbb_TypeDataSource
      // 
      this.cbb_TypeDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbb_TypeDataSource.FormattingEnabled = true;
      this.cbb_TypeDataSource.Location = new System.Drawing.Point(6, 15);
      this.cbb_TypeDataSource.Name = "cbb_TypeDataSource";
      this.cbb_TypeDataSource.Size = new System.Drawing.Size(112, 21);
      this.cbb_TypeDataSource.TabIndex = 22;
      // 
      // button3
      // 
      this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button3.Location = new System.Drawing.Point(288, 14);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(63, 23);
      this.button3.TabIndex = 6;
      this.button3.Text = "Load";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // btn_View
      // 
      this.btn_View.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btn_View.Location = new System.Drawing.Point(525, 14);
      this.btn_View.Name = "btn_View";
      this.btn_View.Size = new System.Drawing.Size(83, 23);
      this.btn_View.TabIndex = 7;
      this.btn_View.Text = "Xem Trước";
      this.btn_View.UseVisualStyleBackColor = true;
      // 
      // button2
      // 
      this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button2.Location = new System.Drawing.Point(249, 14);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(33, 23);
      this.button2.TabIndex = 5;
      this.button2.Text = "Tìm";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button6
      // 
      this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button6.Location = new System.Drawing.Point(443, 14);
      this.button6.Name = "button6";
      this.button6.Size = new System.Drawing.Size(76, 23);
      this.button6.TabIndex = 6;
      this.button6.Text = "Làm Tươi";
      this.button6.UseVisualStyleBackColor = true;
      // 
      // txt_FileName
      // 
      this.txt_FileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txt_FileName.Location = new System.Drawing.Point(177, 16);
      this.txt_FileName.Name = "txt_FileName";
      this.txt_FileName.Size = new System.Drawing.Size(64, 20);
      this.txt_FileName.TabIndex = 4;
      // 
      // btn_Import
      // 
      this.btn_Import.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btn_Import.Location = new System.Drawing.Point(357, 14);
      this.btn_Import.Name = "btn_Import";
      this.btn_Import.Size = new System.Drawing.Size(80, 23);
      this.btn_Import.TabIndex = 6;
      this.btn_Import.Text = "Import";
      this.btn_Import.UseVisualStyleBackColor = true;
      this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(124, 19);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(47, 13);
      this.label3.TabIndex = 3;
      this.label3.Text = "Tập Tin:";
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(847, 450);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.menuStrip1);
      this.Name = "frmMain";
      this.Text = "Quản Lý Database";
      this.Load += new System.EventHandler(this.frmMain_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.groupMain.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.GridViewMain)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.tabControl2.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem cấuHìnhToolStripMenuItem;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.DataGridView GridViewMain;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button btntim;
    private System.Windows.Forms.TextBox txtTim;
    private System.Windows.Forms.ToolStripMenuItem cấuHìnhToolStripMenuItem1;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.ComboBox cbb_TypeDataSource;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button btn_View;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button6;
    private System.Windows.Forms.TextBox txt_FileName;
    private System.Windows.Forms.Button btn_Import;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.TabControl tabControl2;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.TableLayoutPanel palSIPLogs;
    private System.Windows.Forms.GroupBox groupMain;
  }
}


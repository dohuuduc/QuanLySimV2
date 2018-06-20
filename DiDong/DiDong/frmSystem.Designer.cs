namespace QuanLyData {
  partial class frmSystem {
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
      this.components = new System.ComponentModel.Container();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.GridViewColumn = new System.Windows.Forms.DataGridView();
      this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.stt = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ma = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.size = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.isKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.isOrder = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.IsAct = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.IsReport = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.orderid = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.làmTươiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cậpNhậtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.panel2 = new System.Windows.Forms.Panel();
      this.gridviewCharacter = new System.Windows.Forms.DataGridView();
      this.idCharacter = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.stt_character = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.maCharacter = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.nameCharacter = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.orderidCharacter = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.isActCharacter = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.del = new System.Windows.Forms.DataGridViewImageColumn();
      this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.lamTươiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.thêmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.xoaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.panel1 = new System.Windows.Forms.Panel();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.ckhSoLuongHienThi = new System.Windows.Forms.CheckBox();
      this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.radExcel = new System.Windows.Forms.RadioButton();
      this.radText = new System.Windows.Forms.RadioButton();
      this.button1 = new System.Windows.Forms.Button();
      this.ckhDelImport = new System.Windows.Forms.CheckBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.button2 = new System.Windows.Forms.Button();
      this.chkDelRoot = new System.Windows.Forms.CheckBox();
      this.chkDelImport = new System.Windows.Forms.CheckBox();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.ckhvalueskeySearch = new System.Windows.Forms.CheckBox();
      this.button3 = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.cmdBatDongBo = new System.Windows.Forms.ComboBox();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.GridViewColumn)).BeginInit();
      this.contextMenuStrip1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gridviewCharacter)).BeginInit();
      this.contextMenuStrip2.SuspendLayout();
      this.panel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(800, 450);
      this.tabControl1.TabIndex = 0;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.GridViewColumn);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(792, 424);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Quản Lý Cột";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // GridViewColumn
      // 
      this.GridViewColumn.AllowUserToAddRows = false;
      this.GridViewColumn.AllowUserToDeleteRows = false;
      this.GridViewColumn.AllowUserToResizeColumns = false;
      this.GridViewColumn.AllowUserToResizeRows = false;
      this.GridViewColumn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.GridViewColumn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.stt,
            this.ma,
            this.name,
            this.size,
            this.isKey,
            this.isOrder,
            this.IsAct,
            this.IsReport,
            this.orderid});
      this.GridViewColumn.ContextMenuStrip = this.contextMenuStrip1;
      this.GridViewColumn.Dock = System.Windows.Forms.DockStyle.Fill;
      this.GridViewColumn.Location = new System.Drawing.Point(3, 3);
      this.GridViewColumn.MultiSelect = false;
      this.GridViewColumn.Name = "GridViewColumn";
      this.GridViewColumn.RowHeadersVisible = false;
      this.GridViewColumn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.GridViewColumn.Size = new System.Drawing.Size(786, 418);
      this.GridViewColumn.TabIndex = 0;
      this.GridViewColumn.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewColumn_CellContentClick);
      this.GridViewColumn.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.GridViewColumn_CellPainting);
      this.GridViewColumn.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewColumn_CellValueChanged);
      // 
      // id
      // 
      this.id.DataPropertyName = "id";
      this.id.HeaderText = "id";
      this.id.Name = "id";
      this.id.Visible = false;
      // 
      // stt
      // 
      this.stt.DataPropertyName = "STT";
      this.stt.HeaderText = "STT";
      this.stt.Name = "stt";
      this.stt.Width = 30;
      // 
      // ma
      // 
      this.ma.DataPropertyName = "ma";
      this.ma.HeaderText = "Mã Cột";
      this.ma.Name = "ma";
      this.ma.ReadOnly = true;
      // 
      // name
      // 
      this.name.DataPropertyName = "name";
      this.name.HeaderText = "Tên Cột";
      this.name.Name = "name";
      this.name.Width = 380;
      // 
      // size
      // 
      this.size.DataPropertyName = "size";
      this.size.HeaderText = "size";
      this.size.Name = "size";
      this.size.Width = 50;
      // 
      // isKey
      // 
      this.isKey.DataPropertyName = "isKey";
      this.isKey.HeaderText = "Key";
      this.isKey.Name = "isKey";
      this.isKey.Width = 35;
      // 
      // isOrder
      // 
      this.isOrder.DataPropertyName = "isOrder";
      this.isOrder.HeaderText = "SXếp";
      this.isOrder.Name = "isOrder";
      this.isOrder.Width = 35;
      // 
      // IsAct
      // 
      this.IsAct.DataPropertyName = "isAct";
      this.IsAct.HeaderText = "Act";
      this.IsAct.Name = "IsAct";
      this.IsAct.Width = 35;
      // 
      // IsReport
      // 
      this.IsReport.DataPropertyName = "IsReport";
      this.IsReport.HeaderText = "BC";
      this.IsReport.Name = "IsReport";
      this.IsReport.Width = 35;
      // 
      // orderid
      // 
      this.orderid.DataPropertyName = "orderid";
      this.orderid.HeaderText = "Vị trí";
      this.orderid.Name = "orderid";
      this.orderid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.orderid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.orderid.Width = 60;
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.làmTươiToolStripMenuItem,
            this.cậpNhậtToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
      // 
      // làmTươiToolStripMenuItem
      // 
      this.làmTươiToolStripMenuItem.Name = "làmTươiToolStripMenuItem";
      this.làmTươiToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
      this.làmTươiToolStripMenuItem.Text = "Làm Tươi";
      this.làmTươiToolStripMenuItem.Click += new System.EventHandler(this.làmTươiToolStripMenuItem_Click);
      // 
      // cậpNhậtToolStripMenuItem
      // 
      this.cậpNhậtToolStripMenuItem.Name = "cậpNhậtToolStripMenuItem";
      this.cậpNhậtToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
      this.cậpNhậtToolStripMenuItem.Text = "Cập nhật";
      this.cậpNhậtToolStripMenuItem.Click += new System.EventHandler(this.cậpNhậtToolStripMenuItem_Click);
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.panel2);
      this.tabPage2.Controls.Add(this.panel1);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(792, 424);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Thông Số";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.gridviewCharacter);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(248, 3);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(541, 418);
      this.panel2.TabIndex = 3;
      // 
      // gridviewCharacter
      // 
      this.gridviewCharacter.AllowUserToAddRows = false;
      this.gridviewCharacter.AllowUserToDeleteRows = false;
      this.gridviewCharacter.AllowUserToOrderColumns = true;
      this.gridviewCharacter.AllowUserToResizeColumns = false;
      this.gridviewCharacter.AllowUserToResizeRows = false;
      this.gridviewCharacter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gridviewCharacter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idCharacter,
            this.stt_character,
            this.maCharacter,
            this.nameCharacter,
            this.orderidCharacter,
            this.isActCharacter,
            this.del});
      this.gridviewCharacter.ContextMenuStrip = this.contextMenuStrip2;
      this.gridviewCharacter.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gridviewCharacter.Location = new System.Drawing.Point(0, 0);
      this.gridviewCharacter.Name = "gridviewCharacter";
      this.gridviewCharacter.RowHeadersVisible = false;
      this.gridviewCharacter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gridviewCharacter.Size = new System.Drawing.Size(541, 418);
      this.gridviewCharacter.TabIndex = 0;
      this.gridviewCharacter.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridviewCharacter_CellClick);
      this.gridviewCharacter.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridviewCharacter_CellContentClick);
      this.gridviewCharacter.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridviewCharacter_CellPainting);
      this.gridviewCharacter.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridviewCharacter_CellValueChanged);
      // 
      // idCharacter
      // 
      this.idCharacter.DataPropertyName = "id";
      this.idCharacter.HeaderText = "id";
      this.idCharacter.Name = "idCharacter";
      this.idCharacter.Visible = false;
      this.idCharacter.Width = 80;
      // 
      // stt_character
      // 
      this.stt_character.DataPropertyName = "stt";
      this.stt_character.HeaderText = "STT";
      this.stt_character.Name = "stt_character";
      this.stt_character.ReadOnly = true;
      this.stt_character.Width = 50;
      // 
      // maCharacter
      // 
      this.maCharacter.DataPropertyName = "ma";
      this.maCharacter.HeaderText = "Mã";
      this.maCharacter.Name = "maCharacter";
      this.maCharacter.Width = 80;
      // 
      // nameCharacter
      // 
      this.nameCharacter.DataPropertyName = "name";
      this.nameCharacter.HeaderText = "Tên";
      this.nameCharacter.Name = "nameCharacter";
      this.nameCharacter.Width = 280;
      // 
      // orderidCharacter
      // 
      this.orderidCharacter.DataPropertyName = "orderid";
      this.orderidCharacter.HeaderText = "Vị Trí";
      this.orderidCharacter.Name = "orderidCharacter";
      this.orderidCharacter.Width = 70;
      // 
      // isActCharacter
      // 
      this.isActCharacter.DataPropertyName = "isAct";
      this.isActCharacter.HeaderText = "Act";
      this.isActCharacter.Name = "isActCharacter";
      this.isActCharacter.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.isActCharacter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.isActCharacter.Width = 30;
      // 
      // del
      // 
      this.del.HeaderText = "Xoá";
      this.del.Name = "del";
      this.del.Width = 30;
      // 
      // contextMenuStrip2
      // 
      this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lamTươiToolStripMenuItem,
            this.thêmToolStripMenuItem,
            this.xoaToolStripMenuItem});
      this.contextMenuStrip2.Name = "contextMenuStrip2";
      this.contextMenuStrip2.Size = new System.Drawing.Size(125, 70);
      // 
      // lamTươiToolStripMenuItem
      // 
      this.lamTươiToolStripMenuItem.Name = "lamTươiToolStripMenuItem";
      this.lamTươiToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
      this.lamTươiToolStripMenuItem.Text = "Làm Tươi";
      this.lamTươiToolStripMenuItem.Click += new System.EventHandler(this.lamTươiToolStripMenuItem_Click);
      // 
      // thêmToolStripMenuItem
      // 
      this.thêmToolStripMenuItem.Name = "thêmToolStripMenuItem";
      this.thêmToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
      this.thêmToolStripMenuItem.Text = "Thêm";
      this.thêmToolStripMenuItem.Click += new System.EventHandler(this.thêmToolStripMenuItem_Click);
      // 
      // xoaToolStripMenuItem
      // 
      this.xoaToolStripMenuItem.Name = "xoaToolStripMenuItem";
      this.xoaToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
      this.xoaToolStripMenuItem.Text = "Xoá";
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.groupBox4);
      this.panel1.Controls.Add(this.groupBox3);
      this.panel1.Controls.Add(this.linkLabel1);
      this.panel1.Controls.Add(this.groupBox1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
      this.panel1.Location = new System.Drawing.Point(3, 3);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(245, 418);
      this.panel1.TabIndex = 2;
      // 
      // linkLabel1
      // 
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Location = new System.Drawing.Point(3, 403);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(82, 13);
      this.linkLabel1.TabIndex = 1;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "Tham khảo font";
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.cmdBatDongBo);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.ckhSoLuongHienThi);
      this.groupBox1.Controls.Add(this.numericUpDown1);
      this.groupBox1.Controls.Add(this.groupBox2);
      this.groupBox1.Controls.Add(this.button1);
      this.groupBox1.Controls.Add(this.ckhDelImport);
      this.groupBox1.Location = new System.Drawing.Point(3, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(240, 192);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Import";
      // 
      // ckhSoLuongHienThi
      // 
      this.ckhSoLuongHienThi.AutoSize = true;
      this.ckhSoLuongHienThi.Location = new System.Drawing.Point(15, 45);
      this.ckhSoLuongHienThi.Name = "ckhSoLuongHienThi";
      this.ckhSoLuongHienThi.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.ckhSoLuongHienThi.Size = new System.Drawing.Size(115, 17);
      this.ckhSoLuongHienThi.TabIndex = 6;
      this.ckhSoLuongHienThi.Text = "Số Lượng Hiển Thị";
      this.ckhSoLuongHienThi.UseVisualStyleBackColor = true;
      this.ckhSoLuongHienThi.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
      // 
      // numericUpDown1
      // 
      this.numericUpDown1.Location = new System.Drawing.Point(150, 42);
      this.numericUpDown1.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
      this.numericUpDown1.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.numericUpDown1.Name = "numericUpDown1";
      this.numericUpDown1.Size = new System.Drawing.Size(75, 20);
      this.numericUpDown1.TabIndex = 5;
      this.numericUpDown1.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.radExcel);
      this.groupBox2.Controls.Add(this.radText);
      this.groupBox2.Location = new System.Drawing.Point(16, 68);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(210, 48);
      this.groupBox2.TabIndex = 4;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Xuất file";
      // 
      // radExcel
      // 
      this.radExcel.AutoSize = true;
      this.radExcel.Location = new System.Drawing.Point(135, 19);
      this.radExcel.Name = "radExcel";
      this.radExcel.Size = new System.Drawing.Size(51, 17);
      this.radExcel.TabIndex = 1;
      this.radExcel.Text = "Excel";
      this.radExcel.UseVisualStyleBackColor = true;
      // 
      // radText
      // 
      this.radText.AutoSize = true;
      this.radText.Checked = true;
      this.radText.Location = new System.Drawing.Point(23, 19);
      this.radText.Name = "radText";
      this.radText.Size = new System.Drawing.Size(46, 17);
      this.radText.TabIndex = 0;
      this.radText.TabStop = true;
      this.radText.Text = "Text";
      this.radText.UseVisualStyleBackColor = true;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(151, 163);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 3;
      this.button1.Text = "Lưu";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // ckhDelImport
      // 
      this.ckhDelImport.AutoSize = true;
      this.ckhDelImport.Location = new System.Drawing.Point(9, 19);
      this.ckhDelImport.Name = "ckhDelImport";
      this.ckhDelImport.Size = new System.Drawing.Size(217, 17);
      this.ckhDelImport.TabIndex = 2;
      this.ckhDelImport.Text = "Xoá dữ liệu bảng tạm trước khi thêm vào";
      this.ckhDelImport.UseVisualStyleBackColor = true;
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.button2);
      this.groupBox3.Controls.Add(this.chkDelRoot);
      this.groupBox3.Controls.Add(this.chkDelImport);
      this.groupBox3.Location = new System.Drawing.Point(2, 283);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(240, 86);
      this.groupBox3.TabIndex = 1;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Dọn dẹp dữ liệu";
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(151, 54);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(75, 23);
      this.button2.TabIndex = 4;
      this.button2.Text = "Lưu";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // chkDelRoot
      // 
      this.chkDelRoot.AutoSize = true;
      this.chkDelRoot.Location = new System.Drawing.Point(27, 54);
      this.chkDelRoot.Name = "chkDelRoot";
      this.chkDelRoot.Size = new System.Drawing.Size(93, 17);
      this.chkDelRoot.TabIndex = 1;
      this.chkDelRoot.Text = "Xoá bảng gốc";
      this.chkDelRoot.UseVisualStyleBackColor = true;
      // 
      // chkDelImport
      // 
      this.chkDelImport.AutoSize = true;
      this.chkDelImport.Location = new System.Drawing.Point(27, 26);
      this.chkDelImport.Name = "chkDelImport";
      this.chkDelImport.Size = new System.Drawing.Size(92, 17);
      this.chkDelImport.TabIndex = 0;
      this.chkDelImport.Text = "Xoá bảng tạm";
      this.chkDelImport.UseVisualStyleBackColor = true;
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.button3);
      this.groupBox4.Controls.Add(this.ckhvalueskeySearch);
      this.groupBox4.Location = new System.Drawing.Point(3, 195);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(240, 82);
      this.groupBox4.TabIndex = 2;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Chuẩn Hoá";
      // 
      // ckhvalueskeySearch
      // 
      this.ckhvalueskeySearch.AutoSize = true;
      this.ckhvalueskeySearch.Location = new System.Drawing.Point(26, 19);
      this.ckhvalueskeySearch.Name = "ckhvalueskeySearch";
      this.ckhvalueskeySearch.Size = new System.Drawing.Size(163, 17);
      this.ckhvalueskeySearch.TabIndex = 1;
      this.ckhvalueskeySearch.Text = "Chuẩn hoá thông tin tìm kiếm";
      this.ckhvalueskeySearch.UseVisualStyleBackColor = true;
      // 
      // button3
      // 
      this.button3.Location = new System.Drawing.Point(150, 53);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(75, 23);
      this.button3.TabIndex = 5;
      this.button3.Text = "Lưu";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 130);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(90, 13);
      this.label1.TabIndex = 7;
      this.label1.Text = "Bất đồng bộ SQL";
      // 
      // cmdBatDongBo
      // 
      this.cmdBatDongBo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmdBatDongBo.FormattingEnabled = true;
      this.cmdBatDongBo.Location = new System.Drawing.Point(104, 127);
      this.cmdBatDongBo.Name = "cmdBatDongBo";
      this.cmdBatDongBo.Size = new System.Drawing.Size(121, 21);
      this.cmdBatDongBo.TabIndex = 8;
      // 
      // frmSystem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.tabControl1);
      this.Name = "frmSystem";
      this.Text = "Cấu Hình";
      this.Load += new System.EventHandler(this.frmSystem_Load);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.GridViewColumn)).EndInit();
      this.contextMenuStrip1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gridviewCharacter)).EndInit();
      this.contextMenuStrip2.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.DataGridView GridViewColumn;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem cậpNhậtToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem làmTươiToolStripMenuItem;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox ckhDelImport;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.CheckBox chkDelRoot;
    private System.Windows.Forms.CheckBox chkDelImport;
    private System.Windows.Forms.CheckBox ckhSoLuongHienThi;
    private System.Windows.Forms.NumericUpDown numericUpDown1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.RadioButton radExcel;
    private System.Windows.Forms.RadioButton radText;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.DataGridView gridviewCharacter;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    private System.Windows.Forms.ToolStripMenuItem lamTươiToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem thêmToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem xoaToolStripMenuItem;
    private System.Windows.Forms.DataGridViewTextBoxColumn idCharacter;
    private System.Windows.Forms.DataGridViewTextBoxColumn stt_character;
    private System.Windows.Forms.DataGridViewTextBoxColumn maCharacter;
    private System.Windows.Forms.DataGridViewTextBoxColumn nameCharacter;
    private System.Windows.Forms.DataGridViewTextBoxColumn orderidCharacter;
    private System.Windows.Forms.DataGridViewCheckBoxColumn isActCharacter;
    private System.Windows.Forms.DataGridViewImageColumn del;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.DataGridViewTextBoxColumn id;
    private System.Windows.Forms.DataGridViewTextBoxColumn stt;
    private System.Windows.Forms.DataGridViewTextBoxColumn ma;
    private System.Windows.Forms.DataGridViewTextBoxColumn name;
    private System.Windows.Forms.DataGridViewTextBoxColumn size;
    private System.Windows.Forms.DataGridViewCheckBoxColumn isKey;
    private System.Windows.Forms.DataGridViewCheckBoxColumn isOrder;
    private System.Windows.Forms.DataGridViewCheckBoxColumn IsAct;
    private System.Windows.Forms.DataGridViewCheckBoxColumn IsReport;
    private System.Windows.Forms.DataGridViewTextBoxColumn orderid;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.CheckBox ckhvalueskeySearch;
    private System.Windows.Forms.ComboBox cmdBatDongBo;
    private System.Windows.Forms.Label label1;
  }
}
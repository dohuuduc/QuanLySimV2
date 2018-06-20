namespace DiDong {
  partial class frmSQLServer {
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
      this.cmbDatabase = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.button1 = new System.Windows.Forms.Button();
      this.stt = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.TABLE_CATALOG = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.isAct = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.TABLE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.So_Luong_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ListColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.SuspendLayout();
      // 
      // cmbDatabase
      // 
      this.cmbDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbDatabase.FormattingEnabled = true;
      this.cmbDatabase.Location = new System.Drawing.Point(70, 12);
      this.cmbDatabase.Name = "cmbDatabase";
      this.cmbDatabase.Size = new System.Drawing.Size(378, 21);
      this.cmbDatabase.TabIndex = 0;
      this.cmbDatabase.SelectedIndexChanged += new System.EventHandler(this.cmbDatabase_SelectedIndexChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(53, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Database";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.dataGridView1);
      this.groupBox1.Location = new System.Drawing.Point(0, 39);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(532, 408);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Danh Sách Table";
      // 
      // dataGridView1
      // 
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.AllowUserToResizeColumns = false;
      this.dataGridView1.AllowUserToResizeRows = false;
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stt,
            this.TABLE_CATALOG,
            this.isAct,
            this.TABLE_NAME,
            this.So_Luong_Column,
            this.ListColumn});
      this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataGridView1.Location = new System.Drawing.Point(3, 16);
      this.dataGridView1.MultiSelect = false;
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.RowHeadersVisible = false;
      this.dataGridView1.Size = new System.Drawing.Size(526, 389);
      this.dataGridView1.TabIndex = 0;
      this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
      this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(454, 10);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 3;
      this.button1.Text = "Chọn";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // stt
      // 
      this.stt.DataPropertyName = "stt";
      this.stt.HeaderText = "stt";
      this.stt.Name = "stt";
      this.stt.ReadOnly = true;
      this.stt.Width = 30;
      // 
      // TABLE_CATALOG
      // 
      this.TABLE_CATALOG.DataPropertyName = "TABLE_CATALOG";
      this.TABLE_CATALOG.HeaderText = "TABLE_CATALOG";
      this.TABLE_CATALOG.Name = "TABLE_CATALOG";
      this.TABLE_CATALOG.Visible = false;
      // 
      // isAct
      // 
      this.isAct.DataPropertyName = "isAct";
      this.isAct.HeaderText = "Chọn";
      this.isAct.Name = "isAct";
      this.isAct.Width = 45;
      // 
      // TABLE_NAME
      // 
      this.TABLE_NAME.DataPropertyName = "TABLE_NAME";
      this.TABLE_NAME.HeaderText = "Table Name";
      this.TABLE_NAME.Name = "TABLE_NAME";
      this.TABLE_NAME.ReadOnly = true;
      // 
      // So_Luong_Column
      // 
      this.So_Luong_Column.DataPropertyName = "So_Luong_Column";
      this.So_Luong_Column.HeaderText = "SL";
      this.So_Luong_Column.Name = "So_Luong_Column";
      this.So_Luong_Column.ReadOnly = true;
      this.So_Luong_Column.Width = 50;
      // 
      // ListColumn
      // 
      this.ListColumn.DataPropertyName = "Listcolumn";
      this.ListColumn.HeaderText = "Danh Sách Cột";
      this.ListColumn.Name = "ListColumn";
      this.ListColumn.ReadOnly = true;
      this.ListColumn.Width = 300;
      // 
      // frmSQLServer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(535, 444);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cmbDatabase);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmSQLServer";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "SQL Server";
      this.Load += new System.EventHandler(this.frmSQLServer_Load);
      this.groupBox1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox cmbDatabase;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.DataGridViewTextBoxColumn stt;
    private System.Windows.Forms.DataGridViewTextBoxColumn TABLE_CATALOG;
    private System.Windows.Forms.DataGridViewCheckBoxColumn isAct;
    private System.Windows.Forms.DataGridViewTextBoxColumn TABLE_NAME;
    private System.Windows.Forms.DataGridViewTextBoxColumn So_Luong_Column;
    private System.Windows.Forms.DataGridViewTextBoxColumn ListColumn;
  }
}
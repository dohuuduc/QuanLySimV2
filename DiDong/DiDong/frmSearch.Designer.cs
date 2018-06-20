namespace QuanLyData {
  partial class frmSearch {
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.panel3 = new System.Windows.Forms.Panel();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.panel2 = new System.Windows.Forms.Panel();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.btnTim = new System.Windows.Forms.Button();
      this.radioButton2 = new System.Windows.Forms.RadioButton();
      this.radioButton1 = new System.Windows.Forms.RadioButton();
      this.panelTim = new System.Windows.Forms.Panel();
      this.panel1.SuspendLayout();
      this.panel3.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.panel2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.panel3);
      this.panel1.Controls.Add(this.panel2);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(448, 520);
      this.panel1.TabIndex = 0;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.groupBox2);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(0, 43);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(448, 477);
      this.panel3.TabIndex = 1;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.panelTim);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox2.Location = new System.Drawing.Point(0, 0);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(448, 477);
      this.groupBox2.TabIndex = 0;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Các Cột Tìm Kiếm";
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.groupBox1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(448, 43);
      this.panel2.TabIndex = 0;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.btnTim);
      this.groupBox1.Controls.Add(this.radioButton2);
      this.groupBox1.Controls.Add(this.radioButton1);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(448, 43);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      // 
      // btnTim
      // 
      this.btnTim.Location = new System.Drawing.Point(367, 12);
      this.btnTim.Name = "btnTim";
      this.btnTim.Size = new System.Drawing.Size(75, 21);
      this.btnTim.TabIndex = 2;
      this.btnTim.Text = "Tìm";
      this.btnTim.UseVisualStyleBackColor = true;
      // 
      // radioButton2
      // 
      this.radioButton2.AutoSize = true;
      this.radioButton2.Location = new System.Drawing.Point(62, 16);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new System.Drawing.Size(36, 17);
      this.radioButton2.TabIndex = 1;
      this.radioButton2.Text = "Or";
      this.radioButton2.UseVisualStyleBackColor = true;
      // 
      // radioButton1
      // 
      this.radioButton1.AutoSize = true;
      this.radioButton1.Checked = true;
      this.radioButton1.Location = new System.Drawing.Point(12, 16);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new System.Drawing.Size(44, 17);
      this.radioButton1.TabIndex = 0;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "And";
      this.radioButton1.UseVisualStyleBackColor = true;
      // 
      // panelTim
      // 
      this.panelTim.AutoScroll = true;
      this.panelTim.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelTim.Location = new System.Drawing.Point(3, 16);
      this.panelTim.Name = "panelTim";
      this.panelTim.Size = new System.Drawing.Size(442, 458);
      this.panelTim.TabIndex = 0;
      // 
      // frmSearch
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(448, 520);
      this.Controls.Add(this.panel1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmSearch";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Tìm Kiếm Nâng Cao";
      this.Load += new System.EventHandler(this.frmSearch_Load);
      this.panel1.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button btnTim;
    private System.Windows.Forms.RadioButton radioButton2;
    private System.Windows.Forms.RadioButton radioButton1;
    private System.Windows.Forms.Panel panelTim;
  }
}
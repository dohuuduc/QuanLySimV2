namespace DiDong {
  partial class frmColUpdate {
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
      this.chkAll = new System.Windows.Forms.CheckBox();
      this.panel2 = new System.Windows.Forms.Panel();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.button1 = new System.Windows.Forms.Button();
      this.panel3 = new System.Windows.Forms.Panel();
      this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.lblKey = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.lblKey);
      this.panel1.Controls.Add(this.pictureBox1);
      this.panel1.Controls.Add(this.chkAll);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(279, 35);
      this.panel1.TabIndex = 0;
      // 
      // chkAll
      // 
      this.chkAll.AutoSize = true;
      this.chkAll.Location = new System.Drawing.Point(3, 12);
      this.chkAll.Name = "chkAll";
      this.chkAll.Size = new System.Drawing.Size(58, 17);
      this.chkAll.TabIndex = 0;
      this.chkAll.Text = "Tất Cả";
      this.chkAll.UseVisualStyleBackColor = true;
      this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.checkBox1);
      this.panel2.Controls.Add(this.button1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel2.Location = new System.Drawing.Point(0, 389);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(279, 61);
      this.panel2.TabIndex = 1;
      // 
      // checkBox1
      // 
      this.checkBox1.AutoSize = true;
      this.checkBox1.Checked = true;
      this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBox1.Location = new System.Drawing.Point(3, 15);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(150, 17);
      this.checkBox1.TabIndex = 3;
      this.checkBox1.Text = "Chỉ cập nhật (dữ liệu rỗng)";
      this.checkBox1.UseVisualStyleBackColor = true;
      // 
      // button1
      // 
      this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.button1.Location = new System.Drawing.Point(0, 38);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(279, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "Chấp Nhận";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.checkedListBox1);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(0, 35);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(279, 354);
      this.panel3.TabIndex = 2;
      // 
      // checkedListBox1
      // 
      this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.checkedListBox1.FormattingEnabled = true;
      this.checkedListBox1.HorizontalScrollbar = true;
      this.checkedListBox1.Location = new System.Drawing.Point(0, 0);
      this.checkedListBox1.MultiColumn = true;
      this.checkedListBox1.Name = "checkedListBox1";
      this.checkedListBox1.Size = new System.Drawing.Size(279, 354);
      this.checkedListBox1.TabIndex = 1;
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::DiDong.Properties.Resources.Key;
      this.pictureBox1.Location = new System.Drawing.Point(67, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(20, 17);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 1;
      this.pictureBox1.TabStop = false;
      // 
      // lblKey
      // 
      this.lblKey.AutoSize = true;
      this.lblKey.Location = new System.Drawing.Point(93, 13);
      this.lblKey.Name = "lblKey";
      this.lblKey.Size = new System.Drawing.Size(10, 13);
      this.lblKey.TabIndex = 2;
      this.lblKey.Text = "-";
      // 
      // frmColUpdate
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(279, 450);
      this.Controls.Add(this.panel3);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmColUpdate";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Chọn Cột Cập Nhật";
      this.Load += new System.EventHandler(this.frmColUpdate_Load);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.panel3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.CheckBox chkAll;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.CheckedListBox checkedListBox1;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label lblKey;
  }
}
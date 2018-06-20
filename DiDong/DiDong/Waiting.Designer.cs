namespace DiDong {
  partial class Waiting {
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
      this.txtThoiGian = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.lbltieude = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // txtThoiGian
      // 
      this.txtThoiGian.AutoSize = true;
      this.txtThoiGian.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtThoiGian.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
      this.txtThoiGian.Location = new System.Drawing.Point(47, 19);
      this.txtThoiGian.Name = "txtThoiGian";
      this.txtThoiGian.Size = new System.Drawing.Size(63, 15);
      this.txtThoiGian.TabIndex = 7;
      this.txtThoiGian.Text = "00:00:00";
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::DiDong.Properties.Resources.ajax_loader;
      this.pictureBox1.Location = new System.Drawing.Point(3, 3);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(33, 30);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 6;
      this.pictureBox1.TabStop = false;
      // 
      // lbltieude
      // 
      this.lbltieude.AutoSize = true;
      this.lbltieude.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbltieude.Location = new System.Drawing.Point(47, 4);
      this.lbltieude.Name = "lbltieude";
      this.lbltieude.Size = new System.Drawing.Size(92, 15);
      this.lbltieude.TabIndex = 5;
      this.lbltieude.Text = "Đang Xử Lý...";
      // 
      // Waiting
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(172, 46);
      this.ControlBox = false;
      this.Controls.Add(this.txtThoiGian);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.lbltieude);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Waiting";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Vui Lòng Chờ...";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Waiting_FormClosing);
      this.Load += new System.EventHandler(this.Waiting_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label txtThoiGian;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label lbltieude;
  }
}
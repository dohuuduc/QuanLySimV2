using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiDong {
  public partial class frmXemTruoc : Form {
    private DataTable dataSourceDate;
    private string title;

   


    public frmXemTruoc() {
      InitializeComponent();
    }

    public DataTable DataSourceDate {
      get { return dataSourceDate; }
      set { dataSourceDate = value; }
    }

    public string Title {
      get { return title; }
      set { title = value; }
    }

  
    private void frmXemTruoc_Load(object sender, EventArgs e) {
      Application.DoEvents();
      try {
        dataGridView1.DataSource = DataSourceDate;
        this.Text = string.Format("Xem trước {0}", title);
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "frmViewDateOfBirth_Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}

using QuanLyData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DiDong {
  public partial class frmSQLServer : Form {
    public frmSQLServer() {
      InitializeComponent();
    }
    public string _strDatatable;
    public string _strTable;
    public String strTable {
      get { return _strTable; }
      set { _strTable = value; }
    }
    public String strDatatable {
      get { return _strDatatable; }
      set { _strDatatable = value; }
    }
    private void frmSQLServer_Load(object sender, EventArgs e) {
      BindColumn();
    }

    void BindColumn() {
      try {
        string str = string.Format("SELECT DB_NAME(database_id) AS [Database], database_id  FROM sys.databases where collation_name <> 'Vietnamese_CI_AS'");
        DataTable tb = SQLDatabase.ExcDataTable(str);
        cmbDatabase.Invoke((Action)delegate {
          cmbDatabase.DataSource = tb;
          cmbDatabase.ValueMember = "Database"; // --> once hes here, he just jumps out the method
          cmbDatabase.DisplayMember = "Database";
        });

      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "BindGrid");
      }
    }

    private void cmbDatabase_SelectedIndexChanged(object sender, EventArgs e) {
      try {
        string xx= ((System.Windows.Forms.ComboBox)sender).Text;
        string str = string.Format(" SELECT ROW_NUMBER() OVER(ORDER BY t1.TABLE_NAME) AS stt,isAct=0,TABLE_NAME,  stuff((SELECT distinct '; ' + cast(COLUMN_NAME as varchar(200)) " +
                                   " FROM {0}.INFORMATION_SCHEMA.COLUMNS t2 " +
                                   " where t2.TABLE_NAME = t1.TABLE_NAME " +
                                   " FOR XML PATH('')),1,1,'') as Listcolumn,So_Luong_Column = (select COUNT(*) FROM {0}.INFORMATION_SCHEMA.COLUMNS t2 where t2.TABLE_NAME = t1.TABLE_NAME), t1.TABLE_CATALOG " +
                                   " FROM {0}.INFORMATION_SCHEMA.TABLES t1",xx);
        DataTable tb = SQLDatabase.ExcDataTable(str);
        dataGridView1.Invoke((Action)delegate {
          dataGridView1.DataSource = tb;
        });
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "cmbDatabase_SelectedIndexChanged");
      }
    }

    private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) {
      if (e.ColumnIndex == 1 && e.RowIndex >= 0) {
        var value = (bool?)e.FormattedValue;
        e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
        var state = value.HasValue && value.Value ?
            RadioButtonState.CheckedNormal : RadioButtonState.UncheckedNormal;
        var size = RadioButtonRenderer.GetGlyphSize(e.Graphics, state);
        var location = new Point((e.CellBounds.Width - size.Width) / 2,
                                    (e.CellBounds.Height - size.Height) / 2);
        location.Offset(e.CellBounds.Location);
        RadioButtonRenderer.DrawRadioButton(e.Graphics, location, state);
        e.Handled = true;
      }
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {
      bool Value = System.Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["isAct"].Value);
      if (Value) {
        int Index = e.RowIndex;
        for (int row = 0; row <= dataGridView1.Rows.Count - 1; row++) {
          if (row != Index)
            dataGridView1.Rows[row].Cells["isAct"].Value = false;
        }
      }
      else {
        dataGridView1.Rows[e.RowIndex].Cells["isAct"].Value = true;
        int Index = e.RowIndex;
        for (int row = 0; row <= dataGridView1.Rows.Count - 1; row++) {
          if (row != Index)
            dataGridView1.Rows[row].Cells["isAct"].Value = false;
        }
      }
    }

    private void button1_Click(object sender, EventArgs e) {
      try {
        string tablename = "";
        string databasename = "";
        for (int row = 0; row <= dataGridView1.Rows.Count - 1; row++) {
          if (dataGridView1.Rows[row].Cells["isAct"].Value.ToString() == "1") {
            tablename = dataGridView1.Rows[row].Cells["TABLE_NAME"].Value.ToString();
            databasename = dataGridView1.Rows[row].Cells["TABLE_CATALOG"].Value.ToString();
          }
        }
        if (tablename == "") {
          MessageBox.Show("Vui lòng chọn 1 table", "Thông Báo");
        }
        else {
          strTable = tablename;
          strDatatable = databasename;
          this.DialogResult = DialogResult.OK;
          this.Close();
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "button1_Click");
      }
    }
  }
}

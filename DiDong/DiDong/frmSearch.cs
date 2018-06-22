using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyData {
  public partial class frmSearch : Form {
    private List<TableLayoutPanel> groupbox = null;
    private TableLayoutPanel tableLayoutPanel = null;
    private int _index = 0;
    public frmSearch() {
      InitializeComponent();
    }
    private void frmSearch_Load(object sender, EventArgs e) {
      groupbox = new List<TableLayoutPanel>();
      AddController(_index);
    }

    private void AddController(int index) {
      tableLayoutPanel = new TableLayoutPanel();
      tableLayoutPanel = CreateTableLayoutPanel(index);

      ComboBox comboBox = new ComboBox();
      comboBox = CreateComboxCol(index);

      TextBox textBox = new TextBox();
      textBox = CreateTextbox(index);

      ComboBox comboBoxAndOr = new ComboBox();
      comboBoxAndOr = CreateComboxAndOr(index);

      tableLayoutPanel.Controls.Add(comboBox);
      tableLayoutPanel.Controls.Add(textBox);
      tableLayoutPanel.Controls.Add(comboBoxAndOr);

      tableLayoutPanelMain.Controls.Add(tableLayoutPanel);
    }
    private void RemoveController(int Array_Tag) {
      tableLayoutPanelMain.Controls.Remove(groupbox[Array_Tag]);
    }

    private TableLayoutPanel CreateTableLayoutPanel(int index) {
      TableLayoutPanel lbl = new TableLayoutPanel();
      lbl.RowCount = 1;
      lbl.ColumnCount = 4;
      lbl.Name = string.Format("layoutPanel_", index);
      lbl.Height = 15;
      lbl.AutoSize = true;
      return lbl;
    }
   
    private TextBox CreateTextbox(int index) {
      TextBox text = new TextBox();
      text.Location = new Point(120, 20 + index * 35);
      text.Name = string.Format("txt_", index);
      text.Size = new Size(300, 20);
      text.Tag = index;
      return text;
    }
    private ComboBox CreateComboxCol(int index) {
      List<dm_column> _Columns  = SQLDatabase.Loaddm_column("select * from dm_column order by orderid");
      ComboBox com = new ComboBox();
      com.Location = new Point(120, 20 + index * 35);
      com.DropDownStyle = ComboBoxStyle.DropDownList;
      com.Name = string.Format("cmbCol_", index);
      com.DataSource = _Columns;
      com.ValueMember = "ma";
      com.DisplayMember = "name";
      com.Tag = index;
      return com;
    }
    private ComboBox CreateComboxAndOr(int index) {
      DataTable table = new DataTable();
      table.Columns.Add("ma", typeof(string));
      table.Columns.Add("name", typeof(string));

      table.Rows.Add("", "");
      table.Rows.Add("And","And");
      table.Rows.Add("Or", "Or");
      table.Rows.Add("Remover", "Remover");


      ComboBox com = new ComboBox();
      com.Location = new Point(120, 20 + index * 35);
      com.Width = 80;
      com.DropDownStyle = ComboBoxStyle.DropDownList;
      com.Name = string.Format("cmbAndOr_", index);
      com.DataSource = table;
      com.ValueMember = "ma";
      com.DisplayMember = "name";
      com.Tag = index;
      com.SelectedIndexChanged += Com_SelectedIndexChanged;
      
      return com;
    }

    private void Com_SelectedIndexChanged(object sender, EventArgs e) {
      try {
        int Array_Tag = (int)((System.Windows.Forms.ComboBox)sender).Tag;
        string strDK = ((System.Windows.Forms.ComboBox)sender).SelectedValue.ToString();
        if (strDK.Equals("And") || strDK.Equals("Or")) {
          AddController(_index++);
        }
        else if(strDK.Equals("Remover")){
          RemoveController(Array_Tag);
        }
      }
      catch (Exception ex) {

        MessageBox.Show(ex.Message, "Com_SelectedIndexChanged");
      }
    }
  }
}

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

namespace DiDong {
  public partial class frmEditInfo : Form {
    private string _id = "";
    private Dictionary<int, TableLayoutPanel> groupbox = null;
    DataTable table;
    public frmEditInfo() {
      InitializeComponent();
    }
    public String Id {
      get { return _id; }
      set { _id = value; }
    }
    private void frmEditInfo_Load(object sender, EventArgs e) {
      table = SQLDatabase.ExcDataTable(string.Format("select * from root where id='{0}'", Id));
      groupbox = new Dictionary<int, TableLayoutPanel>();
      List<dm_column> _Columns = SQLDatabase.Loaddm_column("select * from dm_column order by orderid");
      int index = 0;
      foreach (var item in _Columns) {
        AddController(item, index++);
      }
    }

    private TableLayoutPanel CreateTableLayoutPanel(dm_column index) {
      TableLayoutPanel lbl = new TableLayoutPanel();
      lbl.RowCount = 1;
      lbl.ColumnCount = 2;
      lbl.Name = string.Format("layoutPanel_", index.ma);
      lbl.Height = 15;
      lbl.AutoSize = true;
      return lbl;
    }
    private void AddController(dm_column model,int index) {
      TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
      tableLayoutPanel = CreateTableLayoutPanel(model);

      Label lbl = new Label();
      lbl = CreateLable(model,index);

      TextBox textBox = new TextBox();
      textBox = CreateTextbox(model,index);

      tableLayoutPanel.Controls.Add(lbl);
      tableLayoutPanel.Controls.Add(textBox);

      groupbox.Add(index, tableLayoutPanel);
      tableLayoutPanelMain.Controls.Add(tableLayoutPanel);
    }


    private TextBox CreateTextbox(dm_column model,int index) {
      TextBox text = new TextBox();
      text.Location = new Point(120, 20 + index * 35);
      text.Name = string.Format("txt_", model.ma);
      text.Tag = model.ma;
      text.Size = new Size(300, 20);
      text.Text = table.Rows[0][model.ma].ToString();
      return text;
    }

    private Label CreateLable(dm_column model,int index) {
      Label text = new Label();
      text.Location = new Point(120, 20 + index * 35);
      text.Name = string.Format("lbl_", model.ma);
      text.Size = new Size(150, 20);
      text.Tag = model.ma;
      text.Text = model.name;
      return text;
    }

    private void button1_Click(object sender, EventArgs e) {
      try {
        string strcommand = "update root set ";
        foreach (var item in groupbox) {
          TableLayoutPanel layoutPanel = (TableLayoutPanel)item.Value.Controls.Container;
          TextBox textBox = (TextBox)layoutPanel.GetControlFromPosition(1, 0);
          if(textBox.Text!="")
            strcommand += string.Format("{0}=N'{1}',", textBox.Tag, textBox.Text);
        }
        strcommand = strcommand.Substring(0, strcommand.Length - 1);
        strcommand += string.Format(" where id='{0}'", Id);
        if (SQLDatabase.ExcNonQuery(strcommand)) {
          MessageBox.Show("Cập Nhật Thành Công");
        }
        else {
          MessageBox.Show("Cập Nhật Thất Bại");
        }
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "button1_Click");
      }
    }
  }
}

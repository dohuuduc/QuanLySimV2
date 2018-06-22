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
  public partial class frmColUpdate : Form {
    private string _strSQL = "";
    public frmColUpdate() {
      InitializeComponent();
    }
    public String strSQL {
      get { return _strSQL; }
      set { _strSQL = value; }
    }

    private void frmColUpdate_Load(object sender, EventArgs e) {
      List<dm_column> dm_Columns = SQLDatabase.Loaddm_column("select * from dm_column order by orderid");
      var checkBoxList = (ListBox)checkedListBox1;
      checkBoxList.DataSource = dm_Columns;
      checkBoxList.DisplayMember = "name";
      checkBoxList.ValueMember = "ma";
    }

    private void chkAll_CheckedChanged(object sender, EventArgs e) {
      for (int x = 0; x < checkedListBox1.Items.Count; x++) {
        checkedListBox1.SetItemChecked(x, chkAll.Checked);
      }
    }
    private string setColUp() {
      string XX = "SET ";
      foreach (dm_column item in (List<dm_column>)checkedListBox1.CheckedItems) {
        XX += string.Format("{0} = COALESCE(t.{0}, s.{0}),",item.ToString());
      }
      return XX.Substring(0, XX.Length - 1);
    }
    private string setColUp2() {
      string XX = "SET ";
      foreach (var item in checkedListBox1.CheckedItems) {
        XX += string.Format("root.{0} = t2.{0},", item.ToString());
      }
      return XX.Substring(0, XX.Length - 1);
    }
    private string setSelectUp() {
      string xx = "";
      foreach (var item in checkedListBox1.CheckedItems) {
        xx += string.Format("COALESCE(CONVERT(VARBINARY(8),{0}),'**NULL_VALUE**') as {0}, ",item.ToString());
      }
      return xx.Substring(0, xx.Length - 1);
    }
    private string inSQL() {
      string xx = "";
      foreach (var item in checkedListBox1.CheckedItems) {
        xx += string.Format("{0},", item.ToString());
      }
      return xx.Substring(0, xx.Length - 1);
    }

    private void button1_Click(object sender, EventArgs e) {
      try {
        dm_column dm_Column = SQLDatabase.Loaddm_column("select * from dm_column where isKey=1").FirstOrDefault();
        string strCommand = "";
        if (checkBox1.Checked) {
          strCommand = " UPDATE t " +
             setColUp() +
             string.Format("from dbo.root t inner join dbo.import s on t.{0}=s.{0}", dm_Column.ma) +
             string.Format("where EXISTS ( select 1 from (SELECT {0},", dm_Column.ma) +
             setSelectUp() +
             string.Format(" from dbo.import ) as qry UNPIVOT(col FOR column_name IN ({0})) AS unpvt WHERE CAST(col AS VARCHAR(15)) = '**NULL_VALUE**' AND t.{1} = {1} )", inSQL(), dm_Column.ma);
        }
        else {
          strCommand = "update dbo.root " +
            setColUp2() +
            "from dbo.root t1 , dbo.import t2" +
            String.Format("where t1.{0}=t2.{0}",dm_Column.ma);
        }
        strSQL = strCommand;
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "button1_Click");
      }
    }
  }
}

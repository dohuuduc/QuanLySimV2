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
    private dm_column columnKey;
    private bool isUpdate = false;
    public frmColUpdate() {
      InitializeComponent();
    }
    public String strSQL {
      get { return _strSQL; }
      set { _strSQL = value; }
    }
    public dm_column ColumnKey {
      get { return columnKey; }
      set { columnKey = value; }
    }
    public bool IsUpdate {
      get { return isUpdate; }
      set { isUpdate = value; }
    }


    private void frmColUpdate_Load(object sender, EventArgs e) {
      List<dm_column> dm_Columns;
      if (IsUpdate) 
        dm_Columns = SQLDatabase.Loaddm_column("select * from dm_column where isKey=0 order by orderid");
      else  
        dm_Columns = SQLDatabase.Loaddm_column("select * from dm_column order by orderid");

      var checkBoxList = (ListBox)checkedListBox1;
      checkBoxList.DataSource = dm_Columns;
      checkBoxList.DisplayMember = "name";
      checkBoxList.ValueMember = "ma";
      lblKey.Text =string.Format("Mã: {0} - Tên: {1} ", ColumnKey.ma, ColumnKey.name);

      checkBox1.Enabled = isUpdate;

    }

    private void chkAll_CheckedChanged(object sender, EventArgs e) {
      for (int x = 0; x < checkedListBox1.Items.Count; x++) {
        checkedListBox1.SetItemChecked(x, chkAll.Checked);
      }
    }
    private string setColUp() {
      string XX = " SET ";
      foreach (dm_column item in checkedListBox1.CheckedItems) {
        XX += string.Format("{0} = COALESCE(t.{0}, s.{0}), ", item.ma);
      }
      return XX.Substring(0, XX.Length - 2);
    }
    private string setColUp2() {
      string XX = "SET ";
      foreach (dm_column item in checkedListBox1.CheckedItems) {
        XX += string.Format("root.{0} = t2.{0}, ", item.ma);
      }
      return XX.Substring(0, XX.Length - 2);
    }
    private string setSelectInsert(string x) {
      string xx = "";
      foreach (dm_column item in checkedListBox1.CheckedItems) {
        xx += string.Format("{0}{1}, ",x==""?"": string.Format("{0}.",x), item.ma);
      }
      return xx.Substring(0, xx.Length - 2);
    }
    private string setSelectUp() {
      string xx = "";
      foreach (dm_column item in checkedListBox1.CheckedItems) {
        xx += string.Format("COALESCE(CONVERT(VARBINARY(8),{0}),'**NULL_VALUE**') as {0}, ",item.ma);
      }
      return xx.Substring(0, xx.Length - 2);
    }
    private string inSQL() {
      string xx = "";
      foreach (dm_column item in checkedListBox1.CheckedItems) {
        xx += string.Format("{0},", item.ma);
      }
      return xx.Substring(0, xx.Length - 1);
    }

    private void button1_Click(object sender, EventArgs e) {
      try {
        if (checkedListBox1.CheckedItems.Count == 0) {
          MessageBox.Show("Vui lòng chọn cột cần cập  nhật", "Thông Báo");
          return;
        }
        dm_column dm_Column = SQLDatabase.Loaddm_column("select * from dm_column where isKey=1").FirstOrDefault();
        dm_batdongbo dm_Batdongbo = SQLDatabase.Loaddm_batdongbo("select * from dm_batdongbo where isAct=1").FirstOrDefault();
        string strCommand = "";
        if (IsUpdate) {
          if (checkBox1.Checked) {
            strCommand = " UPDATE t WITH(TABLOCK) " +
               setColUp() +
               string.Format(" from dbo.root t inner join dbo.import s on t.{0}=s.{0} ", dm_Column.ma) +
               string.Format(" where EXISTS ( select 1 from (SELECT {0}, ", dm_Column.ma) +
               setSelectUp() +
               string.Format(" from dbo.import ) as qry UNPIVOT(col FOR column_name IN ({0})) AS unpvt WHERE CAST(col AS VARCHAR(15)) = '**NULL_VALUE**' AND t.{1} = {1} )", inSQL(), dm_Column.ma) +
               string.Format(" {0}", dm_Batdongbo.ma);
          }
          else {
            strCommand = "update dbo.root WITH(TABLOCK)" +
              setColUp2() +
              " from dbo.root t1 , dbo.import t2" +
              string.Format(" where t1.{0}=t2.{0} ", dm_Column.ma) +
              string.Format(" {0} ", dm_Batdongbo.ma);
          }
          strSQL = strCommand;
        }
        else {
          strCommand  = string.Format(" insert into dbo.root WITH(TABLOCK)({0}) ", setSelectInsert(""));
          strCommand += string.Format(" select {0} from dbo.import a left join dbo.root b on a.{1}=b.{1} where b.{1} is null ", setSelectInsert("a"),dm_Column.ma);
          strCommand += string.Format(" {0} ", dm_Batdongbo.ma);
          strSQL = strCommand;
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

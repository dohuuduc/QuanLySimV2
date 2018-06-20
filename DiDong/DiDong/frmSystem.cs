using DiDong;
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

namespace QuanLyData {
  public partial class frmSystem : Form {
    private string _strDatabase = "";
    private cau_hinh cau_Hinh = null;

    public frmSystem() {
      InitializeComponent();
    }
    public String strDatabase {
      get { return _strDatabase; }
      set { _strDatabase = value; }
    }

    private void frmSystem_Load(object sender, EventArgs e) {
      new Waiting(() => {
        cau_Hinh = SQLDatabase.Loadcau_hinh("select * from cau_hinh").FirstOrDefault();
        BindColumn();
        BindCharacter();
        BindSQLBatdongbo();
      }).ShowDialog();

      if (cau_Hinh.MaxTop == -1) {
        ckhSoLuongHienThi.Checked = true;
      }
      else {
        ckhSoLuongHienThi.Checked = false;
        numericUpDown1.Value = cau_Hinh.MaxTop;
      }
      radText.Checked = cau_Hinh.IsExportTxt;
      radExcel.Checked = !cau_Hinh.IsExportTxt;
      ckhDelImport.Checked = cau_Hinh.DelImportTruocImport;
    }
    void BindColumn() {
      try {
        string str = string.Format("select ROW_NUMBER() OVER(ORDER BY OrderId) AS stt, * from dm_column  order by OrderId");
        DataTable tb = SQLDatabase.ExcDataTable(str);
        GridViewColumn.Invoke((Action)delegate {
          GridViewColumn.DataSource = tb;
        });
         
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "BindGrid");
      }
    }
    void BindCharacter() {
      try {
        string str = string.Format("select * from dm_Character order by OrderId");
        DataTable tb = SQLDatabase.ExcDataTable(str);
        gridviewCharacter.Invoke((Action)delegate {
          gridviewCharacter.DataSource = tb;
        });
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "BindGrid");
      }
    }
    void BindSQLBatdongbo() {
      try {
        string str = string.Format("select * from [dbo].[dm_batdongbo] order by OrderId");
        DataTable tb = SQLDatabase.ExcDataTable(str);
        gridviewSQLDaluong.Invoke((Action)delegate {
          gridviewSQLDaluong.DataSource = tb;
        });
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "BindSQLBatdongbo");
      }
    }
   
    private void cậpNhậtToolStripMenuItem_Click(object sender, EventArgs e) {
      try {
        string str = string.Format("SELECT COLUMN_NAME,CHARACTER_MAXIMUM_LENGTH FROM {0}.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'Root' AND CHARACTER_MAXIMUM_LENGTH IS NOT NULL", _strDatabase);
        DataTable tb = SQLDatabase.ExcDataTable(str);
        foreach (DataRow item in tb.Rows) {
          dm_column root = new dm_column();
          root.name = item["COLUMN_NAME"].ToString();
          root.size = ConvertType.ToInt(item["CHARACTER_MAXIMUM_LENGTH"]);
          root.isAct = true;
          root.isKey = false;
          root.isOrder = false;
          root.IsReport = true;
          root.isSearch = false;
          root.ma = item["COLUMN_NAME"].ToString();
          root.orderid = ConvertType.ToInt(SQLDatabase.ExcDataTable("SELECT MAX(orderid) from dm_column"))+1;

          if (ConvertType.ToInt(SQLDatabase.ExcDataTable(string.Format("select count(*) from dm_column where ma='{0}'", root.ma)).Rows[0][0]) == 0) {
            SQLDatabase.Adddm_column(root);
          }
          else {
            dm_column mode = SQLDatabase.Loaddm_column(string.Format("select * from dm_column where ma='{0}'",root.ma)).FirstOrDefault();
            mode.size = root.size;
            SQLDatabase.Updm_column(mode);
          }
        }
        BindColumn();
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "cậpNhậtToolStripMenuItem_Click");
      }
    }

    private void làmTươiToolStripMenuItem_Click(object sender, EventArgs e) {
      BindColumn();
    }

    private void GridViewColumn_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
      try {
        
        if (e.RowIndex != -1) {
          string Id = GridViewColumn.Rows[e.RowIndex].Cells["id"].Value.ToString();
          string name = GridViewColumn.Rows[e.RowIndex].Cells["name"].Value.ToString();
          bool isActive = (Boolean)GridViewColumn.Rows[e.RowIndex].Cells["IsAct"].Value;
          bool isKey = (Boolean)GridViewColumn.Rows[e.RowIndex].Cells["isKey"].Value;
          bool IsReport = (Boolean)GridViewColumn.Rows[e.RowIndex].Cells["IsReport"].Value;
          bool isSearch = (Boolean)GridViewColumn.Rows[e.RowIndex].Cells["isSearch"].Value;
          bool isOrder = (Boolean)GridViewColumn.Rows[e.RowIndex].Cells["isOrder"].Value;
          int vitri = ConvertType.ToInt(GridViewColumn.Rows[e.RowIndex].Cells["orderid"].Value);

          dm_column dm_column = SQLDatabase.Loaddm_column(string.Format("select * from dm_column where id='{0}'", Id)).FirstOrDefault();
          dm_column.name = name;
          dm_column.isAct = isActive;
          dm_column.isKey = isKey;
          dm_column.IsReport = IsReport;
          dm_column.isSearch = isSearch;
          dm_column.isOrder = isOrder;
          dm_column.orderid = vitri;
          SQLDatabase.Updm_column(dm_column);

         // BindColumn();
        }
      }
      catch (Exception ex) {
        MessageBox.Show("Unable to save the record. There might be a blank cell. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void GridViewColumn_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) {
      if ((e.ColumnIndex ==5) && e.RowIndex >= 0) {
        var value = (bool?)e.FormattedValue;
        e.Paint(e.CellBounds, DataGridViewPaintParts.All &
                                ~DataGridViewPaintParts.ContentForeground);
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

    private void GridViewColumn_CellContentClick(object sender, DataGridViewCellEventArgs e) {
      bool Value = System.Convert.ToBoolean(GridViewColumn.Rows[e.RowIndex].Cells["isKey"].Value);
      string id = GridViewColumn.Rows[e.RowIndex].Cells["id"].Value.ToString();
      if (Value) {
        int Index = e.RowIndex;
        for (int row = 0; row <= GridViewColumn.Rows.Count - 1; row++) {
          if (row != Index)
            GridViewColumn.Rows[row].Cells["isKey"].Value = false;
        }
      }
      else {
        GridViewColumn.Rows[e.RowIndex].Cells["isKey"].Value = true;
        int Index = e.RowIndex;
        for (int row = 0; row <= GridViewColumn.Rows.Count - 1; row++) {
          if (row != Index)
            GridViewColumn.Rows[row].Cells["isKey"].Value = false;
        }
      }

      SQLDatabase.ExcNonQuery("update dm_column set [isKey]=0");
      SQLDatabase.ExcNonQuery(string.Format("update dm_column set [isKey]=1 where id='{0}'",id));
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e) {
      numericUpDown1.Enabled = !ckhSoLuongHienThi.Checked;
    }

    private void button2_Click(object sender, EventArgs e) {
      try {
        if (chkDelImport.Checked) {
          SQLDatabase.ExcNonQuery("TRUNCATE TABLE dbo.import");
        }
        if (chkDelImport.Checked) {
          SQLDatabase.ExcNonQuery("TRUNCATE TABLE dbo.root");
        }
        MessageBox.Show("Hoàn thành xoá số liệu", "Thông báo");
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "button2_Click");
      }
    }

    private void gridviewCharacter_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
      try {

        if (e.RowIndex != -1) {
          string Id = gridviewCharacter.Rows[e.RowIndex].Cells["idCharacter"].Value.ToString();
          string ma = gridviewCharacter.Rows[e.RowIndex].Cells["maCharacter"].Value.ToString();
          string name = gridviewCharacter.Rows[e.RowIndex].Cells["nameCharacter"].Value.ToString();
          bool isActive = (Boolean)gridviewCharacter.Rows[e.RowIndex].Cells["isActCharacter"].Value;
          int vitri = ConvertType.ToInt(gridviewCharacter.Rows[e.RowIndex].Cells["orderidCharacter"].Value);

          dm_Character dm_column = SQLDatabase.Loaddm_Character(string.Format("select * from dm_Character where id='{0}'", Id)).FirstOrDefault();
          dm_column.ma = ma;
          dm_column.name = name;
          dm_column.isAct = isActive;
          dm_column.orderid = vitri;
          SQLDatabase.Updm_Character(dm_column);

          //BindCharacter();
        }
      }
      catch (Exception ex) {
        MessageBox.Show("Unable to save the record. There might be a blank cell. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void lamTươiToolStripMenuItem_Click(object sender, EventArgs e) {
      BindCharacter();
    }

    private void thêmToolStripMenuItem_Click(object sender, EventArgs e) {
      try {
        dm_Character model = new dm_Character();
        model.ma = "";
        model.name = "";
        model.isAct = false;
        model.orderid = ConvertType.ToInt(SQLDatabase.ExcDataTable(string.Format("select max([orderid]) from dm_Character")).Rows[0][0]) + 1;
        SQLDatabase.Adddm_Character(model);
        BindCharacter();
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "thêmToolStripMenuItem_Click");
      }
    }

    private void gridviewCharacter_CellClick(object sender, DataGridViewCellEventArgs e) {
      try {
        if (e.ColumnIndex != -1) {
          if (e.ColumnIndex == 0) {
            string Id = gridviewCharacter.Rows[e.RowIndex].Cells["idCharacter"].Value.ToString();
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc là sẽ xoá Character?", "Xoá Character", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) {
              //do something
              SQLDatabase.ExcNonQuery(String.Format("DELETE FROM dm_Character WHERE ID='{0}'", Id));
              BindCharacter();
            }
          }
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "gridviewCharacter_CellClick");
      }
    }

    private void gridviewCharacter_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) {
      if (e.ColumnIndex ==4 && e.RowIndex >= 0) {
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

    private void gridviewCharacter_CellContentClick(object sender, DataGridViewCellEventArgs e) {
      bool Value = System.Convert.ToBoolean(gridviewCharacter.Rows[e.RowIndex].Cells["isActCharacter"].Value);
      string id = gridviewCharacter.Rows[e.RowIndex].Cells["idCharacter"].Value.ToString();
      if (Value) {
        int Index = e.RowIndex;
        for (int row = 0; row <= gridviewCharacter.Rows.Count - 1; row++) {
          if (row != Index)
            gridviewCharacter.Rows[row].Cells["isActCharacter"].Value = false;
        }
      }
      else {
        gridviewCharacter.Rows[e.RowIndex].Cells["isActCharacter"].Value = true;
        int Index = e.RowIndex;
        for (int row = 0; row <= gridviewCharacter.Rows.Count - 1; row++) {
          if (row != Index)
            gridviewCharacter.Rows[row].Cells["isActCharacter"].Value = false;
        }
      }

      SQLDatabase.ExcNonQuery("update dm_Character set [isAct]=0");
      SQLDatabase.ExcNonQuery(string.Format("update dm_Character set [isAct]=1 where id='{0}'", id));
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
      LinkLabel.Link link = new LinkLabel.Link();
      link.LinkData = "https://msdn.microsoft.com/en-us/library/windows/desktop/dd317756(v=vs.85).aspx";
      linkLabel1.Links.Add(link);
    }

    private void button3_Click(object sender, EventArgs e) {
      try {
        if (ckhvalueskeySearch.Checked) {
          DialogResult dialogResult = MessageBox.Show("Bạn có muốn chuẩn hoá dữ liệu tìm kiếm lại từ đầu tất cả dữ liệu không?\n 1-Yes:Chuẩn hoá lại từ đầu. \n 2-No: Chỉ những thông tin chưa được chuẩn hoá", "Some Title", MessageBoxButtons.YesNo);
          if (dialogResult == DialogResult.Yes) {
            dm_batdongbo model = SQLDatabase.Loaddm_batdongbo("select * from dm_batdongbo where isAct=1").FirstOrDefault();
            SQLDatabase.ExcNonQuery(String.Format("update root set isSearch = 0 {0}", model.ma));
          }

          List<dm_column> dm_Columns = SQLDatabase.Loaddm_column("select * from dm_column where isSearch=1  order by orderid ");
          string strcommand = "update root set valueskeySearch=";
          foreach (dm_column item in dm_Columns) {
            strcommand += string.Format("isnull(dbo.GetUnsignString({0}), '') ", item.ma) + "+";
          }
          strcommand = strcommand.Substring(0, strcommand.Length - 1);
          strcommand += ", isSearch=1 where isSearch=0";
          SQLDatabase.ExcNonQuery(strcommand);
          MessageBox.Show("Chuẩn hoá thông tin tìm kiếm thành công", "Thông Báo");
        }

      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "button3_Click");
      }
    }

    private void button1_Click(object sender, EventArgs e) {
      try {
        cau_hinh cau_Hinh = SQLDatabase.Loadcau_hinh("select * from cau_hinh").FirstOrDefault();
        cau_Hinh.DelImportTruocImport = ckhDelImport.Checked;
        cau_Hinh.MaxTop = ckhSoLuongHienThi.Checked ? -1 : ConvertType.ToInt(numericUpDown1.Value);
        cau_Hinh.IsExportTxt = radText.Checked;
        //cau_Hinh.idBatdongbo = ConvertType.ToInt(cmdBatDongBo.SelectedValue);
        if (SQLDatabase.Upcau_hinh(cau_Hinh)) {
          MessageBox.Show("Lưu cấu hình thành công, vui lòng khởi động lại hệ thống", "Thông Báo");
        }
        else {
          MessageBox.Show("Lưu cấu hình thất bại", "Thông Báo");
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "button1_Click");
      }
    }

    private void gridviewSQLDaluong_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) {
      if (e.ColumnIndex == 5 && e.RowIndex >= 0) {
        var value = (bool?)e.FormattedValue;
        e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
        var state = value.HasValue && value.Value ?  RadioButtonState.CheckedNormal : RadioButtonState.UncheckedNormal;
        var size = RadioButtonRenderer.GetGlyphSize(e.Graphics, state);
        var location = new Point((e.CellBounds.Width - size.Width) / 2,
                                    (e.CellBounds.Height - size.Height) / 2);
        location.Offset(e.CellBounds.Location);
        RadioButtonRenderer.DrawRadioButton(e.Graphics, location, state);
        e.Handled = true;
      }
    }

    private void gridviewSQLDaluong_CellContentClick(object sender, DataGridViewCellEventArgs e) {
      bool Value = System.Convert.ToBoolean(gridviewSQLDaluong.Rows[e.RowIndex].Cells["isActSQLBatdongbo"].Value);
      string id = gridviewSQLDaluong.Rows[e.RowIndex].Cells["idSQLBatdongbo"].Value.ToString();
      if (Value) {
        int Index = e.RowIndex;
        for (int row = 0; row <= gridviewSQLDaluong.Rows.Count - 1; row++) {
          if (row != Index)
            gridviewSQLDaluong.Rows[row].Cells["isActSQLBatdongbo"].Value = false;
        }
      }
      else {
        gridviewSQLDaluong.Rows[e.RowIndex].Cells["isActSQLBatdongbo"].Value = true;
        int Index = e.RowIndex;
        for (int row = 0; row <= gridviewSQLDaluong.Rows.Count - 1; row++) {
          if (row != Index)
            gridviewSQLDaluong.Rows[row].Cells["isActSQLBatdongbo"].Value = false;
        }
      }

      SQLDatabase.ExcNonQuery("update [dm_batdongbo] set [isAct]=0");
      SQLDatabase.ExcNonQuery(string.Format("update [dm_batdongbo] set [isAct]=1 where id='{0}'", id));
    }

    private void xoaToolStripMenuItem1_Click(object sender, EventArgs e) {
      try {
        dm_batdongbo model = new dm_batdongbo();
        model.ma = "";
        model.name = "";
        model.isAct = false;
        model.orderid = ConvertType.ToInt(SQLDatabase.ExcDataTable(string.Format("select max([orderid]) from dm_batdongbo")).Rows[0][0]) + 1;
        SQLDatabase.Adddm_batdongbo(model);
        BindSQLBatdongbo();
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "xoaToolStripMenuItem1_Click");
      }
    }

    private void gridviewSQLDaluong_CellClick(object sender, DataGridViewCellEventArgs e) {
      try {
        if (e.ColumnIndex != -1) {
          if (e.ColumnIndex == 0) {
            string Id = gridviewSQLDaluong.Rows[e.RowIndex].Cells["idSQLBatdongbo"].Value.ToString();
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc là sẽ xoá SQL bất đồng bộ?", "Xoá Character", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) {
              //do something
              SQLDatabase.ExcNonQuery(String.Format("DELETE FROM [dm_batdongbo] WHERE ID='{0}'", Id));
              BindSQLBatdongbo();
            }
          }
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "gridviewSQLDaluong_CellClick");
      }
    }

    private void lamTươiToolStripMenuItem1_Click(object sender, EventArgs e) {
      BindSQLBatdongbo();
    }

    private void gridviewSQLDaluong_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
      try {

        if (e.RowIndex != -1) {
          string Id = gridviewSQLDaluong.Rows[e.RowIndex].Cells["idSQLBatdongbo"].Value.ToString();
          string ma = gridviewSQLDaluong.Rows[e.RowIndex].Cells["maSQLBatdongbo"].Value.ToString();
          string name = gridviewSQLDaluong.Rows[e.RowIndex].Cells["nameSQLBatdongbo"].Value.ToString();
          bool isActive = (Boolean)gridviewSQLDaluong.Rows[e.RowIndex].Cells["isActSQLBatdongbo"].Value;
          int vitri = ConvertType.ToInt(gridviewSQLDaluong.Rows[e.RowIndex].Cells["orderidSQLBatdongbo"].Value);

          dm_batdongbo dm_column = SQLDatabase.Loaddm_batdongbo(string.Format("select * from dm_batdongbo where id='{0}'", Id)).FirstOrDefault();
          dm_column.ma = ma;
          dm_column.name = name;
          dm_column.isAct = isActive;
          dm_column.orderid = vitri;
          SQLDatabase.Updm_batdongbo(dm_column);

          //BindCharacter();
        }
      }
      catch (Exception ex) {
        MessageBox.Show("Unable to save the record. There might be a blank cell. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}

using DiDong;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace QuanLyData {
  public partial class frmSystem : Form {
    private string _strDatabase = "";
    private cau_hinh cau_Hinh = null;
    private Dictionary<int, TableLayoutPanel> groupbox = null;
    private int _index = 0;

    public frmSystem() {
      InitializeComponent();
    }
    public String strDatabase {
      get { return _strDatabase; }
      set { _strDatabase = value; }
    }

    private void frmSystem_Load(object sender, EventArgs e) {
      groupbox = new Dictionary<int, TableLayoutPanel>();
      AddController(_index);
      new Waiting(() => {
        cau_Hinh = SQLDatabase.Loadcau_hinh("select * from cau_hinh").FirstOrDefault();
        BindColumn();
        BindFont();
        BindSQLBatdongbo();
        Bindparallelism();
        Binddm_madausoquocgia();
        BindNhamang();
        BindChuanHoaCotDienThoai();
        BindchkListChuanHoa();
        BindKhuVucLoai();
        BindLoaiKhuVuc();
        BindNhaMang();
        BindChar();
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
      chkDelChartrongfile.Checked = cau_Hinh.isRemoveCharInFileImport;

      cmbChuanHoa.SelectedIndex = 0;
      cmbLoaiTinhThanhXa.SelectedIndex = 0;


      this.Text = string.Format("{0} - {1}", this.Text, SQLDatabase.ExcDataTable("select @@VERSION").Rows[0][0].ToString());

      DataTable table = SQLDatabase.ExcDataTable("SELECT cpu_count AS Logical_CPU_Count , cpu_count / hyperthread_ratio AS Physical_CPU_Count FROM sys.dm_os_sys_info ;");
      lblLogincal.Text = table.Rows[0][0].ToString();
      lblPhysical.Text = table.Rows[0][1].ToString();
    }
    private void button5_Click(object sender, EventArgs e) {
      AddController(++_index);
    }
    private void button6_Click(object sender, EventArgs e) {
      if (_index == 0) return;
      RemoveController();
    }
    private void RemoveController() {
      --_index;
      tableLayoutPanel1.Controls.Remove(groupbox.LastOrDefault().Value);
      groupbox.Remove(groupbox.LastOrDefault().Key);
    }
    private void AddController(int index) {
      TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
      tableLayoutPanel = CreateTableLayoutPanel(index);
      ComboBox comboBoxCol = new ComboBox();
      comboBoxCol = CreateComboxCol(index);
      tableLayoutPanel.Controls.Add(comboBoxCol);

      ComboBox comboBoxLoai = new ComboBox();
      comboBoxLoai = CreateComboxLoai(index);
      tableLayoutPanel.Controls.Add(comboBoxLoai);


      groupbox.Add(index, tableLayoutPanel);
      tableLayoutPanel1.Controls.Add(tableLayoutPanel);
    }

    private TableLayoutPanel CreateTableLayoutPanel(int index) {
      TableLayoutPanel layout = new TableLayoutPanel();
      layout.RowCount = 1;
      layout.ColumnCount = 2;
      layout.Name = string.Format("layoutPanel_", index);
      layout.Height = 15;
      layout.AutoSize = true;
      return layout;
    }
    private ComboBox CreateComboxCol(int index) {
      List<dm_column> _Columns = SQLDatabase.Loaddm_column("select  * from dm_column  order by OrderId");
      ComboBox com = new ComboBox();
      com.Location = new Point(0, 20 + index * 35);
      com.DropDownStyle = ComboBoxStyle.DropDownList;
      com.Name = string.Format("cmbCol_", index);
      com.DataSource = _Columns;
      com.ValueMember = "ma";
      com.DisplayMember = "name";
      com.Tag = index;
      return com;
    }

    private void BindLoaiKhuVuc() {
      string str = string.Format("select  * from dm_khuvucLoai  order by OrderId");
      DataTable tb = SQLDatabase.ExcDataTable(str);
      cmbLoaiTinhThanhXa.Invoke((Action)delegate {
        cmbLoaiTinhThanhXa.DataSource = tb;
        cmbLoaiTinhThanhXa.ValueMember = "id";
        cmbLoaiTinhThanhXa.DisplayMember = "name";
      });
    }

    private ComboBox CreateComboxLoai(int index) {
      List<dm_khuvucLoai> khuvucLoais = SQLDatabase.Loaddm_khuvucLoai("select  * from dm_khuvucLoai  order by OrderId");

      ComboBox com = new ComboBox();
      com.Width = 100;
      com.Location = new Point(120, 20 + index * 35);
      com.DropDownStyle = ComboBoxStyle.DropDownList;
      com.Name = string.Format("cmbLoai_", index);
      com.DataSource = khuvucLoais;
      com.ValueMember = "id";
      com.DisplayMember = "name";
      com.Tag = index;
      return com;
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
    void BindChuanHoaCotDienThoai() {
      try {
        string str = string.Format("select ma,name from dm_column  order by OrderId");
        DataTable tb = SQLDatabase.ExcDataTable(str);
        cmbCotDienThoai.Invoke((Action)delegate {
          cmbCotDienThoai.DataSource = tb;
          cmbCotDienThoai.ValueMember = "ma"; // --> once hes here, he just jumps out the method
          cmbCotDienThoai.DisplayMember = "name";
        });


        cmboCotDienThoaiTinhthanh.Invoke((Action)delegate {
          cmboCotDienThoaiTinhthanh.DataSource = tb;
          cmboCotDienThoaiTinhthanh.ValueMember = "ma"; // --> once hes here, he just jumps out the method
          cmboCotDienThoaiTinhthanh.DisplayMember = "name";
        });
        DataTable dataTable = tb.Copy();
        cmbLocTrung.Invoke((Action)delegate {
          cmbLocTrung.DataSource = dataTable;
          cmbLocTrung.ValueMember = "ma"; // --> once hes here, he just jumps out the method
          cmbLocTrung.DisplayMember = "name";
        });

        dm_column _Columns = SQLDatabase.Loaddm_column("select  * from dm_column  where isKey=1").FirstOrDefault();
        cmbLocTrung.SelectedValue = _Columns.ma;
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "BindGrid");
      }
    }
    void BindchkListChuanHoa() {
      try {
        string str = string.Format("select ma,name from dm_column  order by OrderId");
        DataTable tb = SQLDatabase.ExcDataTable(str);
        chkListChuanHoa.Invoke((Action)delegate {
          chkListChuanHoa.DataSource = tb;
          chkListChuanHoa.ValueMember = "ma"; // --> once hes here, he just jumps out the method
          chkListChuanHoa.DisplayMember = "name";
        });

      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "BindGrid");
      }
    }

    void BindNhamang() {
      try {
        string str = string.Format("select id,nhamang from dm_nhamang where parentId is null order by orderid");
        DataTable tb = SQLDatabase.ExcDataTable(str);
        cmbNhaMang.Invoke((Action)delegate {
          cmbNhaMang.DataSource = tb;
          cmbNhaMang.ValueMember = "id"; // --> once hes here, he just jumps out the method
          cmbNhaMang.DisplayMember = "nhamang";
        });

      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "BindGrid");
      }
    }
    void BindKhuVuc() {
      try {
        int idNhamang = ConvertType.ToInt(cmbNhaMang.SelectedValue.ToString());
        int idloai = cmbLoaiTinhThanhXa.SelectedIndex;

        string str = string.Format("select  ROW_NUMBER() OVER(ORDER BY ma) AS stt,* from dm_khuvuc where loai={0} and Idnhamang={1} order by ma", idloai, idNhamang);
        DataTable tb = SQLDatabase.ExcDataTable(str);
        gridviewKhuVuc.Invoke((Action)delegate {
          gridviewKhuVuc.DataSource = tb;
        });
        groupBox12.Invoke((Action)delegate {
          groupBox12.Text = string.Format("{0} ~ {1}", cmbLoaiTinhThanhXa.Text, tb.Rows.Count);
        });
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "BindGrid");
      }
    }
    void BindKhuVucLoai() {
      try {
        string str = string.Format("select ROW_NUMBER() OVER(ORDER BY orderid) AS stt,* from [dbo].[dm_khuvucLoai] order by orderid");
        DataTable tb = SQLDatabase.ExcDataTable(str);
        gridLoaiKhuvuc.Invoke((Action)delegate {
          gridLoaiKhuvuc.DataSource = tb;
        });
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "BindGrid");
      }
    }

    void Bindparallelism() {
      try {
        string str = string.Format("sp_configure 'max degree of parallelism'");
        DataTable tb = SQLDatabase.ExcDataTable(str);
        dataGridView1.Invoke((Action)delegate {
          dataGridView1.DataSource = tb;
        });

      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "BindGrid");
      }
    }

    void BindFont() {
      try {
        string str = string.Format("select * from dm_Font order by OrderId");
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

    void Binddm_madausoquocgia() {
      try {
        string str = string.Format("select ROW_NUMBER() OVER(ORDER BY ma desc) AS stt,* from [dbo].[dm_madausoquocgia] order by ma desc");
        DataTable tb = SQLDatabase.ExcDataTable(str);
        gridMaDauSoQuocgia.Invoke((Action)delegate {
          gridMaDauSoQuocgia.DataSource = tb;
        });
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "BindSQLBatdongbo");
      }
    }
    void BindNhaMang() {
      try {

        string str = string.Format("select  ROW_NUMBER() OVER(ORDER BY id) AS stt,* from dm_nhamang");
        DataTable tb = SQLDatabase.ExcDataTable(str);
        gridNhamang.Invoke((Action)delegate {
          gridNhamang.DataSource = tb;
        });
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "BindGrid");
      }
    }
        void BindChar()
        {
            try
            {

                string str = string.Format("select  ROW_NUMBER() OVER(ORDER BY id) AS stt,* from dm_Char");
                DataTable tb = SQLDatabase.ExcDataTable(str);
                gridchar.Invoke((Action)delegate {
                    gridchar.DataSource = tb;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BindGrid");
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
          root.orderid = ConvertType.ToInt(SQLDatabase.ExcDataTable("SELECT MAX(orderid) from dm_column")) + 1;

          if (ConvertType.ToInt(SQLDatabase.ExcDataTable(string.Format("select count(*) from dm_column where ma='{0}'", root.ma)).Rows[0][0]) == 0) {
            SQLDatabase.Adddm_column(root);
          }
          else {
            dm_column mode = SQLDatabase.Loaddm_column(string.Format("select * from dm_column where ma='{0}'", root.ma)).FirstOrDefault();
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
      if ((e.ColumnIndex == 5) && e.RowIndex >= 0) {
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
      SQLDatabase.ExcNonQuery(string.Format("update dm_column set [isKey]=1 where id='{0}'", id));
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

          dm_Font dm_column = SQLDatabase.Loaddm_Character(string.Format("select * from dm_Font where id='{0}'", Id)).FirstOrDefault();
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
      BindFont();
    }

    private void thêmToolStripMenuItem_Click(object sender, EventArgs e) {
      try {
        dm_Font model = new dm_Font();
        model.ma = "";
        model.name = "";
        model.isAct = false;
        model.orderid = ConvertType.ToInt(SQLDatabase.ExcDataTable(string.Format("select max([orderid]) from dm_Font")).Rows[0][0]) + 1;
        SQLDatabase.Adddm_Character(model);
        BindFont();
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
              SQLDatabase.ExcNonQuery(String.Format("DELETE FROM dm_Font WHERE ID='{0}'", Id));
              BindFont();
            }
          }
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "gridviewCharacter_CellClick");
      }
    }

    private void gridviewCharacter_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) {
      if (e.ColumnIndex == 4 && e.RowIndex >= 0) {
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

      SQLDatabase.ExcNonQuery("update dm_Font set [isAct]=0");
      SQLDatabase.ExcNonQuery(string.Format("update dm_Font set [isAct]=1 where id='{0}'", id));
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
      LinkLabel.Link link = new LinkLabel.Link();
      link.LinkData = "https://msdn.microsoft.com/en-us/library/windows/desktop/dd317756(v=vs.85).aspx";
      linkLabel1.Links.Add(link);
    }

   

    private void button1_Click(object sender, EventArgs e) {
      try {
        cau_hinh cau_Hinh = SQLDatabase.Loadcau_hinh("select * from cau_hinh").FirstOrDefault();
        cau_Hinh.DelImportTruocImport = ckhDelImport.Checked;
        cau_Hinh.MaxTop = ckhSoLuongHienThi.Checked ? -1 : ConvertType.ToInt(numericUpDown1.Value);
        cau_Hinh.IsExportTxt = radText.Checked;
        cau_Hinh.isRemoveCharInFileImport = chkDelChartrongfile.Checked;
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
        var state = value.HasValue && value.Value ? RadioButtonState.CheckedNormal : RadioButtonState.UncheckedNormal;
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

    private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
      try {
        if (e.RowIndex != -1) {
          int value = ConvertType.ToInt(dataGridView1.Rows[e.RowIndex].Cells["run_value"].Value);
          SQLDatabase.ExcNonQuery(string.Format(" EXEC sp_configure 'show advanced options', 1;"));
          SQLDatabase.ExcNonQuery(string.Format(" RECONFIGURE WITH OVERRIDE;"));
          SQLDatabase.ExcNonQuery(string.Format(" EXEC sp_configure 'max degree of parallelism', {0};", value));
          SQLDatabase.ExcNonQuery("RECONFIGURE WITH OVERRIDE;");
        }
      }
      catch (Exception ex) {
        MessageBox.Show("Unable to save the record. There might be a blank cell. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void lToolStripMenuItem_Click(object sender, EventArgs e) {
      Bindparallelism();
    }

    private void chkAllSxep_CheckedChanged(object sender, EventArgs e) {
      try {
        SQLDatabase.ExcNonQuery(string.Format("update dm_column set isOrder={0}", chkAllSxep.Checked ? 1 : 0));
        BindColumn();
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "chkAllSxep_CheckedChanged");
      }
    }

    private void chkAllAct_CheckedChanged(object sender, EventArgs e) {
      try {
        SQLDatabase.ExcNonQuery(string.Format("update dm_column set isAct={0}", chkAllAct.Checked ? 1 : 0));
        BindColumn();
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "chkAllSxep_CheckedChanged");
      }
    }

    private void chkAllBC_CheckedChanged(object sender, EventArgs e) {
      try {
        SQLDatabase.ExcNonQuery(string.Format("update dm_column set IsReport={0}", chkAllBC.Checked ? 1 : 0));
        BindColumn();
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "chkAllSxep_CheckedChanged");
      }
    }

    private void chkAllSearch_CheckedChanged(object sender, EventArgs e) {
      try {
        SQLDatabase.ExcNonQuery(string.Format("update dm_column set isSearch={0}", chkAllSearch.Checked ? 1 : 0));
        BindColumn();
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "chkAllSxep_CheckedChanged");
      }
    }

    private void làmTươiToolStripMenuItem1_Click(object sender, EventArgs e) {
      Binddm_madausoquocgia();
    }

    private void thêmToolStripMenuItem1_Click(object sender, EventArgs e) {
      try {
        dm_madausoquocgia model = new dm_madausoquocgia();
        model.ma = "";
        model.name = "";
        SQLDatabase.Adddm_madausoquocgia(model);
        Binddm_madausoquocgia();
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "thêmToolStripMenuItem_Click");
      }
    }

    private void gridMaDauSoQuocgia_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
      try {

        if (e.RowIndex != -1) {
          string Id = gridMaDauSoQuocgia.Rows[e.RowIndex].Cells["idmadausoquocgia"].Value.ToString();
          string ma = gridMaDauSoQuocgia.Rows[e.RowIndex].Cells["Mamadausoquocgia"].Value.ToString();
          string name = gridMaDauSoQuocgia.Rows[e.RowIndex].Cells["Namemadausoquocgia"].Value.ToString();


          dm_madausoquocgia madauso = SQLDatabase.Loaddm_madausoquocgia(string.Format("select * from dm_madausoquocgia where id='{0}'", Id)).FirstOrDefault();
          madauso.ma = ma;
          madauso.name = name;
          SQLDatabase.Updm_madausoquocgia(madauso);

          // BindColumn();
        }
      }
      catch (Exception ex) {
        MessageBox.Show("Unable to save the record. There might be a blank cell. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void cmbNhaMang_SelectedIndexChanged(object sender, EventArgs e) {
      BindKhuVuc();
    }

    private void cmbTinhThanhXa_SelectedIndexChanged(object sender, EventArgs e) {
      BindKhuVuc();
    }

    private void button4_Click(object sender, EventArgs e) {
      OpenFileDialog ofd = new OpenFileDialog();
      int idNhamang = ConvertType.ToInt(cmbNhaMang.SelectedValue.ToString());
      int idloai = cmbLoaiTinhThanhXa.SelectedIndex;

      if (radioButton1.Checked)
        ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
      else
        ofd.Filter = "txt files (*.xls)|*.xls|All files (*.*)|*.*";

      ofd.FilterIndex = 1;
      ofd.RestoreDirectory = true;

      string path = ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : "";
      if (path == "") return;
      bool temp = false;
      SQLDatabase.ExcNonQuery(string.Format("delete from dm_khuvuc where Idnhamang='{0}' and loai='{1}'", idNhamang, idloai));
      if (radioButton2.Checked)
        (new Waiting(() => temp = importfileexcel(path, idNhamang, idloai))).ShowDialog();
      else
        (new Waiting(() => temp = importTxt(path, idNhamang, idloai))).ShowDialog();
      if (temp)
        BindKhuVuc();
      else
        MessageBox.Show("Import file thất bại", "Lỗi");
    }

    private bool importfileexcel(string path, int idNhamang, int idloai) {
      try {


        ExcelAdapter excel = new ExcelAdapter(path);
        DataTable tb = excel.ReadFromFile("SELECT * FROM [Sheet1$]");


        foreach (DataRow item in tb.Rows) {
          dm_khuvuc model = new dm_khuvuc();
          model.Idnhamang = idNhamang;
          model.loai = idloai;
          model.ma = item[0].ToString();
          model.name = item[1].ToString();
          SQLDatabase.Adddm_khuvuc(model);
        }
        return true;
      }
      catch (Exception ex) {
        return false;
      }
    }
    private bool importTxt(string path, int idNhamang, int idloai) {
      try {
        DataTable table = new DataTable();
        string[] fileNames, lineParts;

        string line;

        fileNames = path.Split('.');

        StreamReader sReader = new StreamReader(path);
        int SoCot = 999;
        char kytu = '\t';
        /*lay so cot*/
        while ((line = sReader.ReadLine()) != null) {

          lineParts = line.Split(new char[] { kytu });
          if (lineParts.Count() != 0) {
            SoCot = lineParts.Count() <= SoCot ? lineParts.Count() : SoCot;
          }
        }
        /*tao table*/


        for (int i = 0; i < SoCot; i++) {
          table.Columns.Add(string.Format("[{0}]", i.ToString()), typeof(string));
        }
        sReader.DiscardBufferedData();
        sReader.BaseStream.Seek(0, SeekOrigin.Begin);
        sReader.BaseStream.Position = 0;

        while ((line = sReader.ReadLine()) != null) {
          lineParts = line.Split(new char[] { kytu });
          DataRow rows = table.NewRow();
          for (int i = 0; i < SoCot; i++) {
            rows[string.Format("[{0}]", i)] = lineParts[i];
          }
          table.Rows.Add(rows);
        }
        if (table.Rows.Count == 0) return false;
        foreach (DataRow item in table.Rows) {
          dm_khuvuc model = new dm_khuvuc();
          model.Idnhamang = idNhamang;
          model.loai = idloai;
          model.ma = item[0].ToString();
          model.name = item[1].ToString();
          SQLDatabase.Adddm_khuvuc(model);
        }
        return true;
      }
      catch (Exception ex) {
        return false;
      }
    }

    private void làmTươiToolStripMenuItem2_Click(object sender, EventArgs e) {
      BindKhuVuc();
    }

    private void importToolStripMenuItem_Click(object sender, EventArgs e) {
      button4_Click(null, null);
    }

    private void xoáToolStripMenuItem_Click(object sender, EventArgs e) {
      int idNhamang = ConvertType.ToInt(cmbNhaMang.SelectedValue.ToString());
      int idloai = cmbLoaiTinhThanhXa.SelectedIndex;

      SQLDatabase.ExcNonQuery(string.Format("delete from dm_khuvuc where Idnhamang='{0}' and loai='{1}'", idNhamang, idloai));
      BindKhuVuc();
    }

    private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
      try {
        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string fileName = "MaufileImportTinhHuyenXa";
        bool temp = false;
        new Waiting(() => temp = xuatfilemain(filePath + "\\" + fileName), "Vui Lòng Chờ").ShowDialog();
        if (temp)
          MessageBox.Show("Đã xuất thành công file.\n File:" + fileName, "Thông Báo");
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "linkLabel1_LinkClicked");
      }
    }
    private bool xuatfilemain(string filePath) {
      try {
        DataTable table = new DataTable();
        table.Columns.Add("Mã Phường/Xã", typeof(string));
        table.Columns.Add("Tên Phường/Xã", typeof(string));

        ExcelAdapter excel = new ExcelAdapter(filePath);
        excel.CreateAndWrite(table, "Sheet1", 1);
        return true;
      }
      catch (Exception ex) {
        return false;
      }

    }

    private void checkBox1_CheckedChanged_1(object sender, EventArgs e) {
      for (int i = 0; i < chkListChuanHoa.Items.Count; i++)
        chkListChuanHoa.SetItemChecked(i, checkBox1.Checked);
    }

    private void checkBox2_CheckedChanged_1(object sender, EventArgs e) {
      groupBox9.Enabled = checkBox2.Checked;
    }

    private void checkBox3_CheckedChanged(object sender, EventArgs e) {
      groupBox4.Enabled = checkBox3.Checked;
    }

    private void checkBox4_CheckedChanged(object sender, EventArgs e) {
      groupBox11.Enabled = checkBox4.Checked;
    }
    private string strSqlChuanHoaKyTuChar13Char10Char09ToChar32() {
      try {
        dm_batdongbo dm_Batdongbo = SQLDatabase.Loaddm_batdongbo("select * from dm_batdongbo where isAct=1").FirstOrDefault();
        if (chkListChuanHoa.CheckedItems.Count == 0) return "";
        List<string> kq = new List<string>();
        foreach (object itemChecked in chkListChuanHoa.CheckedItems) {
          DataRowView castedItem = itemChecked as DataRowView;
          string ma = castedItem["ma"].ToString();
          kq.Add(string.Format(" {0}=REPLACE(REPLACE(REPLACE({0}, CHAR(13), CHAR(32)), CHAR(10), CHAR(32)),CHAR(09),CHAR(32)),", ma));
        }
        string commandKq = "update dbo.root set ";
        foreach (var item in kq) {
          commandKq += item;
        }

        string xx = string.Format("{0} {1}", commandKq.Substring(0, commandKq.Length - 1), dm_Batdongbo.ma);
        return xx;
      }
      catch (Exception ex) {
        return "";
      }
    }

    private string strSqlChuanHoaPhone() {
      try {
        dm_batdongbo dm_Batdongbo = SQLDatabase.Loaddm_batdongbo("select * from dm_batdongbo where isAct=1").FirstOrDefault();

        string kq = "";
        string ma = cmbCotDienThoai.SelectedValue.ToString();

        List<string> list = new List<string>();
        foreach (var item in SQLDatabase.Loaddm_madausoquocgia("select * from dm_madausoquocgia order by ma desc")) {
          list.Add(string.Format("REPLACE(xxx,'{0}',0)", item.ma));
        }
        string strcommand = "";
        /*có danh sách list*/
        if (list.Count() == 0) return "";
        strcommand = list[0];
        for (int i = 1; i < list.Count; i++) {
          strcommand = strcommand.Replace("xxx", list[i]);
        }
        kq = string.Format("{0}=dbo.[ReplacePhone]({1}),", ma, strcommand.Replace("xxx", ma));
        string commandKq = string.Format("update dbo.root set {0}", kq);
        string xx = string.Format("{0} {1}", commandKq.Substring(0, commandKq.Length - 1), dm_Batdongbo.ma);
        return xx;
      }
      catch (Exception ex) {
        return "";
      }
    }


    private string strSqlChuanHoaTinhThanh() {
      try {
        dm_batdongbo dm_Batdongbo = SQLDatabase.Loaddm_batdongbo("select * from dm_batdongbo where isAct=1").FirstOrDefault();
        string strCommand = "";
        int i = 1;
        foreach (var item in groupbox) {
          TableLayoutPanel layoutPanel = (TableLayoutPanel)item.Value.Controls.Container;
          ComboBox comboxCot = (ComboBox)layoutPanel.GetControlFromPosition(0, 0);
          ComboBox comboxLoai = (ComboBox)layoutPanel.GetControlFromPosition(1, 0);
          strCommand += " update dbo.import " +
          string.Format(" set {0} = c.name ", comboxCot.SelectedValue) +
          string.Format(" from import a inner join dm_nhamang b on a.{0} like b.dauso + '%' ", cmboCotDienThoaiTinhthanh.SelectedValue) +
          string.Format(" inner join dm_khuvuc c on b.parentId = c.Idnhamang and c.ma = {0} ", comboxCot.SelectedValue);
          string.Format(" where loai='{0}' {1} ", comboxLoai.SelectedValue, dm_Batdongbo.ma);
          string.Format(" go ");
        }
        return strCommand;
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "strSqlChuanHoaTinhThanh");
        return "";
      }
    }

    private void button7_Click(object sender, EventArgs e) {
      try {
        /**/
        if (checkBox5.Checked) {
          if (chkListChuanHoa.CheckedItems.Count == 0) {
            MessageBox.Show("Để chuẩn hoá các ký tự xuống dòng của dữ liệu. Vui lòng chọn cột cần chuẩn hoá", "Thông Báo");
            return;
          }
        }
        dm_batdongbo model = SQLDatabase.Loaddm_batdongbo("select * from dm_batdongbo where isAct=1").FirstOrDefault();
        new Waiting(() => {
          if (checkBox2.Checked) {
            SQLDatabase.ExcNonQuery(strSqlChuanHoaPhone());
          }
          if (checkBox3.Checked) {
            
            bool isUpdateAll = cmbChuanHoa.SelectedIndex == 0 ? false : true;

            if (isUpdateAll) {
              SQLDatabase.ExcNonQuery(String.Format("update root WITH(TABLOCK) set isSearch = 0 {0}", model.ma));
            }
            else {
              List<dm_column> dm_Columns = SQLDatabase.Loaddm_column("select * from dm_column where isSearch=1  order by orderid ");
              string strcommand = "update dbo.root WITH(TABLOCK) set valueskeySearch=";
              string dscot = "";
              foreach (dm_column item in dm_Columns) {
                dscot += string.Format("isnull({0},'') +", item.ma);
              }
              dscot = dscot.Substring(0, dscot.Length - 1);
              strcommand += string.Format(" dbo.GetUnsignString({0}) ", dscot);
              strcommand += ", isSearch=1 where isSearch=0 ";
              strcommand += string.Format(" {0} ", model.ma);
              SQLDatabase.ExcNonQuery(strcommand);
            }
          }
          if (checkBox4.Checked) {
            SQLDatabase.ExcNonQuery(strSqlChuanHoaTinhThanh());
          }
          if (checkBox5.Checked) {
            SQLDatabase.ExcNonQuery(strSqlChuanHoaKyTuChar13Char10Char09ToChar32());
          }
          if (checkBox6.Checked) {
            /*Bước 1:delete tất cả các giá trị null*/
            SQLDatabase.ExcNonQuery(string.Format(" delete from dbo.root where {0} is null {1}", cmbLocTrung.SelectedValue,model.ma));
            SQLDatabase.ExcNonQuery("IF OBJECT_ID('dbo.temp_root', 'U') IS NOT NULL  DROP TABLE dbo.temp_root; ");
            SQLDatabase.ExcNonQuery(";WITH cte AS " +
                                   "( " +
                                   string.Format(" SELECT *, ROW_NUMBER() OVER(PARTITION BY {0} ORDER BY create_date DESC) AS rn ", cmbLocTrung.SelectedValue) +
                                   " FROM [dbo].root " +
                                   " )" +
                                   " SELECT * into temp_root " +
                                   "  FROM cte " +
                                   string.Format("  WHERE rn = 1 {0}", model.ma));
            SQLDatabase.ExcNonQuery("TRUNCATE TABLE root");
            SQLDatabase.ExcNonQuery("ALTER TABLE temp_root drop COLUMN rn");
            SQLDatabase.ExcNonQuery(string.Format("Insert into dbo.root select * from temp_root {0}",model.ma));
          }
        }).ShowDialog();
        MessageBox.Show("Kết Thúc Chuẩn Hoá", "Thông Báo");
      }
      catch (Exception) {

        throw;
      }
      string x = strSqlChuanHoaTinhThanh();
    }



    private void gridMaDauSoQuocgia_CellClick(object sender, DataGridViewCellEventArgs e) {
      try {
        if (e.ColumnIndex != -1) {
          if (e.ColumnIndex == 0) {
            string Id = gridMaDauSoQuocgia.Rows[e.RowIndex].Cells["idmadausoquocgia"].Value.ToString();
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc là sẽ xoá mã quốc gia?", "Xoá Mã Quốc Gia", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) {
              SQLDatabase.ExcNonQuery(String.Format("DELETE FROM dm_madausoquocgia WHERE ID='{0}'", Id));
              Binddm_madausoquocgia();
            }
          }
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "gridviewCharacter_CellClick");
      }
    }

    private void checkBox5_CheckedChanged(object sender, EventArgs e) {
      groupBox10.Enabled = checkBox5.Checked;
    }

    private void checkBox6_CheckedChanged(object sender, EventArgs e) {
      groupBoxLocTrung.Enabled = checkBox6.Checked;
    }

    private void lamTươiToolStripMenuItem2_Click(object sender, EventArgs e) {
      BindKhuVucLoai();
    }

    private void gridLoaiKhuvuc_CellClick(object sender, DataGridViewCellEventArgs e) {
      try {
        if (e.ColumnIndex != -1) {
          if (e.ColumnIndex == 0) {
            string Id = gridLoaiKhuvuc.Rows[e.RowIndex].Cells["idloaikhuvuc"].Value.ToString();
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc là sẽ xoá loại khu vực?", "Xoá Loại Khu Vực", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) {
              SQLDatabase.ExcNonQuery(String.Format("DELETE FROM dm_khuvucLoai WHERE ID='{0}'", Id));
              BindKhuVucLoai();
            }
          }
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "gridLoaiKhuvuc_CellClick");
      }
    }

    private void gridLoaiKhuvuc_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
      try {

        if (e.RowIndex != -1) {
          string Id = gridLoaiKhuvuc.Rows[e.RowIndex].Cells["idloaikhuvuc"].Value.ToString();
          string name = gridLoaiKhuvuc.Rows[e.RowIndex].Cells["nameloaikhuvuc"].Value.ToString();
          int orderid = ConvertType.ToInt(gridLoaiKhuvuc.Rows[e.RowIndex].Cells["orderiddm_khuvucLoai"].Value);

          dm_khuvucLoai madauso = SQLDatabase.Loaddm_khuvucLoai(string.Format("select * from [dm_khuvucLoai] where id='{0}'", Id)).FirstOrDefault();
          madauso.name = name;
          madauso.orderid = orderid;

          SQLDatabase.Updm_khuvucLoai(madauso);

        }
      }
      catch (Exception ex) {
        MessageBox.Show("Unable to save the record. There might be a blank cell. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void thêmToolStripMenuItem2_Click(object sender, EventArgs e) {
      try {
        dm_khuvucLoai model = new dm_khuvucLoai();
        model.name = "";
        model.orderid = ConvertType.ToInt(SQLDatabase.ExcDataTable("select max(orderid) from dm_khuvucLoai").Rows[0][0])+1;
        SQLDatabase.Adddm_khuvucLoai(model);
        BindKhuVucLoai();
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "thêmToolStripMenuItem2_Click");
      }
    }

        private void lamTươiToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            BindChar();
        }

        private void gridchar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        string Id = gridchar.Rows[e.RowIndex].Cells["id_char"].Value.ToString();
                        DialogResult dialogResult = MessageBox.Show("Bạn có chắc là sẽ char?", "Xoá char", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            SQLDatabase.ExcNonQuery(String.Format("DELETE FROM dm_char WHERE ID='{0}'", Id));
                            BindChar();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "gridLoaiKhuvuc_CellClick");
            }
        }

        private void gridchar_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex != -1)
                {
                    string Id = gridchar.Rows[e.RowIndex].Cells["id_char"].Value.ToString();
                    string char_char = gridchar.Rows[e.RowIndex].Cells["char_char"].Value.ToString();
                    string note = gridchar.Rows[e.RowIndex].Cells["note_char"].Value.ToString();
                    dm_Char madauso = SQLDatabase.Loaddm_Char(string.Format("select * from [dm_char] where id='{0}'", Id)).FirstOrDefault();
                    madauso.Char = char_char;
                    madauso.note = note;
                    SQLDatabase.Updm_Char(madauso);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save the record. There might be a blank cell. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void thêmToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                dm_Char model = new dm_Char();
                model.Char = "";
                model.note = "";
                SQLDatabase.Adddm_Char(model);
                BindChar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "thêmToolStripMenuItem2_Click");
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.dotnetperls.com/ascii-table");
        }
    }
}

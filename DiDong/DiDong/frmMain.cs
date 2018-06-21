using DiDong;
using DiDong.Properties;
using SchemaSpec;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyData {
  public partial class frmMain : Form {
    private CachedData cachedData;
    private TableLayoutPanel[] groupbox = null;
    private ComboBox[] combox = null;
    private RadioButton[] radioButtons = null;
    private List<dm_column> _Columns = null;
    private int _nTongRowsText;
    private string NamesFile = "";
    private List<string> dscot;
    private string connectionString = "";
    private cau_hinh _cauhinh = null;
    private dm_batdongbo Dm_Batdongbo=null;
    private string _strDatabase;
    private string _strDatabase2;
    private string _strTable2;
    public frmMain() {

      InitializeComponent();
      CreateColumnGridView(GridViewMain);
      CreateColumnGridView(dataGridView_tontai);
      CreateColumnGridView(dataGrid_ListTelNumberNew);
      

      _Columns = SQLDatabase.Loaddm_column("select * from dm_column order by orderid");
      _strDatabase = SQLDatabase.ExcDataTable(string.Format("SELECT DB_NAME(0)AS [DatabaseName]; ")).Rows[0]["DatabaseName"].ToString();

      this.groupbox = new TableLayoutPanel[_Columns.Count()];
      this.radioButtons = new RadioButton[_Columns.Count()];
      this.combox = new ComboBox[_Columns.Count()];
      int index = 0;
      foreach (dm_column item in _Columns) {
        groupbox[index] = CreateTableLayoutPanel(index, item);
        radioButtons[index] = CreateRadio(index, item);
        combox[index] = CreateCombox(index, item);

        groupbox[index].Controls.Add(radioButtons[index]);
        groupbox[index].Controls.Add(combox[index]);
        palSIPLogs.Controls.Add(groupbox[index]);

        index++;
      }
    }

    private void cấuHìnhToolStripMenuItem1_Click(object sender, EventArgs e) {
      try {
        frmSystem frm = new frmSystem();
        frm.strDatabase = _strDatabase;
        if (frm.ShowDialog() == DialogResult.OK) {
         
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "");
      }
    }

    private void frmMain_Load(object sender, EventArgs e) {
      _cauhinh = SQLDatabase.Loadcau_hinh("select * from cau_hinh").FirstOrDefault();
      Dm_Batdongbo = SQLDatabase.Loaddm_batdongbo(string.Format("select * from dm_batdongbo where isAct=1")).FirstOrDefault();
      cbb_TypeDataSource.SelectedIndex = 0;
      dm_Character dm_Character = SQLDatabase.Loaddm_Character("select * from dm_Character where isAct=1").FirstOrDefault();
      groupBox3.Text = string.Format("Dữ Liệu Nguồn (TXT: {0})", dm_Character.name);
    }

    private void button1_Click(object sender, EventArgs e) {
      frmSearch frm = new frmSearch();
      frm.Show();
    }

    private void Radio_Click(object sender, EventArgs e) {
      foreach (var item in radioButtons) {
        item.Checked = false;
      }
     ((RadioButton)sender).Checked = true;
    }

    private void button2_Click(object sender, EventArgs e) {
      OpenFileDialog openFile;
      string[] fileNames;

      try {
        if (cbb_TypeDataSource.SelectedIndex == 0) {
          openFile = new OpenFileDialog();
          openFile.Filter = "Text File (*.txt)|*.txt|All files (*.*)|*.*";

          if (openFile.ShowDialog() == DialogResult.OK) {
            txt_FileName.Text = openFile.FileName;
            NamesFile = openFile.SafeFileName;
            fileNames = openFile.FileName.Split('.');
          }
        }
        else {
          frmSQLServer frm = new frmSQLServer();
          if (frm.ShowDialog() == DialogResult.OK) {
            _strDatabase2 = frm.strDatatable;
            _strTable2 = frm.strTable;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(SQLDatabase.ConnectionString);
            connectionString = txt_FileName.Text = string.Format("Provider=sqloledb;Data Source={0};Initial Catalog={1};Integrated Security = SSPI;",builder.DataSource, frm.strDatatable);
            
          }
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "Open Source File", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void button3_Click(object sender, EventArgs e) {
      FileInfo fileInfo;
      string[] fileName;
      string tableName = string.Empty;
      string name = string.Empty;
      // DataTable tbGetNameDisplay;
      dscot = new List<string>();
      try {
        
        if (string.IsNullOrEmpty(txt_FileName.Text)) {
          MessageBox.Show("Chưa có 'dữ liệu nguồn' cần lưu", "load Data Source", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }
        else {
          if (cbb_TypeDataSource.SelectedIndex == 0) {
            fileInfo = new FileInfo(txt_FileName.Text);
            if (fileInfo.Exists == false) {
              MessageBox.Show("Đường dẫn hoặc tên tập tin của dữ liệu nguồn không đúng. Vui lòng kiểm tra lại !", "load Data Source", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              return;
            }
            else {
              /*kiễm tra định nghĩ cấu hình file txt*/
              string path = Path.GetDirectoryName(txt_FileName.Text);

              SchemaSpec.SchemeDef sdef = new SchemaSpec.SchemeDef();
              if (Settings.Default.SchemaSpec == null) {
                //if (sdef == null)
                //{
                sdef.DelimiterType = SchemaSpec.SchemeDef.DelimType.TabDelimited;
                sdef.UsesHeader = SchemeDef.FirstRowHeader.No;
                List<ItemSpecification> ColumnDefinition = new List<ItemSpecification>();
                int i = 1;
                foreach (dm_column item in _Columns) {
                  ColumnDefinition.Add(new ItemSpecification() { ColumnNumber = i, Name = i.ToString(), ColumnWidth = item.size, TypeData = ItemSpecification.JetDataType.Text });
                  i++;
                }

                sdef.ColumnDefinition = ColumnDefinition;
                Settings.Default.SchemaSpec = sdef;
                Settings.Default.Save();
                //}
              }
              else {
                sdef = Settings.Default.SchemaSpec;
              }
              CreateSchemaIni(txt_FileName.Text);
              dm_Character dm_Character = SQLDatabase.Loaddm_Character(string.Format("select top 1 * from dm_Character where isAct=1")).FirstOrDefault();
              // create a variable to hold the connection string
              string connbit = string.Empty;
              switch (sdef.DelimiterType) {
                case SchemaSpec.SchemeDef.DelimType.CsvDelimited:
                  if (sdef.UsesHeader == SchemaSpec.SchemeDef.FirstRowHeader.Yes)
                    connbit = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + string.Format(@";Extended Properties=""Text;HDR=Yes;FMT=CsvDelimited;CharacterSet={0}""", dm_Character.ma);
                  else
                    connbit = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + string.Format(@";Extended Properties=""Text;HDR=No;FMT=CsvDelimited;CharacterSet={0}""", dm_Character.ma);
                  break;
                case SchemaSpec.SchemeDef.DelimType.CustomDelimited:
                  if (sdef.UsesHeader == SchemaSpec.SchemeDef.FirstRowHeader.Yes)
                    connbit = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + @";Extended Properties=""Text;HDR=Yes;CharacterSet=" + dm_Character.ma + ";FMT=Delimited(" + sdef.CustomDelimiter + ")" + "\"";
                  else
                    connbit = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + @";Extended Properties=""Text;HDR=No;CharacterSet=" + dm_Character.ma + ";FMT=Delimited(" + sdef.CustomDelimiter + ")" + "\"";
                  break;
                case SchemaSpec.SchemeDef.DelimType.FixedWidth:
                  if (sdef.UsesHeader == SchemaSpec.SchemeDef.FirstRowHeader.Yes)
                    connbit = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + string.Format(@";Extended Properties=""Text;HDR=Yes;CharacterSet={0};FMT=FixedLength""", dm_Character.ma);
                  else
                    connbit = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + string.Format(@";Extended Properties=""Text;HDR=No;CharacterSet={0};FMT=FixedLength""", dm_Character.ma);
                  break;
                case SchemaSpec.SchemeDef.DelimType.TabDelimited:
                  if (sdef.UsesHeader == SchemaSpec.SchemeDef.FirstRowHeader.Yes)
                    connbit = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + string.Format(@";Extended Properties=""Text;HDR=Yes;CharacterSet={0};FMT=TabDelimited""", dm_Character.ma);
                  else
                    connbit = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + string.Format(@";Extended Properties=""Text;HDR=No;CharacterSet={0};FMT=TabDelimited""", dm_Character.ma);
                  break;
                default:
                  break;
              }

              // put the connection string into the properties and save the properties
              DiDong.Properties.Settings.Default.ConnString = connbit;
              DiDong.Properties.Settings.Default.Save();

              // make sure we have a connection string before proceeding
              if (String.IsNullOrEmpty(connbit)) {
                MessageBox.Show("Mẫu không hợp lệ; sử dụng tiện ích lược đồ để xác định giản đồ cho tệp bạn đang cần mở", "Thông Báo");
              }

              connectionString = connbit;

              fileName = txt_FileName.Text.Split('.');

              StreamReader sReader;
              string line;
              string[] lineParts;
              //ClearALLControl();
              for (int i = 0; i < combox.Count(); i++) {
                combox[i].Items.Clear();
              }


              for (int i = 0; i < combox.Count(); i++) {
                combox[i].Items.Add("----Chọn----");
              }

              sReader = new StreamReader(txt_FileName.Text);
              _nTongRowsText = File.ReadAllLines(txt_FileName.Text).Count();
              if (sReader.ReadLine() != null) {
                line = sReader.ReadLine();
                char kytu = '\t';
                lineParts = line.Split(new char[] { kytu });
                for (int i = 1; i <= lineParts.Count(); i++) {
                  for (int x = 0; x < combox.Count(); x++) {
                    combox[x].Items.AddRange(new object[] { i.ToString() });
                  }
                }
              }
              sReader.Close();
              for (int i = 0; i < combox.Count(); i++) {
                combox[i].SelectedIndex = 0;
              }
            }
          }
          else {/*sql server*/
            for (int i = 0; i < combox.Count(); i++) {
              combox[i].Items.Clear();
            }

            for (int x = 0; x < combox.Count(); x++) {
              combox[x].Items.Add("----Chọn----");
            }
            DataTable tb = SQLDatabase.ExcDataTable(string.Format(" select *  from {0}.INFORMATION_SCHEMA.COLUMNS", _strDatabase2));
            for (int x = 0; x < combox.Count(); x++) {
              foreach (DataRow item in tb.Rows) {
                combox[x].Items.Add(item["COLUMN_NAME"].ToString());
              }
            }
          }
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "load Data Source");
      }
    }


    /// <summary>
    /// Create a schema.ini file to control the format and data types used 
    /// within the applications - this must be saved in the path of the input file.
    /// It will overwrite any existing schema.ini file there but the whole process
    /// is transparent to the end user.  The specification of the actual input file
    /// received from Intuit/Medfusion must match here exactly.
    /// 
    /// If you wish to conceal any information from the end user just hide the column 
    /// in LoadFileData()
    /// </summary>
    /// <param name="filePath"></param>
    public void CreateSchemaIni(string filePath) {
      try {
        // define a new schema definition and populate it from the 
        // application properties
        SchemaSpec.SchemeDef sdef = new SchemaSpec.SchemeDef();
        if (DiDong.Properties.Settings.Default.SchemaSpec == null) {
          MessageBox.Show("No schema has been defined; prior to opening a CSV file, use the Schema tool to construct a schema definition", "Missing Schema");
          return;
        }
        else {
          sdef = DiDong.Properties.Settings.Default.SchemaSpec;
        }

        // start a string builder to hold the contents of the schema file as it is construction
        StringBuilder sb = new StringBuilder();

        // the first line of the schema file is the file name in brackets
        sb.Append("[" + System.IO.Path.GetFileName(filePath) + "]" + Environment.NewLine);

        // the next line of the schema file will be used to determine whether or not
        // the first line of the file contains column headers or not
        string colHeader = sdef.UsesHeader == SchemaSpec.SchemeDef.FirstRowHeader.No ? "ColNameHeader=False" : "ColNameHeader=True";
        sb.Append(colHeader + Environment.NewLine);

        //  next we need to add the format to the schema file
        switch (sdef.DelimiterType) {
          case SchemaSpec.SchemeDef.DelimType.CsvDelimited:
            // a comma delimited file
            sb.Append("Format=CsvDelimited" + Environment.NewLine);
            break;
          case SchemaSpec.SchemeDef.DelimType.CustomDelimited:
            // a custom delimiter is used here; need to check and make sure the user
            // provided a character to serve as a delimiter
            if (String.IsNullOrEmpty(sdef.CustomDelimiter)) {
              MessageBox.Show("A custom delimiter was not identified for this schema.", "Invalid Schema");
              return;
            }
            sb.Append("Format=Delimited(" + sdef.CustomDelimiter + ")" + Environment.NewLine);
            break;
          case SchemaSpec.SchemeDef.DelimType.FixedWidth:
            // the file columns here have a fixed width; no other delimiter is supplied
            sb.Append("Format=FixedLength" + Environment.NewLine);
            break;
          case SchemaSpec.SchemeDef.DelimType.TabDelimited:
            // the columns here are tab delimited
            sb.Append("Format=TabDelimited" + Environment.NewLine);
            break;
          default:
            break;
        }

        // next each column number, name and data type is added to the schema file
        foreach (SchemaSpec.ItemSpecification s in sdef.ColumnDefinition) {
          string tmp = "Col" + s.ColumnNumber.ToString() + "=" + s.Name + " " + s.TypeData;

          if (s.ColumnWidth > 0)
            tmp += " Width " + s.ColumnWidth.ToString();

          sb.Append(tmp + Environment.NewLine);
        }

        // the schema.ini file has to live in the same folder as the file we are going to open; it has to carry the name
        // schema.ini.  When we connect to the file, the connection will find and use this schema.ini file to 
        // determine how to treat the file contents; only the correct schema.ini file for a particular file type can 
        // be used - you cannot, for example, open a comma delimited file with a schema.ini file defined for a 
        // pipe delimited file.
        using (StreamWriter outfile = new StreamWriter(System.IO.Path.GetDirectoryName(filePath) + @"\schema.ini")) {
          outfile.Write(sb.ToString());
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "Error");
      }
    }

    private TableLayoutPanel CreateTableLayoutPanel(int index, dm_column model) {
      TableLayoutPanel lbl = new TableLayoutPanel();
      lbl.RowCount = 3;
      lbl.ColumnCount = 1;
      lbl.Name = string.Format("layoutPanel_", model.ma);
      lbl.Text = model.name;
      lbl.Height = 15;
      lbl.AutoSize = true;
      return lbl;
    }

    private RadioButton CreateRadio(int index, dm_column model) {
      RadioButton radio = new RadioButton();
      radio.Location = new Point(index, 50);
      radio.Name = string.Format(string.Format("rad_{0}", model.ma));
      radio.Text = string.Format("{0}_{1}", model.name,index);
      radio.Checked = model.isKey;
      radio.Click += Radio_Click;
      return radio;
    }

    private void CreateColumnGridView(DataGridView dataGridView) {
      try {
        List<dm_column> model = SQLDatabase.Loaddm_column("select * from dm_column order by OrderId");
        DataGridViewTextBoxColumn[] columns = new DataGridViewTextBoxColumn[model.Count()];
        for (int i = 0; i < model.Count(); i++) {
          columns[i] = new DataGridViewTextBoxColumn();
        }
        DataGridViewColumn[] aa = new DataGridViewColumn[model.Count()];
        for (int i = 0; i < model.Count(); i++) {
          aa[i] = new DataGridViewTextBoxColumn();
          aa[i] = columns[i];
        }
        dataGridView.Columns.AddRange(aa);
        for (int i = 0; i < model.Count(); i++) {
          columns[i].DataPropertyName = model[i].ma;
          columns[i].HeaderText = model[i].name;
          columns[i].Name = model[i].ma;
          columns[i].Width=180;
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "CreateColumnGridView");
      }
    }

    private ComboBox CreateCombox(int index, dm_column model) {
      ComboBox com = new ComboBox();
      com.Location = new Point(index, 50);
      com.Name = string.Format(string.Format("cbb_{0}", model.ma));
      com.DropDownStyle = ComboBoxStyle.DropDownList;
      return com;
    }
    //http://blog.appconus.com/2017/04/01/net-async-dung-co-tuong-bo-async-la-no-asynchronous-nha/
    //https://stackoverflow.com/questions/44959735/is-it-possible-to-cancel-a-sqlbulkcopy-writetoserver-c
    private async void btn_Import_Click(object sender, EventArgs e) {
      var text = await AnMethodAsync();
    }


    private Task<string> AnMethodAsync() {
      //Do somethine async
      return Task.Run(() =>
      {
        ProcessImport();
        return "my data";
      });
    }

    public void ProcessImport() {
      OleDbDataReader reader = null;
      try {
        SQLDatabase.ExcNonQuery("TRUNCATE TABLE import");

        List<string> model = new List<string>();
        Dictionary<string, string> dict = new Dictionary<string, string>();
        
          foreach (var item in combox) {
          item.Invoke((Action)delegate {
            if (item.SelectedIndex != 0) model.Add(item.Text);
            dict.Add(item.Name, item.SelectedIndex == 0 ? "" : item.Text);
          });
          }
       
        Dictionary<string, string> mode = new Dictionary<string, string>();
        /***********text*****************/
        /*
        OleDbConnection con = new OleDbConnection(connectionString);
        con.Open();

        OleDbDataAdapter dap = new OleDbDataAdapter("select * from [096test.txt]", con);

        DataTable dt = new DataTable();
        dt.TableName = "Data";
        dap.Fill(dt);
        */
        int TypeDataSource = 0;
        cbb_TypeDataSource.Invoke((Action)delegate {
          TypeDataSource = cbb_TypeDataSource.SelectedIndex;
        });

        string strFileName = "";
        txt_FileName.Invoke((Action)delegate {
          strFileName = System.IO.Path.GetFileName(txt_FileName.Text);
        });

        string tableName = TypeDataSource == 0 ? strFileName : _strTable2;

        reader = SQLDatabase.ExcOleReaderDataSource(connectionString, tableName, model.ToArray());

        //----- Get Data from Source 
        progressBar1.Invoke((Action)delegate {
          progressBar1.Maximum = _nTongRowsText;
          progressBar1.Minimum = 0;
        });


        // Set up the bulk copy object.
        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(SQLDatabase.ConnectionString)) {
          //http://csharp-video-tutorials.blogspot.com/2014/09/part-20-sqlbulkcopy-notifyafter-example_27.html
          bulkCopy.NotifyAfter = 5000;
          bulkCopy.BatchSize = 10000;
          bulkCopy.SqlRowsCopied += (sender, e) =>
          {
            progressBar1.Invoke((Action)delegate {
              progressBar1.Value = ConvertType.ToInt(e.RowsCopied);
              progressBar1.Update();
            });

            lblmessage.Invoke((Action)delegate {
              lblmessage.Text = string.Format("{0}%", (ConvertType.ToInt((e.RowsCopied * 100)) / _nTongRowsText).ToString());
              lblmessage.Update();
            });
            Thread.Sleep(0);
          };

          bulkCopy.DestinationTableName = "dbo.import";
          foreach (var item in combox) {
            if (dict.FirstOrDefault(x => x.Key == item.Name).Value != "") {
              SqlBulkCopyColumnMapping cbb_TelNumber = new SqlBulkCopyColumnMapping(dict.FirstOrDefault(x => x.Key == item.Name).Value, item.Name.Substring(4, item.Name.Length - 4));
              bulkCopy.ColumnMappings.Add(cbb_TelNumber);
            }
          }

          try {
            bulkCopy.WriteToServer(reader);
          }
          catch (Exception ex) {
            Console.WriteLine(ex.Message);
          }
          finally {
            progressBar1.Invoke((Action)delegate {
              progressBar1.Value = _nTongRowsText;
              progressBar1.Update();
            });

            lblmessage.Invoke((Action)delegate {
              lblmessage.Text = "100%";
              lblmessage.Update();
            });

            Thread.Sleep(0);

            reader.Close();
            MessageBox.Show("Hoàn tất import dữ liệu", "Thông Báo");
          }
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "ProcessImport");
      }
    }

    private  void BindingGridViewMain() {
      try {
       
        groupMain.Invoke((Action)delegate
        {
          groupMain.Text = "Xem Tất Cả Khách Hàng";
        });
        GridViewMain.Invoke((Action)delegate {
          GridViewMain.Rows.Clear();
          GridViewMain.VirtualMode = true;
        });
          
        //----- Create object to cache data from database
        cachedData = new CachedData();
        cachedData.LastRowIndex = -1;
        string CommandToGetCount = "";
        string CommandToGetData = "";

        
        CommandToGetCount = string.Format("Select COUNT(*) As TotalRow From dbo.root where valueskeySearch like '%{0}%' ", ConvertType.convertToUnSign3(txtTim.Text));
        CommandToGetCount = string.Format("with roots as (select ROW_NUMBER() Over (Order By creatdate asc) As RowNumber, {0} " +
                                         " from dbo.root {1} {2}", Utilities.SelectColumn(), txtTim.Text.Trim() == "" ? "" : string.Format("where valueskeySearch like ''%{0}%''", ConvertType.convertToUnSign3(txtTim.Text.Trim())), Dm_Batdongbo.ma.Trim());
        
        cachedData.CommandToGetCount = CommandToGetCount;
        cachedData.CommandToGetData = CommandToGetData;

        cachedData.UpdateCachedData(0);
        GridViewMain.RowCount = (int)cachedData.TotalRowCount;
        ////----- value textbox
        //GetValueTextbox();
        ////----- Enabled button Edit,Delete
        //EnabledButton();
        ////----- Sum record
        String tongcongGoc = cachedData.TotalRowCount.ToString();
        
        groupMain.Invoke((Action)delegate {
          groupMain.Text = string.Format("Xem Tất Cả Khách Hàng :{0}", tongcongGoc);
        });

      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "BindingGridViewMain");
      }
    }

    private void GridViewMain_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e) {
      int indexRow;
      try {
        if (cachedData.CachedTable.Rows.Count <= 0) return;
        cachedData.UpdateCachedData(e.RowIndex);
        if (cachedData.CachedTable == null)
          return;
        indexRow = e.RowIndex % cachedData.PageSize;
        e.Value = cachedData.CachedTable.Rows[indexRow][e.ColumnIndex];
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "DataGrid CellValueNeeded", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void btntim_Click(object sender, EventArgs e) {
      new Waiting(() => {
        BindingGridViewMain();
      }).ShowDialog();
    }

    private void btn_View_Click(object sender, EventArgs e) {

    }
  }
}

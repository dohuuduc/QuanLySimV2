using DiDong;
using DiDong.Properties;
using SchemaSpec;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
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
    private dm_batdongbo _dm_Batdongbo=null;
    private string _strDB_NameDatabase;
    private string _strDatabase2;
    private string _strTable2;
    private bool _cancelImport;
    private string _strDieuKien;

    public frmMain() {

      InitializeComponent();

      CreateColumnGridView(GridViewMain,true);
      CreateColumnGridView(dataGridView_tontai,false);
      CreateColumnGridView(dataGridView_chuatontai,false);
      

      _Columns = SQLDatabase.Loaddm_column("select * from dm_column order by orderid");
      _strDB_NameDatabase = SQLDatabase.ExcDataTable(string.Format("SELECT DB_NAME(0)AS [DatabaseName]; ")).Rows[0]["DatabaseName"].ToString();

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
        frm.strDatabase = _strDB_NameDatabase;
        frm.ShowDialog();
        _dm_Batdongbo = SQLDatabase.Loaddm_batdongbo(string.Format("select * from dm_batdongbo where isAct=1")).FirstOrDefault();
        _cauhinh = SQLDatabase.Loadcau_hinh("select * from cau_hinh").FirstOrDefault();
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "");
      }
    }

    private void frmMain_Load(object sender, EventArgs e) {
      _cauhinh = SQLDatabase.Loadcau_hinh("select * from cau_hinh").FirstOrDefault();
      _dm_Batdongbo = SQLDatabase.Loaddm_batdongbo(string.Format("select * from dm_batdongbo where isAct=1")).FirstOrDefault();

      cbb_TypeDataSource.SelectedIndex = 0;
      dm_Character dm_Character = SQLDatabase.Loaddm_Character("select * from dm_Character where isAct=1").FirstOrDefault();
      groupBox3.Text = string.Format("Dữ Liệu Nguồn (TXT: {0})", dm_Character.name);
      button6_Click(null, null);
    }

    private async void BindImport() {
      try {
        picChuaTonTai.Invoke((Action)delegate {
            picChuaTonTai.Visible = true;
          });
        lblChuaTonTai.Invoke((Action)delegate {
          lblChuaTonTai.Visible = true;
        });

        picDaTonTai.Invoke((Action)delegate {
          picDaTonTai.Visible = true;
        });
        lblDatontai.Invoke((Action)delegate {
          lblDatontai.Visible = true;
        });
        button2.Invoke((Action)delegate {
          button2.Enabled = false;
        });
        button3.Invoke((Action)delegate {
          button3.Enabled = false;
        });
        btn_Import.Invoke((Action)delegate {
          btn_Import.Enabled = false;
        });


        Task<string> task1 = BindingImportChuaTonTai();
        Task<string> task2 = BindingImportDaTonTai();


        await Task.WhenAny(task1,task2);
        string x = task1.Result;
        // await Task.WhenAll(BindingImportDaTonTai());

        // await Task.WhenAll(strings.Select(s => Task.Run(() => DoSomething(s)));
      //  await BindingImportChuaTonTai();

        picChuaTonTai.Invoke((Action)delegate {
          picChuaTonTai.Visible = false;
        });
        lblChuaTonTai.Invoke((Action)delegate {
          lblChuaTonTai.Visible = false;
        });

        picDaTonTai.Invoke((Action)delegate {
          picDaTonTai.Visible = false;
        });
        lblDatontai.Invoke((Action)delegate {
          lblDatontai.Visible = false;
        });

        button2.Invoke((Action)delegate {
          button2.Enabled = true;
        });
        button3.Invoke((Action)delegate {
          button3.Enabled = true;
        });
        btn_Import.Invoke((Action)delegate {
          btn_Import.Enabled = true;
        });
      }
      catch (Exception) {
        throw;
      }
    }

    private async void button6_Click(object sender, EventArgs e) {
      picChuaTonTai.Invoke((Action)delegate {
        picChuaTonTai.Visible = true;
      });
      lblChuaTonTai.Invoke((Action)delegate {
        lblChuaTonTai.Visible = true;
      });

      picDaTonTai.Invoke((Action)delegate {
        picDaTonTai.Visible = true;
      });
      lblDatontai.Invoke((Action)delegate {
        lblDatontai.Visible = true;
      });
      button2.Invoke((Action)delegate {
        button2.Enabled = false;
      });
      button3.Invoke((Action)delegate {
        button3.Enabled = false;
      });
      btn_Import.Invoke((Action)delegate {
        btn_Import.Enabled = false;
      });

      await BindingImportChuaTonTai() ;
      await BindingImportDaTonTai();

      picChuaTonTai.Invoke((Action)delegate {
        picChuaTonTai.Visible = false;
      });
      lblChuaTonTai.Invoke((Action)delegate {
        lblChuaTonTai.Visible = false;
      });

      picDaTonTai.Invoke((Action)delegate {
        picDaTonTai.Visible = false;
      });
      lblDatontai.Invoke((Action)delegate {
        lblDatontai.Visible = false;
      });

      button2.Invoke((Action)delegate {
        button2.Enabled = true;
      });
      button3.Invoke((Action)delegate {
        button3.Enabled = true;
      });
      btn_Import.Invoke((Action)delegate {
        btn_Import.Enabled = true;
      });
    }
    private Task<string> BindingImportChuaTonTai() {
        return Task.Run(() =>
        {
          string strcommandTop = string.Format(" select COUNT_BIG(*) from dbo.import a "+
                                               " where a.{0} not in (select {0} from dbo.root where {0} is not null) AND "+
                                               " a.{0} is not null {1}",
                                               radioButtons.Where(p => p.Checked).FirstOrDefault().Tag,
                                               _dm_Batdongbo.ma);
          object totalRowCount = SQLDatabase.ExcScalar(strcommandTop);

          if (_cauhinh.MaxTop != 0) {
            string strcommand = string.Format( " select {0} {1} from dbo.import a "+
                                               " where a.{2} not in (select {2} from dbo.root where {2} is not null) AND " +
                                               " a.{2} is not null "+
                                               " order by {3} {4}",
                                      _cauhinh.MaxTop.ToString() == "-1" ? "" : string.Format("top {0}", _cauhinh.MaxTop),
                                      Utilities.SelectColumn(""),
                                      radioButtons.Where(p => p.Checked).FirstOrDefault().Tag,
                                      Utilities.OrderColumn(""),
                                      _dm_Batdongbo.ma);

            DataTable tb = SQLDatabase.ExcDataTable(strcommand);
            dataGridView_chuatontai.Invoke((Action)delegate {
              dataGridView_chuatontai.DataSource = tb;
            });
          }
          if (ConvertType.ToDouble(totalRowCount) > 0) {
            tabPage4.Invoke((Action)delegate {
              tabPage4.Text = string.Format("Khách Hàng Chưa Tồn Tại Ở File Gốc: {0}", ConvertType.FormatNumber(totalRowCount.ToString()));
              tabPage4.Update();
            });
            btnChuaTonTaiAdd.Invoke((Action)delegate {
              btnChuaTonTaiAdd.Enabled = true;
              btnChuaTonTaiAdd.Update();
            });
            btnChuatontaiXuatfile.Invoke((Action)delegate {
              btnChuatontaiXuatfile.Enabled = true;
              btnChuatontaiXuatfile.Update();
            });
            
          }
          else {
            totalRowCount = 0;
            tabPage4.Invoke((Action)delegate {
              tabPage4.Text = string.Format("Khách Hàng Chưa Tồn Tại Ở File Gốc");
            });
            btnChuaTonTaiAdd.Invoke((Action)delegate {
              btnChuaTonTaiAdd.Enabled = false;
            });
            btnChuatontaiXuatfile.Invoke((Action)delegate {
              btnChuatontaiXuatfile.Enabled = false;
            });
          }
          return "my data";
        });
    }
    private Task<string> BindingImportDaTonTai() {
        return Task.Run(() => {
          string strcommandTop = string.Format(" select COUNT_BIG(*) from dbo.import a " +
                                               " where a.{0} in (select {0} from dbo.root where {0} is not null) AND " +
                                               " a.{0} is not null {1}",
                                               radioButtons.Where(p => p.Checked).FirstOrDefault().Tag,
                                               _dm_Batdongbo.ma);
          object totalRowCount = SQLDatabase.ExcScalar(strcommandTop);

          if (_cauhinh.MaxTop != 0) {
            string strcommand = string.Format(" select {0} {1} from dbo.import a " +
                                               " where a.{2} in (select {2} from dbo.root where {2} is not null) AND " +
                                               " a.{2} is not null " +
                                               " order by {3} {4}",
                                      _cauhinh.MaxTop.ToString() == "-1" ? "" : string.Format("top {0}", _cauhinh.MaxTop),
                                      Utilities.SelectColumn(""),
                                      radioButtons.Where(p => p.Checked).FirstOrDefault().Tag,
                                      Utilities.OrderColumn(""),
                                      _dm_Batdongbo.ma);

            DataTable dataTable = SQLDatabase.ExcDataTable(strcommand);
            dataGridView_tontai.Invoke((Action)delegate {
              dataGridView_tontai.DataSource = dataTable;
            });
          }
          //object totalRowCount = SQLDatabase.ExcScalar(strcommandTop);
          if (ConvertType.ToDouble(totalRowCount) > 0) {
            tabPage3.Invoke((Action)delegate {
              tabPage3.Text = string.Format("Khách Hàng Đã Tồn Tại Ở File Gốc: {0}",ConvertType.FormatNumber(totalRowCount.ToString()));
              tabPage3.Update();
            });
            btnDatonCapNhat.Invoke((Action)delegate {
              btnDatonCapNhat.Enabled = true;
            });
            btnDatonXuatFile.Invoke((Action)delegate {
              btnDatonXuatFile.Enabled = true;
            });
            btnDatonXoaGoc.Invoke((Action)delegate {
              btnDatonXoaGoc.Enabled = true;
            });
          }
          else {
            totalRowCount = 0;
            tabPage3.Invoke((Action)delegate {
              tabPage3.Text = string.Format("Khách Hàng Đã Tồn Tại Ở File Gốc");
            });
            btnDatonCapNhat.Invoke((Action)delegate {
              btnDatonCapNhat.Enabled = false;
            });
            btnDatonXuatFile.Invoke((Action)delegate {
              btnDatonXuatFile.Enabled = false;
            });
            btnDatonXoaGoc.Invoke((Action)delegate {
              btnDatonXoaGoc.Enabled = false;
            });
          }
          return "my data";
        });
      }
    private void button1_Click(object sender, EventArgs e) {
      frmSearch frm = new frmSearch();
      if (frm.ShowDialog() == DialogResult.OK) {
        _strDieuKien = frm.strWhere;
        LoadBindRoot();
      }
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
          for (int i = 0; i < combox.Count(); i++) {
            combox[i].Items.Clear();
          }
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
            DataTable tb = SQLDatabase.ExcDataTable(string.Format(" select *  from {0}.INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='{1}'", _strDatabase2,_strTable2));
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
      radio.Tag = model.ma;
      radio.Checked = model.isKey;
      radio.Click += Radio_Click;
      return radio;
    }

    private void CreateColumnGridView(DataGridView dataGridView,bool Cotstt) {
      try {
        List<dm_column> model = new List<dm_column>();
        if(Cotstt)
          model.Add(new dm_column() { ma = "RowNumber", name = "STT" });
        foreach (var item in SQLDatabase.Loaddm_column("select * from dm_column order by OrderId")) {
          model.Add(item);
        } 

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
          columns[i].ReadOnly = true;
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
      try {
        if (btn_Import.Text.Equals("Import")) {
          _cancelImport = false;
          btn_Import.Text = "Stop";
         
          progressBar1.Visible = true;
          lblmessage.Visible = true;
          var text = await AnMethodAsync();
          button6_Click(null, null);
        }
        else {
          _cancelImport = true;
          btn_Import.Text = "Import";
        }
      }
      catch (Exception ex) {

        MessageBox.Show(ex.Message, "btn_Import_Click");
      }
    }
    
    private void ControlSetEnabled(bool enabled) {
      try {
        foreach (var item in groupbox) {
          item.Invoke((Action)delegate {
            item.Enabled = enabled;
          });
        }
        tabControl2.Invoke((Action)delegate {
          tabControl2.Enabled = enabled;
        });
        button6.Invoke((Action)delegate {
          button6.Enabled = enabled;
        });
        btn_View.Invoke((Action)delegate {
          btn_View.Enabled = enabled;
        });
        button3.Invoke((Action)delegate {
          button3.Enabled = enabled;
        });
        button2.Invoke((Action)delegate {
          button2.Enabled = enabled;
        });
        cbb_TypeDataSource.Invoke((Action)delegate {
          cbb_TypeDataSource.Enabled = enabled;
        });
      }
      catch (Exception ex) {
        throw;
      }
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
        ControlSetEnabled(false);
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

            if (_cancelImport) {
              ControlSetEnabled(true);
              e.Abort = true;
            }

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
            ControlSetEnabled(true);
            Thread.Sleep(0);

            reader.Close();
            MessageBox.Show(string.Format("Hoàn tất import dữ liệu\n {0}",_cancelImport ? "Người dùng đã tạm dừng":"", "Thông Báo"));
            progressBar1.Invoke((Action)delegate {
              progressBar1.Visible = false;
              progressBar1.Update();
            });
            lblmessage.Invoke((Action)delegate {
              lblmessage.Visible = false;
              lblmessage.Update();
            });
            btn_Import.Invoke((Action)delegate {
              btn_Import.Text = "Import";
              btn_Import.Update();
            });
            _cancelImport = false;
          }
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "ProcessImport");
      }
    }

    private  void BindInfoRoot(string strChuoiDieuKien) {
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

        
        CommandToGetCount = string.Format("Select COUNT(*) As TotalRow From dbo.root {0} {1}", strChuoiDieuKien, _dm_Batdongbo.ma.Trim());
        CommandToGetData = string.Format(" with Tel as (select ROW_NUMBER() Over (Order By {3} asc) As RowNumber, {0} " +
                                         " from dbo.root {1} {2}) Select * From Tel where ", Utilities.SelectColumn(""), 
                                                                                      strChuoiDieuKien, 
                                                                                      _dm_Batdongbo.ma.Trim(),
                                                                                      Utilities.OrderColumn(""));
        
        cachedData.CommandToGetCount = CommandToGetCount;
        cachedData.CommandToGetData = CommandToGetData;

        cachedData.UpdateCachedData(0);
        GridViewMain.Invoke((Action)delegate {
          GridViewMain.RowCount = (int)cachedData.TotalRowCount;
        });
        ////----- value textbox
        //GetValueTextbox();
        ////----- Enabled button Edit,Delete
        //EnabledButton();
        ////----- Sum record
        String tongcongGoc = cachedData.TotalRowCount.ToString();
        
        groupMain.Invoke((Action)delegate {
          groupMain.Text = string.Format("Xem Tất Cả Khách Hàng :{0}", ConvertType.FormatNumber(tongcongGoc));
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
      _strDieuKien = string.Format(" where valueskeySearch like '%{0}%' ",ConvertType.convertToUnSign3(txtTim.Text.Trim()));
      LoadBindRoot();
    }

    private void LoadBindRoot() {
      new Waiting(() => {
        BindInfoRoot(_strDieuKien);
      }).ShowDialog();
    }
    private void btn_View_Click(object sender, EventArgs e) {
      try {
        if (txt_FileName.Text == "") {
          MessageBox.Show("Vui lòng tìm loại cấu hình cần import", "Thông báo");
          return;
        }
        DataTable table = null;
        if (cbb_TypeDataSource.SelectedIndex == 0) {
          new Waiting(() => {
            table = getDataText();
          }).ShowDialog();
        }
        else {
          new Waiting(() => {
            table = getDataSQL();
          }).ShowDialog();
        }
        if (table==null || table.Rows.Count == 0)
          MessageBox.Show("Không có dữ liệu !", "Xem truoc", MessageBoxButtons.OK, MessageBoxIcon.Information);
        else {
          frmXemTruoc frm = new frmXemTruoc();
          frm.DataSourceDate = table;
          frm.Title = txt_FileName.Text;
          frm.ShowDialog();
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "btn_View_Click");
      }
    }
    private DataTable getDataText() {
      StreamReader sReader;

      DataTable table = new DataTable();
      string[] fileNames, lineParts;

      string line;
      try {

        fileNames = txt_FileName.Text.Split('.');


        sReader = new StreamReader(txt_FileName.Text);
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

        while ((line = sReader.ReadLine()) != null && table.Rows.Count <= _cauhinh.MaxTop) {
          lineParts = line.Split(new char[] { kytu });
          DataRow rows = table.NewRow();
          for (int i = 0; i < SoCot; i++) {

            rows[string.Format("[{0}]", i)] = lineParts[i];
          }
          table.Rows.Add(rows);
        }

        return table;
      
      }
      catch (Exception ex) {
        return null;
        MessageBox.Show(ex.Message, "View DataSource", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private DataTable getDataSQL() {
      string conn = SQLDatabase.ConnectionString;
      int vitri1 = conn.IndexOf("initial catalog=")+ "initial catalog=".Length;
      int vitri2 = conn.IndexOf(";persist");
      string namedatabase = conn.Substring(vitri1, vitri2- vitri1);
      conn=conn.Replace(namedatabase, _strDatabase2);
      return  SQLDatabase.ExcDataTable(string.Format("select {1} * from {0}", _strTable2,_cauhinh.MaxTop==-1? "" :string.Format("top {0}", _cauhinh.MaxTop.ToString())),conn );
      //return aa;
    }

    private void button9_Click(object sender, EventArgs e) {
      cau_hinh cau_Hinh = SQLDatabase.Loadcau_hinh("SELECT * FROM cau_hinh").FirstOrDefault();
      try {
        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        saveFileDialog1.Filter = !cau_Hinh.IsExportTxt ? "Excel 97-2003 WorkBook|*.xls" : "text|*.txt";
        saveFileDialog1.Title = !cau_Hinh.IsExportTxt ? "Xuất file Excel" : "Xuất file text";
        if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
          if (saveFileDialog1.FileName == "") {
            MessageBox.Show("Vui lòng nhập tên file", "Thông Báo");
            return;
          }
          string strsql = string.Format("select {0}  from {1}.dbo.root  where {2} {3}", Utilities.SelectColumn(""), _strDB_NameDatabase, _strDieuKien, _dm_Batdongbo.ma);
          new Waiting(() => {
            string command = string.Format("exec [spExport] 'SET DATEFORMAT DMY {0}','{1}'", strsql , saveFileDialog1.FileName);
            if (SQLDatabase.ExcNonQuery(command)) {
              MessageBox.Show("Xuất file thành công", "Thông Báo");
            }
          });
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "button9_Click");
      }
    }

    //https://social.msdn.microsoft.com/Forums/sqlserver/en-US/619dbb5d-08bc-4bf7-8bdf-9bc473dadf66/update-fields-in-table-only-if-the-field-is-null?forum=transactsql
    private void button4_Click(object sender, EventArgs e) {
      try {
        frmColUpdate frm = new frmColUpdate();
        string macol=  radioButtons.Where(p => p.Checked == true).FirstOrDefault().Tag.ToString();
        dm_column dm_Column = SQLDatabase.Loaddm_column(string.Format("select * from dm_Column where ma='{0}'", macol)).FirstOrDefault();
        if (dm_Column == null) {
          MessageBox.Show("Vui lòng chọn key", "Thông Báo");
          return;
        }
        frm.ColumnKey = dm_Column;
        frm.IsUpdate = true;
        if (frm.ShowDialog() == DialogResult.OK) {
          bool isTrue = false;
          new Waiting(() => {
            isTrue = SQLDatabase.ExcNonQuery(frm.strSQL);
          }).ShowDialog();
          if (isTrue)
            MessageBox.Show("Cập nhật thành công", "Thông báo");
          else
            MessageBox.Show("Cập nhật không thành công", "Thông báo");
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "");
      }
    }

    private void button5_Click(object sender, EventArgs e) {
      try {
        frmColUpdate frm = new frmColUpdate();
        string macol = radioButtons.Where(p => p.Checked == true).FirstOrDefault().Tag.ToString();
        dm_column dm_Column = SQLDatabase.Loaddm_column(string.Format("select * from dm_Column where ma='{0}'", macol)).FirstOrDefault();
        if (dm_Column == null) {
          MessageBox.Show("Vui lòng chọn key", "Thông Báo");
          return;
        }
        frm.ColumnKey = dm_Column;
        frm.IsUpdate = false;
        if (frm.ShowDialog() == DialogResult.OK) {
          bool isTrue = false;
          new Waiting(() => {
            isTrue = SQLDatabase.ExcNonQuery(frm.strSQL);
          }).ShowDialog();
          if (isTrue) {
            MessageBox.Show("Insert thành công \n Hệ thống sẽ làm tươi lại dữ liệu", "Thông báo");
            button6_Click(null, null);
          }
          else
            MessageBox.Show("Insert không thành công", "Thông báo");
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "");
      }

    }

    private void btnDatonXoaGoc_Click(object sender, EventArgs e) {
      try {
        if (MessageBox.Show("Bạn có chắc muốn xoá dữ liệu gốc trùng với dữ liệu tạm ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
          bool isTrue = false;
          new Waiting(() => {
            isTrue = SQLDatabase.ExcNonQuery(string.Format("Delete from dbo.root "+
                                                           "where {0} in (select {0} from dbo.import where {0} is not null) and {0} is not null", radioButtons.Where(p => p.Checked).FirstOrDefault().Tag));
          }).ShowDialog();
          if (isTrue) {
            MessageBox.Show("Xoá xong dữ liệu gốc trùng dữ liệu nguồn \n Hệ thống sẽ làm tươi lại dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button6_Click(null, null);
            }
          }
        }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "btnDatonXoaGoc_Click");
      }
    }

    private void btnDatonXuatFile_Click(object sender, EventArgs e) {
      try {
        string folderpath = "";
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        DialogResult dr = fbd.ShowDialog();
        string fileName = "";
        if (dr == DialogResult.OK) {
          folderpath = fbd.SelectedPath;
        }

        string filePath = folderpath == "" ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : folderpath;
        fileName = string.Format("chuatontai_{0}", _cauhinh.IsExportTxt ? ".txt" : ".xls");

        new Waiting((MethodInvoker)delegate {

          string strcommand = string.Format(@"select {0} from {4}.dbo.import a inner join {4}.dbo.root b on a.{1}=b.{1} order by {2} {3}", Utilities.SelectColumn("a"),
                                                                                                                                   radioButtons.Where(p => p.Checked).FirstOrDefault().Tag.ToString().Replace("'", "''"),
                                                                                                                                   Utilities.OrderColumn(""),
                                                                                                                                   _dm_Batdongbo.ma.Replace("'", "''"),
                                                                                                                                   _strDB_NameDatabase
                                                                                                                                    );

          SQLDatabase.ExcNonQuery(string.Format("[spExport] '{0}','{1}'", strcommand, filePath + "\\" + fileName));
        }, "Vui Lòng Chờ").ShowDialog();

        MessageBox.Show("Đã hoàn thành xuất file đã tồn tại", "Thông Báo");
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "btnDatonXuatFile_Click");
      }
    }

    private void btnChuatontaiXuatfile_Click(object sender, EventArgs e) {
      try {
        string folderpath = "";
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        DialogResult dr = fbd.ShowDialog();
        string fileName = "";
        if (dr == DialogResult.OK) {
          folderpath = fbd.SelectedPath;
        }

        string filePath = folderpath == "" ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : folderpath;
               fileName =string.Format("chuatontai_{0}",_cauhinh.IsExportTxt ? ".txt" : ".xls");

        new Waiting((MethodInvoker)delegate {

          string strcommand = string.Format(@"select {0} from {4}.dbo.import a left join {4}.dbo.root b on a.{1}=b.{1} where b.{1} is null order by {2} {3}", Utilities.SelectColumn("a"),
                                                                                                                                   radioButtons.Where(p => p.Checked).FirstOrDefault().Tag.ToString().Replace("'","''"),
                                                                                                                                   Utilities.OrderColumn(""),
                                                                                                                                   _dm_Batdongbo.ma.Replace("'","''"),
                                                                                                                                   _strDB_NameDatabase
                                                                                                                                    );

          SQLDatabase.ExcNonQuery(string.Format("[spExport] '{0}','{1}'", strcommand, filePath + "\\" + fileName));
        }, "Vui Lòng Chờ").ShowDialog();

        MessageBox.Show("Đã hoàn thành xuất file theo chưa tồn tại", "Thông Báo");
      }
      catch (Exception ex) {

        MessageBox.Show(ex.Message, "uIDVàNameToolStripMenuItem_Click");
      }
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
      try {
        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string fileName = "Mau_File";
        bool temp = false;
        new Waiting(() => temp = xuatfilemain(filePath + "\\" + fileName), "Vui Lòng Chờ").ShowDialog();
        MessageBox.Show("Đã xuất thành công file.", "Thông Báo");
      }
      catch (Exception ex) {

        MessageBox.Show(ex.Message, "linkLabel1_LinkClicked");
      }
    }
    private bool xuatfilemain(string filePath) {
      try {
        DataTable table = new DataTable();
        
        foreach (var item in _Columns)
          table.Columns.Add(item.name, typeof(string));
        ExcelAdapter excel = new ExcelAdapter(filePath);
        excel.CreateAndWrite(table, "Mau", 1);
        return true;
      }
      catch (Exception ex) {
        return false;
      }

    }
  }
}

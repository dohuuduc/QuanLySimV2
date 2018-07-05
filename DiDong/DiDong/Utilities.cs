using QuanLyData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace DiDong {
  public class Utilities {
    public static string SelectColumn(string x) {
      try {
        string strSelect = "";
        List<dm_column> dm_Columns = SQLDatabase.Loaddm_column("select * from dm_column where isAct=1 order by orderid asc");
        foreach (var item in dm_Columns) {
          strSelect += string.Format("{0}{1},",x==""?"": string.Format("{0}.",x), item.ma);
        }
        return strSelect.Substring(0,strSelect.Length-1);
      }
      catch (Exception ex) {
        return "";
      }
    }
    public static string OrderColumn(string x) {
      try {
        string strSelect = "";
        List<dm_column> dm_Columns = SQLDatabase.Loaddm_column("select* from dm_column where isorder=1 order by orderid asc");
        if (dm_Columns.Count() == 0)
          return x == "" ? "create_date" : string.Format("{0}.{1}", x, "create_date");
        foreach (var item in dm_Columns) {
          strSelect += string.Format("{0}{1},", x == "" ? "" : string.Format("{0}.", x), item.ma);
        }
        return strSelect.Substring(0, strSelect.Length - 1);
      }
      catch (Exception ex) {
        return "";
      }
    }

    public static string WhereAll(string dieukien) {
      string strSelect = "where ";
      List<dm_column> dm_Columns = SQLDatabase.Loaddm_column("select* from dm_column where isAct=1 order by orderid asc");
      foreach (var item in dm_Columns) {
        strSelect += string.Format("{0} like N'%{0}%' or,", item.ma,dieukien);
      }
      return strSelect;
    }
   }


  public class ConvertType {
    public static int ToInt(object obj) {
      try {
        if (obj == null)
          return 0;
        int rs = System.Convert.ToInt32(obj);
        if (rs < 0)
          return 0;
        return rs;
      }
      catch {
        return 0;
      }
    }
    public static double ToDouble(object obj) {
      try {
        if (obj == null)
          return 0;
        double rs = System.Convert.ToDouble(obj);
        if (rs < 0)
          return 0;
        return rs;
      }
      catch {
        return 0;
      }
    }
    public static decimal ToDecimal(object obj) {
      try {
        if (obj == null)
          return 0;
        decimal rs = System.Convert.ToDecimal(obj);
        if (rs < 0)
          rs = 0;
        return rs;
      }
      catch { return 0; }
    }
    public static string ToString(object obj) {
      try {
        if (obj == null)
          return "";
        return System.Convert.ToString(obj);
      }
      catch {
        return "";
      }
    }
    public static float ToFloat(object obj) {
      try {
        if (obj == null)
          return 0;
        float rs = float.Parse(obj.ToString());
        if (rs < 0)
          return 0;
        return rs;
      }
      catch {
        return 0;
      }
    }
    public static DateTime ToDateTime(object obj) {
      try {
        if (obj == null)
          return DateTime.Now;
        DateTime dt = System.Convert.ToDateTime(obj, System.Globalization.CultureInfo.InvariantCulture);

        return dt;
      }
      catch {
        return DateTime.Now;
      }
    }
    public static Guid ToGuid(object obj) {
      try {
        if (obj == null)
          return Guid.Empty;
        Guid dt = new Guid(obj.ToString());
        return dt;
      }
      catch {
        return Guid.Empty;
      }
    }

    public static string StripDiacritics(string accented) {
      return Regex.Replace(StripDiacriticsNormalize(accented), @"\s+", "-");
    }
    public static string StripDiacriticsNormalize(string accented) {
      Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
      string strFormD = accented.Normalize(System.Text.NormalizationForm.FormD);
      strFormD = regex.Replace(strFormD, String.Empty);
      strFormD = strFormD.Replace("Đ", "D").Replace("đ", "d");
      return Regex.Replace(strFormD, @"[^A-Za-z0-9 ]", "").Trim().ToLower();
    }
    const string HTML_TAG_PATTERN = "<.*?>";

    public static string StripHTML(object inputString, int charactor) {
      string str = Regex.Replace(ConvertType.ToString(inputString), HTML_TAG_PATTERN, string.Empty);
      if (str.Length > charactor)
        return str.Substring(0, charactor) + "...";
      return str;
    }
    public static string StripHTML(object inputString) {
      string str = Regex.Replace(ConvertType.ToString(inputString), HTML_TAG_PATTERN, string.Empty);
      return str;
    }
    public static string Encode(object str) {
      byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str.ToString());
      return Convert.ToBase64String(encbuff);
    }
    public static string Decode(object str) {
      byte[] decbuff = Convert.FromBase64String(str.ToString());
      return System.Text.Encoding.UTF8.GetString(decbuff);
    }
    public static string FormatNumber(string value) {
      return Convert.ToDecimal(value).ToString("#,##0.00"); //String.Format(CultureInfo.InvariantCulture,"{0:#,##0,,}", value);
    }
    public static string getTime120(System.DateTime dt) {
      int dd = dt.Day;
      int mm = dt.Month;
      int yy = dt.Year;
      int hh = dt.Hour;
      int min = dt.Minute;
      int ss = dt.Second;
      return string.Concat(new string[]
      {
        "convert(datetime,'",
        yy.ToString(),
        "-",
        mm.ToString(),
        "-",
        dd.ToString(),
        " ",
        hh.ToString(),
        ":",
        min.ToString(),
        ":",
        ss.ToString(),
        "',120)"
      });
    }
    public static string convertToUnSign3(string s) {
      Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
      string temp = s.Normalize(NormalizationForm.FormD);
      return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
    }
  }

  public class ExcelAdapter {
    protected string sFilePath;
    public string SFilePath {
      get { if (sFilePath == null) return ""; return sFilePath; }
      set { sFilePath = value; }
    }

    public ExcelAdapter(string filePath) {
      this.SFilePath = filePath;
    }

    public bool DeleteFile() {
      if (File.Exists(this.SFilePath)) {
        File.Delete(this.SFilePath);
        return true;
      }
      else
        return false;
    }

    public bool IsExist() {
      return File.Exists(this.SFilePath);
    }

    public DataTable ReadFromFile(string commandText) {
      string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.sFilePath + ";Extended Properties=\"Excel 8.0;HDR=YES;\"";



      DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");

      DbDataAdapter adapter = factory.CreateDataAdapter();

      DbCommand selectCommand = factory.CreateCommand();
      selectCommand.CommandText = commandText;

      DbConnection connection = factory.CreateConnection();
      connection.ConnectionString = connectionString;

      selectCommand.Connection = connection;

      adapter.SelectCommand = selectCommand;

      DataSet cities = new DataSet();

      adapter.Fill(cities);

      connection.Close();
      adapter.Dispose();

      return cities.Tables[0];
    }

    protected void FormatDate(COMExcel.Worksheet sheet, int rstart, int cstart, int rend, int cend) {
      COMExcel.Range range = (COMExcel.Range)sheet.Range[sheet.Cells[rstart, cstart], sheet.Cells[rend, cend]];
      range.NumberFormat = "DD/MM/YYYY";
    }

    protected void FormatMoney(COMExcel.Worksheet sheet, int rstart, int cstart, int rend, int cend) {
      COMExcel.Range range = (COMExcel.Range)sheet.Range[sheet.Cells[rstart, cstart], sheet.Cells[rend, cend]];
      range.NumberFormat = "#,##0";
    }

    protected void Format(COMExcel.Worksheet sheet, int rstart, int cstart, int rend, int cend, string type) {
      COMExcel.Range range = (COMExcel.Range)sheet.Range[sheet.Cells[rstart, cstart], sheet.Cells[rend, cend]];
      range.NumberFormat = type;
    }

    public string CreateAndWrite(DataTable dt, string sheetName, int noSheet) {
      using (new ExcelUILanguageHelper()) {
        COMExcel.Application exApp = new COMExcel.Application();
        COMExcel.Workbook exBook = exApp.Workbooks.Add(
                      COMExcel.XlWBATemplate.xlWBATWorksheet);
        try {
          // Không hiển thị chương trình excel
          exApp.Visible = false;

          // Lấy sheet 1.
          COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exBook.Worksheets[noSheet];
          exSheet.Name = sheetName;

          //////////////////////
          int rowCount = dt.Rows.Count;
          int colCount = dt.Columns.Count;

          // insert header name             
          for (int j = 1; j <= colCount; j++) {
            exSheet.Cells[1, j] = dt.Columns[j - 1].Caption;
          }

          // format cho header
          COMExcel.Range headr = (COMExcel.Range)exSheet.Range[exSheet.Cells[1, 1], exSheet.Cells[1, colCount]];
          headr.Interior.Color = System.Drawing.Color.YellowGreen.ToArgb();
          headr.Font.Bold = true;
          headr.Font.Name = "Arial";
          headr.Font.Color = System.Drawing.Color.White.ToArgb();
          headr.Cells.RowHeight = 30;
          headr.Cells.ColumnWidth = 20;
          headr.HorizontalAlignment = COMExcel.Constants.xlCenter;


          //format cho cot ngay, tien, so
          for (int i = 1; i <= colCount; i++) {
            if (dt.Columns[i - 1].DataType == Type.GetType("System.DateTime")) {
              FormatDate(exSheet, 2, i, rowCount + 1, i);
            }
            else if (dt.Columns[i - 1].DataType == Type.GetType("System.Decimal")) {
              Format(exSheet, 2, i, rowCount + 1, i, "##0.0");
            }
            else if (dt.Columns[i - 1].DataType == Type.GetType("System.Int64")) {
              FormatMoney(exSheet, 2, i, rowCount + 1, i);
            }
            else if (dt.Columns[i - 1].DataType == Type.GetType("System.Int32")) {
            }
            else {
              Format(exSheet, 2, i, rowCount + 1, i, "@");
            }
          }
          for (int i = 1; i <= rowCount; i++) {
            for (int j = 1; j <= colCount; j++) {
              exSheet.Cells[i + 1, j] = dt.Rows[i - 1][j - 1].ToString();
            }
          }

          //format cho toan bo sheet
          COMExcel.Range Sheet = (COMExcel.Range)exSheet.Range[exSheet.Cells[1, 1], exSheet.Cells[rowCount + 1, colCount]];
          Sheet.Borders.Color = System.Drawing.Color.Black.ToArgb();
          Sheet.WrapText = false;

          // Save file
          exBook.SaveAs(this.SFilePath, COMExcel.XlFileFormat.xlWorkbookNormal,
                           null, null, false, false,
                           COMExcel.XlSaveAsAccessMode.xlExclusive,
                           false, false, false, false, false);


          return "Export file excel thành công.\nĐường dẫn là: " + this.sFilePath;
        }
        catch (Exception ex) {
          Thread.CurrentThread.CurrentCulture.DateTimeFormat = new System.Globalization.CultureInfo("en-US").DateTimeFormat;
          return ex.ToString();
        }
        finally {
          Thread.CurrentThread.CurrentCulture.DateTimeFormat = new System.Globalization.CultureInfo("en-US").DateTimeFormat;
          // Đóng chương trình
          exBook.Close(false, false, false);
          exApp.Quit();
          System.Runtime.InteropServices.Marshal.ReleaseComObject(exBook);
          System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
        }
      }
    }

    public string CreateAndWrite(DataTable[] dtList, string[] sheetNames) {
      using (new ExcelUILanguageHelper()) {
        COMExcel.Application exApp = new COMExcel.Application();
        COMExcel.Workbook exBook = exApp.Workbooks.Add(
                      COMExcel.XlWBATemplate.xlWBATWorksheet);
        try {
          // Không hiển thị chương trình excel
          exApp.Visible = false;

          //List<COMExcel.Worksheet> exSheetList = new List<Microsoft.Office.Interop.Excel.Worksheet>();
          for (int i = 1; i < dtList.Length; i++) {
            //exSheetList.Add((COMExcel.Worksheet)exBook.Worksheets[i]);
            exBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
          }
          int noSheet = 1;
          foreach (DataTable dt in dtList) {
            COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exBook.Worksheets[noSheet];
            exSheet.Name = sheetNames[noSheet - 1];

            //////////////////////
            int rowCount = dt.Rows.Count;
            int colCount = dt.Columns.Count;

            // insert header name             
            for (int j = 1; j <= colCount; j++) {
              exSheet.Cells[1, j] = dt.Columns[j - 1].Caption;
            }

            // format cho header
            COMExcel.Range headr = (COMExcel.Range)exSheet.Range[exSheet.Cells[1, 1], exSheet.Cells[1, colCount]];
            headr.Interior.Color = System.Drawing.Color.Gray.ToArgb();
            headr.Font.Bold = true;
            headr.Font.Name = "Arial";
            headr.Font.Color = System.Drawing.Color.White.ToArgb();
            headr.Cells.RowHeight = 30;
            headr.Cells.ColumnWidth = 20;
            headr.HorizontalAlignment = COMExcel.Constants.xlCenter;


            //format cho cot ngay, tien, so
            for (int i = 1; i <= colCount; i++) {
              if (dt.Columns[i - 1].DataType == Type.GetType("System.DateTime")) {
                FormatDate(exSheet, 2, i, rowCount + 1, i);
              }
              else if (dt.Columns[i - 1].DataType == Type.GetType("System.Decimal")) {
                Format(exSheet, 2, i, rowCount + 1, i, "##0.0");
              }
              else if (dt.Columns[i - 1].DataType == Type.GetType("System.Int64")) {
                FormatMoney(exSheet, 2, i, rowCount + 1, i);
              }
              else if (dt.Columns[i - 1].DataType == Type.GetType("System.Int32")) {
              }
              else {
                Format(exSheet, 2, i, rowCount + 1, i, "@");
              }
            }

            for (int i = 1; i <= rowCount; i++) {
              for (int j = 1; j <= colCount; j++) {
                exSheet.Cells[i + 1, j] = dt.Rows[i - 1][j - 1].ToString();
              }
            }

            //format cho toan bo sheet
            COMExcel.Range Sheet = (COMExcel.Range)exSheet.Range[exSheet.Cells[1, 1], exSheet.Cells[rowCount + 1, colCount]];
            Sheet.Borders.Color = System.Drawing.Color.Black.ToArgb();
            Sheet.WrapText = true;

            noSheet++;
          }
          // Save file
          exBook.SaveAs(this.SFilePath, COMExcel.XlFileFormat.xlWorkbookNormal,
                          null, null, false, false,
                          COMExcel.XlSaveAsAccessMode.xlExclusive,
                          false, false, false, false, false);


          return "Export file excel thành công.\nĐường dẫn là: " + this.sFilePath;
        }
        catch (Exception ex) {
          Thread.CurrentThread.CurrentCulture.DateTimeFormat = new System.Globalization.CultureInfo("en-US").DateTimeFormat;
          return ex.ToString();
        }
        finally {
          Thread.CurrentThread.CurrentCulture.DateTimeFormat = new System.Globalization.CultureInfo("en-US").DateTimeFormat;
          // Đóng chương trình
          exBook.Close(false, false, false);
          exApp.Quit();
          System.Runtime.InteropServices.Marshal.ReleaseComObject(exBook);
          System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
        }
      }
    }

    public class ExcelUILanguageHelper : IDisposable {
      private System.Globalization.CultureInfo m_CurrentCulture;

      public ExcelUILanguageHelper() {
        // save current culture and set culture to en-US            
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        m_CurrentCulture = Thread.CurrentThread.CurrentCulture;
        m_CurrentCulture.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
      }

      #region IDisposable Members

      public void Dispose() {
        // return to normal culture
        Thread.CurrentThread.CurrentCulture = m_CurrentCulture;
      }

      #endregion
    }

  }

  public class CachedData {
    #region Fields

    private int pageSize = 50000;
    private long lastRowIndex = -1;
    private DataTable cachedTable = null;
    private long totalRowCount;
    private string commandToGetCount = "";
    private string columnOrder = "";


    private string commandToGetData = "";
    private string commandBatDongBo = "";

    #endregion  // Fields

    #region Properties

    public int PageSize {
      get { return pageSize; }
      set { pageSize = value; }
    }

    public long LastRowIndex {
      get { return lastRowIndex; }
      set { lastRowIndex = value; }
    }

    public DataTable CachedTable {
      get { return cachedTable; }
      set { cachedTable = value; }
    }

    public long TotalRowCount {
      get { return totalRowCount; }
      set { totalRowCount = value; }
    }


    public string CommandToGetCount {
      set { commandToGetCount = value; }
    }
    public string ColumnOrder {
      get { return columnOrder; }
      set { columnOrder = value; }
    }


    public string CommandToGetData {
      set { commandToGetData = value; }
    }

    public string CommandBatDongBo {
      set { commandBatDongBo = value; }
    }
    #endregion  // Properties

    #region Methods

    /// <summary>
    /// Function get data for Gridview paging
    /// </summary>
    /// <param name="rowIndex"></param>
    public void UpdateCachedData(long rowIndex) {
      DataTable table1;
      long lastIndex, minIndex, maxIndex;
      string sqlCommand;

      try {
        if (commandToGetCount == "")
          return;

        lastIndex = rowIndex - (rowIndex % pageSize);
        if (lastIndex == lastRowIndex)
          return;

        if (lastRowIndex == -1) {
          table1 = SQLDatabase.ExcDataTable(commandToGetCount);
          if (table1 != null && table1.Rows.Count > 0)
            totalRowCount = long.Parse(table1.Rows[0][0].ToString());
          else
            totalRowCount = 0;
        }

        lastRowIndex = lastIndex;
        minIndex = lastRowIndex + 1;
        maxIndex = lastRowIndex + pageSize;


        sqlCommand = commandToGetData;
        sqlCommand += " RowNumber Between " + minIndex.ToString() + " and ";
        sqlCommand += maxIndex.ToString();
        sqlCommand += string.Format(" order by RowNumber ASC {0}", commandBatDongBo);



        cachedTable = SQLDatabase.ExcDataTable(sqlCommand);
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "GetCachedData");
      }
    }

    #endregion  // Methods
  }
}

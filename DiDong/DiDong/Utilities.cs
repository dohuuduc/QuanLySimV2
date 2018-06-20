using QuanLyData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiDong {
  public class Utilities {
    public static string SelectColumn() {
      try {
        string strSelect = "";
        List<dm_column> dm_Columns = SQLDatabase.Loaddm_column("select* from dm_column where isAct=1 order by orderid asc");
        foreach (var item in dm_Columns) {
          strSelect += string.Format("{0},", item.ma);
        }
        return strSelect.Substring(0,strSelect.Length-1);
      }
      catch (Exception ex) {
        return "";
      }
    }
    public static string OrderColumn() {
      try {
        string strSelect = "";
        List<dm_column> dm_Columns = SQLDatabase.Loaddm_column("select* from dm_column where isorder=1 order by orderid asc");
        if (dm_Columns.Count() == 0) return "create_date";
        foreach (var item in dm_Columns) {
          strSelect += string.Format("{0},", item.ma);
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

  public class CachedData {
    #region Fields

    private int pageSize = 50000;
    private long lastRowIndex = -1;
    private DataTable cachedTable = null;
    private long totalRowCount;
    private string commandToGetCount = "";
    private string columnOrder = "";


    private string commandToGetData = "";

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
        sqlCommand += string.Format(" order by {0} ASC", Utilities.OrderColumn());



        cachedTable = SQLDatabase.ExcDataTable(sqlCommand);
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "GetCachedData");
      }
    }

    #endregion  // Methods
  }
}

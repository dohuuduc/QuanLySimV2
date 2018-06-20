using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyData {
  public class cau_hinh {
    public Guid id { get; set; }
    public bool DelImportTruocImport { get; set; }
    public int MaxTop { get; set; }
    public bool IsExportTxt { get; set; }
  }
  public class dm_batdongbo {
    public int id { get; set; }
    public string ma { get; set; }
    public string name { get; set; }
    public int orderid { get; set; }
    public bool isAct { get; set; }
  }
  public class dm_column {
    public Guid id { get; set; }
    public string ma { get; set; }
    public string name { get; set; }
    public int size { get; set; }
    public bool isKey { get; set; }
    public bool isAct { get; set; }
    public bool IsReport { get; set; }
    public bool isOrder { get; set; }
    public bool isSearch { get; set; }
    public int orderid { get; set; }
  }
  public class dm_Character {
    public Guid id { get; set; }
    public string ma { get; set; }
    public string name { get; set; }
    public bool isAct { get; set; }
    public int orderid { get; set; }
  }
  public class data {
    public string fieldName;

    public string value;

    public data(string name, string val) {
      this.fieldName = name;
      this.value = val;
    }

    public data(string line) {
      char[] op = new char[]
      {
                '='
      };
      string[] s = line.Split(op);
      if (s.Length == 1 || s.Length == 0) {
        this.fieldName = null;
        this.value = null;
      }
      else if (s.Length == 2) {
        this.fieldName = s[0];
        this.value = s[1];
      }
      else {
        this.fieldName = s[0];
        this.value = line.Substring(s[0].Length + 1);
      }
    }
  }
  class SQLDatabase {
    #region Fields
    public static string ConnectionString {
      get {
        FileDataDictionary FDD = new FileDataDictionary("setup.con");
        return FDD.getValue("connectionstring");
      }
    }
    #endregion // Fields

    #region dm_batdongbo
    public static List<dm_batdongbo> Loaddm_batdongbo(string sql) {
      SqlConnection cnn = null;
      SqlCommand cmd = null;
      SqlDataReader reader = null;
      dm_batdongbo InfoCOMMANDTABLE;
      List<dm_batdongbo> InfoCOMMANDTABLEs = null;

      try {
        InfoCOMMANDTABLEs = new List<dm_batdongbo>();

        cnn = new SqlConnection();
        cnn.ConnectionString = ConnectionString;
        cnn.Open();
        cnn.FireInfoMessageEventOnUserErrors = false;

        cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.Connection = cnn;
        reader = cmd.ExecuteReader();
        while (reader.Read()) {
          InfoCOMMANDTABLE = new dm_batdongbo();


          if (!reader.IsDBNull(0))
            InfoCOMMANDTABLE.id = reader.GetInt32(0);
          if (!reader.IsDBNull(1))
            InfoCOMMANDTABLE.ma = reader.GetString(1);
          if (!reader.IsDBNull(2))
            InfoCOMMANDTABLE.name = reader.GetString(2);
          if (!reader.IsDBNull(3))
            InfoCOMMANDTABLE.orderid = reader.GetInt32(3);
          if (!reader.IsDBNull(4))
            InfoCOMMANDTABLE.isAct = reader.GetBoolean(4);

          InfoCOMMANDTABLEs.Add(InfoCOMMANDTABLE);
        }
        return InfoCOMMANDTABLEs;
      }
      catch (Exception ex) {
        throw ex;
      }
      finally {
        if (cnn.State == ConnectionState.Open)
          cnn.Close();
      }
    }

    public static bool Adddm_batdongbo(dm_batdongbo record) {
      SqlConnection cnn = null;
      SqlCommand cmd = null;
      object objectID;
      try {
        if (record == null)
          return false;

        cnn = new SqlConnection();
        cnn.ConnectionString = ConnectionString;
        cnn.FireInfoMessageEventOnUserErrors = false;
        cnn.Open();

        cmd = new SqlCommand();
        cmd.Connection = cnn;
        //--- Insert Record
        cmd.CommandText = "Insert into dm_batdongbo( ma, name, orderid, isAct) OUTPUT inserted.id " +
                            "values(  @ma, @name, @orderid, @isAct)";

        cmd.Parameters.AddWithValue("@ma", record.ma);
        cmd.Parameters.AddWithValue("@name", record.name);
        cmd.Parameters.AddWithValue("@orderid", record.orderid);
        cmd.Parameters.AddWithValue("@isAct", record.isAct);
        objectID = cmd.ExecuteScalar();

        if (objectID == null || objectID == DBNull.Value)
          return false;

        record.id = Convert.ToInt32(objectID);
        return true;
      }
      catch (Exception ex) {
        return false;
      }
      finally {
        if (cnn.State == ConnectionState.Open)
          cnn.Close();
      }
    }


    public static bool Updm_batdongbo(dm_batdongbo record) {
      SqlConnection connection = null;
      SqlCommand cmd = null;

      try {
        if (record == null) return false;

        // Make connection to database
        connection = new SqlConnection();
        connection.ConnectionString = ConnectionString;
        connection.FireInfoMessageEventOnUserErrors = false;
        connection.Open();
        // Create command to update GeneralGuessGroup record
        cmd = new SqlCommand();
        cmd.Connection = connection;
        //id, ma, name, size, isKey, IsAct, IsGiaoDien, IsReport, orderid
        cmd.CommandText = "Update dm_batdongbo " +
                            " Set   ma=@ma, " +
                            "       name=@name," +
                            "       orderid=@orderid," +
                            "       IsAct=@IsAct " +
                            " where ID='" + record.id + "'";
        cmd.CommandType = CommandType.Text;

        cmd.Parameters.AddWithValue("@ma", record.ma);
        cmd.Parameters.AddWithValue("@name", record.name);
        cmd.Parameters.AddWithValue("@orderid", record.orderid);
        cmd.Parameters.AddWithValue("@IsAct", record.isAct);

        cmd.ExecuteNonQuery();
        return true;
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "Updm_batdongbo");
        return false;
      }
      finally {
        if (connection != null)
          connection.Close();
      }
    }

    #endregion

    #region Root

    public static List<dm_column> Loaddm_column(string sql) {
      SqlConnection cnn = null;
      SqlCommand cmd = null;
      SqlDataReader reader = null;
      dm_column InfoCOMMANDTABLE;
      List<dm_column> InfoCOMMANDTABLEs = null;

      try {
        InfoCOMMANDTABLEs = new List<dm_column>();

        cnn = new SqlConnection();
        cnn.ConnectionString = ConnectionString;
        cnn.Open();
        cnn.FireInfoMessageEventOnUserErrors = false;

        cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.Connection = cnn;
        reader = cmd.ExecuteReader();
        while (reader.Read()) {
          InfoCOMMANDTABLE = new dm_column();


          if (!reader.IsDBNull(0))
            InfoCOMMANDTABLE.id = reader.GetGuid(0);
          if (!reader.IsDBNull(1))
            InfoCOMMANDTABLE.ma = reader.GetString(1);
          if (!reader.IsDBNull(2))
            InfoCOMMANDTABLE.name = reader.GetString(2);
          if (!reader.IsDBNull(3))
            InfoCOMMANDTABLE.size = reader.GetInt32(3);
          if (!reader.IsDBNull(4))
            InfoCOMMANDTABLE.isKey = reader.GetBoolean(4);
          if (!reader.IsDBNull(5))
            InfoCOMMANDTABLE.isAct = reader.GetBoolean(5);
          if (!reader.IsDBNull(6))
            InfoCOMMANDTABLE.IsReport = reader.GetBoolean(6);
          if (!reader.IsDBNull(7))
            InfoCOMMANDTABLE.isOrder = reader.GetBoolean(7);
          if (!reader.IsDBNull(8))
            InfoCOMMANDTABLE.isSearch = reader.GetBoolean(8);
          if (!reader.IsDBNull(9))
            InfoCOMMANDTABLE.orderid = reader.GetInt32(9);
          InfoCOMMANDTABLEs.Add(InfoCOMMANDTABLE);
        }
        return InfoCOMMANDTABLEs;
      }
      catch (Exception ex) {
        throw ex;
      }
      finally {
        if (cnn.State == ConnectionState.Open)
          cnn.Close();
      }
    }


    public static bool Adddm_column(dm_column record) {
      SqlConnection cnn = null;
      SqlCommand cmd = null;

      object objectID;
      try {
        if (record == null)
          return false;

        cnn = new SqlConnection();
        cnn.ConnectionString = ConnectionString;
        cnn.FireInfoMessageEventOnUserErrors = false;
        cnn.Open();

        cmd = new SqlCommand();
        cmd.Connection = cnn;
        //--- Insert Record
        cmd.CommandText = "Insert into dm_column( ma, name, size, isKey, isAct, IsReport, isSearch, isOrder, orderid) OUTPUT inserted.id " +
                            "values( @ma, @name, @size, @isKey, @isAct, @IsReport, @isSearch, @isOrder, @orderid)";

        cmd.Parameters.AddWithValue("@ma", record.ma);
        cmd.Parameters.AddWithValue("@name", record.name);
        cmd.Parameters.AddWithValue("@size", record.size);
        cmd.Parameters.AddWithValue("@isKey", record.isKey);
        cmd.Parameters.AddWithValue("@IsAct", record.isAct);
        cmd.Parameters.AddWithValue("@IsReport", record.IsReport);
        cmd.Parameters.AddWithValue("@isSearch", record.isSearch);
        cmd.Parameters.AddWithValue("@isOrder", record.isOrder);
        cmd.Parameters.AddWithValue("@orderid", record.orderid);
        Guid guid = (Guid)cmd.ExecuteScalar();

        if (guid == null || guid == Guid.Empty)
          return false;

        record.id = new Guid(guid.ToString());
        return true;
      }
      catch (Exception ex) {
        return false;
      }
      finally {
        if (cnn.State == ConnectionState.Open)
          cnn.Close();
      }
    }

   

    public static bool Updm_column(dm_column record) {
      SqlConnection connection = null;
      SqlCommand cmd = null;

      try {
        if (record == null) return false;

        // Make connection to database
        connection = new SqlConnection();
        connection.ConnectionString = ConnectionString;
        connection.FireInfoMessageEventOnUserErrors = false;
        connection.Open();
        // Create command to update GeneralGuessGroup record
        cmd = new SqlCommand();
        cmd.Connection = connection;
        //id, ma, name, size, isKey, IsAct, IsGiaoDien, IsReport, orderid
        cmd.CommandText = "Update dm_column " +
                            " Set   ma=@ma, " +
                            "       name=@name," +
                            "       size=@size," +
                            "       isKey=@isKey," +
                            "       IsAct=@IsAct," +
                            "       IsReport=@IsReport,"+
                            "       isSearch=@isSearch," +
                            "       isOrder=@isOrder," +
                            "       orderid=@orderid" +
                            " where ID='" + record.id + "'";
        cmd.CommandType = CommandType.Text;

        cmd.Parameters.AddWithValue("@ma", record.ma);
        cmd.Parameters.AddWithValue("@name", record.name);
        cmd.Parameters.AddWithValue("@size", record.size);
        cmd.Parameters.AddWithValue("@isKey", record.isKey);
        cmd.Parameters.AddWithValue("@IsAct", record.isAct);
        cmd.Parameters.AddWithValue("@IsReport", record.IsReport);
        cmd.Parameters.AddWithValue("@isSearch", record.isSearch);
        cmd.Parameters.AddWithValue("@isOrder", record.isOrder);
        cmd.Parameters.AddWithValue("@orderid", record.orderid);

        cmd.ExecuteNonQuery();
        return true;
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "Updatedm_hsct");
        return false;
      }
      finally {
        if (connection != null)
          connection.Close();
      }
    }

    #endregion

    #region cau_hinh

    public static List<cau_hinh> Loadcau_hinh(string sql) {
      SqlConnection cnn = null;
      SqlCommand cmd = null;
      SqlDataReader reader = null;
      cau_hinh InfoCOMMANDTABLE;
      List<cau_hinh> InfoCOMMANDTABLEs = null;

      try {
        InfoCOMMANDTABLEs = new List<cau_hinh>();

        cnn = new SqlConnection();
        cnn.ConnectionString = ConnectionString;
        cnn.Open();
        cnn.FireInfoMessageEventOnUserErrors = false;

        cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.Connection = cnn;
        reader = cmd.ExecuteReader();
        while (reader.Read()) {
          InfoCOMMANDTABLE = new cau_hinh();


          if (!reader.IsDBNull(0))
            InfoCOMMANDTABLE.id = reader.GetGuid(0);
          if (!reader.IsDBNull(1))
            InfoCOMMANDTABLE.DelImportTruocImport = reader.GetBoolean(1);
          if (!reader.IsDBNull(2))
            InfoCOMMANDTABLE.MaxTop = reader.GetInt32(2);
          if (!reader.IsDBNull(3))
            InfoCOMMANDTABLE.IsExportTxt = reader.GetBoolean(3);

          InfoCOMMANDTABLEs.Add(InfoCOMMANDTABLE);
        }
        return InfoCOMMANDTABLEs;
      }
      catch (Exception ex) {
        throw ex;
      }
      finally {
        if (cnn.State == ConnectionState.Open)
          cnn.Close();
      }
    }

    public static bool Upcau_hinh(cau_hinh record) {
      SqlConnection connection = null;
      SqlCommand cmd = null;

      try {
        if (record == null) return false;

        // Make connection to database
        connection = new SqlConnection();
        connection.ConnectionString = ConnectionString;
        connection.FireInfoMessageEventOnUserErrors = false;
        connection.Open();
        // Create command to update GeneralGuessGroup record
        cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "Update cau_hinh " +
                            " Set   DelImportTruocImport=@DelImportTruocImport," +
                            "       MaxTop=@MaxTop," +
                            "       IsExportTxt=@IsExportTxt"+
                            " where ID='" + record.id + "'";
        cmd.CommandType = CommandType.Text;

        cmd.Parameters.AddWithValue("@DelImportTruocImport", record.DelImportTruocImport);
        cmd.Parameters.AddWithValue("@MaxTop", record.MaxTop);
        cmd.Parameters.AddWithValue("@IsExportTxt", record.IsExportTxt);

        cmd.ExecuteNonQuery();
        return true;
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "Upcau_hinh");
        return false;
      }
      finally {
        if (connection != null)
          connection.Close();
      }
    }

    #endregion

    #region dm_Character

    public static List<dm_Character> Loaddm_Character(string sql) {
      SqlConnection cnn = null;
      SqlCommand cmd = null;
      SqlDataReader reader = null;
      dm_Character InfoCOMMANDTABLE;
      List<dm_Character> InfoCOMMANDTABLEs = null;

      try {
        InfoCOMMANDTABLEs = new List<dm_Character>();

        cnn = new SqlConnection();
        cnn.ConnectionString = ConnectionString;
        cnn.Open();
        cnn.FireInfoMessageEventOnUserErrors = false;

        cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.Connection = cnn;
        reader = cmd.ExecuteReader();
        while (reader.Read()) {
          InfoCOMMANDTABLE = new dm_Character();


          if (!reader.IsDBNull(0))
            InfoCOMMANDTABLE.id = reader.GetGuid(0);
          if (!reader.IsDBNull(1))
            InfoCOMMANDTABLE.ma = reader.GetString(1);
          if (!reader.IsDBNull(2))
            InfoCOMMANDTABLE.name = reader.GetString(2);
          if (!reader.IsDBNull(3))
            InfoCOMMANDTABLE.isAct = reader.GetBoolean(3);
          if (!reader.IsDBNull(4))
            InfoCOMMANDTABLE.orderid = reader.GetInt32(4);
          InfoCOMMANDTABLEs.Add(InfoCOMMANDTABLE);
        }
        return InfoCOMMANDTABLEs;
      }
      catch (Exception ex) {
        throw ex;
      }
      finally {
        if (cnn.State == ConnectionState.Open)
          cnn.Close();
      }
    }

    public static bool Updm_Character(dm_Character record) {
      SqlConnection connection = null;
      SqlCommand cmd = null;

      try {
        if (record == null) return false;

        // Make connection to database
        connection = new SqlConnection();
        connection.ConnectionString = ConnectionString;
        connection.FireInfoMessageEventOnUserErrors = false;
        connection.Open();
        // Create command to update GeneralGuessGroup record
        cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "Update dm_Character " +
                            " Set   ma=@ma," +
                            "       name=@name," +
                            "       isAct=@isAct," +
                            "       orderid=@orderid " +
                            " where ID='" + record.id + "'";
        cmd.CommandType = CommandType.Text;

        cmd.Parameters.AddWithValue("@ma", record.ma);
        cmd.Parameters.AddWithValue("@name", record.name);
        cmd.Parameters.AddWithValue("@isAct", record.isAct);
        cmd.Parameters.AddWithValue("@orderid", record.orderid);

        cmd.ExecuteNonQuery();
        return true;
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "Updm_Character");
        return false;
      }
      finally {
        if (connection != null)
          connection.Close();
      }
    }

    public static bool Adddm_Character(dm_Character record) {
      SqlConnection cnn = null;
      SqlCommand cmd = null;
      try {
        if (record == null)
          return false;

        cnn = new SqlConnection();
        cnn.ConnectionString = ConnectionString;
        cnn.FireInfoMessageEventOnUserErrors = false;
        cnn.Open();

        cmd = new SqlCommand();
        cmd.Connection = cnn;
        //--- Insert Record
        cmd.CommandText = "Insert into dm_Character( ma, name, isAct, orderid) OUTPUT inserted.id " +
                            "values( @ma, @name, @isAct, @orderid)";

        cmd.Parameters.AddWithValue("@ma", record.ma);
        cmd.Parameters.AddWithValue("@name", record.name);
        cmd.Parameters.AddWithValue("@isAct", record.isAct);
        cmd.Parameters.AddWithValue("@orderid", record.orderid);

        Guid guid = (Guid)cmd.ExecuteScalar();

        if (guid == null || guid == Guid.Empty)
          return false;

        record.id = new Guid(guid.ToString());
        return true;
      }
      catch (Exception ex) {
        return false;
      }
      finally {
        if (cnn.State == ConnectionState.Open)
          cnn.Close();
      }
    }

    #endregion

    #region Execute SQL

    public static bool ExcNonQuery(string sqlcommand) {
      SqlConnection connection = null;
      SqlCommand command = null;

      try {
        connection = new SqlConnection();
        connection.ConnectionString = ConnectionString;
        connection.Open();
        command = new SqlCommand(sqlcommand, connection);
        command.CommandTimeout = 50000;
        command.ExecuteNonQuery();
        return true;
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "ExcNonQuery");
        return false;
      }
      finally {
        if (connection != null)
          connection.Close();
      }
    }

    public static object ExcScalar(string sqlcommand) {
      SqlConnection connection = null;
      SqlCommand command = null;
      object result = null;

      try {
        connection = new SqlConnection();
        connection.ConnectionString = ConnectionString;
        connection.Open();
        command = new SqlCommand(sqlcommand, connection);
        command.CommandTimeout = 72000;
        result = command.ExecuteScalar();
        return result;
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "ExcScalar");
        return null;
      }
      finally {
        if (connection != null)
          connection.Close();
      }
    }

    public static DataTable ExcDataTable(string sqlcommand) {
      SqlConnection connection = null;
      SqlCommand command = null;
      SqlDataAdapter adp = null;
      DataTable table = null;

      try {
        connection = new SqlConnection();
        connection.ConnectionString = ConnectionString;
        connection.Open();
        command = new SqlCommand(sqlcommand, connection);
        command.CommandTimeout = 72000;
        table = new DataTable();
        adp = new SqlDataAdapter(command);
        adp.Fill(table);
        return table;
      }
      catch (Exception ex) {
        //MessageBox.Show(ex.Message, "ExcDataTable");
        return null;
      }
      finally {
        if (connection != null)
          connection.Close();
      }
    }

    public static bool CheckExist(string sqlcommand) {
      SqlConnection connection = null;
      SqlCommand command = null;
      SqlDataReader reader = null;

      try {
        connection = new SqlConnection();
        connection.ConnectionString = ConnectionString;
        connection.FireInfoMessageEventOnUserErrors = false;
        connection.Open();
        command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = sqlcommand;
        command.CommandType = CommandType.Text;
        reader = command.ExecuteReader();
        if (reader.Read())
          return true;
        else
          return false;
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "CheckExist");
        return false;
      }
      finally {
        if (connection.State == ConnectionState.Open)
          connection.Close();
      }
    }

    #endregion  // Execute SQL

    #region Execute OleDB

    public static DataTable ExcOleDbSchemaTable(string connectionString) {
      OleDbConnection oleConnect = null;
      DataTable table = null;

      try {
        oleConnect = new OleDbConnection();
        oleConnect.ConnectionString = connectionString;
        oleConnect.Open();
        table = oleConnect.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
        return table;
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "ExcOleDbSchemaTable");
        return null;
      }

    }

    public static DataTable ExcOleDbSchemaColumn(string connectionString, string tableName) {
      OleDbConnection oleConnect = null;
      DataTable table = null;

      try {
        oleConnect = new OleDbConnection();
        oleConnect.ConnectionString = connectionString;
        oleConnect.Open();
        table = oleConnect.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tableName, null });
        return table;
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "ExcOleDbSchemaColumn");
        return null;
      }
    }

    public static OleDbDataReader ExcOleReaderDataSource(string connectionString, string tableName, string[] columnNames) {
      OleDbConnection oleConnect = null;
      OleDbCommand oleCommand = null;
      OleDbDataReader oleReader = null;
      string sqlcommand = "Select ";
      string[] getType;

      try {
        getType = connectionString.ToString().Split('.');
        //----- Get string command
        switch (getType[getType.Length - 1]) {
          case "mdb":
          case "MDB":
            for (int i = 0; i < columnNames.Length; i++) {
              if (i == columnNames.Length - 1)
                sqlcommand += "[" + columnNames[i] + "] ";
              else
                sqlcommand += "[" + columnNames[i] + "],";
            }
            sqlcommand += "FROM [" + tableName + "]";
            break;
          case "dbf":
          case "DBF":
            for (int i = 0; i < columnNames.Length; i++) {
              if (i == columnNames.Length - 1)
                sqlcommand += columnNames[i] + " ";
              else
                sqlcommand += columnNames[i] + ",";
            }
            sqlcommand += "FROM [" + tableName + "] Order by " + columnNames[0];
            break;
          default:
            for (int i = 0; i < columnNames.Length; i++) {
              if (i == columnNames.Length - 1)
                sqlcommand += "[" + columnNames[i] + "] ";
              else
                sqlcommand += "[" + columnNames[i] + "],";
            }
            sqlcommand += "FROM [" + tableName + "]";
            break;
        }

        oleConnect = new OleDbConnection(connectionString);
        oleConnect.Open();
        oleCommand = new OleDbCommand(sqlcommand, oleConnect);
        oleReader = oleCommand.ExecuteReader();
        return oleReader;
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "ExcOleReaderDataSource");
        return null;
      }
    }

    public static OleDbDataReader ExcOleReaderDataSource(string connectionString, string tableName, string[] columnNames, string stringWhere) {
      OleDbConnection oleConnect = null;
      OleDbCommand oleCommand = null;
      OleDbDataReader oleReader = null;
      string sqlcommand = "Select ";
      string[] getType;

      try {
        getType = connectionString.ToString().Split('.');
        //----- Get string command
        switch (getType[getType.Length - 1]) {
          case "mdb":
          case "MDB":
            for (int i = 0; i < columnNames.Length; i++) {
              if (i == columnNames.Length - 1)
                sqlcommand += "[" + columnNames[i] + "] ";
              else
                sqlcommand += "[" + columnNames[i] + "],";
            }
            sqlcommand += "FROM [" + tableName + "] Where " + stringWhere;
            break;
          case "dbf":
          case "DBF":
            for (int i = 0; i < columnNames.Length; i++) {
              if (i == columnNames.Length - 1)
                sqlcommand += columnNames[i] + " ";
              else
                sqlcommand += columnNames[i] + ",";
            }
            sqlcommand += "FROM [" + tableName + "] Where " + stringWhere + " Order by " + columnNames[0];
            break;
          default:
            for (int i = 0; i < columnNames.Length; i++) {
              if (i == columnNames.Length - 1)
                sqlcommand += "[" + columnNames[i] + "] ";
              else
                sqlcommand += "[" + columnNames[i] + "],";
            }
            sqlcommand += "FROM [" + tableName + "$] Where " + stringWhere;
            break;
        }

        oleConnect = new OleDbConnection(connectionString);
        oleConnect.Open();
        oleCommand = new OleDbCommand(sqlcommand, oleConnect);
        oleReader = oleCommand.ExecuteReader();
        return oleReader;
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "ExcOleReaderDataSource");
        return null;
      }
    }

    public static OleDbDataReader ExcOleReaderDataSource(string connectionString, string tableName, string[] columnNames, int topRow) {
      OleDbConnection oleConnect = null;
      OleDbCommand oleCommand = null;
      OleDbDataReader oleReader = null;
      string sqlcommand = "Select ";
      string[] getType;

      try {
        getType = connectionString.ToString().Split('.');
        sqlcommand += "Top " + topRow + " ";
        //----- Get string command
        switch (getType[getType.Length - 1]) {
          case "mdb":
          case "MDB":
            for (int i = 0; i < columnNames.Length; i++) {
              if (i == columnNames.Length - 1)
                sqlcommand += "[" + columnNames[i] + "] ";
              else
                sqlcommand += "[" + columnNames[i] + "],";
            }
            sqlcommand += "FROM [" + tableName + "]";
            break;
          case "dbf":
          case "DBF":
            for (int i = 0; i < columnNames.Length; i++) {
              if (i == columnNames.Length - 1)
                sqlcommand += columnNames[i] + " ";
              else
                sqlcommand += columnNames[i] + ",";
            }
            sqlcommand += "FROM [" + tableName + "] Order by " + columnNames[0] + " desc";
            break;
          default:
            for (int i = 0; i < columnNames.Length; i++) {
              if (i == columnNames.Length - 1)
                sqlcommand += "[" + columnNames[i] + "] ";
              else
                sqlcommand += "[" + columnNames[i] + "],";
            }
            sqlcommand += "FROM [" + tableName + "$]";
            break;
        }

        oleConnect = new OleDbConnection(connectionString);
        oleConnect.Open();
        oleCommand = new OleDbCommand(sqlcommand, oleConnect);
        oleReader = oleCommand.ExecuteReader();
        return oleReader;
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "ExcOleReaderDataSource");
        return null;
      }
    }

    public static object ExcOleScalar(string connectionString, string sqlcommand) {
      OleDbConnection oleConnect = null;
      OleDbCommand oleCommand = null;
      object result;

      try {
        oleConnect = new OleDbConnection(connectionString);
        oleConnect.Open();
        oleCommand = new OleDbCommand(sqlcommand, oleConnect);
        result = oleCommand.ExecuteScalar();
        return result;
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "ExcOleScalar");
        return null;
      }
    }

    #endregion  // Execute OleDB
  }
  public class FileDataDictionary {
    private System.Collections.Generic.Dictionary<string, string> ds;

    public FileDataDictionary(string fileName) {
      try {
        this.ds = new System.Collections.Generic.Dictionary<string, string>();
        System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
        while (sr.Peek() != -1) {
          string line = sr.ReadLine();
          if (line.Substring(0, 2) != "//") {
            data d = new data(line);
            if (d.value != null) {
              this.ds.Add(d.fieldName, d.value);
            }
          }
        }
        sr.Close();
      }
      catch (System.Exception e_8D) {
      }
    }

    public string getValue(string name) {
      string result;
      if (this.ds.ContainsKey(name)) {
        result = this.ds[name];
      }
      else {
        result = null;
      }
      return result;
    }

    public bool writeFile(string filePath) {
      if (System.IO.File.Exists(filePath)) {
        System.IO.File.Delete(filePath);
      }
      System.IO.TextWriter writer = new System.IO.StreamWriter(filePath);
      foreach (System.Collections.Generic.KeyValuePair<string, string> pair in this.ds) {
        string line = pair.Key + "=" + pair.Value;
        writer.WriteLine(line);
      }
      writer.Close();
      return false;
    }
  }
}


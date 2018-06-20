using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Used to contain information about the delimited file to be opened
// using the CSV editor application

namespace SchemaSpec
{
    /// <summary>
    /// The item specification class is used to 
    /// define the columns contained within the 
    /// delimited file to be opened.  For each column
    /// we need to know the data type (and I am 
    /// using the jet data types here), the column 
    /// number, the column name, data type, and 
    /// column width (if the file is delimited 
    /// using fixed widths alone) - a list of 
    /// item specification is added to the 
    /// schema definition trailing this class
    /// </summary>
    [Serializable]
    public class ItemSpecification
    {
        // this enumeration is used to 
        // limit the type data property to 
        // a matching jet data type
        public enum JetDataType
        {
            Bit,
            Byte,
            Short,
            Long,
            Currency,
            Single,
            Double,
            DateTime,
            Text,
            Memo
        };

        // the position of the column beginning with 1 to n
        public int ColumnNumber { get; set; }

        // the column name
        public string Name { get; set; }

        // the data type
        public JetDataType TypeData { get; set; }

        // optional column width for fixed width files
        public int ColumnWidth { get; set; }
    }


    /// <summary>
    /// The schema definition class is used to hold the 
    /// contents of the schema.ini file used by the 
    /// connection to open a delimited file (using 
    /// an oledb connection).  The schema dialog is used 
    /// to define a schema definition which is stored as a 
    /// application property
    /// </summary>
    [Serializable]
    public class SchemeDef
    {
        /// <summary>
        /// the constructor will create a default comma delimited 
        /// file definition with an empty list of items specifications 
        /// and will default to set the first row is a header row 
        /// option to false
        /// </summary>
        public SchemeDef()
        {
            DelimiterType = DelimType.CsvDelimited;
            ColumnDefinition = new List<ItemSpecification>();
            UsesHeader = FirstRowHeader.No;
        }

        // this enumeration is used to limit the delimiter types 
        // to one of the four we are interested in which are 
        // comma delimited, tab delimited, custom delimited 
        // (such as a pipe or an underscore), or fixed column 
        // widths
        public enum DelimType
        {
            CsvDelimited,
            TabDelimited,
            CustomDelimited,
            FixedWidth
        };

        // This enum allows the first row is a header 
        // row option to be set to yes or no; that text 
        // is used in the connection string (rather than 
        // true or false)
        public enum FirstRowHeader
        {
            Yes,
            No
        };

        // The properties used to build the schema.ini file include the 
        // delimiter type, a custom delimiter (if used), a list of 
        // column definitions, and a determination as to whether 
        // the first row of the file contains header information rather 
        // than data
        public DelimType DelimiterType { get; set; }
        public string CustomDelimiter { get; set; }
        public List<ItemSpecification> ColumnDefinition { get; set; }
        public FirstRowHeader UsesHeader { get; set; }
    }


}

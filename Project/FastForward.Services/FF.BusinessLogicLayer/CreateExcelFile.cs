//#define INCLUDE_WEB_FUNCTIONS

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.IO;
using FF.DataAccessLayer;
using FF.BusinessObjects;

namespace FF.BusinessLogicLayer
{
    //
    //  November 2013
    //  http://www.mikesknowledgebase.com
    //
    //  Note: if you plan to use this in an ASP.Net application, remember to add a reference to "System.Web", and to uncomment
    //  the "INCLUDE_WEB_FUNCTIONS" definition at the top of this file.
    //
    //  Release history
    //   - Nov 2013: 
    //        Changed "CreateExcelDocument(DataTable dt, string xlsxFilePath)" to remove the DataTable from the DataSet after creating the Excel file.
    //        You can now create an Excel file via a Stream (making it more ASP.Net friendly)
    //   - Jan 2013: Fix: Couldn't open .xlsx files using OLEDB  (was missing "WorkbookStylesPart" part)
    //   - Nov 2012: 
    //        List<>s with Nullable columns weren't be handled properly.
    //        If a value in a numeric column doesn't have any data, don't write anything to the Excel file (previously, it'd write a '0')
    //   - Jul 2012: Fix: Some worksheets weren't exporting their numeric data properly, causing "Excel found unreadable content in '___.xslx'" errors.
    //   - Mar 2012: Fixed issue, where Microsoft.ACE.OLEDB.12.0 wasn't able to connect to the Excel files created using this class.
    //

    public class CreateExcelFile
    {
        public static bool CreateExcelDocument<T>(List<T> list, string xlsxFilePath, out string err)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(ListToDataTable(list));

            return CreateExcelDocument(ds, xlsxFilePath, out err);
        }
        #region HELPER_FUNCTIONS
        //  This function is adapated from: http://www.codeguru.com/forum/showthread.php?t=450171
        //  My thanks to Carl Quirion, for making it "nullable-friendly".
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dt = new DataTable();

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, GetNullableType(info.PropertyType)));
            }
            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    if (!IsNullableType(info.PropertyType))
                        row[info.Name] = info.GetValue(t, null);
                    else
                        row[info.Name] = (info.GetValue(t, null) ?? DBNull.Value);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
        private static Type GetNullableType(Type t)
        {
            Type returnType = t;
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                returnType = Nullable.GetUnderlyingType(t);
            }
            return returnType;
        }
        private static bool IsNullableType(Type type)
        {
            return (type == typeof(string) ||
                    type.IsArray ||
                    (type.IsGenericType &&
                     type.GetGenericTypeDefinition().Equals(typeof(Nullable<>))));
        }

        public static bool CreateExcelDocument(DataTable dt, string xlsxFilePath, out string err)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            bool result = CreateExcelDocument(ds, xlsxFilePath, out err);
            ds.Tables.Remove(dt);
            return result;
        }
        #endregion

#if INCLUDE_WEB_FUNCTIONS
        /// <summary>
        /// Create an Excel file, and write it out to a MemoryStream (rather than directly to a file)
        /// </summary>
        /// <param name="dt">DataTable containing the data to be written to the Excel.</param>
        /// <param name="filename">The filename (without a path) to call the new Excel file.</param>
        /// <param name="Response">HttpResponse of the current page.</param>
        /// <returns>True if it was created succesfully, otherwise false.</returns>
        public static bool CreateExcelDocument(DataTable dt, string filename, System.Web.HttpResponse Response)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                CreateExcelDocumentAsStream(ds, filename, Response);
                ds.Tables.Remove(dt);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }

        public static bool CreateExcelDocument<T>(List<T> list, string filename, System.Web.HttpResponse Response)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(ListToDataTable(list));
                CreateExcelDocumentAsStream(ds, filename, Response);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Create an Excel file, and write it out to a MemoryStream (rather than directly to a file)
        /// </summary>
        /// <param name="ds">DataSet containing the data to be written to the Excel.</param>
        /// <param name="filename">The filename (without a path) to call the new Excel file.</param>
        /// <param name="Response">HttpResponse of the current page.</param>
        /// <returns>Either a MemoryStream, or NULL if something goes wrong.</returns>
        public static bool CreateExcelDocumentAsStream(DataSet ds, string filename, System.Web.HttpResponse Response)
        {
            try
            {
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook, true))
                {
                    WriteExcelFile(ds, document);
                }
                stream.Flush();
                stream.Position = 0;

                Response.ClearContent();
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";

                //  NOTE: If you get an "HttpCacheability does not exist" error on the following line, make sure you have
                //  manually added System.Web to this project's References.

                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                byte[] data1 = new byte[stream.Length];
                stream.Read(data1, 0, data1.Length);
                stream.Close();
                Response.BinaryWrite(data1);
                Response.Flush();
                Response.End();

                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }
#endif      //  End of "INCLUDE_WEB_FUNCTIONS" section

        /// <summary>
        /// Create an Excel file, and write it to a file.
        /// </summary>
        /// <param name="ds">DataSet containing the data to be written to the Excel.</param>
        /// <param name="excelFilename">Name of file to be written.</param>
        /// <returns>True if successful, false if something went wrong.</returns>
        public static bool CreateExcelDocument(DataSet ds, string excelFilename, out string err)
        {
            try
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(excelFilename, SpreadsheetDocumentType.Workbook))
                {
                    WriteExcelFile(ds, document, out err);
                }
                Trace.WriteLine("Successfully created: " + excelFilename);
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message.ToString();
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }

        private static void WriteExcelFile(DataSet ds, SpreadsheetDocument spreadsheet, out string err)
        {
            try
            {
                string _error = string.Empty;
                //  Create the Excel file contents.  This function is used when creating an Excel file either writing 
                //  to a file, or writing to a MemoryStream.
                spreadsheet.AddWorkbookPart();
                spreadsheet.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();

                //  My thanks to James Miera for the following line of code (which prevents crashes in Excel 2010)
                spreadsheet.WorkbookPart.Workbook.Append(new BookViews(new WorkbookView()));

                //  If we don't add a "WorkbookStylesPart", OLEDB will refuse to connect to this .xlsx file !
                WorkbookStylesPart workbookStylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");
                Stylesheet stylesheet = new Stylesheet();
                workbookStylesPart.Stylesheet = stylesheet;

                //  Loop through each of the DataTables in our DataSet, and create a new Excel Worksheet for each.
                uint worksheetNumber = 1;
                foreach (DataTable dt in ds.Tables)
                {
                    //  For each worksheet you want to create
                    string workSheetID = "rId" + worksheetNumber.ToString();
                    string worksheetName = dt.TableName;

                    WorksheetPart newWorksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
                    newWorksheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet();

                    // create sheet data
                    newWorksheetPart.Worksheet.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.SheetData());

                    // save worksheet
                    WriteDataTableToExcelWorksheet(dt, newWorksheetPart, out _error);
                    if (!string.IsNullOrEmpty(_error))
                    {
                        // if "save worksheet" error occured
                        err = _error.ToString();
                        return;
                    }
                    newWorksheetPart.Worksheet.Save();

                    // create the worksheet to workbook relation
                    if (worksheetNumber == 1)
                        spreadsheet.WorkbookPart.Workbook.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheets());

                    spreadsheet.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>().AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheet()
                    {
                        Id = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart),
                        SheetId = (uint)worksheetNumber,
                        Name = dt.TableName
                    });

                    worksheetNumber++;
                }

                spreadsheet.WorkbookPart.Workbook.Save();
                err = string.Empty;
            }
            catch (Exception ex)
            {
                err = ex.Message.ToString();
            }
        }

        //New Method Chamal 24/Jan/2014
        public static bool CreateExcelDocumentDataTable(DataTable dt, string excelFilename, out string err)
        {
            try
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(excelFilename, SpreadsheetDocumentType.Workbook))
                {
                    WriteExcelFile(dt, document, out err);
                }
                Trace.WriteLine("Successfully created: " + excelFilename);
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message.ToString();
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }

        //New Method Chamal 24/Jan/2014
        private static void WriteExcelFile(DataTable dt, SpreadsheetDocument spreadsheet, out string err)
        {
            try
            {
                string _error = string.Empty;
                //  Create the Excel file contents.  This function is used when creating an Excel file either writing 
                //  to a file, or writing to a MemoryStream.
                spreadsheet.AddWorkbookPart();
                spreadsheet.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();

                //  My thanks to James Miera for the following line of code (which prevents crashes in Excel 2010)
                spreadsheet.WorkbookPart.Workbook.Append(new BookViews(new WorkbookView()));

                //  If we don't add a "WorkbookStylesPart", OLEDB will refuse to connect to this .xlsx file !
                WorkbookStylesPart workbookStylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");
                Stylesheet stylesheet = new Stylesheet();
                workbookStylesPart.Stylesheet = stylesheet;

                //  Loop through each of the DataTables in our DataSet, and create a new Excel Worksheet for each.
                uint worksheetNumber = 1;

                //foreach (DataTable dt in ds.Tables)
                //{
                //  For each worksheet you want to create
                string workSheetID = "rId" + worksheetNumber.ToString();
                string worksheetName = dt.TableName;

                WorksheetPart newWorksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
                newWorksheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet();

                // create sheet data
                newWorksheetPart.Worksheet.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.SheetData());

                // save worksheet
                WriteDataTableToExcelWorksheet(dt, newWorksheetPart, out _error);
                if (!string.IsNullOrEmpty(_error))
                {
                    // if "save worksheet" error occured
                    err = _error.ToString();
                    return;
                }
                newWorksheetPart.Worksheet.Save();

                // create the worksheet to workbook relation
                if (worksheetNumber == 1)
                    spreadsheet.WorkbookPart.Workbook.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheets());

                spreadsheet.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>().AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheet()
                {
                    Id = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart),
                    SheetId = (uint)worksheetNumber,
                    Name = dt.TableName
                });

                worksheetNumber++;
                //}

                spreadsheet.WorkbookPart.Workbook.Save();
                err = string.Empty;
            }
            catch (Exception ex)
            {
                err = ex.Message.ToString();
            }
        }

        private static void WriteDataTableToExcelWorksheet(DataTable dt, WorksheetPart worksheetPart, out string err)
        {
            string _errPoint = string.Empty;
            try
            {
                var worksheet = worksheetPart.Worksheet;
                var sheetData = worksheet.GetFirstChild<SheetData>();

                string cellValue = "";

                //  Create a Header Row in our Excel file, containing one header for each Column of data in our DataTable.
                //
                //  We'll also create an array, showing which type each column of data is (Text or Numeric), so when we come to write the actual
                //  cells of data, we'll know if to write Text values or Numeric cell values.
                int numberOfColumns = dt.Columns.Count;
                bool[] IsNumericColumn = new bool[numberOfColumns];

                string[] excelColumnNames = new string[numberOfColumns];
                for (int n = 0; n < numberOfColumns; n++)
                    excelColumnNames[n] = GetExcelColumnName(n);

                //
                //  Create the Header row in our Excel Worksheet
                //
                uint rowIndex = 1;

                var headerRow = new Row { RowIndex = rowIndex };  // add a row at the top of spreadsheet
                sheetData.Append(headerRow);

                for (int colInx = 0; colInx < numberOfColumns; colInx++)
                {
                    DataColumn col = dt.Columns[colInx];
                    AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, headerRow);
                    IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") || (col.DataType.FullName == "System.Int32");
                }

                //
                //  Now, step through each row of data in our DataTable...
                //
                double cellNumericValue = 0;

                GC.Collect();

                foreach (DataRow dr in dt.Rows)
                {
                    // ...create a new row, and append a set of this row's data to it.
                    ++rowIndex;
                    var newExcelRow = new Row { RowIndex = rowIndex };  // add a row at the top of spreadsheet
                    sheetData.Append(newExcelRow);
                    _errPoint = " sheetData.Append(newExcelRow) " + rowIndex.ToString();

                    for (int colInx = 0; colInx < numberOfColumns; colInx++)
                    {
                        cellValue = dr.ItemArray[colInx].ToString();

                        // Create cell with data
                        if (IsNumericColumn[colInx])
                        {
                            //  For numeric cells, make sure our input data IS a number, then write it out to the Excel file.
                            //  If this numeric value is NULL, then don't write anything to the Excel file.
                            cellNumericValue = 0;
                            if (double.TryParse(cellValue, out cellNumericValue))
                            {
                                cellValue = cellNumericValue.ToString();
                                AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow);

                                _errPoint = "AppendNumericCell " + rowIndex.ToString();
                            }
                        }
                        else
                        {
                            //  For text cells, just write the input data straight out to the Excel file.
                            AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow);

                            _errPoint = "AppendTextCell " + rowIndex.ToString();
                        }
                    }

                    Debug.Print(rowIndex.ToString());
                }
                err = string.Empty;
            }
            catch (Exception ex)
            {
                err = _errPoint + " : " + ex.Message.ToString();
            }
        }

        private static void AppendTextCell(string cellReference, string cellStringValue, Row excelRow)
        {
            //  Add a new Excel Cell to our Row 
            Cell cell = new Cell() { CellReference = cellReference, DataType = CellValues.String };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellStringValue;
            cell.Append(cellValue);
            excelRow.Append(cell);

        }

        private static void AppendNumericCell(string cellReference, string cellStringValue, Row excelRow)
        {
            //  Add a new Excel Cell to our Row 
            Cell cell = new Cell() { CellReference = cellReference };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellStringValue;
            cell.Append(cellValue);
            excelRow.Append(cell);
        }

        private static string GetExcelColumnName(int columnIndex)
        {
            //  Convert a zero-based column index into an Excel column reference  (A, B, C.. Y, Y, AA, AB, AC... AY, AZ, B1, B2..)
            //
            //  eg  GetExcelColumnName(0) should return "A"
            //      GetExcelColumnName(1) should return "B"
            //      GetExcelColumnName(25) should return "Z"
            //      GetExcelColumnName(26) should return "AA"
            //      GetExcelColumnName(27) should return "AB"
            //      ..etc..
            //
            if (columnIndex < 26)
                return ((char)('A' + columnIndex)).ToString();

            char firstChar = (char)('A' + (columnIndex / 26) - 1);
            char secondChar = (char)('A' + (columnIndex % 26));

            return string.Format("{0}{1}", firstChar, secondChar);
        }


        //Method 2 for Export Large Datatable to excel 2007 or higher :: Chamal 28-Jan-2013
        //Ref : http://www.codeproject.com/Tips/659666/Export-very-large-data-to-Excel-file

        public static string ExportToExcelxlsx(string _user, string _com, DataTable _dt, int _rowsPerSheet, out string _error)
        {
            try
            {
                InventoryDAL _invDal = new InventoryDAL();
                _invDal.ConnectionOpen();
                MasterCompany mstCompany = _invDal.GetCompByCode(_com);
                _invDal.ConnectionClose();

                string _exportPath = mstCompany.Mc_anal17.ToString();
                if (string.IsNullOrEmpty(_exportPath))
                {
                    _error = "File export path not set!";
                    return string.Empty;
                }

                //int _rowsPerSheet = 50000;
                string _targetFilename = _exportPath + _user + ".xlsx";
                string _errr = string.Empty;
                bool _firstTime = true;
                int i = 0;
                DataTable _resultsDt = new DataTable(_dt.TableName);
                _resultsDt = _dt.Clone();

                foreach (DataRow dtRow in _dt.Rows)
                {
                    _resultsDt.ImportRow(dtRow);
                    i++;
                    if (i == _rowsPerSheet)
                    {
                        i = 0;
                        if (_firstTime == true)
                        {
                            CreateExcelFile.ExportToOxml(_resultsDt, _firstTime, _targetFilename, out _errr);
                            _firstTime = false;
                        }
                        else
                        {
                            CreateExcelFile.ExportToOxml(_resultsDt, _firstTime, _targetFilename, out _errr);
                        }
                        _resultsDt.Clear();
                    }
                }
                if (i > 0)
                {
                    if (_firstTime == true)
                    {
                        CreateExcelFile.ExportToOxml(_resultsDt, _firstTime, _targetFilename, out _errr);
                        _firstTime = false;
                    }
                    else
                    {
                        CreateExcelFile.ExportToOxml(_resultsDt, _firstTime, _targetFilename, out _errr);
                    }
                    _resultsDt.Clear();
                }

                _resultsDt.Dispose();
                _error = _errr;

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                return _targetFilename;
            }
            catch (Exception ex)
            {
                _error = ex.Message.ToString();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                return string.Empty;
            }
        }

        //kapila
        private static void ExportToOxml_multisheet(DataTable TitleData, DataTable ResultsData, bool FirstTime, string FileName, out string Error, string _sheetname, string WithColHeader = "Y")
        {
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";
                //FileName = @"C:\MyExcel.xlsx";
                //Delete the file if it exists. 
                if (FirstTime && File.Exists(FileName))
                {
                    File.Delete(FileName);
                }

                uint sheetId = 1; //Start at the first sheet in the Excel workbook.

                if (FirstTime)
                {
                    //This is the first time of creating the excel file and the first sheet.
                    // Create a spreadsheet document by supplying the filepath.
                    // By default, AutoSave = true, Editable = true, and Type = xlsx.
                    SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.
                        Create(FileName, SpreadsheetDocumentType.Workbook);

                    // Add a WorkbookPart to the document.
                    WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                    workbookpart.Workbook = new Workbook();

                    // Add a WorksheetPart to the WorkbookPart.
                    var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    var sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    var bold1 = new Bold();
                    CellFormat cf = new CellFormat();

                    // Add Sheets to the Workbook.
                    Sheets sheets;
                    sheets = spreadsheetDocument.WorkbookPart.Workbook.
                        AppendChild<Sheets>(new Sheets());

                    // Append a new worksheet and associate it with the workbook.
                    var sheet = new Sheet()
                    {
                        Id = spreadsheetDocument.WorkbookPart.
                            GetIdOfPart(worksheetPart),
                        SheetId = sheetId,
                        Name = _sheetname 
                    };
                    sheets.Append(sheet);

                    //Add Title1 Row.
                    //Lakshan 2016/01/06
                    foreach (DataRow dr in TitleData.Rows)
                    {
                        var titleRow = new Row();
                        foreach (DataColumn column in TitleData.Columns)
                        {
                            var cell = new Cell
                            {
                                DataType = CellValues.String,
                                CellValue = new CellValue(dr[column].ToString())
                            };
                            titleRow.AppendChild(cell);
                        }
                        sheetData.AppendChild(titleRow);
                    }
                    //End

                    //Add Header Row.
                    if (WithColHeader == "Y") //Sanjeewa 2017-03-16
                    {
                        var headerRow = new Row();
                        foreach (DataColumn column in ResultsData.Columns)
                        {
                            var cell = new Cell
                            {
                                DataType = CellValues.String,
                                CellValue = new CellValue(column.ColumnName),
                            };
                            headerRow.AppendChild(cell);
                        }
                        sheetData.AppendChild(headerRow);
                    }

                    foreach (DataRow row in ResultsData.Rows)
                    {
                        var newRow = new Row();
                        foreach (DataColumn col in ResultsData.Columns)
                        {
                            //Added by Prabhath on 8/3/2014
                            if (col.DataType.ToString().Contains("Int") || col.DataType.ToString().Contains("Decimal") || col.DataType.ToString().Contains("Double"))
                            {
                                var cell = new Cell
                                {
                                    DataType = CellValues.Number,
                                    CellValue = new CellValue(row[col].ToString())
                                };

                                newRow.AppendChild(cell);
                            }
                            else if (col.DataType.ToString().Contains("Date"))
                            {//Added by Prabhath on 8/3/2014
                                var cell = new Cell
                                {
                                    DataType = CellValues.Date,
                                    CellValue = new CellValue(Convert.ToString(row[col]))
                                };
                                newRow.AppendChild(cell);
                            }
                            else
                            {
                                //Added by Prabhath on 8/3/2014
                                var cell = new Cell
                                {
                                    DataType = CellValues.String,
                                    CellValue = new CellValue(row[col].ToString())
                                };
                                newRow.AppendChild(cell);
                            }

                        }

                        sheetData.AppendChild(newRow);
                    }
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                else
                {
                    // Open the Excel file that we created before, and start to add sheets to it.
                    var spreadsheetDocument = SpreadsheetDocument.Open(FileName, true);

                    var workbookpart = spreadsheetDocument.WorkbookPart;
                    if (workbookpart.Workbook == null)
                        workbookpart.Workbook = new Workbook();

                    var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    var sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);
                    var sheets = spreadsheetDocument.WorkbookPart.Workbook.Sheets;

                    if (sheets.Elements<Sheet>().Any())
                    {
                        //Set the new sheet id
                        sheetId = sheets.Elements<Sheet>().Max(s => s.SheetId.Value) + 1;
                    }
                    else
                    {
                        sheetId = 1;
                    }

                    // Append a new worksheet and associate it with the workbook.
                    var sheet = new Sheet()
                    {
                        Id = spreadsheetDocument.WorkbookPart.
                            GetIdOfPart(worksheetPart),
                        SheetId = sheetId,
                        Name = _sheetname 
                    };
                    sheets.Append(sheet);

                    //Add Title1 Row.
                    //Lakshan 2016/01/06
                    foreach (DataRow dr in TitleData.Rows)
                    {
                        var titleRow = new Row();
                        foreach (DataColumn column in TitleData.Columns)
                        {
                            var cell = new Cell
                            {
                                DataType = CellValues.String,
                                CellValue = new CellValue(dr[column].ToString())
                            };
                            titleRow.AppendChild(cell);
                        }
                        sheetData.AppendChild(titleRow);
                    }
                    //End

                    //Add the header row here.
                    if (WithColHeader == "Y") //Sanjeewa 2017-03-16
                    {
                        var headerRow = new Row();

                        foreach (DataColumn column in ResultsData.Columns)
                        {
                            var cell = new Cell
                            {
                                DataType = CellValues.String,
                                CellValue = new CellValue(column.ColumnName)
                            };
                            headerRow.AppendChild(cell);
                        }
                        sheetData.AppendChild(headerRow);
                    }

                    foreach (DataRow row in ResultsData.Rows)
                    {
                        var newRow = new Row();

                        foreach (DataColumn col in ResultsData.Columns)
                        {
                            //var cell = new Cell
                            //{
                            //    DataType = CellValues.String,
                            //    CellValue = new CellValue(row[col].ToString())
                            //};
                            //newRow.AppendChild(cell);

                            //Added by Prabhath on 8/3/2014
                            if (col.DataType.ToString().Contains("Int") || col.DataType.ToString().Contains("Decimal"))
                            {
                                var cell = new Cell
                                {
                                    DataType = CellValues.Number,
                                    CellValue = new CellValue(row[col].ToString())
                                };

                                newRow.AppendChild(cell);
                            }
                            else if (col.DataType.ToString().Contains("Date"))
                            {//Added by Prabhath on 8/3/2014
                                var cell = new Cell
                                {
                                    DataType = CellValues.Date,
                                    CellValue = new CellValue(Convert.ToString(row[col]))
                                };
                                newRow.AppendChild(cell);
                            }
                            else
                            {
                                //Added by Prabhath on 8/3/2014
                                var cell = new Cell
                                {
                                    DataType = CellValues.String,
                                    CellValue = new CellValue(row[col].ToString())
                                };
                                newRow.AppendChild(cell);
                            }

                        }

                        sheetData.AppendChild(newRow);
                    }

                    workbookpart.Workbook.Save();
                    // Close the document.
                    spreadsheetDocument.Close();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                Error = string.Empty;
            }
            catch (Exception ex)
            {
                Error = ex.Message.ToString();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        private static void ExportToOxml(DataTable ResultsData, bool FirstTime, string FileName, out string Error)
        {
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";
                //FileName = @"C:\MyExcel.xlsx";
                //Delete the file if it exists. 
                if (FirstTime && File.Exists(FileName))
                {
                    File.Delete(FileName);
                }

                uint sheetId = 1; //Start at the first sheet in the Excel workbook.

                if (FirstTime)
                {
                    //This is the first time of creating the excel file and the first sheet.
                    // Create a spreadsheet document by supplying the filepath.
                    // By default, AutoSave = true, Editable = true, and Type = xlsx.
                    SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.
                        Create(FileName, SpreadsheetDocumentType.Workbook);

                    // Add a WorkbookPart to the document.
                    WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                    workbookpart.Workbook = new Workbook();

                    // Add a WorksheetPart to the WorkbookPart.
                    var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    var sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    var bold1 = new Bold();
                    CellFormat cf = new CellFormat();

                    // Add Sheets to the Workbook.
                    Sheets sheets;
                    sheets = spreadsheetDocument.WorkbookPart.Workbook.
                        AppendChild<Sheets>(new Sheets());

                    // Append a new worksheet and associate it with the workbook.
                    var sheet = new Sheet()
                    {
                        Id = spreadsheetDocument.WorkbookPart.
                            GetIdOfPart(worksheetPart),
                        SheetId = sheetId,
                        Name = "Sheet" + sheetId
                    };
                    sheets.Append(sheet);

                    //Add Header Row.
                    var headerRow = new Row();
                    foreach (DataColumn column in ResultsData.Columns)
                    {
                        var cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(column.ColumnName),
                        };
                        headerRow.AppendChild(cell);
                    }
                    sheetData.AppendChild(headerRow);

                    foreach (DataRow row in ResultsData.Rows)
                    {
                        var newRow = new Row();
                        foreach (DataColumn col in ResultsData.Columns)
                        {
                            //Added by Prabhath on 8/3/2014
                            if (col.DataType.ToString().Contains("Int") || col.DataType.ToString().Contains("Decimal") || col.DataType.ToString().Contains("Double"))
                            {
                                var cell = new Cell
                                {
                                    DataType = CellValues.Number,
                                    CellValue = new CellValue(row[col].ToString())
                                };

                                newRow.AppendChild(cell);
                            }
                            else if (col.DataType.ToString().Contains("Date"))
                            {//Added by Prabhath on 8/3/2014
                                var cell = new Cell
                                {
                                    DataType = CellValues.Date,
                                    CellValue = new CellValue(Convert.ToString(row[col]))
                                };
                                newRow.AppendChild(cell);
                            }
                            else
                            {
                                //Added by Prabhath on 8/3/2014
                                var cell = new Cell
                                {
                                    DataType = CellValues.String,
                                    CellValue = new CellValue(row[col].ToString())
                                };
                                newRow.AppendChild(cell);
                            }

                        }

                        sheetData.AppendChild(newRow);
                    }

                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                else
                {
                    // Open the Excel file that we created before, and start to add sheets to it.
                    var spreadsheetDocument = SpreadsheetDocument.Open(FileName, true);

                    var workbookpart = spreadsheetDocument.WorkbookPart;
                    if (workbookpart.Workbook == null)
                        workbookpart.Workbook = new Workbook();

                    var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    var sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);
                    var sheets = spreadsheetDocument.WorkbookPart.Workbook.Sheets;

                    if (sheets.Elements<Sheet>().Any())
                    {
                        //Set the new sheet id
                        sheetId = sheets.Elements<Sheet>().Max(s => s.SheetId.Value) + 1;
                    }
                    else
                    {
                        sheetId = 1;
                    }

                    // Append a new worksheet and associate it with the workbook.
                    var sheet = new Sheet()
                    {
                        Id = spreadsheetDocument.WorkbookPart.
                            GetIdOfPart(worksheetPart),
                        SheetId = sheetId,
                        Name = "Sheet" + sheetId
                    };
                    sheets.Append(sheet);

                    //Add the header row here.
                    var headerRow = new Row();

                    foreach (DataColumn column in ResultsData.Columns)
                    {
                        var cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(column.ColumnName)
                        };
                        headerRow.AppendChild(cell);
                    }
                    sheetData.AppendChild(headerRow);

                    foreach (DataRow row in ResultsData.Rows)
                    {
                        var newRow = new Row();

                        foreach (DataColumn col in ResultsData.Columns)
                        {
                            //var cell = new Cell
                            //{
                            //    DataType = CellValues.String,
                            //    CellValue = new CellValue(row[col].ToString())
                            //};
                            //newRow.AppendChild(cell);

                            //Added by Prabhath on 8/3/2014
                            if (col.DataType.ToString().Contains("Int") || col.DataType.ToString().Contains("Decimal"))
                            {
                                var cell = new Cell
                                {
                                    DataType = CellValues.Number,
                                    CellValue = new CellValue(row[col].ToString())
                                };

                                newRow.AppendChild(cell);
                            }
                            else if (col.DataType.ToString().Contains("Date"))
                            {//Added by Prabhath on 8/3/2014
                                var cell = new Cell
                                {
                                    DataType = CellValues.Date,
                                    CellValue = new CellValue(Convert.ToString(row[col]))
                                };
                                newRow.AppendChild(cell);
                            }
                            else
                            {
                                //Added by Prabhath on 8/3/2014
                                var cell = new Cell
                                {
                                    DataType = CellValues.String,
                                    CellValue = new CellValue(row[col].ToString())
                                };
                                newRow.AppendChild(cell);
                            }

                        }

                        sheetData.AppendChild(newRow);
                    }

                    workbookpart.Workbook.Save();
                    // Close the document.
                    spreadsheetDocument.Close();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                Error = string.Empty;
            }
            catch (Exception ex)
            {
                Error = ex.Message.ToString();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        //kapila
        public static string ExportToExcelxlsx_multisheet(string _user, string _com, DataTable TitleData, List<DataTable> _dtList, DataTable _dt, int _rowsPerSheet, out string _error, string WithColHeader = "Y")
        {
            try
            {
                InventoryDAL _invDal = new InventoryDAL();
                _invDal.ConnectionOpen();
                MasterCompany mstCompany = _invDal.GetCompByCode(_com);
                _invDal.ConnectionClose();

                string _exportPath = mstCompany.Mc_anal17.ToString();
                if (string.IsNullOrEmpty(_exportPath))
                {
                    _error = "File export path not set!";
                    return string.Empty;
                }

                //int _rowsPerSheet = 50000;
                string _targetFilename = _exportPath + _user + ".xlsx";
                string _errr = string.Empty;
                bool _firstTime = true;
                int i = 0;
                string _sheetName = "";
                DataTable _resultsDt = new DataTable();
                foreach (DataTable dtable in _dtList)
                {
                    if (i == 1) _sheetName = "Scheme Commission";
                    if (i == 2) _sheetName = "Service Charge";
                    if (i == 3) _sheetName = "Additional Parameters";
                    if (i == 4) _sheetName = "Scheme Charges";
                    if (i == 5) _sheetName = "User Defined Schedule";
                    if (i == 6) _sheetName = "Guaranter Parameter";
                    if (i == 7) _sheetName = "Cash Conversion Definition";
                    if (i == 8) _sheetName = "ECD Definition";
                    if (i == 9) _sheetName = "HP Security";
                    if (i == 10) _sheetName = "Proof Document";

                    if (_firstTime == true)
                    {
                        CreateExcelFile.ExportToOxml_multisheet(TitleData, dtable, _firstTime, _targetFilename, out _errr, "Scheme Parameter", WithColHeader);
                        _firstTime = false;
                    }
                    else
                    {
                        CreateExcelFile.ExportToOxml_multisheet(TitleData, dtable, _firstTime, _targetFilename, out _errr, _sheetName, WithColHeader);
                    }
                    i = i + 1;
                }


                _resultsDt.Dispose();
                _error = _errr;

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                return _targetFilename;
            }
            catch (Exception ex)
            {
                _error = ex.Message.ToString();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                return string.Empty;
            }
        }

        //Udesh 12/Oct/2018
        public static string ExportToExcelxlsx_DynamicMultisheet(string _user, string _com, DataTable TitleData, List<DataTable> _dtList, DataTable _dt, int _rowsPerSheet, out string _error, string WithColHeader = "Y")
        {
            try
            {
                InventoryDAL _invDal = new InventoryDAL();
                _invDal.ConnectionOpen();
                MasterCompany mstCompany = _invDal.GetCompByCode(_com);
                _invDal.ConnectionClose();

                string _exportPath = mstCompany.Mc_anal17.ToString();
                if (string.IsNullOrEmpty(_exportPath))
                {
                    _error = "File export path not set!";
                    return string.Empty;
                }

                //int _rowsPerSheet = 50000;
                string _targetFilename = _exportPath + _user + ".xlsx";
                string _errr = string.Empty;
                bool _firstTime = true;
                string _sheetName = "";
                DataTable _resultsDt = new DataTable();
                foreach (DataTable dtable in _dtList)
                {
                    //Apply the name of data table into excel sheet name
                    _sheetName = dtable.TableName;

                    CreateExcelFile.ExportToOxml_multisheet(TitleData, dtable, _firstTime, _targetFilename, out _errr, _sheetName, WithColHeader);
                    if (_firstTime == true)
                    {
                        _firstTime = false;
                    }
                }


                _resultsDt.Dispose();
                _error = _errr;

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                return _targetFilename;
            }
            catch (Exception ex)
            {
                _error = ex.Message.ToString();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                return string.Empty;
            }
        }

        public static string ExportToExcelxlsx(string _user, string _com, DataTable TitleData, DataTable _dt, int _rowsPerSheet, out string _error, string WithColHeader = "Y")
        {
            try
            {
                InventoryDAL _invDal = new InventoryDAL();
                _invDal.ConnectionOpen();
                MasterCompany mstCompany = _invDal.GetCompByCode(_com);
                _invDal.ConnectionClose();

                string _exportPath = mstCompany.Mc_anal17.ToString();
                if (string.IsNullOrEmpty(_exportPath))
                {
                    _error = "File export path not set!";
                    return string.Empty;
                }

                //int _rowsPerSheet = 50000;
                string _targetFilename = _exportPath + _user + ".xlsx";
                string _errr = string.Empty;
                bool _firstTime = true;
                int i = 0;
                DataTable _resultsDt = new DataTable(_dt.TableName);
                _resultsDt = _dt.Clone();

                if (_dt.Rows.Count < 1)
                {
                    CreateExcelFile.ExportToOxml(TitleData, _resultsDt, _firstTime, _targetFilename, out _errr, WithColHeader);//Udesh 15-Oct-2018
                }

                foreach (DataRow dtRow in _dt.Rows)
                {
                    _resultsDt.ImportRow(dtRow);
                    i++;
                    if (i == _rowsPerSheet)
                    {
                        i = 0;
                        if (_firstTime == true)
                        {
                            CreateExcelFile.ExportToOxml(TitleData, _resultsDt, _firstTime, _targetFilename, out _errr, WithColHeader);
                            _firstTime = false;
                        }
                        else
                        {
                            CreateExcelFile.ExportToOxml(TitleData, _resultsDt, _firstTime, _targetFilename, out _errr, WithColHeader);
                        }
                        _resultsDt.Clear();
                    }
                }
                if (i > 0)
                {
                    if (_firstTime == true)
                    {
                        CreateExcelFile.ExportToOxml(TitleData, _resultsDt, _firstTime, _targetFilename, out _errr, WithColHeader);
                        _firstTime = false;
                    }
                    else
                    {
                        CreateExcelFile.ExportToOxml(TitleData, _resultsDt, _firstTime, _targetFilename, out _errr, WithColHeader);
                    }
                    _resultsDt.Clear();
                }

                _resultsDt.Dispose();
                _error = _errr;

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                return _targetFilename;
            }
            catch (Exception ex)
            {
                _error = ex.Message.ToString();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                return string.Empty;
            }
        }

        public static string ExportToExcelxls(string _user, string _com, DataTable TitleData, DataTable _dt, int _rowsPerSheet, out string _error, string WithColHeader = "Y")
        {
            try
            {
                InventoryDAL _invDal = new InventoryDAL();
                _invDal.ConnectionOpen();
                MasterCompany mstCompany = _invDal.GetCompByCode(_com);
                _invDal.ConnectionClose();

                string _exportPath = mstCompany.Mc_anal17.ToString();
                if (string.IsNullOrEmpty(_exportPath))
                {
                    _error = "File export path not set!";
                    return string.Empty;
                }

                //int _rowsPerSheet = 50000;
                string _targetFilename = _exportPath + _user + ".xls";
                string _errr = string.Empty;
                bool _firstTime = true;
                int i = 0;
                DataTable _resultsDt = new DataTable(_dt.TableName);
                _resultsDt = _dt.Clone();

                if (_dt.Rows.Count < 1)
                {
                    CreateExcelFile.ExportToOxml(TitleData, _resultsDt, _firstTime, _targetFilename, out _errr, WithColHeader);//Udesh 15-Oct-2018
                }

                foreach (DataRow dtRow in _dt.Rows)
                {
                    _resultsDt.ImportRow(dtRow);
                    i++;
                    if (i == _rowsPerSheet)
                    {
                        i = 0;
                        if (_firstTime == true)
                        {
                            CreateExcelFile.ExportToOxml(TitleData, _resultsDt, _firstTime, _targetFilename, out _errr, WithColHeader);
                            _firstTime = false;
                        }
                        else
                        {
                            CreateExcelFile.ExportToOxml(TitleData, _resultsDt, _firstTime, _targetFilename, out _errr, WithColHeader);
                        }
                        _resultsDt.Clear();
                    }
                }
                if (i > 0)
                {
                    if (_firstTime == true)
                    {
                        CreateExcelFile.ExportToOxml(TitleData, _resultsDt, _firstTime, _targetFilename, out _errr, WithColHeader);
                        _firstTime = false;
                    }
                    else
                    {
                        CreateExcelFile.ExportToOxml(TitleData, _resultsDt, _firstTime, _targetFilename, out _errr, WithColHeader);
                    }
                    _resultsDt.Clear();
                }

                _resultsDt.Dispose();
                _error = _errr;

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                return _targetFilename;
            }
            catch (Exception ex)
            {
                _error = ex.Message.ToString();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                return string.Empty;
            }
        }
        private static void ExportToOxml(DataTable TitleData, DataTable ResultsData, bool FirstTime, string FileName, out string Error, string WithColHeader = "Y")
        {
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";
                //FileName = @"C:\MyExcel.xlsx";
                //Delete the file if it exists. 
                if (FirstTime && File.Exists(FileName))
                {
                    File.Delete(FileName);
                }

                uint sheetId = 1; //Start at the first sheet in the Excel workbook.

                if (FirstTime)
                {
                    //This is the first time of creating the excel file and the first sheet.
                    // Create a spreadsheet document by supplying the filepath.
                    // By default, AutoSave = true, Editable = true, and Type = xlsx.
                    SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.
                        Create(FileName, SpreadsheetDocumentType.Workbook);

                    // Add a WorkbookPart to the document.
                    WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                    workbookpart.Workbook = new Workbook();

                    // Add a WorksheetPart to the WorkbookPart.
                    var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    var sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    var bold1 = new Bold();
                    CellFormat cf = new CellFormat();

                    // Add Sheets to the Workbook.
                    Sheets sheets;
                    sheets = spreadsheetDocument.WorkbookPart.Workbook.
                        AppendChild<Sheets>(new Sheets());

                    // Append a new worksheet and associate it with the workbook.
                    var sheet = new Sheet()
                    {
                        Id = spreadsheetDocument.WorkbookPart.
                            GetIdOfPart(worksheetPart),
                        SheetId = sheetId,
                        Name = "Sheet" + sheetId
                    };
                    sheets.Append(sheet);

                    //Add Title1 Row.
                    //Lakshan 2016/01/06
                    foreach (DataRow dr in TitleData.Rows)
                    {
                        var titleRow = new Row();
                        foreach (DataColumn column in TitleData.Columns)
                        {
                            var cell = new Cell
                            {
                                DataType = CellValues.String,
                                CellValue = new CellValue(dr[column].ToString())
                            };
                            titleRow.AppendChild(cell);
                        }
                        sheetData.AppendChild(titleRow);
                    }
                    //End

                    //Add Header Row.
                    if (WithColHeader == "Y") //Sanjeewa 2017-03-16
                    {
                        var headerRow = new Row();
                        foreach (DataColumn column in ResultsData.Columns)
                        {
                            var cell = new Cell
                            {
                                DataType = CellValues.String,
                                CellValue = new CellValue(column.ColumnName),
                            };
                            headerRow.AppendChild(cell);
                        }
                        sheetData.AppendChild(headerRow);
                    }

                    foreach (DataRow row in ResultsData.Rows)
                    {
                        var newRow = new Row();
                        foreach (DataColumn col in ResultsData.Columns)
                        {
                            //Added by Prabhath on 8/3/2014
                            if (col.DataType.ToString().Contains("Int") || col.DataType.ToString().Contains("Decimal") || col.DataType.ToString().Contains("Double"))
                            {
                                var cell = new Cell
                                {
                                    DataType = CellValues.Number,
                                    CellValue = new CellValue(row[col].ToString())
                                };

                                newRow.AppendChild(cell);
                            }
                            else if (col.DataType.ToString().Contains("Date"))
                            {//Added by Prabhath on 8/3/2014
                                var cell = new Cell
                                {
                                    DataType = CellValues.Date,
                                    CellValue = new CellValue(Convert.ToString(row[col]))
                                };
                                newRow.AppendChild(cell);
                            }
                            else
                            {
                                //Added by Prabhath on 8/3/2014
                                var cell = new Cell
                                {
                                    DataType = CellValues.String,
                                    CellValue = new CellValue(row[col].ToString())
                                };
                                newRow.AppendChild(cell);
                            }

                        }

                        sheetData.AppendChild(newRow);
                    }
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                else
                {
                    // Open the Excel file that we created before, and start to add sheets to it.
                    var spreadsheetDocument = SpreadsheetDocument.Open(FileName, true);

                    var workbookpart = spreadsheetDocument.WorkbookPart;
                    if (workbookpart.Workbook == null)
                        workbookpart.Workbook = new Workbook();

                    var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    var sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);
                    var sheets = spreadsheetDocument.WorkbookPart.Workbook.Sheets;

                    if (sheets.Elements<Sheet>().Any())
                    {
                        //Set the new sheet id
                        sheetId = sheets.Elements<Sheet>().Max(s => s.SheetId.Value) + 1;
                    }
                    else
                    {
                        sheetId = 1;
                    }

                    // Append a new worksheet and associate it with the workbook.
                    var sheet = new Sheet()
                    {
                        Id = spreadsheetDocument.WorkbookPart.
                            GetIdOfPart(worksheetPart),
                        SheetId = sheetId,
                        Name = "Sheet" + sheetId
                    };
                    sheets.Append(sheet);

                    //Add Title1 Row.
                    //Lakshan 2016/01/06
                    foreach (DataRow dr in TitleData.Rows)
                    {
                        var titleRow = new Row();
                        foreach (DataColumn column in TitleData.Columns)
                        {
                            var cell = new Cell
                            {
                                DataType = CellValues.String,
                                CellValue = new CellValue(dr[column].ToString())
                            };
                            titleRow.AppendChild(cell);
                        }
                        sheetData.AppendChild(titleRow);
                    }
                    //End

                    //Add the header row here.
                    if (WithColHeader == "Y") //Sanjeewa 2017-03-16
                    {
                        var headerRow = new Row();

                        foreach (DataColumn column in ResultsData.Columns)
                        {
                            var cell = new Cell
                            {
                                DataType = CellValues.String,
                                CellValue = new CellValue(column.ColumnName)
                            };
                            headerRow.AppendChild(cell);
                        }
                        sheetData.AppendChild(headerRow);
                    }

                    foreach (DataRow row in ResultsData.Rows)
                    {
                        var newRow = new Row();

                        foreach (DataColumn col in ResultsData.Columns)
                        {
                            //var cell = new Cell
                            //{
                            //    DataType = CellValues.String,
                            //    CellValue = new CellValue(row[col].ToString())
                            //};
                            //newRow.AppendChild(cell);

                            //Added by Prabhath on 8/3/2014
                            if (col.DataType.ToString().Contains("Int") || col.DataType.ToString().Contains("Decimal"))
                            {
                                var cell = new Cell
                                {
                                    DataType = CellValues.Number,
                                    CellValue = new CellValue(row[col].ToString())
                                };

                                newRow.AppendChild(cell);
                            }
                            else if (col.DataType.ToString().Contains("Date"))
                            {//Added by Prabhath on 8/3/2014
                                var cell = new Cell
                                {
                                    DataType = CellValues.Date,
                                    CellValue = new CellValue(Convert.ToString(row[col]))
                                };
                                newRow.AppendChild(cell);
                            }
                            else
                            {
                                //Added by Prabhath on 8/3/2014
                                var cell = new Cell
                                {
                                    DataType = CellValues.String,
                                    CellValue = new CellValue(row[col].ToString())
                                };
                                newRow.AppendChild(cell);
                            }

                        }

                        sheetData.AppendChild(newRow);
                    }

                    workbookpart.Workbook.Save();
                    // Close the document.
                    spreadsheetDocument.Close();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                Error = string.Empty;
            }
            catch (Exception ex)
            {
                Error = ex.Message.ToString();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }
    }
}

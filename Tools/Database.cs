using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Tools;

namespace Tools
{
    class Convert_ExcelFiles
    {
        public static int WorkSheet_To_DataTable(string filePath, string worksheetName, ref DataTable dt)
        {
            int retryCount = 0;
        retryWorkSheet_To_DataTable:
            try
            {

                using XLWorkbook workBook = new XLWorkbook(filePath);
                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(worksheetName);

                //Loop through the Worksheet rows.
                bool firstRow = true;
                foreach (IXLRow row in workSheet.Rows())
                {
                    //Use the first row to add columns to DataTable.
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                            if (dt.Columns[cell.Value.ToString()] != dt.Columns[cell.Address.ColumnNumber - 1])
                            {
                                Console.WriteLine($"WorkSheet_To_DataTable error: Added column {cell.Value} was not column number {cell.Address.ColumnNumber - 1}");
                            }
                        }
                        firstRow = false;
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();
                        foreach (IXLCell cell in row.Cells())
                        {
                            // "^1" == "dt.Rows.Count - 1"
                            dt.Rows[^1][cell.Address.ColumnNumber - 1] = cell.Value.ToString();
                        }
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                return -1;
            }
            catch (System.ArgumentException ex)
            {
                if (ex.Message == $"There isn't a worksheet named '{worksheetName}'.")
                {
                    Console.WriteLine($"No {worksheetName} worksheet found");
                    return 0;
                }
                MyConsole.WriteLine(ConsoleColor.Red, $"Error opening {filePath}\nMessage: {ex.Message}");
                return -2;
            }
            catch (System.IO.IOException ex)
            {
                if (retryCount++ == 0)
                {
                    MyConsole.WriteLine(ConsoleColor.Red, $"Error opening {filePath}\nMessage: {ex.Message}\n" +
                        "(Retry 1 time before returning error)\nPress any key to retry...");
                    Console.ReadKey();
                    goto retryWorkSheet_To_DataTable;
                }
                throw;
            }
            return 1;
        }
        public static bool DataTable_To_Worksheet(string dirAndFileName, string tableName, DataTable dataTable, bool overwriteExistingWorksheet = false)
        {
            try
            {
                //Trying to modify existing document in case an worksheet was previously added (don't want to lose that data)
                var workbook = new XLWorkbook(dirAndFileName);

                if (workbook.Worksheets.TryGetWorksheet(tableName, out IXLWorksheet worksheet))
                {
                    //worksheet already exists
                    if (overwriteExistingWorksheet) workbook.Worksheets.Delete(tableName);
                    else return false;
                }
                worksheet = workbook.Worksheets.Add(dataTable, tableName);

                worksheet.Columns().AdjustToContents();
                foreach (var column in worksheet.Columns())
                {
                    if (column.Width > 40) column.Width = 40;
                }

                workbook.Save();

            }
            //File doesn't exist, create it:
            catch (System.IO.FileNotFoundException)
            {
                var workbook = new XLWorkbook();
                workbook.Worksheets.Add(dataTable, tableName);
                workbook.SaveAs(dirAndFileName);
            }
            catch (Exception ex)
            {
                MyConsole.WriteLine(ConsoleColor.Red, $"Failed to save file with exception: {ex.Message}");
                Console.ReadKey();
            }
            return true;
        }
    }
}

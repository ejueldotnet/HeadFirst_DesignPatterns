using ClosedXML.Excel;
using System;
using System.Data;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Data.SqlClient;
using HeadFirst_DesignPatterns;

namespace Tools
{
    class Sql
    {
        private static void ValidateParameter(string parameter)
        {

            if (string.IsNullOrEmpty(parameter))
            {
                throw new ArgumentNullException(nameof(parameter));
            }
        }
        public static void RunQuery_SqlQuery(string sqlQuery, ref DataTable results, string overrideConnectionString_Mvp = "")
        {
            //ToDo: Test and add support for array of results (multiple select statements in single query)
            ValidateParameter(sqlQuery);

            DateTime start = DateTime.Now;
            try
            {
                string connectionString = "";
                if (overrideConnectionString_Mvp.Length > 0)
                {
                    connectionString = overrideConnectionString_Mvp;
                }
                else
                {
                    connectionString = AppSettings.connectionString_SQL;
                }
                MyConsole.WriteLine(ConsoleColor.Green, $"Running SQL db query...");
                using SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(results);
                da.Dispose();
                connection.Close();
            }
            catch (SqlException e)
            {
                TimeSpan span = DateTime.Now - start;
                MyConsole.WriteLine(ConsoleColor.Red, $"***** Query ran for {span.Seconds} seconds before throwing exception\n\n");
                //Console.WriteLine(e.ToString());
                //Console.ReadKey();
                throw e;
            }
        }
        /// <summary>
        /// Used to perform insert/update/delete purely via SQL query (no parameters). Uses transaction
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="overrideConnectionString"></param>
        /// <param name="minRowsAffected"></param>
        /// <param name="maxRowsAffected"></param>
        /// <returns></returns>
        public static bool RunQuery_SqlTransaction(string sqlQuery, string overrideConnectionString = "", int minRowsAffected = 0, int maxRowsAffected = 0)
        {
#warning untested function: RunQuery_SqlTransaction
//ToDo: build tests
            ValidateParameter(sqlQuery);

            bool isTransactionSuccessful = false;
            string connectionString = !string.IsNullOrEmpty(overrideConnectionString) ? overrideConnectionString: AppSettings.connectionString_SQL;
            

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            SqlTransaction transaction;

            // Start a local transaction.
            transaction = connection.BeginTransaction("RunQuery_SqlTransaction");

            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            command.Connection = connection;
            command.Transaction = transaction;

            try
            {
                command.CommandText = sqlQuery;
                int rowsAffected = command.ExecuteNonQuery();

                //ToDo: SqlTransaction - Confirm number of rows impacted are in an expected range before committing transaction
#warning Test rollback of transactions
                if(maxRowsAffected > 0 && 
                    (rowsAffected < minRowsAffected || rowsAffected > maxRowsAffected))
                {
                    MyConsole.WriteLine($"Expected rows affected between {minRowsAffected} and {maxRowsAffected} (actual was {rowsAffected})");
                    Rollback(transaction);
                }
                else
                {
                    // Attempt to commit the transaction.
                    transaction.Commit();
                    Console.WriteLine("Transaction successfully committed");
                    isTransactionSuccessful = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);

                Rollback(transaction);
            }
            //ToDo: Test what happens if transaction is never rolled back
            //Idea: Begin transaction before executing code, then commit transaction after code completes (might cause issues if multi-threading). Would require passing in the 
            return isTransactionSuccessful;
        }

        private static void Rollback(SqlTransaction transaction)
        {
            MyConsole.WriteLine("Attempt to roll back the transaction");
            try
            {
                transaction.Rollback();
            }
            catch (Exception ex2)
            {
                // This catch block will handle any errors that may have occurred
                // on the server that would cause the rollback to fail, such as
                // a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                Console.WriteLine("  Message: {0}", ex2.Message);
            }
        }

        public static void RunQuery_CdsSqlQuery_UsingClientIdAndSecret(string sqlQuery, ref DataTable results, string pClientId = "", string pClientSecretKey = "", string pSqlConnectionString = "", string pResourceId = "")
        {
            ValidateParameter(sqlQuery);
            string clientId = !string.IsNullOrEmpty(pClientId) ? pClientId : AppSettings.dataverse_appId;
            string clientSecretKey = !string.IsNullOrEmpty(pClientSecretKey) ? pClientSecretKey : AppSettings.dataverse_appSecretKey;
            string sqlConnectionString = !string.IsNullOrEmpty(pSqlConnectionString) ? pSqlConnectionString : AppSettings.connectionString_Dataverse;
            string resourceId = !string.IsNullOrEmpty(pResourceId) ? pResourceId : AppSettings.dataverse_debugCrmResourceId;

            string AadInstance = "https://login.microsoftonline.com/{0}";

            AuthenticationContext authenticationContext = new AuthenticationContext(string.Format(AadInstance, AppSettings.dataverse_aadTenantId));
            ClientCredential clientCredential = new ClientCredential(clientId, clientSecretKey);

            AuthenticationResult authenticationResult = authenticationContext.AcquireTokenAsync(resourceId, clientCredential).Result;

            MyConsole.WriteLine(ConsoleColor.Green, $"Running Dataverse query...");

            var conn = new SqlConnection(sqlConnectionString)
            {
                AccessToken = authenticationResult.AccessToken
            };
            SqlCommand command = new SqlCommand(sqlQuery, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            results.Load(reader);
        }
    }
    class ExcelFiles
    {
        public static int WorkSheet_To_DataTable(string filePath, string worksheetName, ref DataTable dt, bool promptRetry = false)
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
                if (promptRetry && retryCount++ == 0)
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

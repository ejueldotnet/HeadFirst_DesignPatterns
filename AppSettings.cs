using System;
using System.Collections.Generic;
using System.Text;

namespace HeadFirst_DesignPatterns
{
    class AppSettings
    {
        public static string[] defaultOption_Main = {"0"};
        public static string[] defaultOption_Ch1 = {"0"};

        public static readonly string dataverse_appId = "";
        public static readonly string dataverse_appSecretKey = "";
        public static readonly string dataverse_aadTenantId = "";
        private static readonly string dataverse_prod_environment = "";
        private static readonly string dataverse_test_environment = "";
        public static readonly string connectionString_Dataverse = $"Data Source={dataverse_prod_environment}.crm.dynamics.com,5558;Persist Security Info=False;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False";
        public static readonly string dataverse_debugCrmResourceId = $"{dataverse_test_environment}.crm.dynamics.com";

        private static readonly string SQL_url = "";
        private static readonly int SQL_port = 1433;
        private static readonly string SQL_Catalog = "";
        private static readonly string SQL_UserId = "";
        private static readonly string SQL_UserPassword = "";
        private static readonly string SQL_ApplicationName = "";
        public static readonly string connectionString_SQL = $"Server=tcp:{SQL_url},{SQL_port};Initial Catalog = {SQL_Catalog}; Persist Security Info=False;User ID = {SQL_UserId}; Password={SQL_UserPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout = 60; Application Name = \"{SQL_ApplicationName}\";";

    }
}

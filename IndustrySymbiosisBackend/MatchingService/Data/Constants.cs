using System;
namespace DataManagementService.Data
{
    public class Constants
    {
        public const string DB_CONNECTION_STRING = "Data Source=tcp:insym.database.windows.net,1433;Initial Catalog=Industrie-Symbiose_db;User Id=insym_adm@insym;Password=jio:90u?..Q++";

        public const string PRODUCTION_LINE_TABLE_NAME = "production_line";
        public const string PRODUCTION_LINE_COLUMN_NAME = "name";
        public const string PRODUCTION_LINE_COLUMN_PRODUCTION_FACILITY = "fk_production_facility";

    }
}

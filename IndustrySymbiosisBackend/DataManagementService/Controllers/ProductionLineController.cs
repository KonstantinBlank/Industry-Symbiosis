using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DataManagementService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductionLineController : ControllerBase
    {

        /// <summary>
        /// get all Production Lines by enterpriseID
        /// </summary>
        /// <returns>
        /// json with all Production Lines  with Production Line attributes from an Enterprise
        /// </returns>
        [HttpGet("productionlines/{enterpriseID}")]
        public ActionResult GetProductionLinesByEnterpriseID(string enterpriseID)
        {
            string JSONString = string.Empty;


            try
            {
                string connectionString;
                connectionString = "Data Source=tcp:insym.database.windows.net,1433;Initial Catalog=Industrie-Symbiose_db;User Id=insym_adm@insym;Password=jio:90u?..Q++";

                using (SqlConnection connection = new SqlConnection(connectionString)) //auto close connection with using
                {
                    string queryString;
                    queryString = @$"SELECT enterprise_user.first_name, enterprise_user.surname, enterprise_user.email
                                    FROM enterprise_user
                                    WHERE enterprise_user.fk_enterprise = {enterpriseID};";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Connection.Open();
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(dataReader);
                            JSONString = JsonConvert.SerializeObject(dataTable);
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(JSONString);
    }
    
    
        //set production facility by ProductionFacilityID
    
    
    
    }
}


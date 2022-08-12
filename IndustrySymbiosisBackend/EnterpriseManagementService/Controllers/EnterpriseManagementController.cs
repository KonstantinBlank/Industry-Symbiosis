using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EnterpriseManagementService.Controllers
{
    [Route("api/[controller]")] // api/enterprisemanagement
    [ApiController]
    public class EnterpriseManagementController : ControllerBase
    {
        /// <summary>
        /// get all enterprises
        /// </summary>
        /// <returns></returns>
        [HttpGet("enterprises")] // api/enterprisemanagement/enterprises
        public ActionResult GetEnterprises()
        {
            string JSONString = string.Empty;

            try
            {
                string connectionString;
                connectionString = "Data Source=tcp:insym.database.windows.net,1433;Initial Catalog=Industrie-Symbiose_db;User Id=insym_adm@insym;Password=jio:90u?..Q++";

                using (SqlConnection connection = new SqlConnection(connectionString)) //auto close connection with using
                {
                    string queryString;
                    queryString = @"SELECT 
                                    enterprise.id, enterprise.name, enterprise_address.address_record_1, enterprise_address.address_record_2, enterprise_address.street, enterprise_address.house_number, enterprise_address.postcode, enterprise_address.city
                                    FROM enterprise
                                    LEFT JOIN enterprise_address
                                    ON enterprise.fk_address = enterprise_address.id;";

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

        /// <summary>
        /// get all users by enterpriseID
        /// </summary>
        /// <returns></returns>
        [HttpGet("user/{enterpriseID}")] // api//user/{enterpriseID}
        public ActionResult GetUsersByEnterpriseID(string enterpriseID)
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

    }
}

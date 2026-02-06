using Microsoft.AspNetCore.Mvc;
using System;
using EnterpriseManagement.Services;


namespace EnterpriseManagement.Controllers
{
    [Route("api/enterprises/")]
    [ApiController]
    public class EnterpriseController : ControllerBase
    {

        private EnterpriseService _enterpriseService = new EnterpriseService();

        /// <summary>
        /// get all enterprises
        /// </summary>
        /// <returns>
        /// json with all enterprises with address
        /// </returns>
        [HttpGet("get/all")]
        public ActionResult GetAll()
        {
            string enterprises = _enterpriseService.GetAll();

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(enterprises);
        }

        [HttpGet("get/{enterpriseId}")]
        public ActionResult GetById(int enterpriseId)
        {
            string enterprise = _enterpriseService.GetById(enterpriseId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(enterprise);
        }

        [HttpPost("create/")]
        public IActionResult Create(string name, string postAddressRecord1, string postAddressRecord2, string street, string houseNumber, string postcode, string city)
        {
            string enterprise = _enterpriseService.Create(name, postAddressRecord1, postAddressRecord2, street, houseNumber, postcode, city);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(enterprise);
        }

        [HttpPost("update/")]
        public IActionResult Update(int enterpriseId, int addressId, string? name = null, string? postAddressRecord1 = null, string? postAddressRecord2 = null, string? street = null, string? houseNumber = null, string? postcode = null, string? city = null)
        {
            int updatedRows = _enterpriseService.Update(enterpriseId, addressId, name, postAddressRecord1, postAddressRecord2, street, houseNumber, postcode, city);
            
            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(updatedRows);
        }
    }
}

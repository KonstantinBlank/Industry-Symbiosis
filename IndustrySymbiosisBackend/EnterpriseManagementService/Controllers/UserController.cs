using Microsoft.AspNetCore.Mvc;
using System;
using EnterpriseManagement.Services;

namespace EnterpriseManagement.Controllers
{
    [Route("api/users/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _userService = new UserService();


        [HttpGet("get/{userId}")]
        public ActionResult GetById(int userId)
        {
            string user = _userService.GetById(userId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(user);
        }


        [HttpGet("get/enterprise/{enterpriseID}")]
        public ActionResult GetUsersFromEnterprise(int enterpriseId)
        {
            string users = _userService.GetAllFromEnterprise(enterpriseId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(users);
        }

        [HttpPost("create/")]
        public IActionResult Create(int enterpriseId, string firstName, string surname, string email)
        {
            string user = _userService.Create(enterpriseId, firstName, surname, email);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(user);
        }

        [HttpPost("update/")]
        public IActionResult Update(int userId, string? firstName, string? surname, string? email)
        {
            int updatedRows = _userService.Update(userId, firstName, surname, email);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(updatedRows);
        }
    }
}

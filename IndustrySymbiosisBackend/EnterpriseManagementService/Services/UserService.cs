using System;
using DataManagementService.Services;
using EnterpriseManagement.Interfaces;
using EnterpriseManagementService.Data;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EnterpriseManagement.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {
        }

        public string Create(int enterpriseId, string firstName, string surname, string email)
        {
            User user = new User(enterpriseId, firstName, surname, email);

            string query = @$"INSERT INTO enterprise_user (first_name, surname, email, fk_enterprise)
                              VALUES (@firstname, @surname, @email, @fk_enterprise)
                              SELECT SCOPE_IDENTITY()";
            IDictionary<string, object> parameterPairs = new Dictionary<string, object>();
            parameterPairs.Add("firstname", user.FirstName);
            parameterPairs.Add("surname", user.Surname);
            parameterPairs.Add("email", user.Email);
            parameterPairs.Add("fk_enterprise", user.EnterpriseId);

            int userId = SqlConnectionHelper.CreateEntry(query, parameterPairs);
            user.SetId(userId);

            Console.WriteLine("user entry was successfully created!");

            return JsonConvert.SerializeObject(user);
        }

        public string GetById(int userId)
        {
            string query = @$"SELECT id, first_name, surname, email, fk_enterprise as enterprise
                              FROM enterprise_user
                              WHERE id = {userId};";

            string user = SqlConnectionHelper.GetTable(query);

            return user;
        }

        public string GetAllFromEnterprise(int enterpriseId)
        {
            string query = @$"SELECT id, first_name, surname, email, fk_enterprise as enterprise
                                 FROM enterprise_user
                                 WHERE fk_enterprise = {enterpriseId};";

            string users = SqlConnectionHelper.GetTable(query);

            return users;
        }

        public int Update(int userId, string? firstName, string? surname, string? email)
        {
            // user cannot change the enterprise
            User user = new User(userId, -1, firstName, surname, email);

            IDictionary<string, object?> parameterPairs = new Dictionary<string, object?>();
            parameterPairs.Add(new KeyValuePair<string, object?>("first_name", user.FirstName));
            parameterPairs.Add(new KeyValuePair<string, object?>("surname", user.Surname));
            parameterPairs.Add(new KeyValuePair<string, object?>("email", user.Email));

            int result = SqlConnectionHelper.UpdateEntry("enterprise_user", user.Id.ToString(), parameterPairs);

            return result;
        }
    }
}

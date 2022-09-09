using System;
namespace EnterpriseManagement.Interfaces
{
    public interface IUserService
    {
        public string GetAllFromEnterprise(int enterpriseId);
        public string GetById(int userId);
        public string Create(int enterpriseId, string firstName, string surname, string email);
        public int Update(int UserId, string? firstName, string? surname, string? email);
    }
}


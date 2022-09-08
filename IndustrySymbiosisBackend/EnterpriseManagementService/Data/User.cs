using System;
namespace EnterpriseManagementService.Data
{
    public class User
    {
        public int Id { get; private set; }
        public string? FirstName { get; private set; }
        public string? Surname { get; private set; }
        public string? Email { get; private set; }
        public int? EnterpriseId { get; private set; }

        public User(int enterpriseId, string firstName, string surname, string email)
        {
            Id = -1;
            EnterpriseId = enterpriseId;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            Surname = surname ?? throw new ArgumentNullException(nameof(surname));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        public User(int id, int? enterpriseId, string? firstName, string? surname, string? email)
        {
            Id = id;
            EnterpriseId = enterpriseId;
            FirstName = firstName;
            Surname = surname;
            Email = email;
        }

        public void SetId(int id)
        {
            if (Id == -1)
            {
                Id = id;
            }
            else
            {
                throw new InvalidOperationException($"The Id for the User was already set. It's {Id}.");
            }
        }
    }
}


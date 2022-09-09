using System;

namespace DataManagementService.Data
{
    public class EnergySource : IQueryObject
    {
        public int Id { get; private set; }
        public string? Name { get; private set; }
        public float? RenewableShare { get; private set; }
        public bool? IsIntern { get; private set; }
        public int? EnterpriseId { get; private set; }

        /// <summary>
        /// constructor for creating
        /// </summary>
        /// <param name="name"></param>
        /// <param name="renewableShare"></param>
        /// <param name="isIntern"></param>
        /// <param name="enterpriseId"></param>
        public EnergySource(string name, float renewableShare, bool isIntern, int enterpriseId)
        {
            Id = -1;
            Name = name;
            RenewableShare = renewableShare;
            IsIntern = isIntern;
            EnterpriseId = enterpriseId;
        }

        /// <summary>
        /// constructor for updating
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="renewableShare"></param>
        /// <param name="isIntern"></param>
        public EnergySource(int id, string? name, float? renewableShare, bool? isIntern)
        {
            Id = id;
            Name = name;
            RenewableShare = renewableShare;
            IsIntern = isIntern;

            // you cannot change the enterprise of an energy source
            EnterpriseId = -1;
        }

        public void SetId(int id)
        {
            if (Id == -1)
            {
                Id = id;
            }
            else
            {
                throw new InvalidOperationException($"The energy source id was already set. It's {Id}.");
            }
        }
    }
}


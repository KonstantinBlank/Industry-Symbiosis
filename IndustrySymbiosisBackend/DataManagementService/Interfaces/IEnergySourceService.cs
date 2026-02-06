namespace DataManagementService.Interfaces
{
    public interface IEnergySourceService
    {
        public string Get(int enterpriseId);
        public string Create(string name, float renewableShare, bool isIntern, int enterpriseId);
        public int Update(int id, string? name = null, float? renewableShare = null, bool? isIntern = null);
    }
}

using System;
namespace DataManagementService.Data
{
    public class Stream
    {
        public int Id { get; private set; }
        public int ProductionLineProcessId { get; private set; }
        public bool? IsInput { get; private set; }
        public int? MaterialId { get; private set; }
        public int? EnergyId { get; private set; }
        public int? Amount { get; private set; }
        public int? Interval { get; private set; }

        public string? MaterialName { get; private set; }
        // for materials
        public string? Unit { get; private set; }
        public bool? IsEmission { get; private set; }
        // for energy
        public float? RenewableShare { get; private set; }
        public bool? IsIntern { get; private set; }

        public Stream(int id, int productionLineProcessId, bool? isInput, int? materialId, int? energyId, int? amount, int? interval)
        {
            Id = id;
            ProductionLineProcessId = productionLineProcessId;
            IsInput = isInput;
            MaterialId = materialId;
            EnergyId = energyId;
            Amount = amount;
            Interval = interval;

            MaterialName = null;
            Unit = null;
            IsEmission = null;
            RenewableShare = -1;
            IsIntern = null;
        }
    }
}


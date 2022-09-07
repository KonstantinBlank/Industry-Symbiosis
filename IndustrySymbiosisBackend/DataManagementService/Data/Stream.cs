using System;
namespace DataManagementService.Data
{
    public class Stream
    {
        public int Id { get; private set; }
        public int? ProductionLineProcessId { get; private set; }
        public bool? IsInput { get; private set; }
        public int? MaterialId { get; private set; }
        public int? EnergyId { get; private set; }
        public int? Amount { get; private set; }
        public int? Interval { get; private set; }

        public string? Name { get; private set; }
        // for materials
        public string? Unit { get; private set; }
        public bool? IsEmission { get; private set; }
        // for energy
        public float? RenewableShare { get; private set; }
        public bool? IsIntern { get; private set; }

        public Stream(int id, int? productionLineProcessId, bool? isInput, int? materialId, int? energyId, int? amount, int? interval)
        {
            Id = id;
            ProductionLineProcessId = productionLineProcessId;
            IsInput = isInput;
            MaterialId = materialId;
            EnergyId = energyId;
            Amount = amount;
            Interval = interval;

            Name = null;
            Unit = null;
            IsEmission = null;
            RenewableShare = -1;
            IsIntern = null;
        }

        public Stream(int productionLineProcessId, bool isInput, int? materialId, int? energyId, int amount, int interval)
        {
            Id = -1;
            ProductionLineProcessId = productionLineProcessId;
            IsInput = isInput;
            MaterialId = materialId;
            EnergyId = energyId;
            Amount = amount;
            Interval = interval;

            Name = null;
            Unit = null;
            IsEmission = null;
            RenewableShare = null;
            IsIntern = null;
        }


        public void SetEnergyId(int id)
        {
            if (EnergyId == -1)
            {
                EnergyId = id;
            }
            else
            {
                throw new InvalidOperationException($"The energy id was already set. It's {Id}.");
            }
        }

        public void SetMaterialId(int id)
        {
            if (MaterialId == -1)
            {
                MaterialId = id;
            }
            else
            {
                throw new InvalidOperationException($"The material id was already set. It's {Id}.");
            }
        }

        public void SetStreamId(int id)
        {
            if (Id == -1)
            {
                Id = id;
            }
            else
            {
                throw new InvalidOperationException($"The stream id was already set. It's {Id}.");
            }
        }
    }
}


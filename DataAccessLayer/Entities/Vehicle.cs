using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using Dapper;

namespace DataAccessLayer.Entities
{
    public class Vehicle
    {
        private const int TaxBasicShift = 5;
        private const int TaxMultiplier = 30;
        private const double WeightCoefficient = 0.0013;

        public int VehicleId { get; set; }
        public string ModelName { get; set; }
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }
        public string RegistrationNumber { get; set; }
        public int ManufactureYear { get; set; }
        public double Mileage { get; set; }
        public double Weight { get; set; }
        public string EngineType { get; set; }
        public double EngineCapacity { get; set; }
        public double EngineConsumption { get; set; }
        public double EnergyTankCapacity { get; set; }
        public double TaxPerMonth => Weight * WeightCoefficient
                                   + VehicleType.TaxCoeff * TaxMultiplier
                                   + TaxBasicShift;
        public double MaxKm => EnergyTankCapacity / EngineConsumption;
        public Color Color { get; set; }  
    }
}

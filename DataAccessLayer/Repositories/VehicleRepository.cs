using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using Dapper;

namespace DataAccessLayer.Repositories
{
    public class VehicleRepository : RepositoryBase, IRepository<Vehicle>
    {
        private const string sqlCreate = "INSERT INTO Vehicles ("
            + "VehicleTypeId, ModelName, RegistrationNumber, WeightKg, ManufactureYear, "
            + "Mileage, Color, EngineType, EngineCapacity, EngineConsumption, EnergyTankCapacity) "
            + "VALUES("
            + "@VehicleTypeId, @ModelName, @RegistrationNumber, @Weight, @ManufactureYear, "
            + "@Mileage, @Color, @EngineType, @EngineCapacity, @EngineConsumption, @EnergyTankCapacity)";

        private const string sqlDelete = "DELETE FROM Vehicles WHERE VehicleId = @VehicleId";

        private const string sqlGetAll = "SELECT V.*, VT.VehicleTypeId AS VTId, VT.TypeName, VT.TaxCoeff "
                                       + "FROM Vehicles AS V "
                                       + "INNER JOIN VehicleTypes AS VT ON V.VehicleTypeId = VT.VehicleTypeId";

        private const string sqlGetById = "SELECT V.*, VT.Id AS VTId, VT.TypeName, VT.TaxCoeff "
                                        + "FROM Vehicles AS V "
                                        + "INNER JOIN VehicleTypes AS VT ON V.VehicleTypeId = VT.VehicleTypeId "
                                        + "WHERE V.Id = @id";

        private const string sqlUpdate = "UPDATE Vehicles SET "
            + "VehicleTypeId = @VehicleTypeId, "
            + "ModelName = @ModelName, "
            + "RegistrationNumber = @RegistrationNumber, "
            + "WeightKg = @Weight, "
            + "ManufactureYear = @ManufactureYear, "
            + "Mileage = @Mileage, "
            + "Color = @Color, "
            + "EngineType = @EngineType, "
            + "EngineCapacity = @EngineCapacity, "
            + "EngineConsumption = @EngineConsumption, "
            + "EnergyTankCapacity = @EnergyTankCapacity "
            + "WHERE Id = @Id";

        public VehicleRepository(string connection) : base(connection) { }

        public void Create(Vehicle instance) => connection.Execute(sqlCreate, instance);

        public void Delete(int id) => connection.Execute(sqlDelete, new { id });

        public IEnumerable<Vehicle> GetAll()
        {
            return connection.Query<Vehicle, VehicleType, Vehicle>
            (
                sqlGetAll,
                (vehicle, vehicleType) =>
                {
                    vehicle.VehicleType = vehicleType;
                    return vehicle;
                },
                splitOn: "VTId"
            );
        }

        public Vehicle GetById(int id)
        {
            return connection.Query<Vehicle, VehicleType, Vehicle>
            (
                sqlGetById,
                (vehicle, vehicleType) =>
                {
                    vehicle.VehicleType = vehicleType;
                    return vehicle;
                },
                splitOn: "VTId",
                param: new { id }
            )
            .FirstOrDefault();
        }

        public void Update(Vehicle instance) => connection.Execute(sqlUpdate, instance);
    }
}

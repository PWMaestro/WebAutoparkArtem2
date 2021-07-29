using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.DataTransferObjects;

namespace DataAccessLayer.Repositories
{
    public class VehicleRepository : RepositoryBase, IVehicleRepository
    {
        private const string sqlCreate = "INSERT INTO Vehicles ("
            + "VehicleTypeId, ModelName, RegistrationNumber, Weight, ManufactureYear, "
            + "Mileage, Color, EngineType, EngineCapacity, EngineConsumption, EnergyTankCapacity) "
            + "VALUES("
            + "@VehicleTypeId, @ModelName, @RegistrationNumber, @Weight, @ManufactureYear, "
            + "@Mileage, @Color, @EngineType, @EngineCapacity, @EngineConsumption, @EnergyTankCapacity)";

        private const string sqlDelete = "DELETE FROM Vehicles WHERE VehicleId = @id";

        private const string sqlGetAll = "SELECT V.*, VT.VehicleTypeId AS VTId, VT.TypeName, VT.TaxCoeff "
                                       + "FROM Vehicles AS V "
                                       + "INNER JOIN VehicleTypes AS VT ON V.VehicleTypeId = VT.VehicleTypeId";

        private const string sqlGetById = "SELECT V.*, VT.VehicleTypeId AS VTId, VT.TypeName, VT.TaxCoeff "
                                        + "FROM Vehicles AS V "
                                        + "INNER JOIN VehicleTypes AS VT ON V.VehicleTypeId = VT.VehicleTypeId "
                                        + "WHERE V.VehicleId = @id";

        private const string sqlUpdate = "UPDATE Vehicles SET "
            + "VehicleTypeId = @VehicleTypeId, "
            + "ModelName = @ModelName, "
            + "RegistrationNumber = @RegistrationNumber, "
            + "Weight = @Weight, "
            + "ManufactureYear = @ManufactureYear, "
            + "Mileage = @Mileage, "
            + "Color = @Color, "
            + "EngineType = @EngineType, "
            + "EngineCapacity = @EngineCapacity, "
            + "EngineConsumption = @EngineConsumption, "
            + "EnergyTankCapacity = @EnergyTankCapacity "
            + "WHERE VehicleId = @VehicleId";

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

        public IEnumerable<Vehicle> GetAll(SortOrder vehicleSortOrder)
        {
            string column = GetSqlSortingColumn(vehicleSortOrder.Column);
            string direction = GetSqlSortingDir(vehicleSortOrder.Direction);

            return connection.Query<Vehicle, VehicleType, Vehicle>
            (
                sqlGetAll + $" ORDER BY {column} {direction}",
                (vehicle, vehicleType) =>
                {
                    vehicle.VehicleType = vehicleType;
                    return vehicle;
                },
                splitOn: "VTId"
            );
        }

        private string GetSqlSortingColumn(string columnAlias) => columnAlias switch
        {
            "id" => "V.VehicleId",
            "name" => "V.ModelName",
            "type" => "VT.TypeName",
            "mileage" => " V.Mileage",
            _ => "V.VehicleId"
        };

        private string GetSqlSortingDir(string direction) => direction switch
        {
            "forward" => "ASC",
            "reverse" => "DESC",
            _ => "ASC"
        };

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

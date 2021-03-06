using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories
{
    public class OrderRepository : RepositoryBase, IOrderRepository
    {
        private const string sqlCreate = "INSERT INTO Orders (VehicleId) VALUES(@VehicleId)";

        private const string sqlCreateAndReturn = "INSERT INTO Orders (VehicleId) "
                                                + "OUTPUT INSERTED.OrderId, INSERTED.VehicleId "
                                                + "VALUES(@VehicleId)";

        private const string sqlDelete = "DELETE FROM Orders WHERE OrderId = @id";

        private const string sqlGetAll = "SELECT "
                                           + "O.*, V.VehicleId AS VId, V.VehicleTypeId, V.ModelName, V.RegistrationNumber, V.Weight, V.ManufactureYear, "
                                           + "V.Mileage, V.Color, V.EngineType, V.EngineCapacity, V.EngineConsumption, V.EnergyTankCapacity, "
                                           + "OE.OrderElementId AS OEId, OE.OrderId, OE.SpareId, OE.SpareQuantity, "
                                           + "SP.SpareId AS SPId, SP.Name, "
                                           + "VT.VehicleTypeId AS VTId, VT.TypeName "
                                       + "FROM Orders AS O "
                                           + "LEFT JOIN OrderElements  AS OE  ON O.OrderId = OE.OrderId "
                                           + "     JOIN Vehicles       AS V   ON O.VehicleId = V.VehicleId "
                                           + "LEFT JOIN Spares         AS SP  ON OE.SpareId = SP.SpareId "
                                           + "     JOIN VehicleTypes   AS VT  ON VT.VehicleTypeId = V.VehicleTypeId";

        private const string sqlGetById = "SELECT "
                                            + "O.*, V.VehicleId AS VId, V.VehicleTypeId, V.ModelName, V.RegistrationNumber, V.Weight, V.ManufactureYear, "
                                            + "V.Mileage, V.Color, V.EngineType, V.EngineCapacity, V.EngineConsumption, V.EnergyTankCapacity, "
                                            + "OE.OrderElementId AS OEId, OE.OrderId, OE.SpareId, OE.SpareQuantity, "
                                            + "SP.SpareId AS SPId, SP.Name "
                                        + "FROM Orders AS O "
                                            + "LEFT JOIN OrderElements  AS OE  ON O.OrderId = OE.OrderId "
                                            + "     JOIN Vehicles       AS V   ON O.VehicleId = V.VehicleId "
                                            + "LEFT JOIN Spares         AS SP  ON OE.SpareId = SP.SpareId " 
                                        + "WHERE O.OrderId = @id";

        private const string sqlUpdate = "UPDATE Orders SET VehicleId = @VehicleId WHERE OrderId = @id";

        public OrderRepository(string connection) : base(connection) { }

        public void Create(Order instance) => connection.Execute(sqlCreate, instance);

        public Order CreateAndReturn(Order instance)
        {
            return connection.QuerySingle<Order> (sqlCreateAndReturn, instance);
        }

        public void Delete(int id) => connection.Execute(sqlDelete, new { id });

        public IEnumerable<Order> GetAll()
        {
            return connection.Query<Order, Vehicle, VehicleType, Order>
            (
                sqlGetAll,
                (order, vehicle, vType) =>
                {
                    order.Vehicle = vehicle;
                    order.Vehicle.VehicleType = vType;
                    return order;
                },
                splitOn: "VId,VTId"
            );
        }

        public Order GetById(int id)
        {
            var orderElements = new List<OrderElement>();
            var collection = connection.Query<Order, Vehicle, OrderElement, Spare, Order>
            (
                sqlGetById,
                (order, vehicle, orderElement, spare) =>
                {
                    order.Vehicle = vehicle;
                    orderElement.Part = spare;
                    orderElements.Add(orderElement);
                    return order;
                },
                splitOn: "VId,OEId,SPId",
                param: new { id }
            );

            var order = collection.First();
            order.OrderElements = orderElements;
            
            return order;
        }

        public void Update(Order instance) => connection.Execute(sqlUpdate, instance);
    }
}

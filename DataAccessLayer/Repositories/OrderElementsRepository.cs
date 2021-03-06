using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public class OrderElementsRepository : RepositoryBase, IRepository<OrderElement>
    {
        private const string sqlCreate = "INSERT INTO OrderElements (OrderId, SpareId, SpareQuantity) "
                                       + "VALUES (@OrderId, @SpareId, @SpareQuantity)";

        private const string sqlDelete = "DELETE FROM OrderElements WHERE OrderElementId = @id";

        private const string sqlGetAll = "SELECT "
                                           + "OE.*, SP.* "
                                       + "FROM OrderElements AS OE "
                                       + "JOIN Spares AS SP ON OE.SpareId = SP.SpareId";

        private const string sqlGetById = "SELECT * FROM OrderElements WHERE OrderElementId = @id";

        private const string sqlUpdate = "UPDATE OrderElements "
                                       + "SET OrderId = @OrderId, SpareId = @SpareId, SpareQuantity = @SpareQuantity "
                                       + "WHERE OrderElementId = @id";

        public OrderElementsRepository(string connection) : base(connection) { }

        public void Create(OrderElement instance) => connection.Execute(sqlCreate, instance);

        public void Delete(int id) => connection.Execute(sqlDelete, new { id });

        public IEnumerable<OrderElement> GetAll()
        {
            return connection.Query<OrderElement, Spare, OrderElement>
            (
                sqlGetAll,
                (orderElement, spare) =>
                {
                    orderElement.Part = spare;
                    return orderElement;
                },
                splitOn: "SPId"
            );
        }

        public OrderElement GetById(int id) => connection.QueryFirst<OrderElement>(sqlGetById, new { id });

        public void Update(OrderElement instance) => connection.Execute(sqlUpdate, instance);
    }
}

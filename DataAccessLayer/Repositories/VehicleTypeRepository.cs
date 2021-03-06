using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Dapper;

namespace DataAccessLayer.Repositories
{
    public class VehicleTypeRepository : RepositoryBase, IRepository<VehicleType>
    {
        private const string sqlCreate = "INSERT INTO VehicleTypes (TypeName, TaxCoeff) VALUES(@TypeName, @TaxCoeff)";
        private const string sqlDelete = "DELETE FROM VehicleTypes WHERE VehicleTypeId = @id";
        private const string sqlGetAll = "SELECT * FROM VehicleTypes";
        private const string sqlGetById = "SELECT * FROM VehicleTypes WHERE VehicleTypeId = @id";
        private const string sqlUpdate = "UPDATE VehicleTypes SET TypeName = @TypeName, TaxCoeff = @TaxCoeff "
                                       + "WHERE VehicleTypeId = @VehicleTypeId";

        public VehicleTypeRepository(string connection) : base(connection) { }

        public void Create(VehicleType instance) => connection.Execute(sqlCreate, instance);

        public void Delete(int id) => connection.Execute(sqlDelete, new { id });

        public IEnumerable<VehicleType> GetAll() => connection.Query<VehicleType>(sqlGetAll);

        public VehicleType GetById(int id) => connection.QueryFirst<VehicleType>(sqlGetById, new { id });

        public void Update(VehicleType instance) => connection.Execute(sqlUpdate, instance);
    }
}

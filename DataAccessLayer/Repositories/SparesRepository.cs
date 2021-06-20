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
    public class SparesRepository : RepositoryBase, IRepository<Spares>
    {
        private const string sqlCreate = "INSERT INTO Spares (PartName) VALUES(@PartName)";
        private const string sqlDelete = "DELETE FROM Spares WHERE PartId = @PartId";
        private const string sqlGetAll = "SELECT * FROM Spares";
        private const string sqlGetById = "SELECT * FROM Spares WHERE PartId = @PartId";
        private const string sqlUpdate = "UPDATE Spares SET PartName = @PartName WHERE PartId = @PartId";

        public SparesRepository(string connection) : base(connection) { }

        public void Create(Spares instance) => connection.Execute(sqlCreate, instance);

        public void Delete(int id) => connection.Execute(sqlDelete, new { id });

        public IEnumerable<Spares> GetAll() => connection.Query<Spares>(sqlGetAll);

        public Spares GetById(int id) => connection.QueryFirst<Spares>(sqlGetById, new { id });

        public void Update(Spares instance) => connection.Execute(sqlUpdate, instance);
    }
}

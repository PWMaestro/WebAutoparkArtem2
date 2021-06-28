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
    public class SparesRepository : RepositoryBase, IRepository<Spare>
    {
        private const string sqlCreate = "INSERT INTO Spares (Name) VALUES(@Name)";
        private const string sqlDelete = "DELETE FROM Spares WHERE Id = @Id";
        private const string sqlGetAll = "SELECT * FROM Spares";
        private const string sqlGetById = "SELECT * FROM Spares WHERE Id = @Id";
        private const string sqlUpdate = "UPDATE Spares SET Name = @Name WHERE Id = @Id";

        public SparesRepository(string connection) : base(connection) { }

        public void Create(Spare instance) => connection.Execute(sqlCreate, instance);

        public void Delete(int id) => connection.Execute(sqlDelete, new { id });

        public IEnumerable<Spare> GetAll() => connection.Query<Spare>(sqlGetAll);

        public Spare GetById(int id) => connection.QueryFirst<Spare>(sqlGetById, new { id });

        public void Update(Spare instance) => connection.Execute(sqlUpdate, instance);
    }
}

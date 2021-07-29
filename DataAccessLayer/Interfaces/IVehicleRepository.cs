using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.DataTransferObjects;


namespace DataAccessLayer.Interfaces
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        public IEnumerable<Vehicle> GetAll(SortOrder sortOrder);
    }
}

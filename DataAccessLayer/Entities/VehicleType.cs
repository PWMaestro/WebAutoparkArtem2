using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class VehicleType
    {
        public int VehicleTypeId { get; set; }
        public string TypeName { get; set; }
        public double TaxCoeff { get; set; }
    }
}

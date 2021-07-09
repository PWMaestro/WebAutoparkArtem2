using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class OrderElement
    {
        public int OrderElementId { get; set; }
        public int OrderId { get; set; }
        public int SpareId { get; set; }
        public Spare Part { get; set; }
        public int SpareQuantity { get; set; }
    }
}

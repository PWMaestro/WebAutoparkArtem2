using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.DataTransferObjects
{
    public class SortOrder
    {
        public string Column { get; set; }
        public string Direction { get; set; }

        public SortOrder(string column, string direction)
        {
            Column = column;
            Direction = direction;
        }
    }
}

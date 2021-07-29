using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAutopark.Models
{
    public class CreateOrderElementModel
    {
        public OrderElement OrderElement { get; set; }
        public SelectList SpareId { get; set; }
    }
}

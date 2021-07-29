using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAutopark.Models
{
    public class CreateUpdateVehicleModel
    {
        public Vehicle Vehicle { get; set; }
        public SelectList VehicleTypeIds { get; set; }
    }
}

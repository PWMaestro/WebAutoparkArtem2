using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Repositories;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;

namespace WebAutopark.Controllers
{
    public class VehicleTypeController : Controller
    {
        private readonly IRepository<VehicleType> _vehicleTypeRepo;

        public VehicleTypeController(IRepository<VehicleType> vehicleTypeRepo)
        {
            _vehicleTypeRepo = vehicleTypeRepo;
        }

        public IActionResult Index()
        {
            var vehicleTypes = _vehicleTypeRepo.GetAll().OrderBy(e => e.VehicleTypeId);
            return View(vehicleTypes);
        }
        }
    }
}

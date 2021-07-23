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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VehicleType vehicleType)
        {
            _vehicleTypeRepo.Create(vehicleType);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var vehicleType = _vehicleTypeRepo.GetById(id);
            return View(vehicleType);
        }

        [HttpPost]
        public IActionResult Update(VehicleType vehicleType)
        {
            _vehicleTypeRepo.Update(vehicleType);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _vehicleTypeRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

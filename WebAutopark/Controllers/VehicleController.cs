using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using DataAccessLayer.DataTransferObjects;

namespace WebAutopark.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IRepository<VehicleType> _vehicleTypeRepo;

        public VehicleController(IVehicleRepository vehicleRepository, IRepository<VehicleType> vehicleTypeRepository)
        {
            _vehicleRepo = vehicleRepository;
            _vehicleTypeRepo = vehicleTypeRepository;
        }

        [HttpGet]
        public IActionResult Index(SortOrder sortOrder)
        {
            var vehicles = _vehicleRepo.GetAll(sortOrder);
            return View(vehicles);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var vehicle = _vehicleRepo.GetById(id);
            return View(vehicle);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            var model = new Vehicle();
            ViewBag.vehicleTypes = new SelectList(_vehicleTypeRepo.GetAll(), "VehicleTypeId", "TypeName");

            return View(model);
        }
        
        [HttpPost]
        public IActionResult Create(Vehicle vehicle)
        {
            _vehicleRepo.Create(vehicle);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var model = _vehicleRepo.GetById(id);
            ViewBag.vehicleTypes = new SelectList(_vehicleTypeRepo.GetAll(), "VehicleTypeId", "TypeName");

            return View(model);
        }
        
        [HttpPost]
        public IActionResult Update(Vehicle vehicle)
        {
            _vehicleRepo.Update(vehicle);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            _vehicleRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

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
    public class SpareController : Controller
    {
        private readonly IRepository<Spare> _spareRepo;

        public SpareController(IRepository<Spare> spareRepo)
        {
            _spareRepo = spareRepo;
        }

        public IActionResult Index()
        {
            return View(_spareRepo.GetAll().OrderBy(sp => sp.SpareId));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name)
        {
            _spareRepo.Create(new Spare { Name = name });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.spare = _spareRepo.GetById(id);
            return View();
        }

        [HttpPost]
        public IActionResult Update(string name, int id)
        {
            _spareRepo.Update(new Spare { Name = name, SpareId = id });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _spareRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

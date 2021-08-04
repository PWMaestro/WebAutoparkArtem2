using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Repositories;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using WebAutopark.Models;

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
            var spares = _spareRepo.GetAll().OrderBy(sp => sp.SpareId);
            return View(spares);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Spare spare)
        {
            _spareRepo.Create(spare);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var spare = _spareRepo.GetById(id);
            return View(spare);
        }

        [HttpPost]
        public IActionResult Update(Spare spare)
        {
            _spareRepo.Update(spare);
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _spareRepo.Delete(id);
            return Ok();
        }
    }
}

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
    }
}

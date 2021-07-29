using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAutopark.Controllers
{
    public class OrderElementController : Controller
    {
        private readonly IRepository<OrderElement> _orderElementRepo;
        private readonly IRepository<Spare> _spareRepo;

        public OrderElementController(IRepository<OrderElement> orderElementRepo, IRepository<Spare> spareRepo)
        {
            _orderElementRepo = orderElementRepo;
            _spareRepo = spareRepo;
        }

        [HttpPost]
        public IActionResult CreateOrderElement(OrderElement orderElement)
        {
            _orderElementRepo.Create(orderElement);
            return RedirectToAction("Details", "Order", new { id = orderElement.OrderId });
        }

        [HttpGet]
        public IActionResult Create(OrderElement orderElement)
        {
            var model = new CreateOrderElementModel
            {
                OrderElement = orderElement,
                SpareId = new SelectList(_spareRepo.GetAll(), "SpareId", "Name")
            };

            return View(model);
        }
    }
}

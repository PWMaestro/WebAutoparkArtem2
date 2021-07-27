using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace WebAutopark.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRepository<Order> _orderRepo;

        public OrderController(IRepository<Order> orderRepository)
        {
            _orderRepo = orderRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var orders = _orderRepo.GetAll();
            return View(orders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            _orderRepo.Create(order);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }
    }
}

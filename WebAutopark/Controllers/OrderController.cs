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
        private readonly IOrderRepository _orderRepo;
        private readonly IVehicleRepository _vehicleRepo;

        public OrderController(IOrderRepository orderRepository, IVehicleRepository vehicleRepository)
        {
            _orderRepo = orderRepository;
            _vehicleRepo = vehicleRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var orders = _orderRepo.GetAll();
            return View(orders);
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            var newOrder = _orderRepo.CreateAndReturn(order);
            return RedirectToAction
            (
                "Create",
                "OrderElement",
                new OrderElement() { OrderId = newOrder.OrderId }
            );
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var order = _orderRepo.GetById(id);
            return View(order);
        }
    }
}

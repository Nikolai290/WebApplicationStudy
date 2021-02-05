using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;
using WebApplication3.Models.Services;

namespace WebApplication3.Controllers {
    public class OrderController : Controller {

        OrderManager orderManager = new OrderManager();
        EmployeeManager employeeManager = new EmployeeManager();
        public IActionResult Index(DateTime date, int Shift, int OrderAreaId) {
            var order = orderManager.Get(DateTime.Now.Date, 1, 1);

            ViewBag.IsClose = order.IsClose;
            ViewBag.Disps = employeeManager.GetEmployeesByStringFind("Диспетчер");
            ViewBag.Chiefs = employeeManager.GetEmployeesByStringFind("Начальник");
            ViewBag.Masters = employeeManager.GetEmployeesByStringFind("Горный мастер");
            ViewBag.Machines = null;

            return View("Order");
        }
    }
}

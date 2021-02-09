using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;
using WebApplication3.Models.Services;

namespace WebApplication3.Controllers {
    public class OrderController : Controller {


        private IDbManager dbManager;
        private OrderManager orderManager;
        private EmployeeManager employeeManager;
        private OrderAreaManager areaManager;

        public OrderController() {
            dbManager = new DbManager();
            orderManager = new OrderManager(dbManager);
            employeeManager = new EmployeeManager(dbManager);
            areaManager = new OrderAreaManager(dbManager);
        }

        [HttpGet]
        public IActionResult Index(DateTime Date, int Shift = 1, int OrderAreaId = 1) {
            if (Date.Year < 2000)
                Date = DateTime.Now.Date;
            Order order = orderManager.Get(Date, Shift, OrderAreaId);

            ViewBag.Areas = areaManager.GetAll();
            ViewBag.Disps = employeeManager.GetEmployeesByStringFind("Диспетчер");
            ViewBag.Chiefs = employeeManager.GetEmployeesByStringFind("Начальник");
            ViewBag.Masters = employeeManager.GetEmployeesByStringFind("Горный мастер");
            ViewBag.Machines = null;

            dbManager.Commit();
            return View("Order", order);
        }

        [HttpPost]
        public IActionResult Index(DateTime date, int shift, int orderAreaId, int Dispetcher, int Chief, int[] Masters) {
            var areas = areaManager.GetAll();

            var disp = employeeManager.GetEmployeeById(Dispetcher);
            var chief = employeeManager.GetEmployeeById(Chief);
            var masters = new List<Employee>();
            foreach (var id in Masters)
                masters.Add(employeeManager.GetEmployeeById(id));

            Order order = new Order()
                .SetBase(date, shift)
                .SetStaff(disp, chief, masters)
                .SetArea(areas.Where(x => x.Id == orderAreaId).First());

            orderManager.SaveOrUpdate(order);
            ViewBag.Areas = areas;
            ViewBag.Disps = employeeManager.GetEmployeesByStringFind("Диспетчер");
            ViewBag.Chiefs = employeeManager.GetEmployeesByStringFind("Начальник");
            ViewBag.Masters = employeeManager.GetEmployeesByStringFind("Горный мастер");

            dbManager.Commit();
            return View("Order", order);
        }
    }
}

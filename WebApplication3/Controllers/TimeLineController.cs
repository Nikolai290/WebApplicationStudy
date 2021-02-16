using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;
using WebApplication3.Models.Services;
using WebApplication3.Models.DbAccess;

namespace WebApplication3.Controllers {
    public class TimeLineController : Controller {

        private IDbManager dbManager;
        private OrderManager orderManager;
        private OrderAreaManager areaManager;
        private MachineryOnShiftManager machOnShiftManager;




        public TimeLineController() {
            dbManager = new DbManager();
            orderManager = new OrderManager(dbManager);
            areaManager = new OrderAreaManager(dbManager);
            machOnShiftManager = new MachineryOnShiftManager(dbManager);

        }

        [HttpGet]
        public IActionResult Index(DateTime Date, int Shift = 1, int OrderAreaId = 1, int IdOrder = 0) {
            if (Date.Year < 1950)
                Date = DateTime.Now.Date;

            var order = IdOrder > 0 ? 
                orderManager.GetById(IdOrder) :
                orderManager.Get(Date, Shift, OrderAreaId);

            ViewBag.Areas = areaManager.GetAll();
            var startHour = order.Shift == 1 ? 
                new DateTime(1, 1, 1, 8, 0, 0) :
                new DateTime(1, 1, 1, 20, 0, 0);
             var hours = new List<string>();
            for (int i = 0; i < 12; i++) {
                hours.Add(startHour.AddHours(i).ToString("HH:mm"));
            }
            ViewBag.Hours = hours;


            dbManager.Commit();
            return View("TimeLine", order);
        }
    }
}

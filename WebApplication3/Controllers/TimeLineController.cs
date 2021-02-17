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
        private WorkManager workManager;




        public TimeLineController() {
            dbManager = new DbManager();
            orderManager = new OrderManager(dbManager);
            areaManager = new OrderAreaManager(dbManager);
            machOnShiftManager = new MachineryOnShiftManager(dbManager);
            workManager = new WorkManager(dbManager);

        }

        private void FillViewBag(Order order) {
            ViewBag.WorkTypes = workManager.GetTypes();
            ViewBag.SortCoal = workManager.GetSorts();
            ViewBag.Areas = areaManager.GetAll();

            var startHour = order.Shift == 1 ?
                new DateTime(1, 1, 1, 8, 0, 0) :
                new DateTime(1, 1, 1, 20, 0, 0);
            var hours = new List<string>();
            for (int i = 0; i < 12; i++) {
                hours.Add(startHour.AddHours(i).ToString("HH:mm"));
            }
            hours.Add(startHour.AddHours(11).ToString("HH:59"));

            ViewBag.Hours = hours;
        }

        [HttpGet]
        public IActionResult Index(DateTime Date, int IdOrder, int machId, int workId, int Shift = 1, int OrderAreaId = 1) {
            if (Date.Year < 1950)
                Date = DateTime.Now.Date;

            var order = IdOrder > 0 ?
                orderManager.GetById(IdOrder) :
                orderManager.Get(Date, Shift, OrderAreaId);
            var work = workManager.GetById(workId);

            ViewBag.Work = work;
            ViewBag.MachName = machOnShiftManager.GetById(machId)?.Name;

            FillViewBag(order);
            dbManager.Commit();
            return View("TimeLine", order);

        }


        [HttpPost]
        public IActionResult Index(int machId, int sort, int typework, int workId,
            DateTime startTime, DateTime endTime,
            string note,
            string volume, string weight, string ash, string heat, string wet,
            int wagons) {
            var dvolume = Convert.ToDouble(volume);
            var dweight = Convert.ToDouble(weight);
            var dash = Convert.ToDouble(ash);
            var dheat = Convert.ToDouble(heat);
            var dwet = Convert.ToDouble(wet);

            var mach = machOnShiftManager.GetById(machId);
            Work work = workManager.NewWork( workId,
                machId, typework, startTime, endTime, note, 
                sort, dvolume, dweight, dash, dheat, dwet, wagons);







            var order = orderManager.GetById(mach.Order.Id);



            ViewBag.Work = work;
            ViewBag.MachName = mach?.Name;
            FillViewBag(order);

            dbManager.Commit();
            return View("TimeLine", order);
        }

        [HttpGet]
        public IActionResult Delete(int orderId, int workId) {

            var order = orderManager.GetById(orderId);
            workManager.Delete(workId);

            FillViewBag(order);

            dbManager.Commit();
            return View("TimeLine", order);
        }
    }
}

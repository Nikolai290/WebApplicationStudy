using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;
using WebApplication3.Models.Services;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.ViewModels;

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



        [HttpGet]
        public IActionResult Index(TimelineGetDTO dto) { 
            var model = workManager.GetModelForTimeline(dto);
            dbManager.Commit();
            return View("TimeLine", model);

        }


        [HttpPost]
        public IActionResult Index(TimelineGetDTO dto, AddWorkDTO dtoPost) {



            var work = workManager.NewWork(dtoPost);
            dto.WorkId = work.Id;
            var model = workManager.GetModelForTimeline(dto);
            

            dbManager.Commit();
            return View("TimeLine", model);
        }

        [HttpGet]
        public IActionResult Delete(TimelineGetDTO dto) {

            workManager.Delete(dto.WorkId);

            var model = workManager.GetModelForTimeline(dto);

            dbManager.Commit();
            return View("TimeLine", model);
        }
    }
}

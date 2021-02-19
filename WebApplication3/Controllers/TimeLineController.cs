using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models.Services;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.ViewModels;
using System.Threading.Tasks;

namespace WebApplication3.Controllers {
    public class TimeLineController : Controller {

        private readonly IDbManager dbManager;
        private readonly TimeLineManager workManager;

        public TimeLineController() {
            dbManager = new DbManager();
            workManager = new TimeLineManager(dbManager);
        }



        [HttpGet]
        public IActionResult Index(TimelineGetDTO dto) { 
            var model = workManager.GetModelForTimeline(dto);
            dbManager.Commit();
            return View("TimeLine", model);
        }


        [HttpPost]
        public async Task<IActionResult> Index(TimelineGetDTO dto, AddWorkDTO dtoPost) {
            var work = workManager.NewWork(dtoPost);
            var model = workManager.GetModelForTimeline(dto);
            model.Work = await work;
            dbManager.Commit();
            return View("TimeLine", model);
        }

        [HttpGet]
        public IActionResult Delete(TimelineGetDTO dto) {
            workManager.Delete(dto.WorkId);
            dto.WorkId = 0;
            var model = workManager.GetModelForTimeline(dto);
            dbManager.Commit();
            return View("TimeLine", model);
        }
    }
}

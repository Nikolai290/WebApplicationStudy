using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Services;
using WebApplication3.Models.ViewModels;

namespace WebApplication3.Controllers {
    public class MachineryTypesController : Controller {
        private readonly IDbManager dbManager;
        private readonly MachineryTypesManager typesManager;

        public MachineryTypesController() {
            dbManager = new DbManager();
            typesManager = new MachineryTypesManager(dbManager);
        }

        public IActionResult Index(int id) {
            var model = typesManager.GetMachineryTypesViewModel(id);
            dbManager.Commit();
            return View("MachineryTypes", model);
        }

        [HttpPost]
        public IActionResult Index(MachineryTypeDTO dto) {
            var model = typesManager.SaveOrUpdateMachineryType(dto);
            dbManager.Commit();
            return View("MachineryTypes", model);
        }

        public IActionResult Delete(int delId) {
            var model = typesManager.Delete(delId);
            dbManager.Commit();
            return View("MachineryTypes", model);
        }
    }
}

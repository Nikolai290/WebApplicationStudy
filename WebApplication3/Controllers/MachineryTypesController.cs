using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Services;
using WebApplication3.Models.ViewModels.MachineriesTypes;

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
        public IActionResult Index(MachinariesTypeViewModel model) {
            if (!ModelState.IsValid) {
                model = typesManager.FillViewModel(model);
                dbManager.Commit();
                return View("MachineryTypes", model);
            }
            model = typesManager.SaveOrUpdateMachineryType(model);
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

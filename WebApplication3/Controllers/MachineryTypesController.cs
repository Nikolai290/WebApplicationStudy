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

        public IActionResult Index(int typeId) {
            var model = typesManager.GetMachineryTypesViewModel(typeId);
            dbManager.Commit();
            return View("Machinery", model);
        }

        [HttpPost]
        public IActionResult Index(MachineryTypeDTO dto) {
            var model = typesManager.SaveOrUpdateMachineryType(dto);
            dbManager.Commit();
            return View("Machinery", model);
        }

        public IActionResult Delete(int typeId) {
            var model = typesManager.Delete(typeId);
            dbManager.Commit();
            return View("Machinery", model);
        }
    }
}

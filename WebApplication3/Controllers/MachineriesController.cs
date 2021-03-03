using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Services;
using WebApplication3.Models.ViewModels;
using System.Collections.Generic;

namespace WebApplication3.Controllers {
    public class MachineriesController : Controller {

        private readonly IDbManager dbManager;
        private readonly MachineryManager machineryManager;

        public MachineriesController() {
            dbManager = new DbManager();
            machineryManager = new MachineryManager(dbManager);
        }
        public IActionResult Index(int id) {
            var model = machineryManager.GetMachineryViewModel(id);
            dbManager.Commit();
            return View("Machineries", model);
        }

        [HttpPost]
        public IActionResult Index(MachineryDTO dto) {
            var model = machineryManager.SaveOrUpdateMachinery(dto);
            dbManager.Commit();
            if(model.Conflict !=null)
                return View("Conflict", model);


            return View("Machineries", model);
        }

        public IActionResult SolveConflict(IList<SolveConflictDTO> dto, MachineryDTO dto1) {
            bool result = machineryManager.SolveConflict(dto);

            if (result) {
                var model = machineryManager.SaveOrUpdateMachinery(dto1);
                dbManager.Commit();
                return View("Machineries", model);
            }
            dbManager.Commit();
            return BadRequest("Не удалось. Выберите другое оборудование.");
        }

        public IActionResult Delete(int delId) {
            var model  = machineryManager.Delete(delId);
            dbManager.Commit();
            return View("Machineries", model);
        }

    }
}

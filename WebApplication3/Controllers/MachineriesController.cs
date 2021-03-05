using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Services;
using WebApplication3.Models.ViewModels;
using WebApplication3.Models.ViewModels.Machineries;
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
        public IActionResult Index(MachineriesViewModel dto) {

            if (!ModelState.IsValid) {
                var model1 = machineryManager.GetMachineryViewModel(dto.Id);
                model1.Name = dto.Name;
                model1.TypeId = dto.TypeId;
                dbManager.Commit();
                return View("Machineries", model1);
            }


            var model = machineryManager.SaveOrUpdateMachinery(dto);
            dbManager.Commit();
            if(model.Conflict !=null)
                return View("Conflict", model);


            return View("Machineries", model);
        }

        public IActionResult SolveConflict(IList<SolveConflictDTO> dto, MachineriesViewModel dto1) {
            bool result = machineryManager.SolveConflict(dto);
            var model = machineryManager.SaveOrUpdateMachinery(dto1);

            if (result) {
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

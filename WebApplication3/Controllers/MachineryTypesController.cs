using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Services;
using WebApplication3.Models.ViewModels;
using WebApplication3.Models.Entities;

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
        public IActionResult Index(MachineryDTO dto) {
            var model = typesManager.SaveOrUpdateMachineryType(dto);
            dbManager.Commit();
            return View("Machinery", model);
        }

        public IActionResult Delete(int typeId) {
            typesManager.Delete(typeId);
            var model = typesManager.GetMachineryTypesViewModel(typeId);
            dbManager.Commit();
            return View("Machinery", model);
        }
    }
}

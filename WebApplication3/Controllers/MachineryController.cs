﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Services;
using WebApplication3.Models.ViewModels;
using WebApplication3.Models.Entities;

namespace WebApplication3.Controllers {
    public class MachineryController : Controller {

        private readonly IDbManager dbManager;
        private readonly MachineryManager machineryManager;

        public MachineryController() {
            dbManager = new DbManager();
            machineryManager = new MachineryManager(dbManager);
        }
        public IActionResult Index(int machineId) {
            var model = machineryManager.GetMachineryViewModel(machineId);
            dbManager.Commit();
            return View("Machinery", model);
        }

        [HttpPost]
        public IActionResult Index(MachineryDTO dto) {
            var model = machineryManager.SaveOrUpdateMachinery(dto);
            dbManager.Commit();
            return View("Machinery", model);
        }

        public IActionResult Delete(int machineId) {
            machineryManager.Delete(machineId);
            var model = machineryManager.GetMachineryViewModel(machineId);
            dbManager.Commit();
            return View("Machinery", model);
        }

    }
}
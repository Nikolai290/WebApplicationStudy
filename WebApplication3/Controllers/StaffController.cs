using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;
using WebApplication3.Models.Services;
using WebApplication3.Models.ViewModels;
using WebApplication3.Models.ViewModels.Employees;


namespace WebApplication3.Controllers {
    public class StaffController : Controller {

        private readonly IDbManager dbManager;
        private readonly PositionManager positionManager;
        private readonly EmployeeManager employeeManager;

        public StaffController() {
            dbManager = new DbManager();
            employeeManager = new EmployeeManager(dbManager);
            positionManager = new PositionManager(dbManager);
        }

        [HttpGet]
        public IActionResult Index(string find) {

            var empls = String.IsNullOrEmpty(find) ?
                employeeManager.GetAll().ToList() :
                employeeManager.GetEmployeesByStringFind(find);

            dbManager.Commit();
            return View("Staff", empls);
        }


        [HttpGet]
        public IActionResult Add(AddEmployeeDTO dto) {
            var model = employeeManager.GetAddEmployeeViewModel(dto);
            dbManager.Commit();
            return View("Add", model);
        }


        [HttpPost]
        public IActionResult Add(AddEmployeeViewModel dto) {


            if (!ModelState.IsValid) {
                dto.Positions = dbManager.GetAll<Position>().ToList();
                dbManager.Commit();
                return View("Add", dto);
            }

            var result = employeeManager.AddAsync(dto);

            //string message = result ? "Объект сохранён" : "Не удалось";

            dbManager.Commit();
            string message = result != null ? $"Объект сохранён" : "Не удалось";
            var res = new ResultViewModel(message, message, $"/staff", "Назад");

            return View("Result", res);
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            var result = employeeManager.PseudoDelete(id);
            dbManager.Commit();
            string message = result ? $"Объект удалён" : "Не удалось";
            var res = new ResultViewModel(message, message, $"/staff", "Назад");

            return View("Result", res);
        }

    }

}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;
using WebApplication3.Models.Services;
using WebApplication3.Models.ViewModels;


namespace WebApplication3.Controllers {
    public class StaffController : Controller {

        private IDbManager dbManager;
        private PositionManager positionManager;
        private EmployeeManager employeeManager;

        public StaffController() {
            dbManager = new DbManager();
            employeeManager = new EmployeeManager(dbManager);
            positionManager = new PositionManager(dbManager);
        }

        [HttpGet]
        public IActionResult Index(string find) {

            var empls = String.IsNullOrEmpty(find) ? 
                employeeManager.GetAllEmployee().ToList() :
                employeeManager.GetEmployeesByStringFind(find);

            dbManager.Commit();
            return View("Staff", empls);
        }


        [HttpGet]
        public IActionResult Add(int id) {
            ViewBag.Positions = positionManager.GetAll().ToList();
            Employee emp = id > 0 ?
                employeeManager.GetEmployeeById(id) :
                new Employee();

            dbManager.Commit();
            return View("Add", emp);
        }


        [HttpPost]
        public IActionResult Add(StaffAddDTO emp) {


            var result = employeeManager.CreateNewEmployee(emp);

            string message = result ? "Объект сохранён" : "Не удалось";

            dbManager.Commit();
            return View("Result", message);
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            bool result = employeeManager.DeleteEmployee(id);
            string message = result ? "Объект удалён" : "Не удалось";

            dbManager.Commit();
            return View("Result", message);
        }

    }

}

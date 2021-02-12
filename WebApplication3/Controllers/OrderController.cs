using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;
using WebApplication3.Models.Services;

namespace WebApplication3.Controllers {
    public class OrderController : Controller {


        private IDbManager dbManager;
        private OrderManager orderManager;
        private EmployeeManager employeeManager;
        private OrderAreaManager areaManager;
        private MachineryOnShiftManager machOnShiftManager;
        private MachineryManager machMan;


        public OrderController() {
            dbManager = new DbManager();
            orderManager = new OrderManager(dbManager);
            employeeManager = new EmployeeManager(dbManager);
            areaManager = new OrderAreaManager(dbManager);
            machOnShiftManager = new MachineryOnShiftManager(dbManager);
            machMan = new MachineryManager(dbManager);

        }

        private void FillViewBagForIndex(Order order) {
            ViewBag.Areas = areaManager.GetAll();
            ViewBag.Disps = employeeManager.GetFreeDispetchers(order);
            ViewBag.Chiefs = employeeManager.GetFreeChiefs(order);
            ViewBag.Masters = employeeManager.GetFreeMasters(order);
            ViewBag.Machines = orderManager.GetAddingListMachinesExcludeRepeats(order);
            ViewBag.MachOnShift = order?.Machineries;
        }

        [HttpGet]
        public IActionResult Index(DateTime Date, int Shift = 1, int OrderAreaId = 1, int IdOrder = 0) {
            if (Date.Year < 1950)
                Date = DateTime.Now.Date;

            Order order = IdOrder > 0 ? orderManager.GetById(IdOrder) : orderManager.Get(Date, Shift, OrderAreaId);

            FillViewBagForIndex(order);

            dbManager.Commit();
            return View("Order", order);
        }

        [HttpPost]
        public IActionResult Index(DateTime date, int shift, int orderAreaId, int Dispetcher, int Chief, int[] Masters, int orderId) {
            var area = areaManager.GetAll().Where(x => x.Id == orderAreaId).First();

            var disp = employeeManager.GetEmployeeById(Dispetcher);
            var chief = employeeManager.GetEmployeeById(Chief);
            var masters = new List<Employee>();
            foreach (var id in Masters)
                masters.Add(employeeManager.GetEmployeeById(id));
            Order order = orderId > 0 ? orderManager.GetById(orderId) : new Order().SetBase(date, shift);


            order
                .SetStaff(disp, chief, masters)
                .SetArea(area);

            if (orderId == 0)
                orderManager.Create(order);
            // И так сохраняет


            FillViewBagForIndex(order);
            dbManager.Commit();

            return View("Order", order);
        }


        private void FillViewBagForAdding(int order, MachineryOnShift mach) {
            ViewBag.OrderId = order;
            ViewBag.Title = mach.Name;
            ViewBag.Areas = machOnShiftManager.GetAreas();
            ViewBag.Fields = machOnShiftManager.GetFields();
            ViewBag.Horizons = machOnShiftManager.GetHorizons();
            ViewBag.Groups = machOnShiftManager.GetGroups();
            ViewBag.Plasts = machOnShiftManager.GetPlasts();
            ViewBag.Crew = employeeManager.GetFreeDrivers(order, mach.Id);

        }

        [HttpGet]
        public IActionResult AddMachineToOrder(int order = 0, int machine = 0, int mosId = 0) {
            //mosId - MachineryOnShiftId
            MachineryOnShift mach;
            if (mosId > 0) {
                mach = machOnShiftManager.GetById(mosId);
                order = mach.Order.Id;
            } else {
                mach = new MachineryOnShift(machMan.GetById(machine));
            }


            FillViewBagForAdding(order, mach);

            dbManager.Commit();
            return View("AddMachineToOrder", mach);
        }


        [HttpPost]
        public IActionResult AddMachineToOrder(
            int OrderId,
            int machine,
            int area,
            int field,
            int plast,
            int horizon,
            int group,
            int[] crew,
            bool HighAsh,
            bool PZO,
            int number = 0,
            double picket = 0,
            double weight = 0,
            double volume = 0,
            double overex = 0,
            double ash = 0,
            double heat = 0,
            double wet = 0,
            int transport = 0,
            int repair = 0,
            int holidays = 0,
            int machId = 0
            ) {

            var Area = machOnShiftManager.GetAreas().Where(x => x.Id == area).First();
            var Field = machOnShiftManager.GetFields().Where(x => x.Id == field).First();
            var Horizon = machOnShiftManager.GetHorizons().Where(x => x.Id == horizon).First();
            var Group = machOnShiftManager.GetGroups().Where(x => x.Id == group).First();
            var Plast = machOnShiftManager.GetPlasts().Where(x => x.Id == plast).First();
            var empls = employeeManager.GetAllEmployee();
            IList<Employee> Crew = new List<Employee>();
            foreach (var id in crew) {
                Crew.Add(empls.Where(x => x.Id == id).First());
                if (Crew.Count >= 10)
                    break;
            }

            var order = orderManager.GetById(OrderId);
            MachineryOnShift Machine;

            if (machId > 0) {
                Machine = machOnShiftManager.GetById(machId);
                machine = Machine.Id;
            } else {
                var mach = machMan.GetById(machine);
                Machine = new MachineryOnShift(mach);
                order.AddMachines(Machine);
                Machine.SetOrder(order);
            }
            Machine.SetLocation(Area, Field, Horizon, Plast, picket)
                .SetGroup(Group, number)
                .SetOrderProperties(weight, volume, overex, ash, heat, wet, HighAsh)
                .SetDownTime(transport, repair, holidays)
                .SetCrew(Crew)
                .SetPZO(PZO);
            var result = machId > 0 ? machOnShiftManager.Update(Machine) : machOnShiftManager.Create(Machine);
            dbManager.Commit();

            string message = result ? $"Объект {Machine.Name} сохранён" : "Не удалось";

            ViewBag.Link = $"/order?IdOrder={order.Id}";
            ViewBag.Button = "Назад";
            return View("Result", message);
        }

        [HttpGet]
        public IActionResult Delete(int mosId, int IdOrder) {
            var result = machOnShiftManager.DeleteById(mosId);
            dbManager.Commit();
            string message = result ? "Объект удалён" : "Не удалось";
            ViewBag.Link = $"/order?IdOrder={IdOrder}";
            ViewBag.Button = "Назад";
            return View("Result", message);
        }

    }
}

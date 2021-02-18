using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication3.Models.Entities;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.ViewModels;

namespace WebApplication3.Models.Services {
    public class EmployeeManager {

        private IDbManager dbManager;

        public EmployeeManager(IDbManager dbManager) {
            this.dbManager = dbManager;
        }

        public bool CreateNewEmployee(Employee emp) {
            bool result;
            if (IsValid(emp, true)) {
                result = dbManager.Add(emp);
            } else {
                result = false;
            }

            return result;
        }

        public bool CreateNewEmployee(StaffAddDTO model) {
            bool result = false;
            Employee emp;
            model.Position = dbManager.GetById<Position>(model.PosId);
                
            if (model.Id > 0) {
                emp = GetEmployeeById(model.Id);
                if (IsNotTrueTableNumber(model.TableNumber) <= 1) {
                    emp.Create(model);
                    result = true;
                }

            } else {
                emp = new Employee();
                if (IsNotTrueTableNumber(model.TableNumber) == 0) {
                    emp.Create(model);
                    result = dbManager.Add(emp);
                }
            }

            return result;
        }

        public bool DeleteEmployee(Employee emp) {
            emp.SetNulls();
            return dbManager.Delete(emp);
        }

        public bool DeleteEmployee(int id)
            => DeleteEmployee(GetEmployeeById(id));

        public IQueryable<Employee> GetAllEmployee()
            => dbManager.GetAll<Employee>();
        public List<Order> GetAllOrderOnThisDateAndShift(Order order)
            => dbManager.GetAll<Order>().Where(x => x.Date == order.Date && x.Shift == order.Shift).ToList();

        public IList<Employee> GetFreeDispetchers(Order order) {
            var orders = GetAllOrderOnThisDateAndShift(order).Where(x => x.Id != order.Id).ToList();
            var busyEmpls = new List<Employee>();
            orders.ForEach(x => busyEmpls.Add(x.Dispetcher));
            if (busyEmpls.Count == 0)
                busyEmpls.Add(new Employee());
            var freeEmpls = GetEmployeesByStringFind("Диспетчер").Difference(busyEmpls);

            return freeEmpls;
        }

        public IList<Employee> GetFreeChiefs(Order order) {
            var orders = GetAllOrderOnThisDateAndShift(order).Where(x => x.Id != order.Id).ToList();
            var busyEmpls = new List<Employee>();
            orders.ForEach(x => busyEmpls.Add(x.Chief));
            if (busyEmpls.Count == 0)
                busyEmpls.Add(new Employee());
            var freeEmpls = GetEmployeesByStringFind("Начальник").Difference(busyEmpls);

            return freeEmpls;
        }
        public IList<Employee> GetFreeMasters(Order order) {
            var orders = GetAllOrderOnThisDateAndShift(order).Where(x => x.Id != order.Id).ToList();
            var busyEmpls = new List<Employee>();
            orders.ForEach(x => busyEmpls.AddRange(x.MiningMaster));
            if (busyEmpls.Count == 0)
                busyEmpls.Add(new Employee());
            var freeEmpls = GetEmployeesByStringFind("Горный мастер").Difference(busyEmpls);

            return freeEmpls;
        }
        public IList<Employee> GetFreeDrivers(Order order, int machId) {
            var machs = GetAllBusyMachinesOnThisDateAndShift(order).Where(x => x.Id != machId).ToList();

            var busyEmpls = new List<Employee>();
            machs?.ForEach(x => busyEmpls.AddRange(x.Crew));
            if (busyEmpls.Count == 0)
                busyEmpls.Add(new Employee());
            var freeEmpls = GetEmployeesByStringFind("Машинист").Difference(busyEmpls);

            return freeEmpls;
        }
        public IList<Employee> GetFreeDrivers(int orderId, int machId)
            => GetFreeDrivers(dbManager.GetById<Order>(orderId), machId);

        public List<MachineryOnShift> GetAllBusyMachinesOnThisDateAndShift(Order order) {
            var machs = new List<MachineryOnShift>();
            GetAllOrderOnThisDateAndShift(order).ForEach(x => x.Machineries.ToList().ForEach(x => machs.Add(x)));
            return machs;
        }



        public Employee GetEmployeeById(int id)
            => dbManager.GetById<Employee>(id);

        public IList<Employee> GetEmployeeByPosition(Position position)
            => dbManager.GetAll<Employee>().Where(x => x.Position.Id == position.Id).ToList();

        public IList<Employee> GetEmployeesByStringFind(string find)
            => GetAllEmployee().ToList().Where(x => x.ToString().ToUpper().Contains(find.ToUpper())).ToList();


        public bool IsValid(Employee emp, bool newEmployee) {



            return true;
        }

        private int IsNotTrueTableNumber(int tableNumber) {
            if (tableNumber > 1000 && tableNumber < 1000000)
                return GetAllEmployee().Where(x => x.TableNumber == tableNumber).Count();
            else
                return 999;
            }


    }
}

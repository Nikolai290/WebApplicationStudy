﻿using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication3.Models.Entities;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models;

namespace WebApplication3.Models.Services {
    public class EmployeeManager {

        //PositionManager positionManager;
        private IDbManager dbManager;
        OrderManager orderManager;

        public EmployeeManager(IDbManager dbManager) {
            this.dbManager = dbManager;
            //positionManager = new PositionManager(dbManager);
            orderManager = new OrderManager(dbManager);
        }

        public bool CreateNewEmployee(Employee emp) {
            bool result;
            if (IsValid(emp, true)) {
                result = dbManager.Add<Employee>(emp);
            } else {
                result = false;
            }

            return result;
        }

        public bool DeleteEmployee(Employee emp) {
            emp.SetNulls();
            return dbManager.Delete(emp);
        }

        public bool DeleteEmployee(int id)
            => DeleteEmployee(GetEmployeeById(id));

        public IList<Employee> GetAllEmployee() 
            => dbManager.GetAll<Employee>().ToList();

        public IList<Employee> GetFreeDispetchers(Order order) {
            var orders = orderManager.GetAllOrderOnThisDateAndShift(order).Where(x => x.Id != order.Id).ToList();
            var busyEmpls = new List<Employee>();
            orders.ForEach(x => busyEmpls.Add(x.Dispetcher));
            if (busyEmpls.Count == 0)
                busyEmpls.Add(new Employee());
            var freeEmpls = GetEmployeesByStringFind("Диспетчер").Difference(busyEmpls);

            return freeEmpls;
        }

        public IList<Employee> GetFreeChiefs(Order order) {
            var orders = orderManager.GetAllOrderOnThisDateAndShift(order).Where(x => x.Id != order.Id).ToList();
            var busyEmpls = new List<Employee>();
            orders.ForEach(x => busyEmpls.Add(x.Chief));
            if (busyEmpls.Count == 0)
                busyEmpls.Add(new Employee());
            var freeEmpls = GetEmployeesByStringFind("Начальник").Difference(busyEmpls);

            return freeEmpls;
        }
        public IList<Employee> GetFreeMasters(Order order) {
            var orders = orderManager.GetAllOrderOnThisDateAndShift(order).Where(x => x.Id != order.Id).ToList();
            var busyEmpls = new List<Employee>();
            orders.ForEach(x => busyEmpls.AddRange(x.MiningMaster));
            if (busyEmpls.Count == 0)
                busyEmpls.Add(new Employee());
            var freeEmpls = GetEmployeesByStringFind("Горный мастер").Difference(busyEmpls);

            return freeEmpls;
        }
        public IList<Employee> GetFreeDrivers(Order order, int machId) { 
            var machs = orderManager.GetAllBusyMachinesOnThisDateAndShift(order).Where(x => x.Id != machId).ToList();

            var busyEmpls = new List<Employee>();
            machs.ForEach(x => busyEmpls.AddRange(x.Crew));
            if (busyEmpls.Count == 0)
                busyEmpls.Add(new Employee());
            var freeEmpls = GetEmployeesByStringFind("Машинист").Difference(busyEmpls);

            return freeEmpls;
        }
        public IList<Employee> GetFreeDrivers(int orderId, int machId)
            => GetFreeDrivers(orderManager.GetById(orderId), machId);


            public Employee Compare(Employee emp) {
            if(emp.Position !=null)
                emp.Position = dbManager.GetById<Position>(emp.Position.Id);
            return emp;
        }

        

        public Employee GetEmployeeById(int id)
            => (dbManager.GetById<Employee>(id));

        public IList<Employee> GetEmployeeByPosition(Position position)
            => dbManager.GetAll<Employee>().Where(x => x.Position.Id == position.Id).ToList();

        public IList<Employee> GetEmployeesByStringFind(string find)
            => GetAllEmployee().Where(x => x.ToString().ToUpper().Contains(find.ToUpper())).ToList();


        public bool IsValid(Employee emp, bool newEmployee) {

            if (IsNullOrEmptyNameLastname(emp)) {
                throw new Exception("Ошибка! Введите имя и фамилю!");
            }
            if (IsVerylongName(emp)) {
                throw new Exception("Ошибка! Фамилия, имя или отчество не могут быть длиннее 20 символов!");
            }
            if (newEmployee)
                if (IsNotTrueTableNumber(emp)) {
                    throw new Exception("Ошибка! Табельный номер должен содержать 4-6 симоволов и не должен совпадать с существующими!");
                }
            //if (isNullOrEmptyPosition(emp)) {
            //    throw new Exception("Ошибка! Должность не заполнена или не подгружена из базы!");
            //}
            return true;
        }

        private bool IsNullOrEmptyNameLastname(Employee emp)
            => (String.IsNullOrEmpty(emp.Lastname) || String.IsNullOrEmpty(emp.Name));

        private bool IsVerylongName(Employee emp)
            => !(emp.Lastname.Length < 20 || emp.Name.Length < 20 || emp.Fathername.Length < 20);
        private bool IsNotTrueTableNumber(Employee emp)
            => !(emp.TableNumber > 1000 && emp.TableNumber < 1000000 &&
            GetAllEmployee().Where(x => x.TableNumber == emp.TableNumber).ToList().Count == 0);
        private bool IsNullOrEmptyPosition(Employee emp)
            => (emp.Position == null ||
            String.IsNullOrEmpty(emp.Position.Name) ||
            String.IsNullOrEmpty(emp.Position.Subname));



        public bool UpdateEmployee(Employee emp, int id) {
            bool result = false;
            if (IsValid(emp, false)) {
                Employee upd = GetEmployeeById(id);
                emp.CopyTo(upd);
                result = dbManager.Update(upd);
            }

            return result;
        }

    }
}

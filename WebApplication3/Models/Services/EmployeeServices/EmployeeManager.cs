using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication3.Models.Entities;
using WebApplication3.Models.DbAccess;
namespace WebApplication3.Models.Services.EmployeeServices {
    public class EmployeeManager {

        //PositionManager positionManager;
        DbManager dbManager = new DbManager();

        public EmployeeManager() {
            //positionManager = new PositionManager();
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
            
            return dbManager.Delete(emp);
        }

        public bool DeleteEmployee(int id)
            => DeleteEmployee(GetEmployeeById(id));

        public IList<Employee> GetAllEmployee() {
            var emps = dbManager.GetAll<Employee>();

            foreach (var emp in emps) {
                Compare(emp);
            }

            return emps;
        }
        
        public Employee Compare(Employee emp) {
            if(emp.Position !=null)
                emp.Position = dbManager.GetById<Position>(emp.Position.Id);
            return emp;
        }

        public Employee GetEmployeeById(int id)
            => Compare(dbManager.GetById<Employee>(id));

        public IList<Employee> GetEmployeeByPosition(Position position)
            => dbManager.GetAll<Employee>().Where(x => x.Position.Id == position.Id).ToList();

        public IList<Employee> GetEmployeesByStringFind(string find) {
            IList<Employee> result = new List<Employee>();

            var emps = GetAllEmployee();

            foreach (var emp in emps) {
                if (emp.ToString().ToUpper().Contains(find.ToUpper())) {
                    result.Add(emp);
                }
            }


            return result;
        }


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
            => !(emp.TableNumber > 1000 & emp.TableNumber < 1000000 &
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


        public Employee GetEmployee(Employee emp) {
            if (IsValid(Compare(emp), false)) {
                return dbManager.Get(emp);
            }
            return null;
        }
    }
}

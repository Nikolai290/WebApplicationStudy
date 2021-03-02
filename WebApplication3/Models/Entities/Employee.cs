using System;
using System.Linq;
using System.Collections.Generic;
using WebApplication3.Models.ViewModels;

namespace WebApplication3.Models.Entities {
    public class Employee : DbEntities {
        public virtual string Name { get; protected set; }
        public virtual string Lastname { get; protected set; }
        public virtual string Fathername { get; protected set; }
        public virtual int TableNumber { get; protected set; }
        public virtual Position Position { get; set; }
        public virtual IList<Order> Orders { get; protected set; }
        public virtual IList<MachineryOnShift> Machinaries { get; protected set; }

        public Employee() { }

        public Employee(string name, string lastname, string fathername) {
            Name = name;
            Lastname = lastname;
            Fathername = fathername;
        }


        public Employee(string name, string lastname, string fathername, int tablenum) : this(name, lastname, fathername) {
            TableNumber = tablenum;
        }

        public Employee(string name, string lastname, string fathername, int tablenum, Position position) : this(name, lastname, fathername, tablenum) {
            this.Position = position;
            //position.Employees.Add(this);
        }

        public override string ToString() {
            string source = this.Id + " " + this.Lastname + " " + this.Name + " " + this.Fathername + " " + this.TableNumber;
            if (Position !=null)
                source += " " + this.Position.Name + " " + this.Position.Subname;
            

            return source;
        }
        public virtual Employee SetNameByFind(string find) {
            if (!String.IsNullOrEmpty(find)) {
                IList<string> lines = find.Trim().Split(" ").Where(x => !String.IsNullOrEmpty(x)).ToList();
                if(lines.Count >= 1)
                    Lastname = lines[0];
                if (lines.Count >= 2)
                    Name = lines[1];
                if (lines.Count >= 3) 
                    Fathername = lines[2];
                if (lines.Count >= 4)
                    TableNumber = Int32.TryParse(lines[3],out int number)? number : 0;

            }
            return this;
        }

        public virtual string GetFIO()
            => (Lastname + " " + Name + " " +Fathername);

        public virtual string GetShortFioProf() {
            string line = Lastname + " " + Name[0] + ". " + Fathername?[0] + ". (" + Position.Subname + ")";

            return line;
        }
        public virtual void SetNulls() {
            Position?.Employees.Remove(this);
            Position = null;
            Orders = null;
            Machinaries = null;
        }

        public virtual void CopyTo(Employee emp) {
            emp.Name = this.Name;
            emp.Lastname = this.Lastname;
            emp.Fathername = this.Fathername;
            emp.TableNumber = this.TableNumber;
            emp.Position = this.Position;
        }

        public virtual Employee Create(StaffAddDTO model) {
            Name = model.Name;
            Lastname = model.Lastname;
            Fathername = model.Fathername;
            TableNumber = model.TableNumber;
            Position = model.Position;

            return this;
        }
    }
}

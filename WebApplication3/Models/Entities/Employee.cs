using System.Collections.Generic;

namespace WebApplication3.Models.Entities {
    public class Employee : DbEntities {
        public virtual string Name { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string Fathername { get; set; }
        public virtual int TableNumber { get; set; }
        public virtual Position Position { get; set; }
        public virtual IList<Order> Orders { get; set; }

        public Employee() {
            new Position();
        }

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

        public virtual string GetFIO()
            => (Lastname + " " + Name + " " +Fathername);

        public virtual string GetShortFioProf() {
            string line = Lastname + " " + Name[0] + ". " + Fathername + ". (" + Position.Subname + ")";

            return line;
        }

        public virtual void CopyTo(Employee emp) {
            emp.Name = this.Name;
            emp.Lastname = this.Lastname;
            emp.Fathername = this.Fathername;
            emp.TableNumber = this.TableNumber;
            emp.Position = this.Position;
        }

    }
}

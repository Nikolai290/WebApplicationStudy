using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Entities {
    public class Order : DbEntities {

        // Compare of Date, Shift and Area has been unique
        
        // Общие
        public virtual DateTime Date { get; protected set; }
        public virtual int Shift { get; protected set; }
        public virtual bool IsClose { get; protected set; }

        // Персонал
        public virtual Employee Dispetcher { get; protected set; }
        public virtual Employee Chief { get; protected set; }
        public virtual IList<Employee> MiningMaster { get; protected set; }

        // Добыча, вскрыша, автовсркыша
        public virtual OrderArea Area { get; protected set; }

        public virtual IList<MachineryOnShift> Machineries { get; protected set; }
        public virtual bool AllPzo { get; protected set; }

        public Order() {
            MiningMaster = new List<Employee>();
            Machineries = new List<MachineryOnShift>();
        }

        public virtual Order CloseInverse() {
            IsClose = !IsClose;
            return this;
        }

        public virtual Order SetBase(DateTime date, int shift) {
            Date = date;
            Shift = shift;
            return this;
        }

        public virtual Order SetStaff(Employee disp, Employee chief, IList<Employee> masters) {
            Dispetcher = disp;
            Chief = chief;
            MiningMaster = masters;
            return this;
        }

        public virtual Order SetArea(OrderArea area) {
            Area = area;
            return this;
        }

        public virtual Order SetMachineries(IList<MachineryOnShift> machs) {
            Machineries = machs;
            return this;
        }

        public virtual Order AddMachines(params MachineryOnShift[] machs) {
            foreach (var m in machs) {
                Machineries.Add(m);
            }
            return this;
        }

        public virtual Order AddMaster (params Employee[] masters) {
            foreach (var master in masters) {
                MiningMaster.Add(master);
            }
            return this;
        }

        public virtual Order InverseSetAllPZO() {
            AllPzo = !AllPzo;

            foreach (var m in Machineries) {
                m.PZO = AllPzo;
            }

            return this;
        }

    }
}

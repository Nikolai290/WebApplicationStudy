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
        public virtual List<Employee> MiningMaster { get; protected set; }

        // Добыча, вскрыша, автовсркыша
        public virtual OrderArea Area { get; protected set; }

        public virtual List<MachineryOnShift> Machineries { get; protected set; }






        public Order() { }

    }
}

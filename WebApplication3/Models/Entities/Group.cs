using System.Collections.Generic;

namespace WebApplication3.Models.Entities {
    public class Group : DbEntities {

        public virtual int Number { get; protected set; }
        public virtual IList<MachineryOnShift> MachineryOnShift { get; protected set; }

        public Group() { }
        public Group(int num) {
            if (IsValid(num))
                Number = num;
        }

        private bool IsValid(int num)
            => (num > 0 && num < 1000);
    }
}

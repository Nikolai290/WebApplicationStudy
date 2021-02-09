using FluentNHibernate.Mapping;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Mapping {
    public class OrderMap : ClassMap<Order> {
        public OrderMap() {
            // Base
            Id(x => x.Id);
            Map(x => x.Date);
            Map(x => x.Shift);
            Map(x => x.IsClose);

            // Employees
            References(x => x.Dispetcher).Not.LazyLoad();
            References(x => x.Chief).Not.LazyLoad();
            HasManyToMany(x => x.MiningMaster).Not.LazyLoad(); // max 10

            // Other
            References(x => x.Area);

            HasMany(x => x.Machineries).Not.LazyLoad();


        }
    }
}

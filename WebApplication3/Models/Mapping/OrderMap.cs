using FluentNHibernate.Mapping;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Mapping {
    public class OrderMap : ClassMap<Order> {
        public OrderMap() {
            // Base
            Id(x => x.Id);
            Map(x => x.IsDelete);
            Map(x => x.Date);
            Map(x => x.Shift);
            Map(x => x.IsClose);

            // Employees
            References(x => x.Dispetcher).Not.LazyLoad().Cascade.All();
            References(x => x.Chief).Not.LazyLoad().Cascade.All();
            HasManyToMany(x => x.MiningMaster).Not.LazyLoad().Cascade.All(); // max 10

            // Other
            References(x => x.Area).Cascade.All();

            HasMany(x => x.Machineries).Not.LazyLoad().Cascade.All();


        }
    }
}

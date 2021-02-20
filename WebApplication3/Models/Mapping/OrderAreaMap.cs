using FluentNHibernate.Mapping;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Mapping {
    public class OrderAreaMap : ClassMap<OrderArea> {
        public OrderAreaMap() {
            Id(x => x.Id);
            Map(x => x.IsDelete);
            Map(x => x.Name);
            HasMany(x => x.Orders)
                .Inverse()
                .Cascade.All();
        }
    }
}

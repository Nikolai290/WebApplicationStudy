using FluentNHibernate.Mapping;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Mapping {
    public class PositionMap : ClassMap<Position> {
        
        public PositionMap() {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Subname);
            HasMany(x => x.Employees)
                .Inverse()
                .Cascade.All()
                .Not.LazyLoad();
        }
    }
}

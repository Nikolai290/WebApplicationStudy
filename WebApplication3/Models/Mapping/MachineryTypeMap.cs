using FluentNHibernate.Mapping;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Mapping {
    public class MachineryTypeMap : ClassMap<MachineryType> {
        public MachineryTypeMap() {
            Id(x => x.Id);
            Map(x => x.IsDelete);
            Map(x => x.Name);
            HasManyToMany(x => x.Areas)
                .Cascade.All().Not.LazyLoad();
            HasMany(x => x.Machineries)
                .Inverse()
                .Cascade.All();
        }
    }
}

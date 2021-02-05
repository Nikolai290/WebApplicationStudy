using FluentNHibernate.Mapping;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Mapping {
    public class GroupMap : ClassMap<Group> {
        public GroupMap() {
            Id(x => x.Id);
            Map(x => x.Number);
            HasMany(x => x.MachineryOnShift)
                .Inverse()
                .Cascade.All();
        }
    }
}

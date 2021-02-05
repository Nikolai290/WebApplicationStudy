using FluentNHibernate.Mapping;
using WebApplication3.Models.Entities;
namespace WebApplication3.Models.Mapping {
    public class QuarryPlastMap : ClassMap<QuarryPlast> {
        public QuarryPlastMap() {
            Id(x => x.Id);
            Map(x => x.Name);
            HasMany(x => x.MachineryOnShift)
                .Inverse()
                .Cascade.All();
        }
    }
}
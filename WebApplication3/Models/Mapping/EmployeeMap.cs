using FluentNHibernate.Mapping;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Mapping {
    public class EmployeeMap : ClassMap<Employee> {
        
        public EmployeeMap() {
            Id(x => x.Id);
            Map(x => x.IsDelete);
            Map(x => x.Name);
            Map(x => x.Lastname);
            Map(x => x.Fathername);
            Map(x => x.TableNumber);
            References(x => x.Position).Not.LazyLoad();
            HasManyToMany(x => x.Orders);
            HasManyToMany(x => x.Machinaries);
        }
    }
}

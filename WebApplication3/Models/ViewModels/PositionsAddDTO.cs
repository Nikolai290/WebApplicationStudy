﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.ViewModels {
    public class PositionsAddDTO {

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Subname { get; set; }
        public virtual IList<Employee> Employees { get; set; }
        public virtual IList<int> emplsIds { get; set; }
    }
}
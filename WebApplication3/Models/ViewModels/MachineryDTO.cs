﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.ViewModels {
    public class MachineryDTO {

        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public MachineryType Type { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;
using WebApplication3.Models.DbAccess;

namespace WebApplication3.Models.Services {

    public class OrderAreaManager {
        DbManager dbManager = new DbManager();


        public IList<OrderArea> GetAll()
            => dbManager.GetAll<OrderArea>();
    }
}

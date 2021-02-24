using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.ViewModels;

namespace WebApplication3.Models.Services {
    public class MachineryTypesManager {
        private readonly IDbManager dbManager;

        public MachineryTypesManager(IDbManager dbManager) {
            this.dbManager = dbManager;
        }

        internal object GetMachineryTypesViewModel(int typeId) {
            throw new NotImplementedException();
        }

        internal object SaveOrUpdateMachineryType(MachineryDTO dto) {
            throw new NotImplementedException();
        }

        internal void Delete(object machineId) {
            throw new NotImplementedException();
        }
    }
}

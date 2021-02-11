using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.DbAccess {
    public interface IDbManager {
        bool Add<T>(T obj) where T : DbEntities;
        bool Update<T>(T obj) where T : DbEntities;
        T GetById<T>(int id) where T : DbEntities;
        IList<T> GetAll<T>() where T : DbEntities;
        bool DeleteById<T>(int id) where T : DbEntities;
        bool Delete<T>(T obj) where T : DbEntities;
        void Commit();
        IList<T> GetAllByString<T>(string entityName) where T : DbEntities;
    }
}

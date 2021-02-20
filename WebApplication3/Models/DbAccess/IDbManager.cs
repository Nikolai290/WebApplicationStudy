using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.DbAccess {
    public interface IDbManager {
        bool AddAsync<T>(T obj) where T : DbEntities;
        T GetById<T>(int id) where T : DbEntities;
        T GetByIdForce<T>(int id) where T : DbEntities;
        IList<T> GetByListId<T>(IList<int> ids) where T : DbEntities;
        IQueryable<T> GetAll<T>() where T : DbEntities;
        IQueryable<T> GetAllForce<T>() where T : DbEntities;
        bool PseudoDelete<T>(int id) where T : DbEntities;
        //bool DeleteById<T>(int id) where T : DbEntities;
        //bool DeleteAsync<T>(T obj) where T : DbEntities;
        void Commit();
        //Task<T> GetByIdAsync<T>(int id) where T : DbEntities;

    }
}

using ApiDairy.Data.Interfaces;
using ApiDairy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDairy.Data.Repositories
{
    public class ClassRepository : IBaseRepository<Class>
    {
        private DataContext dbClass;

        public ClassRepository(DataContext _dbClass)
        {
            dbClass = _dbClass;
        }

        #region CRUD+
        public void Create(Class @class)
        {
            dbClass.Classes.Add(@class);
        }

        public void Delete(int id)
        {
            Class c = dbClass.Classes.Find(id);
            if (c != null)
                dbClass.Classes.Remove(c);
        }

        public Class Get(string id)
        {
            return dbClass.Classes.Find(id);
        }

        public async Task<ActionResult<IEnumerable<Class>>> GetAll()
        {
            return await dbClass.Classes.ToListAsync();
        }

        public void Save()
        {
            dbClass.SaveChanges();
        }

        public void Update(Class @class)
        {
            dbClass.Entry(@class).State = EntityState.Modified;
        }
        #endregion

        #region Dispose
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbClass.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}

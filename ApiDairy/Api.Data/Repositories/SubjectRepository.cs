using ApiDairy.Data.Interfaces;
using ApiDairy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDairy.Data.Repositories
{
    public class SubjectRepository : IBaseRepository<Subject>
    {
        private DataContext dbSub;

        public SubjectRepository(DataContext _dbSub)
        {
            dbSub = _dbSub;
        }

        #region CRUD+
        public void Create(Subject sub)
        {
            dbSub.Subjects.Add(sub);
        }

        public void Delete(int id)
        {
            Subject s = dbSub.Subjects.Find(id);
            if (s != null)
                dbSub.Subjects.Remove(s);
        }

        public Subject Get(string id)
        {
            return dbSub.Subjects.Find(id);
        }

        public async Task<ActionResult<IEnumerable<Subject>>> GetAll()
        {
            return await dbSub.Subjects.ToListAsync();
        }

        public void Save()
        {
            dbSub.SaveChanges();
        }

        public void Update(Subject sub)
        {
            dbSub.Entry(sub).State = EntityState.Modified;
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
                    dbSub.Dispose();
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
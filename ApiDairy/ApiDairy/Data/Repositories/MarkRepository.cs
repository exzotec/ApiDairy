using ApiDairy.Data.Interfaces;
using ApiDairy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDairy.Data.Repositories
{
    public class MarkRepository : IBaseRepository<Mark>
    {
        private DataContext dbMark;

        public MarkRepository(DataContext _dbMark)
        {
            dbMark = _dbMark;
        }

        #region CRUD+
        public void Create(Mark mark) //
        {
            dbMark.Marks.Add(mark);
        }

        public void Delete(int id) //
        {
            Mark mark = dbMark.Marks.Find(id);
            if (mark != null)
                dbMark.Marks.Remove(mark);
        }

        public Mark Get(string id) //
        {
            return dbMark.Marks.Find(id);
        }

        public async Task<ActionResult<IEnumerable<Mark>>> GetAll() //
        {
            return await dbMark.Marks.ToListAsync();
        }

        public void Save() //
        {
            dbMark.SaveChanges();
        }

        public void Update(Mark mark) //
        {
            dbMark.Entry(mark).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                    dbMark.Dispose();
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

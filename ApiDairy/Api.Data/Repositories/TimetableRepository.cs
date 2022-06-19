using ApiDairy.Data.Interfaces;
using ApiDairy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDairy.Data.Repositories
{
    public class TimetableRepository : IBaseRepository<Timetable>
    {
        private DataContext dbTT;

        public TimetableRepository(DataContext _dbTT)
        {
            dbTT = _dbTT;
        }

        #region CRUD+
        public void Create(Timetable timetable) //
        {
            dbTT.Timetables.Add(timetable);
        }

        public void Delete(int id) //
        {
            Timetable timetable = dbTT.Timetables.Find(id);
            if (timetable != null)
                dbTT.Timetables.Remove(timetable);
        }

        public Timetable Get(string id) //
        {
            return dbTT.Timetables.Find(id);
        }

        public async Task<ActionResult<IEnumerable<Timetable>>> GetAll() //
        {
            return await dbTT.Timetables.ToListAsync();
        }

        public void Save() //
        {
            dbTT.SaveChanges();
        }

        public void Update(Timetable timetable) //
        {
            dbTT.Entry(timetable).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                    dbTT.Dispose();
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

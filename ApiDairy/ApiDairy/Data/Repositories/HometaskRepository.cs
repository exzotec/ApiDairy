using ApiDairy.Data.Interfaces;
using ApiDairy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ApiDairy.Data.Repositories
{
    public class HometaskRepository : IBaseRepository<Hometask>
    {
        private DataContext dbHT;

        public HometaskRepository(DataContext _dbHT)
        {
            dbHT = _dbHT;
        }

        #region CRUD+
        public void Create(Hometask hometask) //
        {
            dbHT.Hometasks.Add(hometask);
        }

        public void Delete(int id) //
        {
            Hometask hometask = dbHT.Hometasks.Find(id);
            if (hometask != null)
                dbHT.Hometasks.Remove(hometask);
        }

        public Hometask Get(string id) //
        {
            return dbHT.Hometasks.Find(id);
        }

        public async Task<ActionResult<IEnumerable<Hometask>>> GetAll() //
        {
            return await dbHT.Hometasks.ToListAsync();
        }

        public void Save() //
        {
            dbHT.SaveChanges();
        }

        public void Update(Hometask hometask) //
        {
            dbHT.Entry(hometask).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                    dbHT.Dispose();
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

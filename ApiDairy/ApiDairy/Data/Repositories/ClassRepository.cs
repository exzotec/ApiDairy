using ApiDairy.Data.Interfaces;
using ApiDairy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApiDairy.Data.Repositories
{
    public class KlassRepository : IBaseRepository<Klass>
    {
        private DataContext dbKlass;

        public KlassRepository(DataContext _dbKlass)
        {
            dbKlass = _dbKlass;
        }

        #region CRUD+
        public void Create(Klass @class)
        {
            dbKlass.Classes.Add(@class);
        }

        public void Delete(int id)
        {
            Klass c = dbKlass.Classes.Find(id);
            if (c != null)
                dbKlass.Classes.Remove(c);
        }

        public Klass Get(string id)
        {
            return dbKlass.Classes.Find(id);
        }

        public async Task<ActionResult<IEnumerable<Klass>>> GetAll()
        {
            return await dbKlass.Classes.ToListAsync();
        }

        public void Save()
        {
            dbKlass.SaveChanges();
        }

        public void Update(Klass @class)
        {
            dbKlass.Entry(@class).State = EntityState.Modified;
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
                    dbKlass.Dispose();
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

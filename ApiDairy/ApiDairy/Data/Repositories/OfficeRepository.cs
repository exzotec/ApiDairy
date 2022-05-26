﻿using ApiDairy.Data.Interfaces;
using ApiDairy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ApiDairy.Data.Repositories
{
    public class OfficeRepository : IBaseRepository<Office>
    {
        private DataContext dbOffice;

        public OfficeRepository()
        {
            this.dbOffice = new DataContext();
        }

        #region CRUD+
        public void Create(Office office)
        {
            dbOffice.Offices.Add(office);
        }

        public void Delete(int id)
        {
            Office office = dbOffice.Offices.Find(id);
            if (office != null)
                dbOffice.Offices.Remove(office);
        }

        public Office Get(int id)
        {
            return dbOffice.Offices.Find(id);
        }

        public IEnumerable<Office> GetList()
        {
            return dbOffice.Offices;
        }

        public void Save()
        {
            dbOffice.SaveChanges();
        }

        public void Update(Office office)
        {
            dbOffice.Entry(office).State = EntityState.Modified;
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
                    dbOffice.Dispose();
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

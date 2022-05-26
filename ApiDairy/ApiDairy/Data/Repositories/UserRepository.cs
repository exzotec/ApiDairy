using ApiDairy.Data.Interfaces;
using ApiDairy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ApiDairy.Data.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private DataContext dbUser;

        public UserRepository()
        {
            this.dbUser = new DataContext();
        }

        #region CRUD+
        public void Create(User user) //
        {
            dbUser.Users.Add(user);
        }

        public void Delete(int id) //
        {
            User user = dbUser.Users.Find(id);
            if (user != null)
                dbUser.Users.Remove(user);
        }

        public User Get(int id) //
        {
            return dbUser.Users.Find(id);
        }

        public IEnumerable<User> GetList() //
        {
            return dbUser.Users;
        }

        public void Save() //
        {
            dbUser.SaveChanges();
        }

        public void Update(User user) //
        {
            dbUser.Entry(user).State = EntityState.Modified;
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
                    dbUser.Dispose();
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

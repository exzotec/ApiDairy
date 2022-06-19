using ApiDairy.Data.Interfaces;
using ApiDairy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDairy.Data.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private DataContext dbUser;

        public UserRepository(DataContext _dbUser)
        {
            dbUser = _dbUser;
        }

        #region CRUD+
        public void Create(User user) //
        {
            dbUser.users.Add(user);
        }

        public void Delete(int id) //
        {
            User user = dbUser.users.Find(id);
            if (user != null)
                dbUser.users.Remove(user);
        }

        public User Get(string id) //
        {
            return dbUser.users.Find(id);
        }

        public async Task<ActionResult<IEnumerable<User>>> GetAll() //
        {
            return await dbUser.users.ToListAsync();
        }

        public void Save() //
        {
            dbUser.SaveChanges();
        }

        public void Update(User user) //
        {
            dbUser.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

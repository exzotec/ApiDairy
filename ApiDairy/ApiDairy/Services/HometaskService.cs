using ApiDairy.Data;
using ApiDairy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDairy.Services
{
    public class HometaskService
    {
        private DataContext db;
        private IMemoryCache HometaskCache;
        public HometaskService(DataContext context, IMemoryCache memoryCache)
        {
            db = context;
            HometaskCache = memoryCache;
        }

        public async Task<ActionResult<IEnumerable<Hometask>>> GetAll() //
        {
            return await db.Hometasks.ToListAsync();
        }

        public async Task AddHometask(Hometask hometask)
        {
            db.Hometasks.Add(hometask);
            int n = await db.SaveChangesAsync();
            if (n > 0)
            {
                HometaskCache.Set(hometask.id, hometask, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1440)
                });
            }
        }

        public async Task<Hometask> GetHometask(int id)
        {
            Hometask hometask = null;
            if (!HometaskCache.TryGetValue(id, out hometask))
            {
                hometask = await db.Hometasks.FirstOrDefaultAsync(p => p.id == id);
                if (hometask != null)
                {
                    HometaskCache.Set(hometask.id, hometask,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }
            return hometask;
        }
    }
}

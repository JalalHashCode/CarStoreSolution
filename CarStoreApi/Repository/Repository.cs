﻿using CarStoreApi.Data;
using CarStoreApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarStoreApi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> DbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.DbSet = _db.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = DbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = DbSet;

            //Filteration
            if (filter != null)
            {
                query = query.Where(filter);
            }

           
            return await query.ToListAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            DbSet.Remove(entity);
            await SaveAsync();

        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        
    }
}

using Amplifier.AspNetCore.Entities;
using Amplifier.AspNetCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Amplifier.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Base class that implements <see cref="IRepositoryBase{TEntity, TKey}"/>
    /// </summary>
    /// <typeparam name="TEntity">Entity type of this repository.</typeparam>
    /// <typeparam name="TKey">Primary key of the Entity.</typeparam>
    public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// Entity Framework DbContext.
        /// </summary>
        protected readonly DbContext _dbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Create an Entity.
        /// </summary>
        /// <param name="entity">Entity to create.</param>
        /// <returns>Primary key type of the entity.</returns>
        public virtual TKey Create(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return entity.Id;
        }

        /// <summary>
        /// Create an Entity.
        /// </summary>
        /// <param name="entity">Entity to create.</param>
        /// <returns>Primary key type of the entity.</returns>
        public virtual async Task<TKey> CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        /// <summary>
        /// Delete an entity by given Id.
        /// </summary>
        /// <param name="id">Id of the entity.</param> 
        public virtual async Task DeleteAsync(TKey id)
        {
            var entity = (await GetAllListAsync(x => x is TEntity && EqualityComparer<TKey>.Default.Equals(id, (x as TEntity).Id))).FirstOrDefault();
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param> 
        public virtual void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="id">Id of the entity.</param> 
        public virtual void Delete(TKey id)
        {
            var entity = (GetAllList(x => x is TEntity && EqualityComparer<TKey>.Default.Equals(id, (x as TEntity).Id))).FirstOrDefault();
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }


        /// <summary>
        /// Get an IQueryable from the entire table.
        /// </summary>
        /// <returns>Entities IQueryable.</returns>
        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        /// <summary>
        /// Get all entities by given condition.
        /// </summary>
        /// <param name="expression">Lambda Expression.</param>
        /// <returns>Entities IEnumerable.</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbContext.Set<TEntity>().Where(expression).ToListAsync();
        }

        /// <summary>
        /// Get all entities by given condition.
        /// </summary>
        /// <param name="expression">Lambda Expression.</param>
        /// <returns>Entities IEnumerable.</returns>
        public virtual IEnumerable<TEntity> GetAllList(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Set<TEntity>().Where(expression).AsEnumerable();
        }

        /// <summary>
        /// Get all entities by given condition.
        /// </summary>        
        /// <returns>Entities IEnumerable.</returns>
        public virtual IEnumerable<TEntity> GetAllList()
        {
            return _dbContext.Set<TEntity>().AsEnumerable();
        }

        /// <summary>
        /// Get all entities by given condition.
        /// </summary>        
        /// <returns>Entities IEnumerable.</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllListAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        /// <summary>
        /// Get an Entity by Id.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        /// <returns>Entity</returns>
        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return (await GetAllListAsync(x => x is TEntity && EqualityComparer<TKey>.Default.Equals(id, (x as TEntity).Id))).FirstOrDefault();
        }

        /// <summary>
        /// Get an Entity by Id.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        /// <returns>Entity</returns>
        public virtual TEntity GetById(TKey id)
        {
            return (GetAllList(x => x is TEntity && EqualityComparer<TKey>.Default.Equals(id, (x as TEntity).Id))).FirstOrDefault();
        }

        /// <summary>
        /// Update an entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <returns>Updated entity.</returns>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Update an entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <returns>Updated entity.</returns>
        public virtual TEntity Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }        
    }
}

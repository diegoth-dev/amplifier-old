using Amplifier.AspNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Amplifier.AspNetCore.Repositories
{
    /// <summary>
    /// This Interface must be implemented by all repositories.
    /// </summary>
    /// <typeparam name="TEntity">Entity type of this repository</typeparam>
    /// <typeparam name="TKey">Primary key of the Entity</typeparam>
    public interface IRepositoryBase<TEntity, TKey>
           where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// Update an entity.
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <returns>Updated entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Update an entity.
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <returns>Updated entity</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Get all entities by given condition.
        /// </summary>
        /// <param name="expression">Lambda Expression</param>
        /// <returns>Entities IEnumerable</returns>
        Task<IEnumerable<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Get all entities by given condition.
        /// </summary>        
        /// <returns>IEnumerable of entities</returns>
        Task<IEnumerable<TEntity>> GetAllListAsync();

        /// <summary>
        /// Get an IEnumerable from the entire table.
        /// </summary>
        /// <param name="expression">Lambda Expression</param>
        /// <returns>Entities IQueryable.</returns>
        IEnumerable<TEntity> GetAllList(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Get an IEnumerable from the entire table.
        /// </summary>
        /// <returns>Entities IQueryable.</returns>
        IEnumerable<TEntity> GetAllList();

        /// <summary>
        /// Get an IQueryable from the entire table.
        /// </summary>
        /// <returns>Entities IQueryable.</returns>
        IQueryable<TEntity> GetAll();        

        /// <summary>
        /// Create an Entity.
        /// </summary>
        /// <param name="entity">Entity to create</param>
        /// <returns>Primary key type of the entity</returns>
        Task<TKey> CreateAsync(TEntity entity);

        /// <summary>
        /// Create an Entity.
        /// </summary>
        /// <param name="entity">Entity to create</param>
        /// <returns>Primary key type of the entity</returns>
        TKey Create(TEntity entity);

        /// <summary>
        /// Delete an entity by given Id.
        /// </summary>
        /// <param name="id">Id of the entity</param>        
        Task DeleteAsync(TKey id);

        /// <summary>
        /// Delete an entity by given Id.
        /// </summary>
        /// <param name="id">Id of the entity</param>        
        void Delete(TKey id);

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>        
        void Delete(TEntity entity);

        /// <summary>
        /// Get an Entity by Id.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>Entity</returns>
        Task<TEntity> GetByIdAsync(TKey id);

        /// <summary>
        /// Get an Entity by Id.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>Entity</returns>
        TEntity GetById(TKey id);
    }
}

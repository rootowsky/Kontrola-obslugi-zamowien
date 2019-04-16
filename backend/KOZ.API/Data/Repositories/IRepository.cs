using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KOZ.API.Data.Repositories
{
    public interface IRepository<T> : IReadRepository<T>
    {
        /// <summary>
        /// Inserts entity into repository.
        /// </summary>
        /// <param name="entity">The entity to be inserted.</param>
        void Insert(T entity);
        /// <summary>
        /// Deletes entity from repository.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        void Delete(T entity);
        /// <summary>
        /// Updates existing entity in repository. Nothing will be changed if entity does not exists in repository.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        void Update(T entity);
        /// <summary>
        /// Updates existing entity in repository or inserts new one if does not exists.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        void UpdateOrInsert(T entity);
        /// <summary>
        /// Saves the changes made in repository
        /// </summary>
        void Save();
    }
}

using System.Collections.Generic;

namespace KOZ.API.Data.Repositories
{
    public interface IReadRepository<out T>
    {
        /// <summary>
        /// Returns all entities.
        /// </summary>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Returns the entity by its identifier.
        /// </summary>
        /// <param name="entityId">The identifier of the entity</param>
        T GetById(int entityId);
    }
}

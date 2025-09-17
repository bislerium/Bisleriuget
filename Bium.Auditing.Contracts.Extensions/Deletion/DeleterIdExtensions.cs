using Bium.Auditing.Contracts.Deletion;

namespace Bium.Auditing.Contracts.Extensions.Deletion
{
    /// <summary>
    /// Provides extension methods for setting the deleter identifier on entities
    /// that implement <see cref="IHasDeleterId{TPrimaryKey}"/>.
    /// </summary>
    public static class DeleterIdExtensions
    {
        /// <summary>
        /// Sets the <see cref="IHasDeleterId{TPrimaryKey}.DeletedBy"/> property to the specified deleter identifier
        /// and marks the entity as deleted by setting <c>IsDeleted</c> to <c>true</c>.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the deleter's identifier (e.g., int, Guid).</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="deletedBy">The identifier of the deleter.</param>
        public static void SetDeletedBy<TPrimaryKey>(this IHasDeleterId<TPrimaryKey> entity, TPrimaryKey deletedBy)
            where TPrimaryKey : struct
        {
            entity.DeletedBy = deletedBy;
            entity.IsDeleted = true;
        }
    }
}
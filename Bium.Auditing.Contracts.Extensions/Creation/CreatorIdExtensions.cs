using Bium.Auditing.Contracts.Creation;

namespace Bium.Auditing.Contracts.Extensions.Creation
{
    /// <summary>
    /// Provides extension methods for setting the creator identifier on entities
    /// that implement <see cref="IHasCreatorId{TPrimaryKey}"/>.
    /// </summary>
    public static class CreatorIdExtensions
    {
        /// <summary>
        /// Sets the <see cref="IHasCreatorId{TPrimaryKey}.CreatedBy"/> property
        /// to the specified creator identifier.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the creator's identifier (e.g., int, Guid).</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="createdBy">The identifier of the creator.</param>
        public static void SetCreatedBy<TPrimaryKey>(this IHasCreatorId<TPrimaryKey> entity, TPrimaryKey createdBy)
            where TPrimaryKey : struct =>
            entity.CreatedBy = createdBy;
    }
}
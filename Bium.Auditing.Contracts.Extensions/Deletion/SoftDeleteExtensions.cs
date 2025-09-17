using Bium.Auditing.Contracts.Deletion;

namespace Bium.Auditing.Contracts.Extensions.Deletion
{
    /// <summary>
    /// Provides extension methods for marking entities as soft deleted.
    /// </summary>
    public static class SoftDeleteExtensions
    {
        /// <summary>
        /// Marks the entity as deleted by setting its <see cref="ISoftDeletable.IsDeleted"/> property to <c>true</c>.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public static void SetIsDeleted(this ISoftDeletable entity) =>
            entity.IsDeleted = true;
    }
}
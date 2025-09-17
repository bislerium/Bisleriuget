using System;
using Bium.Auditing.Contracts.Modification;

namespace Bium.Auditing.Contracts.Extensions.Modification
{
    /// <summary>
    /// Provides extension methods for setting modification audit properties on entities
    /// that implement <see cref="IModificationAudited{TPrimaryKey, TDateTime}"/>.
    /// </summary>
    public static class ModificationAuditExtensions
    {
        /// <summary>
        /// Sets the modification audit properties with the current UTC time
        /// for entities that use <see cref="DateTime"/> as the modification timestamp type.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the modifier's identifier (e.g., int, Guid).</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="modifiedBy">The identifier of the modifier.</param>
        public static void SetModified<TPrimaryKey>(this IModificationAudited<TPrimaryKey, DateTime> entity,
            TPrimaryKey modifiedBy)
            where TPrimaryKey : struct
        {
            entity.ModifiedAt = DateTime.UtcNow;
            entity.ModifiedBy = modifiedBy;
        }

        /// <summary>
        /// Sets the modification audit properties with the current UTC time
        /// for entities that use <see cref="DateTimeOffset"/> as the modification timestamp type.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the modifier's identifier (e.g., int, Guid).</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="modifiedBy">The identifier of the modifier.</param>
        public static void SetModified<TPrimaryKey>(this IModificationAudited<TPrimaryKey, DateTimeOffset> entity,
            TPrimaryKey modifiedBy)
            where TPrimaryKey : struct
        {
            entity.ModifiedAt = DateTimeOffset.UtcNow;
            entity.ModifiedBy = modifiedBy;
        }

        /// <summary>
        /// Sets the modification audit properties with the specified modification time.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the modifier's identifier (e.g., int, Guid).</typeparam>
        /// <typeparam name="TDateTime">The type of the modification timestamp (e.g., DateTime, DateTimeOffset).</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="modifiedBy">The identifier of the modifier.</param>
        /// <param name="modifiedAt">The modification timestamp.</param>
        public static void SetModified<TPrimaryKey, TDateTime>(this IModificationAudited<TPrimaryKey, TDateTime> entity,
            TPrimaryKey modifiedBy, TDateTime modifiedAt)
            where TPrimaryKey : struct
            where TDateTime : struct
        {
            entity.ModifiedAt = modifiedAt;
            entity.ModifiedBy = modifiedBy;
        }
    }
}
using System;
using Bium.Auditing.Contracts.Deletion;

namespace Bium.Auditing.Contracts.Extensions.Deletion
{
    /// <summary>
    /// Provides extension methods for setting deletion audit properties on entities
    /// that implement <see cref="IDeletionAudited{TPrimaryKey, TDateTime}"/>.
    /// </summary>
    public static class DeletionAuditExtensions
    {
        /// <summary>
        /// Sets the deletion audit properties with the current UTC time
        /// for entities that use <see cref="DateTime"/> as the deletion timestamp type.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the deleter's identifier (e.g., int, Guid).</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="createdBy">The identifier of the deleter.</param>
        public static void SetDeleted<TPrimaryKey>(this IDeletionAudited<TPrimaryKey, DateTime> entity,
            TPrimaryKey createdBy)
            where TPrimaryKey : struct
        {
            entity.DeletedAt = DateTime.UtcNow;
            entity.DeletedBy = createdBy;
            entity.IsDeleted = true;
        }

        /// <summary>
        /// Sets the deletion audit properties with the current UTC time
        /// for entities that use <see cref="DateTimeOffset"/> as the deletion timestamp type.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the deleter's identifier (e.g., int, Guid).</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="createdBy">The identifier of the deleter.</param>
        public static void SetDeleted<TPrimaryKey>(this IDeletionAudited<TPrimaryKey, DateTimeOffset> entity,
            TPrimaryKey createdBy)
            where TPrimaryKey : struct
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
            entity.DeletedBy = createdBy;
            entity.IsDeleted = true;
        }

        /// <summary>
        /// Sets the deletion audit properties with the specified deletion time.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the deleter's identifier (e.g., int, Guid).</typeparam>
        /// <typeparam name="TDateTime">The type of the deletion timestamp (e.g., DateTime, DateTimeOffset).</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="deletedBy">The identifier of the deleter.</param>
        /// <param name="deletedAt">The deletion timestamp.</param>
        public static void SetDeleted<TPrimaryKey, TDateTime>(this IDeletionAudited<TPrimaryKey, TDateTime> entity,
            TPrimaryKey deletedBy, TDateTime deletedAt)
            where TPrimaryKey : struct
            where TDateTime : struct
        {
            entity.DeletedAt = deletedAt;
            entity.DeletedBy = deletedBy;
            entity.IsDeleted = true;
        }
    }
}
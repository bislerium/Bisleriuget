using System;
using Bium.Auditing.Contracts.Deletion;

namespace Bium.Auditing.Contracts.Extensions.Deletion
{
    /// <summary>
    /// Provides extension methods for setting deletion time properties on entities
    /// that implement <see cref="IHasDeletionTime{TDateTime}"/>.
    /// </summary>
    public static class DeletionTimeExtensions
    {
        /// <summary>
        /// Sets the <see cref="IHasDeletionTime{DateTime}.DeletedAt"/> property
        /// to the current UTC <see cref="DateTime"/> and marks the entity as deleted.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public static void SetDeletedNow(this IHasDeletionTime<DateTime> entity)
        {
            entity.DeletedAt = DateTime.UtcNow;
            entity.IsDeleted = true;
        }

        /// <summary>
        /// Sets the <see cref="IHasDeletionTime{DateTimeOffset}.DeletedAt"/> property
        /// to the current UTC <see cref="DateTimeOffset"/> and marks the entity as deleted.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public static void SetDeletedNow(this IHasDeletionTime<DateTimeOffset> entity)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
            entity.IsDeleted = true;
        }

        /// <summary>
        /// Sets the <see cref="IHasDeletionTime{TDateTime}.DeletedAt"/> property
        /// to the specified <paramref name="deletedAt"/> value and marks the entity as deleted.
        /// </summary>
        /// <typeparam name="TDateTime">The type of the deletion timestamp (e.g., DateTime, DateTimeOffset).</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="deletedAt">The deletion timestamp to assign.</param>
        public static void SetDeletedAt<TDateTime>(this IHasDeletionTime<TDateTime> entity, TDateTime deletedAt)
            where TDateTime : struct
        {
            entity.DeletedAt = deletedAt;
            entity.IsDeleted = true;
        }
    }
}
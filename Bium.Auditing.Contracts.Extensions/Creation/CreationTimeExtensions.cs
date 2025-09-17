using System;
using Bium.Auditing.Contracts.Creation;

namespace Bium.Auditing.Contracts.Extensions.Creation
{
    /// <summary>
    /// Provides extension methods for setting creation time properties on entities
    /// that implement <see cref="IHasCreationTime{TDateTime}"/>.
    /// </summary>
    public static class CreationTimeExtensions
    {
        /// <summary>
        /// Sets the <see cref="IHasCreationTime{DateTime}.CreatedAt"/> property
        /// to the current UTC <see cref="DateTime"/>.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public static void SetCreatedNow(this IHasCreationTime<DateTime> entity) =>
            entity.CreatedAt = DateTime.UtcNow;

        /// <summary>
        /// Sets the <see cref="IHasCreationTime{DateTimeOffset}.CreatedAt"/> property
        /// to the current UTC <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public static void SetCreatedNow(this IHasCreationTime<DateTimeOffset> entity) =>
            entity.CreatedAt = DateTimeOffset.UtcNow;

        /// <summary>
        /// Sets the <see cref="IHasCreationTime{TDateTime}.CreatedAt"/> property
        /// to the specified <paramref name="createdAt"/> value.
        /// </summary>
        /// <typeparam name="TDateTime">The type of the creation timestamp (e.g., DateTime, DateTimeOffset).</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="createdAt">The creation timestamp to assign.</param>
        public static void SetCreatedAt<TDateTime>(this IHasCreationTime<TDateTime> entity, TDateTime createdAt)
            where TDateTime : struct => entity.CreatedAt = createdAt;
    }
}
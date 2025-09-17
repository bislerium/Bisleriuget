using System;
using Bium.Auditing.Contracts.Modification;

namespace Bium.Auditing.Contracts.Extensions.Modification
{
    /// <summary>
    /// Provides extension methods for setting modification time properties on entities
    /// that implement <see cref="IHasModificationTime{TDateTime}"/>.
    /// </summary>
    public static class ModificationTimeExtensions
    {
        /// <summary>
        /// Sets the <see cref="IHasModificationTime{DateTime}.ModifiedAt"/> property
        /// to the current UTC <see cref="DateTime"/>.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public static void SetModifiedNow(this IHasModificationTime<DateTime> entity) =>
            entity.ModifiedAt = DateTime.UtcNow;

        /// <summary>
        /// Sets the <see cref="IHasModificationTime{DateTimeOffset}.ModifiedAt"/> property
        /// to the current UTC <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public static void SetModifiedNow(this IHasModificationTime<DateTimeOffset> entity) =>
            entity.ModifiedAt = DateTimeOffset.UtcNow;

        /// <summary>
        /// Sets the <see cref="IHasModificationTime{TDateTime}.ModifiedAt"/> property
        /// to the specified <paramref name="modifiedAt"/> value.
        /// </summary>
        /// <typeparam name="TDateTime">The type of the modification timestamp (e.g., DateTime, DateTimeOffset).</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="modifiedAt">The modification timestamp to assign.</param>
        public static void SetModifiedAt<TDateTime>(this IHasModificationTime<TDateTime> entity, TDateTime modifiedAt)
            where TDateTime : struct =>
            entity.ModifiedAt = modifiedAt;
    }
}
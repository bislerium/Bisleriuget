using System;
using Bium.Auditing.Contracts.Creation;

namespace Bium.Auditing.Contracts.Extensions.Creation
{
    /// <summary>
    /// Provides extension methods for setting creation audit properties on entities
    /// that implement <see cref="ICreationAudited{TPrimaryKey, TDateTime}"/>.
    /// </summary>
    public static class CreationAuditExtensions
    {
        /// <summary>
        /// Sets the creation audit properties with the current UTC time
        /// for entities that use <see cref="DateTime"/> as the creation timestamp type.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the primary key (e.g., int, Guid).</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="createdBy">The identifier of the creator.</param>
        public static void SetCreated<TPrimaryKey>(this ICreationAudited<TPrimaryKey, DateTime> entity,
            TPrimaryKey createdBy)
            where TPrimaryKey : struct
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = createdBy;
        }

        /// <summary>
        /// Sets the creation audit properties with the current UTC time
        /// for entities that use <see cref="DateTimeOffset"/> as the creation timestamp type.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the primary key (e.g., int, Guid).</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="createdBy">The identifier of the creator.</param>
        public static void SetCreated<TPrimaryKey>(this ICreationAudited<TPrimaryKey, DateTimeOffset> entity,
            TPrimaryKey createdBy)
            where TPrimaryKey : struct
        {
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.CreatedBy = createdBy;
        }

        /// <summary>
        /// Sets the creation audit properties with the specified creation time.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the primary key (e.g., int, Guid).</typeparam>
        /// <typeparam name="TDateTime">The type of the creation timestamp (e.g., DateTime, DateTimeOffset).</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="createdBy">The identifier of the creator.</param>
        /// <param name="createdAt">The creation timestamp.</param>
        public static void SetCreated<TPrimaryKey, TDateTime>(this ICreationAudited<TPrimaryKey, TDateTime> entity,
            TPrimaryKey createdBy, TDateTime createdAt)
            where TPrimaryKey : struct
            where TDateTime : struct
        {
            entity.CreatedAt = createdAt;
            entity.CreatedBy = createdBy;
        }
    }
}
using System;
using Bium.Auditing.Contracts.Creation;
using Bium.Auditing.Contracts.Deletion;
using Bium.Auditing.Contracts.Modification;

namespace Bium.Auditing.Contracts.Extensions
{
    /// <summary>
    /// Provides extension methods for applying audit-related properties to entities
    /// that implement <see cref="IAuditKind"/>.
    /// </summary>
    public static class AuditKindExtensions
    {
        /// <summary>
        /// Attempts to set the creation time to the current UTC time.
        /// Supports <see cref="DateTime"/> and <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns><c>true</c> if the entity supports creation time; otherwise, <c>false</c>.</returns>
        public static bool TryApplyCreationTime(this IAuditKind entity)
        {
            switch (entity)
            {
                case IHasCreationTime<DateTime> d: d.CreatedAt = DateTime.UtcNow; break;
                case IHasCreationTime<DateTimeOffset> d: d.CreatedAt = DateTimeOffset.UtcNow; break;
                default: return false;
            }

            return true;
        }

        /// <summary>
        /// Attempts to set the creation time to the specified value.
        /// </summary>
        /// <typeparam name="TDateTime">The type of the creation timestamp.</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="createdAt">The creation time to assign.</param>
        /// <returns><c>true</c> if the entity supports creation time; otherwise, <c>false</c>.</returns>
        public static bool TryApplyCreationTime<TDateTime>(this IAuditKind entity, TDateTime createdAt)
            where TDateTime : struct
        {
            if (!(entity is IHasCreationTime<TDateTime> hasCreationTime)) return false;
            hasCreationTime.CreatedAt = createdAt;
            return true;
        }

        /// <summary>
        /// Attempts to set the modification time to the current UTC time.
        /// Supports <see cref="DateTime"/> and <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns><c>true</c> if the entity supports modification time; otherwise, <c>false</c>.</returns>
        public static bool TryApplyModificationTime(this IAuditKind entity)
        {
            switch (entity)
            {
                case IHasModificationTime<DateTime> d: d.ModifiedAt = DateTime.UtcNow; break;
                case IHasModificationTime<DateTimeOffset> dto: dto.ModifiedAt = DateTimeOffset.UtcNow; break;
                default: return false;
            }

            return true;
        }

        /// <summary>
        /// Attempts to set the modification time to the specified value.
        /// </summary>
        /// <typeparam name="TDateTime">The type of the modification timestamp.</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="modifiedAt">The modification time to assign.</param>
        /// <returns><c>true</c> if the entity supports modification time; otherwise, <c>false</c>.</returns>
        public static bool TryApplyModificationTime<TDateTime>(this IAuditKind entity, TDateTime modifiedAt)
            where TDateTime : struct
        {
            if (!(entity is IHasModificationTime<TDateTime> hasModificationTime)) return false;
            hasModificationTime.ModifiedAt = modifiedAt;
            return true;
        }

        /// <summary>
        /// Attempts to set the deletion time to the current UTC time.
        /// Supports <see cref="DateTime"/> and <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns><c>true</c> if the entity supports deletion time; otherwise, <c>false</c>.</returns>
        public static bool TryApplyDeletionTime(this IAuditKind entity)
        {
            switch (entity)
            {
                case IHasDeletionTime<DateTime> d: d.DeletedAt = DateTime.UtcNow; break;
                case IHasDeletionTime<DateTimeOffset> dto: dto.DeletedAt = DateTimeOffset.UtcNow; break;
                default: return false;
            }

            return true;
        }

        /// <summary>
        /// Attempts to set the deletion time to the specified value.
        /// </summary>
        /// <typeparam name="TDateTime">The type of the deletion timestamp.</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="deletedAt">The deletion time to assign.</param>
        /// <returns><c>true</c> if the entity supports deletion time; otherwise, <c>false</c>.</returns>
        public static bool TryApplyDeletionTime<TDateTime>(this IAuditKind entity, TDateTime deletedAt)
            where TDateTime : struct
        {
            if (!(entity is IHasDeletionTime<TDateTime> hasDeletionTime)) return false;
            hasDeletionTime.DeletedAt = deletedAt;
            return true;
        }

        /// <summary>
        /// Attempts to mark the entity as soft deleted.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns><c>true</c> if the entity supports soft deletion; otherwise, <c>false</c>.</returns>
        public static bool TryApplySoftDelete(this IAuditKind entity)
        {
            if (!(entity is ISoftDeletable softDeletable)) return false;
            softDeletable.IsDeleted = true;
            return true;
        }

        /// <summary>
        /// Attempts to set the creator identifier on the entity.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the creator's identifier.</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="createdBy">The identifier of the creator.</param>
        /// <returns><c>true</c> if the entity supports creator ID; otherwise, <c>false</c>.</returns>
        public static bool TryApplyCreatorId<TPrimaryKey>(this IAuditKind entity, TPrimaryKey createdBy)
            where TPrimaryKey : struct
        {
            if (!(entity is IHasCreatorId<TPrimaryKey> hasCreatorId)) return false;
            hasCreatorId.CreatedBy = createdBy;
            return true;
        }

        /// <summary>
        /// Attempts to set the modifier identifier on the entity.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the modifier's identifier.</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="modifiedBy">The identifier of the modifier.</param>
        /// <returns><c>true</c> if the entity supports modifier ID; otherwise, <c>false</c>.</returns>
        public static bool TryApplyModifierId<TPrimaryKey>(this IAuditKind entity, TPrimaryKey modifiedBy)
            where TPrimaryKey : struct
        {
            if (!(entity is IHasModifierId<TPrimaryKey> hasModifierId)) return false;
            hasModifierId.ModifiedBy = modifiedBy;
            return true;
        }

        /// <summary>
        /// Attempts to set the deleter identifier on the entity.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the deleter's identifier.</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="deletedBy">The identifier of the deleter.</param>
        /// <returns><c>true</c> if the entity supports deleter ID; otherwise, <c>false</c>.</returns>
        public static bool TryApplyDeleterId<TPrimaryKey>(this IAuditKind entity, TPrimaryKey deletedBy)
            where TPrimaryKey : struct
        {
            if (!(entity is IHasDeleterId<TPrimaryKey> hasDeleterId)) return false;
            hasDeleterId.DeletedBy = deletedBy;
            return true;
        }
    }
}
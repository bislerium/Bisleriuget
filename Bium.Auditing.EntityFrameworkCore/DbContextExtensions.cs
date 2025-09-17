using System.Collections.Generic;
using System.Linq;
using Bium.Auditing.Contracts;
using Bium.Auditing.Contracts.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bium.Auditing.EntityFrameworkCore
{
    /// <summary>
    /// Provides extension methods for applying auditing logic to tracked entities in a <see cref="DbContext"/>.
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Applies automatic auditing timestamps to all auditable entities tracked by the <see cref="DbContext"/>.
        /// </summary>
        /// <param name="dbContext">The <see cref="DbContext"/> containing the tracked entities to audit.</param>
        /// <remarks>
        /// Iterates over all auditable entities retrieved by <c>GetAuditableEntries(dbContext)</c> and applies
        /// the appropriate creation, modification, or deletion timestamps depending on each entity's <see cref="EntityState"/>.
        /// This method does not track the user performing the operations.
        /// </remarks>
        public static void ApplyAuditing(this DbContext dbContext)
        {
            foreach (var entry in GetAuditableEntries(dbContext))
            {
                ApplyAudit(entry);
            }
        }

        /// <summary>
        /// Applies auditing information to all auditable entities tracked by the <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the identifier of the user performing the actions (e.g., int, Guid).</typeparam>
        /// <param name="dbContext">The <see cref="DbContext"/> containing the tracked entities to audit.</param>
        /// <param name="performedBy">The identifier of the user performing the operations.</param>
        /// <remarks>
        /// Iterates over all auditable entities retrieved by <c>GetAuditableEntries(dbContext)</c> and applies
        /// the appropriate creation, modification, or deletion information depending on each entity's <see cref="EntityState"/>.
        /// </remarks>
        public static void ApplyAuditing<TPrimaryKey>(this DbContext dbContext, TPrimaryKey performedBy)
            where TPrimaryKey : struct
        {
            foreach (var entry in GetAuditableEntries(dbContext))
            {
                ApplyAudit(entry, performedBy);
            }
        }

        /// <summary>
        /// Applies auditing information to all auditable entities tracked by the <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the identifier of the user performing the actions (e.g., int, Guid).</typeparam>
        /// <typeparam name="TDateTime">The type representing the timestamp of the actions (e.g., DateTime, DateTimeOffset).</typeparam>
        /// <param name="dbContext">The <see cref="DbContext"/> containing the tracked entities to audit.</param>
        /// <param name="performedBy">The identifier of the user performing the operations.</param>
        /// <param name="performedAt">The timestamp at which the operations are performed.</param>
        /// <remarks>
        /// Iterates over all auditable entities retrieved by <c>GetAuditableEntries(dbContext)</c> and applies
        /// the appropriate creation, modification, or deletion information depending on each entity's <see cref="EntityState"/>.
        /// </remarks>
        public static void ApplyAuditing<TPrimaryKey, TDateTime>(this DbContext dbContext, TPrimaryKey performedBy,
            TDateTime performedAt)
            where TPrimaryKey : struct
            where TDateTime : struct
        {
            foreach (var entry in GetAuditableEntries(dbContext))
            {
                ApplyAudit(entry, performedBy, performedAt);
            }
        }


        /// <summary>
        /// Retrieves all auditable entity entries from the given <see cref="DbContext"/> 
        /// that are in a state requiring auditing (e.g., Added, Modified, Deleted).
        /// </summary>
        /// <param name="dbContext">The current <see cref="DbContext"/> instance.</param>
        /// <returns>An enumerable of auditable entity entries.</returns>
        private static IEnumerable<EntityEntry<IAuditKind>> GetAuditableEntries(DbContext dbContext) =>
            dbContext.ChangeTracker
                .Entries<IAuditKind>()
                .Where(e => e.State is not (EntityState.Detached or EntityState.Unchanged));

        /// <summary>
        /// Applies automatic auditing timestamps to an <see cref="IAuditKind"/> entity based on its current <see cref="EntityState"/>.
        /// </summary>
        /// <param name="entry">The <see cref="EntityEntry{IAuditKind}"/> representing the entity to audit.</param>
        /// <remarks>
        /// Depending on the <see cref="EntityState"/> of the entity, this method will:
        /// <list type="bullet">
        /// <item>
        /// <description>If <see cref="EntityState.Added"/>, sets the creation time.</description>
        /// </item>
        /// <item>
        /// <description>If <see cref="EntityState.Modified"/>, sets the modification time.</description>
        /// </item>
        /// <item>
        /// <description>If <see cref="EntityState.Deleted"/> and the entity supports soft delete, sets the deletion time and marks the entity as modified instead of physically deleting it.</description>
        /// </item>
        /// <item>
        /// <description>If <see cref="EntityState.Detached"/> or <see cref="EntityState.Unchanged"/>, no auditing changes are applied.</description>
        /// </item>
        /// </list>
        /// </remarks>
        private static void ApplyAudit(EntityEntry<IAuditKind> entry)
        {
            var entity = entry.Entity;

            switch (entry.State)
            {
                case EntityState.Added:
                    entity.TryApplyCreationTime();
                    break;

                case EntityState.Modified:
                    entity.TryApplyModificationTime();
                    break;

                case EntityState.Deleted:
                    if (entity.TryApplySoftDelete())
                    {
                        entity.TryApplyDeletionTime();
                        entry.State = EntityState.Modified; // soft delete
                    }

                    break;

                case EntityState.Detached:
                case EntityState.Unchanged:
                default:
                    break;
            }
        }

        /// <summary>
        /// Applies auditing information to an <see cref="IAuditKind"/> entity based on its current <see cref="EntityState"/>.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the identifier of the user performing the action (e.g., int, Guid).</typeparam>
        /// <param name="entry">The <see cref="EntityEntry{IAuditKind}"/> representing the entity to audit.</param>
        /// <param name="performedBy">The identifier of the user performing the operation.</param>
        /// <remarks>
        /// Depending on the <see cref="EntityState"/> of the entity, this method will:
        /// <list type="bullet">
        /// <item>
        /// <description>If <see cref="EntityState.Added"/>, sets creation time and creator ID.</description>
        /// </item>
        /// <item>
        /// <description>If <see cref="EntityState.Modified"/>, sets modification time and modifier ID.</description>
        /// </item>
        /// <item>
        /// <description>If <see cref="EntityState.Deleted"/> and the entity supports soft delete, sets deletion time, deleter ID, and marks the entity as modified instead of physically deleting it.</description>
        /// </item>
        /// <item>
        /// <description>If <see cref="EntityState.Detached"/> or <see cref="EntityState.Unchanged"/>, no auditing changes are applied.</description>
        /// </item>
        /// </list>
        /// </remarks>
        private static void ApplyAudit<TPrimaryKey>(EntityEntry<IAuditKind> entry, TPrimaryKey performedBy)
            where TPrimaryKey : struct
        {
            var entity = entry.Entity;

            switch (entry.State)
            {
                case EntityState.Added:
                    entity.TryApplyCreationTime();
                    entity.TryApplyCreatorId(performedBy);
                    break;

                case EntityState.Modified:
                    entity.TryApplyModificationTime();
                    entity.TryApplyModifierId(performedBy);
                    break;

                case EntityState.Deleted:
                    if (entity.TryApplySoftDelete())
                    {
                        entity.TryApplyDeletionTime();
                        entity.TryApplyDeleterId(performedBy);
                        entry.State = EntityState.Modified; // soft delete
                    }

                    break;
                case EntityState.Detached:
                case EntityState.Unchanged:
                default:
                    break;
            }
        }

        /// <summary>
        /// Applies auditing information to an entity based on its current state.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the identifier of the user performing the action (e.g., int, Guid).</typeparam>
        /// <typeparam name="TDateTime">The type representing the timestamp of the action (e.g., DateTime, DateTimeOffset).</typeparam>
        /// <param name="entry">The <see cref="EntityEntry{IAuditKind}"/> representing the entity to audit.</param>
        /// <param name="performedBy">The identifier of the user performing the operation.</param>
        /// <param name="performedAt">The timestamp at which the operation is performed.</param>
        /// <remarks>
        /// Depending on the <see cref="EntityState"/> of the entity, this method will:
        /// <list type="bullet">
        /// <item>
        /// <description>If <see cref="EntityState.Added"/>, sets creation time and creator ID.</description>
        /// </item>
        /// <item>
        /// <description>If <see cref="EntityState.Modified"/>, sets modification time and modifier ID.</description>
        /// </item>
        /// <item>
        /// <description>If <see cref="EntityState.Deleted"/> and the entity supports soft delete, sets deletion time, deleter ID, and marks the entity as modified instead of physically deleting it.</description>
        /// </item>
        /// <item>
        /// <description>If <see cref="EntityState.Detached"/> or <see cref="EntityState.Unchanged"/>, no auditing changes are applied.</description>
        /// </item>
        /// </list>
        /// </remarks>
        private static void ApplyAudit<TPrimaryKey, TDateTime>(EntityEntry<IAuditKind> entry, TPrimaryKey performedBy,
            TDateTime performedAt)
            where TPrimaryKey : struct
            where TDateTime : struct
        {
            var entity = entry.Entity;

            switch (entry.State)
            {
                case EntityState.Added:
                    entity.TryApplyCreationTime(performedAt);
                    entity.TryApplyCreatorId(performedBy);
                    break;

                case EntityState.Modified:
                    entity.TryApplyModificationTime(performedAt);
                    entity.TryApplyModifierId(performedBy);
                    break;

                case EntityState.Deleted:
                    if (entity.TryApplySoftDelete())
                    {
                        entity.TryApplyDeletionTime(performedAt);
                        entity.TryApplyDeleterId(performedBy);
                        entry.State = EntityState.Modified; // soft delete
                    }

                    break;
                case EntityState.Detached:
                case EntityState.Unchanged:
                default:
                    break;
            }
        }
    }
}
using Bium.Auditing.Contracts;
using Bium.Auditing.Contracts.Creation;
using Bium.Auditing.Contracts.Deletion;
using Bium.Auditing.Contracts.Modification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bium.Auditing.EntityFrameworkCore.Extensions;

public static class DbContextExtensions
{
    public static void ApplyAuditTrails<TPrimaryKey>(this DbContext dbContext, TPrimaryKey userId)
        where TPrimaryKey : struct
    {
        foreach (var entry in GetAuditableEntries(dbContext))
        {
            ApplyAudit(entry, userId);
        }
    }

    public static void ApplyAuditTrails(this DbContext dbContext)
    {
        foreach (var entry in GetAuditableEntries(dbContext))
        {
            ApplyAudit(entry);
        }
    }

    private static IEnumerable<EntityEntry<IAuditKind>> GetAuditableEntries(DbContext dbContext) =>
        dbContext.ChangeTracker
            .Entries<IAuditKind>()
            .Where(e => e.State is not (EntityState.Detached or EntityState.Unchanged));

    private static void ApplyAudit(EntityEntry<IAuditKind> entry)
    {
        var entity = entry.Entity;

        switch (entry.State)
        {
            case EntityState.Added:
                SetCreationTime(entity);
                break;

            case EntityState.Modified:
                SetModificationTime(entity);
                break;

            case EntityState.Deleted:
                if (TryApplySoftDelete(entity))
                {
                    SetDeletionTime(entity);
                    entry.State = EntityState.Modified; // soft delete
                }

                break;
            case EntityState.Detached:
            case EntityState.Unchanged:
            default:
                break;
        }
    }

    private static void ApplyAudit<TPrimaryKey>(EntityEntry<IAuditKind> entry, TPrimaryKey id)
        where TPrimaryKey : struct
    {
        var entity = entry.Entity;

        switch (entry.State)
        {
            case EntityState.Added:
                SetCreationTime(entity);
                SetCreatorId(entity, id);
                break;

            case EntityState.Modified:
                SetModificationTime(entity);
                SetModifierId(entity, id);
                break;

            case EntityState.Deleted:
                if (TryApplySoftDelete(entity))
                {
                    SetDeletionTime(entity);
                    SetDeleterId(entity, id);
                    entry.State = EntityState.Modified; // soft delete
                }

                break;
            case EntityState.Detached:
            case EntityState.Unchanged:
            default:
                break;
        }
    }

    private static void SetCreationTime(IAuditKind entity)
    {
        switch (entity)
        {
            case IHasCreationTime<DateTime> d: d.CreatedAt = DateTime.UtcNow; break;
            case IHasCreationTime<DateTimeOffset> dto: dto.CreatedAt = DateTimeOffset.UtcNow; break;
        }
    }

    private static void SetModificationTime(IAuditKind entity)
    {
        switch (entity)
        {
            case IHasModificationTime<DateTime> d: d.ModifiedAt = DateTime.UtcNow; break;
            case IHasModificationTime<DateTimeOffset> dto: dto.ModifiedAt = DateTimeOffset.UtcNow; break;
        }
    }

    private static void SetDeletionTime(IAuditKind entity)
    {
        switch (entity)
        {
            case IHasDeletionTime<DateTime> d: d.DeletedAt = DateTime.UtcNow; break;
            case IHasDeletionTime<DateTimeOffset> dto: dto.DeletedAt = DateTimeOffset.UtcNow; break;
        }
    }

    private static bool TryApplySoftDelete(IAuditKind entity)
    {
        if (entity is not ISoftDeletable softDeletable) return false;
        softDeletable.IsDeleted = true;
        return true;
    }

    private static void SetCreatorId<TPrimaryKey>(IAuditKind entity, TPrimaryKey primaryKey)
        where TPrimaryKey : struct
    {
        if (entity is IHasCreatorId<TPrimaryKey> hasCreatorId)
            hasCreatorId.CreatedBy = primaryKey;
    }

    private static void SetModifierId<TPrimaryKey>(IAuditKind entity, TPrimaryKey primaryKey)
        where TPrimaryKey : struct
    {
        if (entity is IHasModifierId<TPrimaryKey> hasModifierId)
            hasModifierId.ModifiedBy = primaryKey;
    }

    private static void SetDeleterId<TPrimaryKey>(IAuditKind entity, TPrimaryKey primaryKey)
        where TPrimaryKey : struct
    {
        if (entity is IHasDeleterId<TPrimaryKey> hasDeleterId)
            hasDeleterId.DeletedBy = primaryKey;
    }
}
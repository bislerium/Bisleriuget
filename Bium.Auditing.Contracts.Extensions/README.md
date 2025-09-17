# Bium.Auditing.Contracts.Extensions&nbsp;[![NuGet version (Bium.Auditing.Contracts.Extensions)](https://img.shields.io/nuget/v/Bium.Auditing.Contracts.Extensions.svg?style=flat-square)](https://www.nuget.org/packages/Bium.Auditing.Contracts.Extensions/)&nbsp;[![NuGet Downloads](https://img.shields.io/nuget/dt/Bium.Auditing.Contracts.Extensions.svg?style=flat-square)](https://www.nuget.org/packages/Bium.Auditing.Contracts.Extensions/)

`Bium.Auditing.Contracts.Extensions` provides a comprehensive set of reusable extension methods for auditing entities in .NET applications. It simplifies managing creation, modification, deletion, soft delete metadata, and user tracking on entities implementing standard auditing interfaces.

## Features

### Creation Audit Extensions (`CreationAuditExtensions`)
- **`SetCreated<TPrimaryKey>(entity, createdBy)`**  
  Sets creation metadata (`CreatedAt` and `CreatedBy`) using the current UTC time.
- **`SetCreated<TPrimaryKey, TDateTime>(entity, createdBy, createdAt)`**  
  Sets creation metadata with a specified timestamp.

### Creation Time Extensions (`CreationTimeExtensions`)
- **`SetCreatedNow(entity)`**  
  Sets the creation time to current UTC (`DateTime` or `DateTimeOffset`).
- **`SetCreatedAt<TDateTime>(entity, createdAt)`**  
  Sets a specific creation time.

### Creator ID Extensions (`CreatorIdExtensions`)
- **`SetCreatedBy<TPrimaryKey>(entity, createdBy)`**  
  Sets the `CreatedBy` property.

### Modification Audit Extensions (`ModificationAuditExtensions`)
- **`SetModified<TPrimaryKey>(entity, modifiedBy)`**  
  Sets modification metadata using current UTC time (`DateTime` or `DateTimeOffset`).
- **`SetModified<TPrimaryKey, TDateTime>(entity, modifiedBy, modifiedAt)`**  
  Sets modification metadata with a specified timestamp.

### Modification Time Extensions (`ModificationTimeExtensions`)
- **`SetModifiedNow(entity)`**  
  Sets modification time to current UTC.
- **`SetModifiedAt<TDateTime>(entity, modifiedAt)`**  
  Sets a specific modification time.

### Modifier ID Extensions (`ModifierIdExtensions`)
- **`SetModifiedBy<TPrimaryKey>(entity, modifiedBy)`**  
  Sets the `ModifiedBy` property.

### Deletion Audit Extensions (`DeletionAuditExtensions`)
- **`SetDeleted<TPrimaryKey>(entity, deletedBy)`**  
  Sets deletion metadata (`DeletedAt`, `DeletedBy`, `IsDeleted`) using current UTC time.
- **`SetDeleted<TPrimaryKey, TDateTime>(entity, deletedBy, deletedAt)`**  
  Sets deletion metadata with a specified timestamp.

### Deletion Time Extensions (`DeletionTimeExtensions`)
- **`SetDeletedNow(entity)`**  
  Sets deletion time to current UTC and marks entity as deleted.
- **`SetDeletedAt<TDateTime>(entity, deletedAt)`**  
  Sets a specific deletion time and marks entity as deleted.

### Deleter ID Extensions (`DeleterIdExtensions`)
- **`SetDeletedBy<TPrimaryKey>(entity, deletedBy)`**  
  Sets the `DeletedBy` property and marks the entity as deleted.

### Soft Delete Extensions (`SoftDeleteExtensions`)
- **`SetIsDeleted(entity)`**  
  Marks the entity as soft deleted (`IsDeleted = true`).

### Audit Kind Extensions (`AudtiKindExtensions`)

- **`TryApplyCreationTime(entity)`**
  Attempts to set the creation time to the current UTC time. Returns `true` if the entity supports creation time, otherwise `false`.
- **`TryApplyCreationTime<TDateTime>(entity, createdAt)`**
  Attempts to set a specific creation time. Returns `true` if supported, otherwise `false`.
- **`TryApplyModificationTime(entity)`**
  Attempts to set the modification time to the current UTC time. Returns `true` if the entity supports modification time, otherwise `false`.
- **`TryApplyModificationTime<TDateTime>(entity, modifiedAt)`**
  Attempts to set a specific modification time. Returns `true` if supported, otherwise `false`.
- **`TryApplyDeletionTime(entity)`**
  Attempts to set the deletion time to the current UTC time. Returns `true` if the entity supports deletion time, otherwise `false`.
- **`TryApplyDeletionTime<TDateTime>(entity, deletedAt)`**
  Attempts to set a specific deletion time. Returns `true` if supported, otherwise `false`.
- **`TryApplySoftDelete(entity)`**
  Attempts to mark the entity as soft deleted. Returns `true` if the entity supports soft delete, otherwise `false`.
- **`TryApplyCreatorId<TPrimaryKey>(entity, createdBy)`**
  Attempts to set the creator identifier. Returns `true` if supported, otherwise `false`.
- **`TryApplyModifierId<TPrimaryKey>(entity, modifiedBy)`**
  Attempts to set the modifier identifier. Returns `true` if supported, otherwise `false`.
- **`TryApplyDeleterId<TPrimaryKey>(entity, deletedBy)`**
  Attempts to set the deleter identifier. Returns `true` if supported, otherwise `false`.

## Getting Started
Here’s a set of example usages for **all the auditing extension methods**:

```csharp
using Bium.Auditing.Contracts.Extensions;
using System;

class Program
{
    static void Main()
    {
        // Example entity implementing auditing interfaces
        var entity = new MyEntity(); // Assume MyEntity implements IAuditKind, ISoftDeletable, etc.

        // -------------------------
        // Creation Audit Extensions
        // -------------------------
        entity.SetCreatedNow();                 // Sets CreatedAt to DateTime.UtcNow
        entity.SetCreatedBy(1001);              // Sets CreatedBy
        entity.SetCreated(1001, DateTime.UtcNow.AddMinutes(-10)); // Sets CreatedBy and custom CreatedAt

        // -------------------------
        // Modification Audit Extensions
        // -------------------------
        entity.SetModifiedNow();                // Sets ModifiedAt to DateTime.UtcNow
        entity.SetModifiedBy(2002);             // Sets ModifiedBy
        entity.SetModified(2002, DateTime.UtcNow.AddMinutes(-5)); // Sets ModifiedBy and custom ModifiedAt

        // -------------------------
        // Deletion Audit Extensions
        // -------------------------
        entity.SetDeletedNow();                 // Sets DeletedAt to DateTime.UtcNow and IsDeleted = true
        entity.SetDeletedBy(3003);              // Sets DeletedBy and IsDeleted = true
        entity.SetDeleted(3003, DateTime.UtcNow.AddMinutes(-1)); // Sets DeletedBy and custom DeletedAt

        // -------------------------
        // Soft Delete
        // -------------------------
        entity.SetIsDeleted();                  // Marks the entity as soft deleted

        // -------------------------
        // AuditKind Extensions
        // -------------------------
        IAuditKind auditEntity = entity;

        bool applied;

        applied = auditEntity.TryApplyCreationTime();                      // Sets CreatedAt to UTC now if supported
        applied = auditEntity.TryApplyCreationTime(DateTime.UtcNow.AddHours(-1)); // Sets custom CreatedAt
        applied = auditEntity.TryApplyModificationTime();                  // Sets ModifiedAt to UTC now
        applied = auditEntity.TryApplyModificationTime(DateTime.UtcNow.AddHours(-2)); // Sets custom ModifiedAt
        applied = auditEntity.TryApplyDeletionTime();                      // Sets DeletedAt to UTC now
        applied = auditEntity.TryApplyDeletionTime(DateTime.UtcNow.AddHours(-3)); // Sets custom DeletedAt
        applied = auditEntity.TryApplySoftDelete();                        // Sets IsDeleted = true
        applied = auditEntity.TryApplyCreatorId(1001);                     // Sets CreatedBy
        applied = auditEntity.TryApplyModifierId(2002);                    // Sets ModifiedBy
        applied = auditEntity.TryApplyDeleterId(3003);                     // Sets DeletedBy
    }
}

// Example entity definition for demonstration
public class MyEntity : IAuditable<int, DateTime>
{
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime ModifiedAt { get; set; }
    public int ModifiedBy { get; set; }
    public DateTime DeletedAt { get; set; }
    public int DeletedBy { get; set; }
    public bool IsDeleted { get; set; }
}
```

### Notes

* All `Set*` methods update the entity directly.
* All `TryApply*` methods return a `bool` indicating whether the operation was applied (useful if the entity doesn’t implement the expected interface).
* The examples demonstrate both **default UTC timestamps** and **custom timestamps**.

## Contributing

Contributions are welcome! Please follow the standard GitHub workflow:

1. Fork the repository
2. Create a new branch (`feature/your-feature`)
3. Commit your changes
4. Open a pull request

## License

`Bium.Auditing.Contracts.Extensions` is licensed under the MIT License.
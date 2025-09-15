# Bium.Auditing.Contracts

`Bium.Auditing.Contracts` provides a set of standardized contracts (interfaces) for auditing entities in .NET applications. It allows developers to implement **creation, modification, and deletion auditing** consistently across their domain entities, supporting both **timestamp-only auditing** and **full actor (user) auditing** with navigation references.

[![NuGet version (Bium.Auditing.Contracts)](https://img.shields.io/nuget/v/Bium.Auditing.Contracts.svg?style=flat-square)](https://www.nuget.org/packages/Bium.Auditing.Contracts/)

---

## Features

* **Marker Interfaces**:

    * `IAuditKind` – Identifies auditable entities.
    * `IEntityKind` – Marks domain entities.

* **Creation Auditing**:

    * `IHasCreationTime<TDateTime>` – Tracks creation timestamp.
    * `IHasCreatorId<TPrimaryKey>` – Tracks creator by ID.
    * `IHasCreator<TUser, TPrimaryKey>` – Tracks creator entity reference.
    * `ICreationAudited<TPrimaryKey, TDateTime>` – Combines creation time + creator ID.
    * `ICreationAudited<TUser, TPrimaryKey, TDateTime>` – Combines creation time + creator entity + ID.

* **Modification Auditing**:

    * `IHasModificationTime<TDateTime>` – Tracks last modification timestamp.
    * `IHasModifierId<TPrimaryKey>` – Tracks last modifier by ID.
    * `IHasModifier<TUser, TPrimaryKey>` – Tracks last modifier entity reference.
    * `IModificationAudited<TPrimaryKey, TDateTime>` – Combines modification time + modifier ID.
    * `IModificationAudited<TUser, TPrimaryKey, TDateTime>` – Combines modification time + modifier entity + ID.

* **Deletion Auditing & Soft Delete**:

    * `ISoftDeletable` – Supports soft deletion via `IsDeleted`.
    * `IHasDeletionTime<TDateTime>` – Tracks deletion timestamp.
    * `IHasDeleterId<TPrimaryKey>` – Tracks deleter by ID.
    * `IHasDeleter<TUser, TPrimaryKey>` – Tracks deleter entity reference.
    * `IDeletionAudited<TPrimaryKey, TDateTime>` – Combines deletion time + deleter ID.
    * `IDeletionAudited<TUser, TPrimaryKey, TDateTime>` – Combines deletion time + deleter entity + ID.

* **Composite Auditing**:

    * `IAuditable<TDateTime>` – Timestamp-only auditing (creation, modification, deletion).
    * `IAuditable<TPrimaryKey, TDateTime>` – Timestamp + actor ID auditing.
    * `IAuditable<TUser, TPrimaryKey, TDateTime>` – Full auditing with navigation to user entities.

* **Auditable Entities**:

    * `IAuditableEntity<TPrimaryKey, TDateTime>` – Base contract for entities with primary key + auditing.
    * `IAuditableEntity<TUser, TPrimaryKey, TDateTime>` – Base contract with user references for full actor auditing.

* **Generic Primary Keys ```<TPrimaryKey>```**: Support for flexible primary key types (int, long, Guid, etc.).
* **Timestamp Flexibility ```<TDateTime>```**: Supports `DateTime` or `DateTimeOffset` for creation/modification/deletion times.
* **Highly Customizable**: Extend or implement granular interfaces to fit your domain model and auditing requirements.

---

## Getting Started

### 1. IAuditable

### 1.1 Timestamp-Only Auditing

```csharp
public class Product : IAuditable<DateTime>
{
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}
```

### 1.2 Actor ID Auditing

```csharp
public class Order : IAuditable<Guid, DateTimeOffset>
{
    public DateTimeOffset CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }

    public DateTimeOffset? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? DeletedBy { get; set; }
}
```

### 1.3 Full Actor Auditing (with user entity references)

```csharp
public class AppUser : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
}

public class Order : IAuditable<AppUser, Guid, DateTimeOffset>
{
    public DateTimeOffset CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public AppUser Creator { get; set; } = null!;

    public DateTimeOffset? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public AppUser? Modifier { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? DeletedBy { get; set; }
    public AppUser? Deleter { get; set; }
}
```

### 2. IAuditableEntity

### 2.1 Timestamp-Only Auditing

```csharp
public class Product : IAuditableEntity<DateTime>
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}
```

### 2.2 Full Actor Auditing

```csharp
public class AppUser : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
}

public class Order : IAuditableEntity<AppUser, Guid, DateTimeOffset>
{
    public Guid Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public AppUser Creator { get; set; } = null!;

    public DateTimeOffset? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public AppUser? Modifier { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? DeletedBy { get; set; }
    public AppUser? Deleter { get; set; }
}
```

---

## Why `struct` for `TPrimaryKey`?

All primary key types (`TPrimaryKey`) in `Bium.Auditing.Contracts` are constrained to `struct` for multiple important reasons:

1. **Value Type Semantics**

    * `struct` ensures that the primary key is a value type rather than a reference type.
    * Value types are immutable by default, which reduces unintended side effects in your entities and ensures predictable behavior for equality and hashing.

2. **Performance Benefits**

    * Value types are allocated on the stack (or inlined in objects for fields), avoiding heap allocations that reference types incur.
    * Operations such as equality checks, comparisons, and dictionary lookups are faster for value types like `int`, `long`, or `Guid`.

3. **Consistency Across ORMs**

    * Most ORMs, including **Entity Framework Core**, assume primary keys are value types for generating database keys and relationships.
    * Using value types avoids potential issues with nullability, reference equality, and serialization.

4. **Compile-Time Safety**

    * Constraining to `struct` prevents accidental use of reference types (like `string`, `object`, or custom classes) as primary keys.
    * This ensures that entities always use **lightweight, comparable, and immutable types** for IDs, preventing runtime errors or inconsistent behavior.

5. **Supports Nullable IDs Where Needed**

    * Nullable value types (`Guid?`, `int?`, etc.) allow tracking of unset or optional relationships while still enforcing type safety.
    * For example, `DeletedBy` or `ModifiedBy` can be `null` to indicate no action yet.

6. **Broad Compatibility**

    * `struct` covers all typical primary key types used in databases:

        * `int` or `long` for auto-increment keys.
        * `Guid` for distributed unique identifiers.
        * `short`, `byte`, or other numeric types when required.
    * This makes the auditing interfaces highly **generic and reusable**.

**Summary:** Constraining `TPrimaryKey` to `struct` ensures **safety, performance, and consistency** while providing flexibility for common database primary key types, making it ideal for enterprise-grade auditing systems.

---

## Contributing

Contributions are welcome! Please follow the standard GitHub workflow:

1. Fork the repository
2. Create a new branch (`feature/your-feature`)
3. Commit your changes
4. Open a pull request

---

## License

`Bium.Auditing.Contracts` is licensed under the MIT License. See [LICENSE](LICENSE) for details.

---
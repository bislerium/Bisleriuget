# Bium.Auditing.EntityFrameworkCore&nbsp;[![NuGet version](https://img.shields.io/nuget/v/Bium.Auditing.EntityFrameworkCore.svg?style=flat-square)](https://www.nuget.org/packages/Bium.Auditing.EntityFrameworkCore/)&nbsp;[![NuGet Downloads](https://img.shields.io/nuget/dt/Bium.Auditing.EntityFrameworkCore.svg?style=flat-square)](https://www.nuget.org/packages/Bium.Auditing.EntityFrameworkCore/)

`Bium.Auditing.EntityFrameworkCore` is a lightweight library that provides automatic auditing capabilities for Entity
Framework Core. It simplifies tracking creation, modification, and deletion information (including soft deletes) for
your entities. This package is ideal for applications where audit trails are required for data changes.

> This package is designed to be used with a `SaveChangesInterceptor` to automatically apply auditing logic.

## Features

### DbContext Auditing Extensions (DbContextExtensions)

- **`ApplyAuditing(dbContext)`**  
  Applies automatic auditing timestamps to all tracked entities without tracking the user.

- **`ApplyAuditing<TPrimaryKey>(dbContext, performedBy)`**
  Applies auditing timestamps using the current UTC time and sets the user performing the operation.
    - The `performedBy` argument should be provided manually, retrieved from `HttpContextAccessor`, or obtained via a
      custom user service.

- **`ApplyAuditing<TPrimaryKey, TDateTime>(dbContext, performedBy, performedAt)`**
  Applies auditing timestamps using a custom time (`DateTime` or `DateTimeOffset`) and sets the user performing the
  operation.
    - The `performedBy` argument should be provided manually, retrieved from `HttpContextAccessor`, or obtained via a
      custom user service.
    - The `performedAt` argument should be provided manually or via a custom datetime provider.

## Getting Started

`ApplyAuditing` automatically applies audit information (creation, modification, deletion) to all entities implementing
`IAuditKind` when `SaveChanges` or `SaveChangesAsync` is called. Using it with a `SaveChangesInterceptor` ensures audit
trails are applied consistently without manually calling the methods in each repository or service.

### Key Points

- Automatically sets **creation time** and **creator ID** for new entities.
- Automatically sets **modification time** and **modifier ID** for updated entities.
- Supports **soft deletes**: automatically sets **deletion time** and **deleter ID** without physically removing
  records.
- Easy integration with any `DbContext`.

## Interceptor Examples

### 1. `ApplyAuditing(dbContext)`

```csharp
public class AuditingInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context != null)
            eventData.Context.ApplyAuditing();

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context != null)
            eventData.Context.ApplyAuditing();

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
```

### 2. `ApplyAuditing<TPrimaryKey>(dbContext, performedBy)`

```csharp
public class AuditingWithUserInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;

    public AuditingWithUserInterceptor(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context != null)
            eventData.Context.ApplyAuditing(_currentUserService.UserId);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context != null)
            eventData.Context.ApplyAuditing(_currentUserService.UserId);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
```

### 3. `ApplyAuditing<TPrimaryKey, TDateTime>(dbContext, performedBy, performedAt)`

```csharp
public class AuditingWithUserAndTimeInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AuditingWithUserAndTimeInterceptor(ICurrentUserService currentUserService, IDateTimeProvider dateTimeProvider)
    {
        _currentUserService = currentUserService;
        _dateTimeProvider = dateTimeProvider;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context != null)
            eventData.Context.ApplyAuditing(_currentUserService.UserId, _dateTimeProvider.UtcNow);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context != null)
            eventData.Context.ApplyAuditing(_currentUserService.UserId, _dateTimeProvider.UtcNow);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
```

### Interceptor Usage

1. **No user tracking** → `AuditingInterceptor`
2. **Track user** → `AuditingWithUserInterceptor`
3. **Track user + custom timestamp** → `AuditingWithUserAndTimeInterceptor`

> Make sure to register the interceptor as a scoped service and add it to your DbContext configuration.

## Contributing

Contributions are welcome! Please follow the standard GitHub workflow:

1. Fork the repository
2. Create a new branch (`feature/your-feature`)
3. Commit your changes
4. Open a pull request

## License

`Bium.Auditing.EntityFrameworkCore` is licensed under the MIT License.
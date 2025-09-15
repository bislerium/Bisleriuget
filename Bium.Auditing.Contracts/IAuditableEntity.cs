using System;
using Bium.Auditing.Contracts.Entity;

namespace Bium.Auditing.Contracts
{
    /// <summary>
    /// Base contract for auditable entities that have a primary key and support timestamp plus actor auditing.
    /// </summary>
    /// <typeparam name="TPrimaryKey">
    /// The type of the entity's primary key (e.g., <see cref="Guid"/>, <see cref="int"/>).
    /// </typeparam>
    /// <typeparam name="TDateTime">
    /// The type used for timestamps (e.g., <see cref="DateTime"/>, <see cref="DateTimeOffset"/>).
    /// </typeparam>
    public interface IAuditableEntity<TPrimaryKey, TDateTime>
        : IEntity<TPrimaryKey>, IAuditable<TPrimaryKey, TDateTime>
        where TPrimaryKey : struct, IEquatable<TPrimaryKey>
        where TDateTime : struct
    {
    }

    /// <summary>
    /// Base contract for auditable entities that include a reference to a user entity,
    /// along with primary key and timestamp auditing for creation, modification, and deletion.
    /// </summary>
    /// <typeparam name="TUser">
    /// The type of the user entity associated with auditing operations.
    /// </typeparam>
    /// <typeparam name="TPrimaryKey">
    /// The type of the entity's primary key (e.g., <see cref="Guid"/>, <see cref="int"/>).
    /// </typeparam>
    /// <typeparam name="TDateTime">
    /// The type used for timestamps (e.g., <see cref="DateTime"/>, <see cref="DateTimeOffset"/>).
    /// </typeparam>
    public interface IAuditableEntity<TUser, TPrimaryKey, TDateTime>
        : IAuditable<TUser, TPrimaryKey, TDateTime>, IAuditableEntity<TPrimaryKey, TDateTime>
        where TPrimaryKey : struct, IEquatable<TPrimaryKey>
        where TUser : class, IEntity<TPrimaryKey>
        where TDateTime : struct
    {
    }
}
using System;
using Bisleriuget.Auditing.Contracts.Creation;
using Bisleriuget.Auditing.Contracts.Deletion;
using Bisleriuget.Auditing.Contracts.Entity;
using Bisleriuget.Auditing.Contracts.Modification;

namespace Bisleriuget.Auditing.Contracts
{
    /// <summary>
    /// Represents an entity that supports timestamp-only auditing for creation, modification, and deletion.
    /// </summary>
    /// <typeparam name="TDateTime">The type used for timestamps (e.g., <see cref="DateTime"/>, <see cref="DateTimeOffset"/>).</typeparam>
    public interface IAuditable<TDateTime>
        : IHasCreationTime<TDateTime>,
            IHasModificationTime<TDateTime>,
            IHasDeletionTime<TDateTime>
        where TDateTime : struct
    {
    }

    /// <summary>
    /// Represents an entity that supports timestamp and actor auditing for creation, modification, and deletion.
    /// Tracks the user ID who performed each operation.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key used for auditing (typically a struct like <see cref="Guid"/>).</typeparam>
    /// <typeparam name="TDateTime">The type used for timestamps (e.g., <see cref="DateTime"/>, <see cref="DateTimeOffset"/>).</typeparam>
    public interface IAuditable<TPrimaryKey, TDateTime>
        : IAuditable<TDateTime>,
            ICreationAudited<TPrimaryKey, TDateTime>,
            IModificationAudited<TPrimaryKey, TDateTime>,
            IDeletionAudited<TPrimaryKey, TDateTime>
        where TPrimaryKey : struct, IEquatable<TPrimaryKey>
        where TDateTime : struct
    {
    }

    /// <summary>
    /// Represents an entity that supports detailed auditing for creation, modification, and deletion,
    /// including references to the user entity who performed each action.
    /// </summary>
    /// <typeparam name="TUser">The type of the user entity associated with auditing operations.</typeparam>
    /// <typeparam name="TPrimaryKey">The type of the primary key used for auditing (typically a struct like <see cref="Guid"/>).</typeparam>
    /// <typeparam name="TDateTime">The type used for timestamps (e.g., <see cref="DateTime"/>, <see cref="DateTimeOffset"/>).</typeparam>
    public interface IAuditable<TUser, TPrimaryKey, TDateTime>
        : IAuditable<TPrimaryKey, TDateTime>,
            ICreationAudited<TUser, TPrimaryKey, TDateTime>,
            IModificationAudited<TUser, TPrimaryKey, TDateTime>,
            IDeletionAudited<TUser, TPrimaryKey, TDateTime>
        where TUser : class, IEntity<TPrimaryKey>
        where TPrimaryKey : struct, IEquatable<TPrimaryKey>
        where TDateTime : struct
    {
    }
}
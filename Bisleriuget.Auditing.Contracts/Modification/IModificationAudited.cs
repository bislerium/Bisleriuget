using Bisleriuget.Auditing.Contracts.Entity;

namespace Bisleriuget.Auditing.Contracts.Modification
{
    /// <summary>
    /// Represents an entity that tracks the modification time and the ID of the user who last modified the entity.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key for the last modifier user.</typeparam>
    /// <typeparam name="TDateTime">The type used to represent the modification timestamp (e.g., DateTime, DateTimeOffset).</typeparam>
    public interface IModificationAudited<TPrimaryKey, TDateTime> : IHasModificationTime<TDateTime>
        where TDateTime : struct
        where TPrimaryKey : struct
    {
        /// <summary>
        /// Gets or sets the primary key of the user who last modified the entity.
        /// </summary>
        TPrimaryKey? ModifiedBy { get; set; }
    }

    /// <summary>
    /// Represents an entity that tracks the modification time and the user who last modified the entity, along with their ID.
    /// </summary>
    /// <typeparam name="TUser">The type of the user entity associated with the modifier.</typeparam>
    /// <typeparam name="TPrimaryKey">The type of the primary key for the last modifier user.</typeparam>
    /// <typeparam name="TDateTime">The type used to represent the modification timestamp (e.g., DateTime, DateTimeOffset).</typeparam>
    public interface IModificationAudited<TUser, TPrimaryKey, TDateTime> : IModificationAudited<TPrimaryKey, TDateTime>
        where TUser : class, IEntity<TPrimaryKey>
        where TPrimaryKey : struct
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the user entity who last modified the entity.
        /// </summary>

        TUser? Modifier { get; set; }
    }
}
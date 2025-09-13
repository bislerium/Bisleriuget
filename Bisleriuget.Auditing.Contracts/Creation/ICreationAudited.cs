using Bisleriuget.Auditing.Contracts.Entity;

namespace Bisleriuget.Auditing.Contracts.Creation
{
    /// <summary>
    /// Represents an entity that tracks the creation time and the creator's user ID.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key for the creator user.</typeparam>
    /// <typeparam name="TDateTime">The type used to represent the creation timestamp (e.g., DateTime, DateTimeOffset).</typeparam>
    public interface ICreationAudited<TPrimaryKey, TDateTime> : IHasCreationTime<TDateTime>
        where TPrimaryKey : struct
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the primary key of the user who created the entity.
        /// </summary>
        TPrimaryKey CreatedBy { get; set; }
    }

    /// <summary>
    /// Represents an entity that tracks the creation time and the creator user, along with their ID.
    /// </summary>
    /// <typeparam name="TUser">The type of the user entity associated with the creator.</typeparam>
    /// <typeparam name="TPrimaryKey">The type of the primary key for the creator user.</typeparam>
    /// <typeparam name="TDateTime">The type used to represent the creation timestamp (e.g., DateTime, DateTimeOffset).</typeparam>
    public interface ICreationAudited<TUser, TPrimaryKey, TDateTime> : ICreationAudited<TPrimaryKey, TDateTime>
        where TUser : class, IEntity<TPrimaryKey>
        where TPrimaryKey : struct
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the user entity that created the entity.
        /// </summary>
        TUser Creator { get; set; }
    }
}
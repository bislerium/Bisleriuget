using System;
using Bium.Auditing.Contracts.Entity;

namespace Bium.Auditing.Contracts.Creation
{
    /// <summary>
    /// Defines a contract for entities that capture auditing information 
    /// about their creation, including the timestamp and the identifier 
    /// of the user who created them.
    /// </summary>
    /// <typeparam name="TPrimaryKey">
    /// The type of the primary key for the creator user 
    /// (e.g., <see cref="int"/>, <see cref="long"/>, <see cref="Guid"/>).
    /// </typeparam>
    /// <typeparam name="TDateTime">
    /// The type used to represent the creation timestamp 
    /// (e.g., <see cref="DateTime"/>, <see cref="DateTimeOffset"/>).
    /// </typeparam>
    public interface ICreationAudited<TPrimaryKey, TDateTime>
        : IHasCreationTime<TDateTime>, IHasCreatorId<TPrimaryKey>
        where TPrimaryKey : struct
        where TDateTime : struct
    {
    }

    /// <summary>
    /// Defines a contract for entities that capture auditing information 
    /// about their creation, including the timestamp, the identifier of 
    /// the creator user, and a navigation reference to the creator entity.
    /// </summary>
    /// <typeparam name="TUser">
    /// The type of the user entity associated with the creator, which must 
    /// implement <see cref="IEntity{TPrimaryKey}"/>.
    /// </typeparam>
    /// <typeparam name="TPrimaryKey">
    /// The type of the primary key for the creator user 
    /// (e.g., <see cref="int"/>, <see cref="long"/>, <see cref="Guid"/>).
    /// </typeparam>
    /// <typeparam name="TDateTime">
    /// The type used to represent the creation timestamp 
    /// (e.g., <see cref="DateTime"/>, <see cref="DateTimeOffset"/>).
    /// </typeparam>
    public interface ICreationAudited<TUser, TPrimaryKey, TDateTime>
        : ICreationAudited<TPrimaryKey, TDateTime>, IHasCreator<TUser, TPrimaryKey>
        where TUser : class, IEntity<TPrimaryKey>
        where TPrimaryKey : struct
        where TDateTime : struct
    {
    }
}
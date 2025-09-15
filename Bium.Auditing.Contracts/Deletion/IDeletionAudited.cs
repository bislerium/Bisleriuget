using System;
using Bium.Auditing.Contracts.Entity;

namespace Bium.Auditing.Contracts.Deletion
{
    /// <summary>
    /// Defines a contract for entities that capture auditing information 
    /// about their deletion, including the deletion timestamp and the identifier 
    /// of the user who deleted them.
    /// </summary>
    /// <typeparam name="TPrimaryKey">
    /// The type of the primary key for the deleter user 
    /// (e.g., <see cref="int"/>, <see cref="long"/>, <see cref="Guid"/>).
    /// </typeparam>
    /// <typeparam name="TDateTime">
    /// The type used to represent the deletion timestamp 
    /// (e.g., <see cref="DateTime"/>, <see cref="DateTimeOffset"/>).
    /// </typeparam>
    public interface IDeletionAudited<TPrimaryKey, TDateTime>
        : IHasDeletionTime<TDateTime>, IHasDeleterId<TPrimaryKey>
        where TPrimaryKey : struct
        where TDateTime : struct
    {
    }

    /// <summary>
    /// Defines a contract for entities that capture auditing information 
    /// about their deletion, including the deletion timestamp, the identifier of 
    /// the deleter user, and a navigation reference to the deleter entity.
    /// </summary>
    /// <typeparam name="TUser">
    /// The type of the user entity associated with the deleter, which must 
    /// implement <see cref="IEntity{TPrimaryKey}"/>.
    /// </typeparam>
    /// <typeparam name="TPrimaryKey">
    /// The type of the primary key for the deleter user 
    /// (e.g., <see cref="int"/>, <see cref="long"/>, <see cref="Guid"/>).
    /// </typeparam>
    /// <typeparam name="TDateTime">
    /// The type used to represent the deletion timestamp 
    /// (e.g., <see cref="DateTime"/>, <see cref="DateTimeOffset"/>).
    /// </typeparam>
    public interface IDeletionAudited<TUser, TPrimaryKey, TDateTime>
        : IDeletionAudited<TPrimaryKey, TDateTime>, IHasDeleter<TUser, TPrimaryKey>
        where TUser : class, IEntity<TPrimaryKey>
        where TPrimaryKey : struct
        where TDateTime : struct
    {
    }

}
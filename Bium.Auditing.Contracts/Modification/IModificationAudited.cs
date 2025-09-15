using System;
using Bium.Auditing.Contracts.Entity;

namespace Bium.Auditing.Contracts.Modification
{
    /// <summary>
    /// Defines a contract for entities that capture auditing information 
    /// about their last modification, including the modification timestamp 
    /// and the identifier of the user who last modified them.
    /// </summary>
    /// <typeparam name="TPrimaryKey">
    /// The type of the primary key for the last modifier user 
    /// (e.g., <see cref="int"/>, <see cref="long"/>, <see cref="Guid"/>).
    /// </typeparam>
    /// <typeparam name="TDateTime">
    /// The type used to represent the modification timestamp 
    /// (e.g., <see cref="DateTime"/>, <see cref="DateTimeOffset"/>).
    /// </typeparam>
    public interface IModificationAudited<TPrimaryKey, TDateTime>
        : IHasModificationTime<TDateTime>, IHasModifierId<TPrimaryKey>
        where TDateTime : struct
        where TPrimaryKey : struct
    {
    }

    /// <summary>
    /// Defines a contract for entities that capture auditing information 
    /// about their last modification, including the modification timestamp, 
    /// the identifier of the last modifier, and a navigation reference to the modifier entity.
    /// </summary>
    /// <typeparam name="TUser">
    /// The type of the user entity associated with the last modifier, which must 
    /// implement <see cref="IEntity{TPrimaryKey}"/>.
    /// </typeparam>
    /// <typeparam name="TPrimaryKey">
    /// The type of the primary key for the last modifier user 
    /// (e.g., <see cref="int"/>, <see cref="long"/>, <see cref="Guid"/>).
    /// </typeparam>
    /// <typeparam name="TDateTime">
    /// The type used to represent the modification timestamp 
    /// (e.g., <see cref="DateTime"/>, <see cref="DateTimeOffset"/>).
    /// </typeparam>
    public interface IModificationAudited<TUser, TPrimaryKey, TDateTime>
        : IModificationAudited<TPrimaryKey, TDateTime>, IHasModifier<TUser, TPrimaryKey>
        where TUser : class, IEntity<TPrimaryKey>
        where TPrimaryKey : struct
        where TDateTime : struct
    {
    }
}
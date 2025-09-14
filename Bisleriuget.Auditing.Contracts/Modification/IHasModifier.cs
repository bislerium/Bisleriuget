using Bisleriuget.Auditing.Contracts.Entity;

namespace Bisleriuget.Auditing.Contracts.Modification
{
    public interface IHasModifier<TUser, TPrimaryKey> : IHasModifierId<TPrimaryKey>
        where TUser : class, IEntity<TPrimaryKey>
        where TPrimaryKey : struct
    {
        /// <summary>
        /// Gets or sets the user entity who last modified the entity.
        /// </summary>
        TUser? Modifier { get; set; }
    }
}
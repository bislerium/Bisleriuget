using Bium.Auditing.Contracts.Modification;

namespace Bium.Auditing.Contracts.Extensions.Modification
{
    /// <summary>
    /// Provides extension methods for setting the modifier identifier on entities
    /// that implement <see cref="IHasModifierId{TPrimaryKey}"/>.
    /// </summary>
    public static class ModifierIdExtensions
    {
        /// <summary>
        /// Sets the <see cref="IHasModifierId{TPrimaryKey}.ModifiedBy"/> property
        /// to the specified modifier identifier.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the modifier's identifier (e.g., int, Guid).</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="modifiedBy">The identifier of the modifier.</param>
        public static void SetModifiedBy<TPrimaryKey>(this IHasModifierId<TPrimaryKey> entity, TPrimaryKey modifiedBy)
            where TPrimaryKey : struct =>
            entity.ModifiedBy = modifiedBy;
    }
}
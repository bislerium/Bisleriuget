namespace Bisleriuget.Auditing.Contracts.Modification
{
    public interface IHasModifierId<TPrimaryKey> : IAuditKind
        where TPrimaryKey : struct
    {
        /// <summary>
        /// Gets or sets the primary key of the user who last modified the entity.
        /// </summary>
        TPrimaryKey? ModifiedBy { get; set; }
    }
}
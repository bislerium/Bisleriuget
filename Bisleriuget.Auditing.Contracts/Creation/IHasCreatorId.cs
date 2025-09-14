namespace Bisleriuget.Auditing.Contracts.Creation
{
    public interface IHasCreatorId<TPrimaryKey> : IAuditKind
        where TPrimaryKey : struct
    {
        /// <summary>
        /// Gets or sets the primary key of the user who created the entity.
        /// </summary>
        TPrimaryKey CreatedBy { get; set; }
    }
}
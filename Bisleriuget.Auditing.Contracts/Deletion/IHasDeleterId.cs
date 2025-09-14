namespace Bisleriuget.Auditing.Contracts.Deletion
{
    public interface IHasDeleterId<TPrimaryKey> : ISoftDeletable
        where TPrimaryKey : struct
    {
        /// <summary>
        /// Gets or sets the primary key of the user who deleted the entity.
        /// </summary>
        TPrimaryKey? DeletedBy { get; set; }
    }
}
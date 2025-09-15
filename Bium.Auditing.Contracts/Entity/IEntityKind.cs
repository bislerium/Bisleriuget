namespace Bium.Auditing.Contracts.Entity
{
    /// <summary>
    /// Serves as a marker interface for all entity types.
    /// </summary>
    /// <remarks>
    /// This interface does not define any members.  
    /// It is used to indicate that a type represents a domain entity, 
    /// allowing the application or infrastructure (e.g., repositories, 
    /// unit of work, or entity framework integration) to apply 
    /// entity-specific behavior.
    /// </remarks>
    public interface IEntityKind
    {
    }
}
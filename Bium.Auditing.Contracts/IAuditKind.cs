namespace Bium.Auditing.Contracts
{
    /// <summary>
    /// Serves as a marker interface for all auditable entity types.
    /// </summary>
    /// <remarks>
    /// This interface does not define any members.  
    /// It is used to identify entities that are subject to auditing, 
    /// enabling the application or infrastructure (e.g., interceptors, 
    /// middleware, or auditing services) to apply audit-specific 
    /// behavior such as tracking creation, modification, or deletion.
    /// </remarks>
    public interface IAuditKind
    {
    }
}
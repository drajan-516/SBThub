namespace SBThub.Domain.Common;

public abstract class Entity
{
    protected Entity(Guid uuid) => Uuid = uuid;
    
    protected Entity() { }

    public int Id { get; protected set; }

    public Guid Uuid { get; protected set; } = Guid.NewGuid();
    
    public DateTimeOffset CreatedOn { get; protected set; } = DateTimeOffset.UtcNow;
}

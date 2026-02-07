namespace EstimadorAPI.Domain.Common;


/// <summary>
/// Clase base para todas las entidades del dominio
/// </summary>
public abstract class Entity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;

    protected Entity() { }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity entity)
            return false;

        return Id == entity.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Entity? left, Entity? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Id == right.Id;
    }

    public static bool operator !=(Entity? left, Entity? right)
    {
        return !(left == right);
    }
}
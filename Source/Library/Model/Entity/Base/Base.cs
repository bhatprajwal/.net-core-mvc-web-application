namespace Entity;

[Serializable]
public abstract class Base<T>
    where T : struct
{
    public T Id { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}

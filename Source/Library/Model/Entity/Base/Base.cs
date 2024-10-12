namespace Models;

[Serializable]
public class Base<T>
{
    public T Id { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}

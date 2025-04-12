namespace Entity;

/// <summary>
/// Base
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
public abstract class Base<T>
    where T : struct
{
    /// <summary>
    /// Id
    /// </summary>
    public T Id { get; set; }

    /// <summary>
    /// IsActive
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// IsDeleted
    /// </summary>
    public bool IsDeleted { get; set; }
}

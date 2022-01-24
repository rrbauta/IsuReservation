using IsuReservation.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IsuReservation.Models.Entities;

public abstract class BaseEntity
{
    public List<BaseDomainEvent> Events = new();

    /// <summary>
    ///     UserMetaData Backing Field
    ///     https://docs.microsoft.com/en-us/ef/core/modeling/backing-field
    /// </summary>
    protected virtual string? MetaData { get; set; }

    /// <summary>
    ///     Identifier of the entity.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Created date of the entity.
    /// </summary>
    public DateTime DateCreated { get; set; } = DateTime.Now;

    /// <summary>
    ///     Modified date of the entity.
    /// </summary>
    public DateTime DateModified { get; set; } = DateTime.Now;

    /// <summary>
    ///     Entity deleted. This field will be used to implement a soft delete
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    ///     Serialize the object as a json data, null fields will be not rendered.
    /// </summary>
    /// <returns>serialized json</returns>
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, (Formatting) System.Xml.Formatting.None,
            new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
    }

    /// <summary>
    ///     Get json backing field deserialized as metadata.
    /// </summary>
    /// <typeparam name="T">core model metadata wrapper</typeparam>
    /// <returns>metadata from backing field</returns>
    public T? GetMetaData<T>()
        where T : BaseMetaData, new()
    {
        if (MetaData != null)
            return JObject.Parse(MetaData).ToObject<T>() ?? new T();

        return default;
    }

    /// <summary>
    ///     Set metadata to backing field.
    /// </summary>
    /// <typeparam name="T">core model metadata wrapper</typeparam>
    /// <param name="json">model instance holding data</param>
    public void SetMetaData<T>(T json)
    {
        MetaData = JsonConvert.SerializeObject(json);
    }
}
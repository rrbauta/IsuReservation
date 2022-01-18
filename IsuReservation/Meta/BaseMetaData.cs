using Newtonsoft.Json;

namespace IsuReservation.Meta;

/// <summary>
///     Describe a BasicMetadata
/// </summary>
public interface IBaseMetaData
{
    string ToJson();
}

/// <summary>
///     implement a basic meta data
/// </summary>
public abstract class BaseMetaData : IBaseMetaData
{
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.None,
            new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
    }
} // class
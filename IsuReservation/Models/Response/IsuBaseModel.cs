using Newtonsoft.Json;

namespace IsuReservation.Models.Response;

public abstract class IsuBaseModel
{
    /// <summary>
    ///     Serialize the object as a json data, null fields will be not rendered.
    /// </summary>
    /// <returns>serialized json string</returns>
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.None,
            new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
    }
} // class
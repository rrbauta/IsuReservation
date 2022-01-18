using Newtonsoft.Json;

namespace IsuReservation.Abstract;

public interface IBaseMetaData
{
    string ToJson();
}

public abstract class BaseMetaData : IBaseMetaData
{
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.None,
            new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
    }
} // class
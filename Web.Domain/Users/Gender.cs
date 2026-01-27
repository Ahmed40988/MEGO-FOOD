using System.Text.Json.Serialization;

namespace Web.Domain.Users
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        Male = 0,
        Female = 1
    }
}

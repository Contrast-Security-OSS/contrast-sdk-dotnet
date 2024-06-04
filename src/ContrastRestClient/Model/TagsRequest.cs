using Newtonsoft.Json;

namespace Contrast.Model
{
    public class TagsRequest
    {
        [JsonProperty(PropertyName ="tag")]
        string Tag { get; set; }
    }
}
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Contrast.Model
{
    public class TagsApplicationsUpdateRequest
    {
        [JsonProperty(PropertyName ="applications_id")]
        List<string> applications_id {  get; set; }
        [JsonProperty(PropertyName = "links")]
        List<string> links {  get; set; }
        [JsonProperty(PropertyName ="links")]
        List<string> tags {  get; set; }
        [JsonProperty(PropertyName ="tags_remove")]
        List<string> tags_remove {  get; set; }

    }
}
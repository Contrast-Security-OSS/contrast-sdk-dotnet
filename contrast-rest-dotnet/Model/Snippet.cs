using System.Collections.Generic;
using System.Runtime.Serialization;

namespace contrast_rest_dotnet.Model
{
    [DataContract]
    public class Snippet
    {
        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "formattedText")]
        public string FormattedText { get; set; }

        [DataMember(Name = "formattedTextVariables")]
        public Dictionary<string, string> FormattedTextVariables { get; set; }
    }
}

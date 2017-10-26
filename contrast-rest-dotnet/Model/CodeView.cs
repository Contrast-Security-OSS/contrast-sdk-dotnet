using System.Collections.Generic;
using System.Runtime.Serialization;

namespace contrast_rest_dotnet.Model
{
    [DataContract]
    public class CodeView
    {
        /// <summary>
        /// List of code lines.
        /// </summary>
        [DataMember(Name = "lines")]
        public List<CodeLine> Lines { get; set; }

        /// <summary>
        /// If the code view is nested.
        /// </summary>
        [DataMember(Name = "nested")]
        public bool Nested { get; set; }
    }
    
    [DataContract]
    public class CodeLine
    {
        /// <summary>
        /// Formatted fragments of code.
        /// </summary>
        [DataMember(Name = "fragments")]
        public List<LineFragment> Fragments { get; set; }

        /// <summary>
        /// Full line of code.
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }
    }

    [DataContract]
    public class LineFragment
    {
        /// <summary>
        /// Type of fragment.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Fragment content.
        /// </summary>
        [DataMember(Name = "value")]
        public string value { get; set; }
    }
}

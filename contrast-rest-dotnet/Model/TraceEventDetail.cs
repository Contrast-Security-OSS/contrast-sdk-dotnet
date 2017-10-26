using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace contrast_rest_dotnet.Model
{
    [DataContract]
    public class TraceEventDetail
    {
        /// <summary>
        /// [Optional] Class name
        /// </summary>
        [DataMember(Name = "className")]
        public string ClassName { get; set; }

        /// <summary>
        /// Last custom frame.
        /// </summary>
        [DataMember(Name = "lastCustomFrame")]
        public long? LastCustomFrame { get; set; }

        /// <summary>
        /// [Optional] Method
        /// </summary>
        [DataMember(Name = "method")]
        public string Method { get; set; }

        /// <summary>
        /// [Optional] Object
        /// </summary>
        [DataMember(Name = "object")]
        public string Object { get; set; }

        /// <summary>
        /// If the object is being tracked.
        /// </summary>
        [DataMember(Name = "objectTracked")]
        public bool ObjectTracked { get; set; }

        /// <summary>
        /// List of parameters
        /// </summary>
        [DataMember(Name = "parameters")]
        public List<EventParameter> Parameters { get; set; }

        /// <summary>
        /// If the return is tracked.
        /// </summary>
        [DataMember(Name = "returnTracked")]
        public bool ReturnTracked { get; set; }

        /// <summary>
        /// [Optional] Return value.
        /// </summary>
        [DataMember(Name = "returnValue")]
        public string ReturnValue { get; set; }

        /// <summary>
        /// List of stack traces.
        /// </summary>
        [DataMember(Name = "stackTraces")]
        public List<Stacktrace> StackTraces { get; set; }
    }

    [DataContract]
    public class EventParameter
    {
        [DataMember(Name = "parameter")]
        public string Parameter { get; set; }

        [DataMember(Name = "tracked")]
        public bool Tracked { get; set; }
    }

    [DataContract]
    public class Stacktrace
    {
        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }

    [DataContract]
    public class TraceEventDetailResponse
    {
        /// <summary>
        /// Event
        /// </summary>
        [DataMember(Name = "event")]
        public TraceEventDetail Event { get; set; }

        /// <summary>
        /// List of messges
        /// </summary>
        [DataMember(Name = "messages")]
        public List<string> Messages { get; set; }

        /// <summary>
        /// Indicates whether API response was successful or not
        /// </summary>
        [DataMember(Name = "succes")]
        public bool Success { get; set; }
    }
}

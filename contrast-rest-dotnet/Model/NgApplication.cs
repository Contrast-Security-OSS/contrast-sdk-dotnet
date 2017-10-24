using System;
using System.Collections.Generic;
using contrast_rest_dotnet.Serialization;
using System.Runtime.Serialization;

namespace contrast_rest_dotnet.Model
{
    [DataContract]
    public class ApplicationIdentity
    {
        /// <summary>
        /// Application id.
        /// </summary>
        [DataMember(Name = "app_id")]
        public string AppId { get; set; }

        /// <summary>
        /// If this application is a child of another.
        /// </summary>
        [DataMember(Name = "child")]
        public bool Child { get; set; }

        /// <summary>
        /// Application context path.
        /// </summary>
        [DataMember(Name = "context_path")]
        public string ContextPath { get; set; }

        /// <summary>
        /// Application importance
        /// </summary>
        [DataMember(Name = "importance")]
        public int? Importance { get; set; }

        /// <summary>
        /// Application language.
        /// </summary>
        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "last_seen")]
        public long? LastSeenRawValue { get; set; }

        /// <summary>
        /// Time last seen.
        /// </summary>
        public DateTime? LastSeen { get; set; }

        /// <summary>
        /// License level. Allowed values: ReadOnly, Unlicensed, Licensed.
        /// </summary>
        [DataMember(Name = "license_level")]
        public string LicenseLevel { get; set; }

        /// <summary>
        /// If this application is master.
        /// </summary>
        [DataMember(Name = "master")]
        public bool Master { get; set; }

        /// <summary>
        /// Parent app id.
        /// </summary>
        [DataMember(Name = "parent_app_id")]
        public string ParentAppId { get; set; }

        /// <summary>
        /// List of allowed roles.
        /// </summary>
        [DataMember(Name = "roles")]
        public List<string> Roles { get; set; }

        /// <summary>
        /// Number of app modules
        /// </summary>
        [DataMember(Name = "total_modules")]
        public long TotalModules { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            LastSeen = MicrosecondDateTimeConverter.ConvertFromEpochTime(LastSeenRawValue);
        }
    }

    [DataContract]
    public class NgApplicationTraceBase : ApplicationIdentity
    {
        /// <summary>
        /// Application name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}

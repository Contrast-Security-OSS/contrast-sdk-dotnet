using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace contrast_rest_dotnet.Model
{
    [DataContract]
    public class RemediationPolicy
    {
        /// <summary>
        /// Apply to all applications.
        /// </summary>
        [DataMember(Name = "all_applications")]
        public bool AllApplications { get; set; }

        /// <summary>
        /// Appy to all rules.
        /// </summary>
        [DataMember(Name = "all_rules")]
        public bool AllRules { get; set; }

        /// <summary>
        /// Apply to all server environments.
        /// </summary>
        [DataMember(Name = "all_server_environments")]
        public bool AllServerEnvironments { get; set; }

        //TODO Add application_importance if required.

        [DataMember(Name = "applications")]
        public List<ApplicationIdentity> Applications { get; set; }

        /// <summary>
        /// If this policy is enabled.
        /// </summary>
        [DataMember(Name = "enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        /// Remediation policy name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Remediation policy id.
        /// </summary>
        [DataMember(Name = "policy_id")]
        public long? PolicyId { get; set; }

        /// <summary>
        /// Remediation days.
        /// </summary>
        [DataMember(Name = "remediation_days")]
        public int? RemediationDays { get; set; }

        //TODO Include rule_severity, rules and server_environments if required.

        /// <summary>
        /// Action to be takend by remediation policy on vulnerabilities violation. Allowed values: NOTIFIY, AUTO_REMEDIATE.
        /// </summary>
        [DataMember(Name = "strategy")]
        public string Strategy { get; set; }

        /// <summary>
        /// If this policy is valid.
        /// </summary>
        [DataMember(Name = "valid")]
        public bool Valid { get; set; }
    }
}

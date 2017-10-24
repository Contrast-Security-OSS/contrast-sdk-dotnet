using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contrast_rest_dotnet
{
    internal static class NgEndpoints
    {
        internal static string APPLICATIONS = "api/ng/{0}/applications/{1}";
        internal static string APPLICATION_LIBRARIES = "api/ng/{0}/applications/{1}/libraries";
        internal static string APPLICATION_TRACES = "api/ng/{0}/traces/{1}/filter";
        internal static string DEFAULT_ORGANIZATION = "api/ng/profile/organizations/default";
        internal static string ENGINE_DOTNET = "api/{0}/engine/{1}/dotnet";//TODO Update
        internal static string ENGINE_JAVA = "api/{0}/engine/{1}/java";//TODO Update
        internal static string LIBRARIES = "api/ng/{0}/libraries";
        internal static string ORGANIZATIONS = "api/ng/profile/organizations/";
        internal static string ORGANIZATION_TRACES = "api/ng/{0}/orgtraces/filter";
        internal static string PROFILES = "api/{0}/engine/profiles/{1}";//TODO Update
        internal static string SERVERS = "api/ng/{0}/servers/{1}";
        internal static string SERVER_TRACES = "api/ng/{0}/servertraces/{1}/filter";
        internal static string TRACE_SUMMARY = "api/ng/{0}/traces/{1}";
        internal static string TRACE_EVENTS = "api/ng/{0}/traces/{1}/events/summary";
        internal static string TRACE_EVENT_DETAIL = "api/ng/{0}/traces/{1}/events/{2}/details";
    }
}

# Contrast REST Client

[![Build status](https://ci.appveyor.com/api/projects/status/7tcoujkmbwl2hg85?svg=true)](https://ci.appveyor.com/project/rduran0/contrast-sdk-dotnet)

This library provides a simple REST client for retrieving data from Contrast Team Server's REST API as plain old C# objects.  

This library is also provided as a nuget package: https://www.nuget.org/packages/ContrastRestClient/. 

Please see http://www.contrastsecurity.com for more information about how Contrast can help secure your applications.

## 3.0 Changelog

The 3.X line of packages has a few changes from the 2.X line that you might need to address in your code.  You may need to reload your .sln file if you've worked with the package in the 2.X line.

* Muti-Targeted `netstandard2.0` and `net45`.
* Namespace changed from `contrast_rest_dotnet` to `Contrast`.
* Removed the method `TeamServerClient.CheckForTrace`.
* Renamed `TeamServerClient` to `Client`.
* Removed deprecated `Endpoints` class.
* Renamed the following symbols:

```
AgentType.Java1_5 -> AgentType.Java15

Client.GetApplicationTraceFilterSubfilters -> Client.GetApplicationTraceFilterSubFilters
Client.GetServerTraceFilterSubfilters -> Client.GetServerTraceFilterSubFilters

ContrastRestClient.PostApplicatonSpecificMessage -> ContrastRestClient.PostApplicationSpecificMessage
IContrastRestClient.PostApplicatonSpecificMessage -> IContrastRestClient.PostApplicationSpecificMessage

LineFragment.value -> LineFragment.Value

ContrastApplication.AppID -> ContrastApplication.AppId
ContrastApplication.Stauts -> ContrastApplication.Status

Organization.name -> Organization.Name
Organization.shortname -> Organization.ShortName
Organization.timezone -> Organization.Timezone
Organization.organization_uuid -> Organization.OrganizationId
Organization.AppsOnboarded -> Organization.AppsOnBoarded
Organization.IsSuperadmin -> Organization.IsSuperAdmin
Organization.Superadmin -> Organization.SuperAdmin

OrganizationResponse.success -> OrganizationResponse.Organizations
OrganizationResponse.count -> OrganizationResponse.Count
OrganizationResponse.org_disabled -> OrganizationResponse.OrganizationDisabled

DefaultOrganizationResponse.org_disabled -> DefaultOrganizationResponse.Success
DefaultOrganizationResponse.messages -> DefaultOrganizationResponse.Messages
DefaultOrganizationResponse.organization -> DefaultOrganizationResponse.Organization
DefaultOrganizationResponse.roles -> DefaultOrganizationResponse.Roles
DefaultOrganizationResponse.enterprise -> DefaultOrganizationResponse.Enterprise

Trace.Uuid -> Trace.Id
TraceNote.CreatorUUID -> TraceNote.CreatorId
TraceNote.LastUpdaterUUID -> TraceNote.LastUpdaterId

TraceBreakdown.Confirmed -> TraceBreakdown.ConfirmedVulnerabilities
TraceBreakdown.Criticals -> TraceBreakdown.CriticalVulnerabilities
TraceBreakdown.Fixed -> TraceBreakdown.FixedVulnerabilities
TraceBreakdown.HighVulns -> TraceBreakdown.HighVulnerabilities
TraceBreakdown.LowVulns -> TraceBreakdown.LowVulnerabilities
TraceBreakdown.Mediums -> TraceBreakdown.MediumVulnerabilities
TraceBreakdown.NoProblemVulns -> TraceBreakdown.NoProblemVulnerabilities
TraceBreakdown.notes -> TraceBreakdown.Notes
TraceBreakdown.SafeVulns -> TraceBreakdown.SafeVulnerabilities

TraceStatus.CONFIRMED_STATUS -> TraceStatus.Confirmed
TraceStatus.SUSPICIOUS_STATUS -> TraceStatus.Suspicious
TraceStatus.NOT_A_PROBLEM_STATUS -> TraceStatus.NotAProblem
TraceStatus.REMEDIATED_STATUS -> TraceStatus.Remediated
TraceStatus.REPORTED_STATUS -> TraceStatus.Reported
TraceStatus.FIXED_STATUS -> TraceStatus.Fixed

TraceMarkStatusRequest.Substatus -> TraceMarkStatusRequest.SubStatus
```

## Contrast API Credentials
To access the API, you'll first need access Contrast (https://app.contrastsecurity.com/Contrast/login.html) or an on-premises installation of Contrast.

Your API credentials can be found by following these steps:

1. Log in to Contrast
2. Click the down arrow next to your login name in the page header
3. Click on "Your Account"
4. Your API credentials will be listed under "YOUR KEYS"

More API documentation can be found here: https://support.contrastsecurity.com/entries/24184140-Accessing-the-API

## Sample Client Application
The SampleContrastClient uses the App.config to store API credentials. To use the sample application, copy the API values from above into the appropriate appSettings entries:

```
  <appSettings>
    <add key="TeamServerUrl" value="https://app.contrastsecurity.com/Contrast/"/>
    <add key="TeamServerUserName" value=""/>
    <add key="TeamServerApiKey" value=""/>
    <add key="TeamServerServiceKey" value=""/>
  </appSettings>
```

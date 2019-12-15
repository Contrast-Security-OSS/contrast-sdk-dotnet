# Contrast REST Client

[![Build status](https://ci.appveyor.com/api/projects/status/7tcoujkmbwl2hg85?svg=true)](https://ci.appveyor.com/project/rduran0/contrast-sdk-dotnet)

This library provides a simple REST client for retrieving data from Contrast Team Server's REST API as plain old C# objects.  

This library is also provided as a nuget package: https://www.nuget.org/packages/ContrastRestClient/. 

Please see http://www.contrastsecurity.com for more information about how Contrast can help secure your applications.

## 3.0 Changelog

The 3.X line of packages has a few changes from the 2.X line that you might need to address in your code

* namespace changed from `contrast_rest_dotnet` to `Contrast`
* Removed the method `TeamServerClient.CheckForTrace`
* Renamed `TeamServerClient` to `Client`
* Removed deprecated `Endpoints` class

## Dependencies
* Newtonsoft.Json


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

# Contrast REST Client

[![Build status](https://ci.appveyor.com/api/projects/status/7tcoujkmbwl2hg85?svg=true)](https://ci.appveyor.com/project/rduran0/contrast-sdk-dotnet)

This library provides a simple REST client for retrieving data from Contrast Team Server's REST API as plain old C# objects.  

This library is also provided as a nuget package: https://www.nuget.org/packages/ContrastRestClient/. 

Please see http://www.contrastsecurity.com for more information about how Contrast can help secure your applications.


## Contrast TeamServer API Credentials
To access the TeamServer API, you'll first need access to TeamServer - either SAAS (https://app.contrastsecurity.com/Contrast/login.html) or an on-premises installation of TeamServer.

To begin using the Contrast API you will need to retrieve your API-Key from the server. To do this, you will need to log in to your TeamServer account and have the application e-mail you a generated API-Key.

1. Log in to TeamServer
2. Click the down arrow next to your login name in the page header
3. Click on API Key
4. Click either Email me the current "API Key" or "Rotate the key and email me a new key"

The TeamServer client requires the TeamServer URL, account username, service key, API key and TeamServer URL.

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
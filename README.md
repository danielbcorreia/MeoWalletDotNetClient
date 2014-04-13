# MEO Wallet API Client

## Requirements and dependencies
* .NET Framework 4.5
* HttpClient (System.Net.Http and System.Net.Http.Formatting)
* Newtonsoft.Json (JSON.Net)

## Configuration
Some of the client components expect you to have some AppSettings in place. 
You can always do your configuration by code anyway, but it will be more verbose.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <!-- these can be specified by code when calling the begin checkout operation -->
    <add key="confirmWalletCallbackUrl" value="http://localhost:1234/credit/confirm" />
    <add key="cancelWalletCallbackUrl" value="http://localhost:1234/credit/cancel" />
    <!-- these can be specified on the MeoWalletHttpClientFactory constructor -->
    <add key="MerchantApiKey" value="TODO" />
    <add key="WalletBaseUri" value="https://services.wallet.codebits.eu/api/v2/" />
  </appSettings>
</configuration>
```

## License
MIT License

Copyright (c) 2014 Daniel Correia

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

## Architecture
The HttpClient is created by the MeoWalletHttpClientFactory. 
This factory builds a delegating handler pipeline, to inject the MeoWalletAuthenticationHandler. This is is handler that inserts the Authorization header in every request.
The factory also adds an "Accept" header and sets the Base URI of the API, so you don't have to worry about it when making the HTTP requests.
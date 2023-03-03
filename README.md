# .NET Client for AnyPay
AnyPay is a payment system that allows you to accept payments using the API.

This **.NET** library will help you work with **[AnyPay](https://anypay.io/)** through the [Any Pay API](https://anypay.io/doc/api).

## Install

Use the [nuget package](https://www.nuget.org/packages/AnyPay/).

## Usage
```csharp
var anyPayClient = new AnyPayClient(
    "YOUR_API_ID",
    "YOUR_API_KEY",
    12345 //Your project id
);

var myBalance = await anyPayClient.GetBalanceAsync(cancellationToken);
```

## Tests
There are no tests in the project, if you want to help write them, then send a pull request.
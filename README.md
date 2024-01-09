# .NET Client for AnyPay

[![package](https://img.shields.io/nuget/vpre/AnyPay.svg?label=AnyPay&style=flat-square)](https://www.nuget.org/packages/AnyPay)
[![downloads](https://img.shields.io/nuget/dt/AnyPay.svg?style=flat-square&label=Package%20Downloads)](https://www.nuget.org/packages/AnyPay)
[![contributors](https://img.shields.io/github/contributors/BloodShadow174/AnyPay.NET.svg?style=flat-square&label=Contributors)](https://github.com/BloodShadow174/AnyPay.NET/graphs/contributors)
[![license](https://img.shields.io/github/license/BloodShadow174/AnyPay.NET.svg?style=flat-square&maxAge=2592000&label=License)](https://raw.githubusercontent.com/BloodShadow174/AnyPay.NET/master/LICENSE)

AnyPay is a payment system that allows you to accept payments using the API.

This **.NET** library will help you work with **[AnyPay](https://anypay.io/)** through the [AnyPay API](https://anypay.io/doc/api).

## Install

Use the [nuget package](https://www.nuget.org/packages/AnyPay/).

## Usage
```csharp
var anyPayClient = new AnyPayClient(
    "YOUR_API_ID",
    "YOUR_API_KEY",
    "YOUR_SECRET_KEY",
    12345 //Your project id
);

var myBalance = await anyPayClient.GetBalanceAsync(cancellationToken);
```

## Tests
There are no tests in the project, if you want to help write them, then send a pull request.
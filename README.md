# InterReact&nbsp;&nbsp; [![License](https://img.shields.io/badge/Version-0.1.1-blue)]() [![License](https://img.shields.io/badge/license-Apache%202.0-7755BB.svg)](https://opensource.org/licenses/Apache-2.0)

***Reactive C# API to Interactive Brokers***
- **.NET 6.0** library
- compatible with Interactive Brokers API 9.85.02 (Aug 5, 2021)
- dependencies: RxSockets, StringEnums, Reactive Extensions, NodaTime
- demo applications: Console, WPF

```csharp
interface IInterReactClient : IAsyncDisposable
{
    Request Request { get; }
    IObservable<object> Response { get; }
    Services Services { get; }
}
```
### Example ###
```csharp
using System;
using System.Threading.Tasks;
using System.Reactive.Linq;
using InterReact;
```
```csharp
// Create the InterReact client by first connecting to TWS/Gateway on the local host.
IInterReactClient client = await InterReactClientBuilder
    .Create()
    .BuildAsync();

// Create a contract object.
Contract contract = new()
{
   SecurityType = SecurityType.Stock,
   Symbol       = "SPY",
   Currency     = "USD",
   Exchange     = "SMART"
};

// Create and then subscribe to the observable which can observe ticks for the contract.
IDisposable subscription = client
    .Services
    .CreateTickObservable(contract)
    .OfTickClass(selector => selector.PriceTick)
    .Subscribe(onNext: priceTick => 
        Console.WriteLine($"Price = {priceTick.Price}"));

Console.WriteLine(Environment.NewLine + "press a key to exit...");
Console.ReadKey();
Console.Clear();

// Dispose the subscription to stop receiving ticks.
subscription.Dispose();

// Disconnect from TWS/Gateway.
await client.DisposeAsync();
```
### Notes ###

Interactive Brokers Trader Workstation (TWS) or Gateway must be running with API access enabled. In TWS, navigate to Edit / Global Configuration / API / Settings and ensure the option "Enable ActiveX and Socket Clients" is selected.

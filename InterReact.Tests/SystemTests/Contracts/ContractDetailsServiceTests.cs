﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace InterReact.SystemTests.Contracts;

public class ContractDetailsServiceTests : TestCollectionBase
{
    public ContractDetailsServiceTests(ITestOutputHelper output, TestFixture fixture) : base(output, fixture) { }
    [Fact]
    public async Task TestSingle()
    {
        Contract contract = new() { SecurityType = SecurityType.Stock, Symbol = "IBM", Currency = "USD", Exchange = "SMART" };

        IList<ContractDetails> cds = await Client
            .Services
            .CreateContractDetailsObservable(contract)
            .ToList();

        Assert.Single(cds);
    }

    [Fact]
    public async Task TestMulti()
    {
        Contract contract = new() { SecurityType = SecurityType.Stock, Symbol = "IBM", Currency = "USD" };

        var cds = await Client
            .Services
            .CreateContractDetailsObservable(contract)
            .ToList();

        Assert.True(cds.Count > 1); // multiple exchanges
    }

    [Fact]
    public async Task TestInvalid()
    {
        var contract = new Contract { ContractId = 99999 };

        var alertException = await Assert.ThrowsAsync<AlertException>(async () => await Client
            .Services
            .CreateContractDetailsObservable(contract));

        Assert.Equal(200, alertException.Alert.Code);
    }

    [Fact]
    public async Task TestTimeout()
    {
        var contract = new Contract { SecurityType = SecurityType.Stock, Symbol = "IBM", Currency = "EUR" };

        await Assert.ThrowsAsync<TimeoutException>(async () => await Client
            .Services
            .CreateContractDetailsObservable(contract)
            .Timeout(TimeSpan.Zero));
    }
}

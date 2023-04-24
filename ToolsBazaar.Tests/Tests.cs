using Castle.DynamicProxy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Reflection;
using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Domain.OrderAggregate;

namespace ToolsBazaar.Tests;

public class Tests
{
    [Fact]
    public void SampleTest()
    {
        var x = 10;

        x.Should().Be(10);
    }

    [Fact]
    public void GivenOrdersCalls_ExpectToReturn_Top5Customer()
    {
        var orders = new List<Order>();
        var customers = new List<Customer>();
        var startDate = DateTime.Now;
        var endDate = DateTime.Now.AddDays(2);
        var customer = Substitute.For<ICustomerRepository>();
        customer.GetTop(orders,startDate,  endDate).Returns(customers);
        Assert.Equal((IEnumerable<Customer>)customer
            .GetTop(orders, startDate, endDate), 
                customers.AsEnumerable());

    }

    [Fact]
    public void GivenOrdersCalls_ExpectToReturn_Top5Customer_ShouldBeReturnNull()
    {
        var orders = new List<Order>();
        var customers = new List<Customer>();
        var startDate = DateTime.Now.AddDays(2);
        var endDate = DateTime.Now;
        var customer = Substitute.For<ICustomerRepository>();
        customer.GetTop(orders, startDate, endDate).Returns(customers);
        Assert.Empty((IEnumerable<Customer>)customer.GetTop(orders,startDate, endDate));

    }

}
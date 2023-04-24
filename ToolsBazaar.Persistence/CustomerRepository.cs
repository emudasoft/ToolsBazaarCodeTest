using System.Numerics;
using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Domain.OrderAggregate;

namespace ToolsBazaar.Persistence;

public class CustomerRepository : ICustomerRepository
{
    public IEnumerable<Customer> GetAll() => DataSet.AllCustomers;

    public IEnumerable<Customer> GetTop(IEnumerable<Order> orders, DateTime startDate, DateTime endDate)
    {
        IEnumerable<Order> ordersBetweenDate = orders
                                   .Where(x => x.Date > startDate
                                   && x.Date <= endDate)
                                   .ToList();

        var customerAndTotalPrice = (from _orders in ordersBetweenDate
                                     select new
                                     {
                                         total = _orders.Items.Sum(s => s.Price),
                                         customer = _orders.Customer
                                     }).
                     OrderByDescending(x => x.total).Take(5).ToList();

        var result = from cus in customerAndTotalPrice
                     select cus.customer;
        return result;
    }

    public void UpdateCustomerName(int customerId, string name)
    {
        var customer = DataSet.AllCustomers.FirstOrDefault(c => c.Id == customerId);
        customer.UpdateName(name);
    }
}
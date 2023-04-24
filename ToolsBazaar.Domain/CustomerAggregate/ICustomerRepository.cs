using ToolsBazaar.Domain.OrderAggregate;

namespace ToolsBazaar.Domain.CustomerAggregate;

public interface ICustomerRepository
{
    IEnumerable<Customer> GetAll();
    IEnumerable<Customer> GetTop(IEnumerable<Order> orders, DateTime startDate, DateTime endDate);
    void UpdateCustomerName(int customerId, string name);
}

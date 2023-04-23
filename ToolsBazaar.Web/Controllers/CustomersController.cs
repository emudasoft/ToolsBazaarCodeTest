using Microsoft.AspNetCore.Mvc;
using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Domain.OrderAggregate;

namespace ToolsBazaar.Web.Controllers;

public record CustomerDto(string Name);

[ApiController]
[Route("[controller]")]

public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(ILogger<CustomersController> logger, ICustomerRepository customerRepository, IOrderRepository orderRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
    }

    [HttpPut("{customerId:int}")]
    public void UpdateCustomerName(int customerId, [FromRoute] CustomerDto dto)
    {
        try
        {
            _logger.LogInformation($"Updating customer #{customerId} name...");
            _customerRepository.UpdateCustomerName(customerId, dto.Name);
        }
        catch (Exception ex)
        {

            _logger.LogInformation($"Updating customer #{customerId} name...");
            throw ex;
        }


    }
    [HttpGet("top")]
    public IEnumerable<Customer> GetTop()
    {
        _logger.LogInformation($"The top five customers who spent the most between 1/1/2015 and 31/12/2022");


        IEnumerable<Order> orders = _orderRepository.GetAll()
                                    .Where(x => x.Date > DateTime.Parse("1/1/2015")
                                    && x.Date <= DateTime.Parse("12/31/2022"))
                                    .ToList();

        var customerAndTotalPrice = (from _orders in orders
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
}
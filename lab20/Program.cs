using lab20.Interfaces;
using lab20.Models;
using lab20.Repositories;
using lab20.Services;

namespace lab20;

class Program
{
    static void Main()
    {
        // Dependency Injection
        IOrderValidator validator = new OrderValidator();
        IOrderRepository repository = new InMemoryOrderRepository();
        IEmailService emailService = new ConsoleEmailService();

        OrderService orderService = new OrderService(
            validator,
            repository,
            emailService);

        // Valid order
        Order validOrder = new Order(1, "Ivan Petrenko", 1200m);
        orderService.ProcessOrder(validOrder);

        // Invalid order
        Order invalidOrder = new Order(2, "Oksana Shevchenko", -300m);
        orderService.ProcessOrder(invalidOrder);

        Console.ReadLine();
    }
}

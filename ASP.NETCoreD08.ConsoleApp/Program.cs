namespace ASP.NETCoreD08.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var orderService = new OrderService(
            //    new PaymentService(),
            //    new OrderRepository(),
            //    new EmailService());

            // Dependency Injection
            // resolve dependencies from a container
            // IPaymentService resolve to PaymentService => new PaymentService()
            // IPaymentService resolve to PaymentService => new MockPaymentService()
            // IOrderRepository resolve to OrderRepository => new OrderRepository()
            // IOrderRepository resolve to OrderRepository => new MockOrderRepository()
            // IEmailService resolve to EmailService => new EmailService()
            // IEmailService resolve to EmailService => new MockEmailService()

            // var orderService = container.Resolve<OrderService>();
        }
    }
}

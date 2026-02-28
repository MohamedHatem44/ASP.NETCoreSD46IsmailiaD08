namespace ASP.NETCoreD08.ConsoleApp
{
    // Tight Coupling
    // Hard To Test
    // Validation Logic Mixed With Business Logic
    public class OrderService
    {
        //var paymentService = new PaymentService();
        //var orderRepository = new OrderRepository();
        //var emailService = new EmailService();
        private readonly IPaymentService _paymentService; // => new PaymentService()
        private readonly IOrderRepository _orderRepository; // => new OrderRepository()
        private readonly IEmailService _emailService; // => new EmailService()

        public OrderService(IPaymentService paymentService, IOrderRepository orderRepository, IEmailService emailService)
        {
            _paymentService = paymentService;
            _orderRepository = orderRepository;
            _emailService = emailService;
        }

        public void PlaceOrder(string orderId, string customerId, decimal amount)
        {
            // 1- Process Payment
            //Console.WriteLine("Processing payment...");
            //var paymentService = new PaymentService();
            _paymentService.ProcessPayment(orderId, amount);

            // 2- Save Order to Database
            //Console.WriteLine("Saving order to database...");
            //var orderRepository = new OrderRepository();
            _orderRepository.SaveOrder(orderId, customerId, amount);

            // 3- Send Confirmation Email
            //Console.WriteLine("Sending confirmation email...");
            //var emailService = new EmailService();
            _emailService.SendConfirmationEmail(customerId, orderId);
        }
    }

    #region PaymentService
    public interface IPaymentService
    {
        void ProcessPayment(string orderId, decimal amount);
    }

    public class PaymentService : IPaymentService
    {
        public void ProcessPayment(string orderId, decimal amount)
        {
            Console.WriteLine($"Processing payment for Order {orderId} with amount {amount}...");
        }
    }
    #endregion

    #region OrderRepository
    public interface IOrderRepository
    {
        void SaveOrder(string orderId, string customerId, decimal amount);
    }

    public class OrderRepository : IOrderRepository
    {
        public void SaveOrder(string orderId, string customerId, decimal amount)
        {
            Console.WriteLine($"Saving order {orderId} for customer {customerId} with amount {amount} to database...");
        }
    }
    #endregion

    #region EmailService
    public interface IEmailService
    {
        void SendConfirmationEmail(string customerId, string orderId);
    }

    public class EmailService : IEmailService
    {
        public void SendConfirmationEmail(string customerId, string orderId)
        {
            Console.WriteLine($"Sending confirmation email to customer {customerId} for order {orderId}...");
        }
    } 
    #endregion
}




namespace ASP.NETCoreD08.Helper
{
    public class Print : IPrint
    {
        public Guid Id { get; set; }

        public Print()
        {
            Id = Guid.NewGuid();
        }

        public void PrintDateTime()
        {
            Console.WriteLine($"Print Service {DateTime.UtcNow}");
        }
    }
}

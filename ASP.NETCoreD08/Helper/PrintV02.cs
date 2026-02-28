namespace ASP.NETCoreD08.Helper
{
    public class PrintV02 : IPrint
    {
        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void PrintDateTime()
        {
            Console.WriteLine($"Print Service V02 {DateTime.UtcNow}");
        }

        public void PrintDateTime2()
        {
            Console.WriteLine($"Print Service V02 {DateTime.UtcNow}");
        }
    }
}

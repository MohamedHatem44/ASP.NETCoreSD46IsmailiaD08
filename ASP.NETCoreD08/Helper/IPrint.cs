namespace ASP.NETCoreD08.Helper
{
    public interface IPrint
    {
        Guid Id { get; set; }
        void PrintDateTime();
    }
}
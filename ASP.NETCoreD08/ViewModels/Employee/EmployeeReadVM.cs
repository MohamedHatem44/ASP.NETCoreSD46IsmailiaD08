namespace ASP.NETCoreD08.ViewModels.Employee
{
    public class EmployeeReadVM
    {
        /*------------------------------------------------------------------*/
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public string? Department { get; set; }
        /*------------------------------------------------------------------*/
    }
}

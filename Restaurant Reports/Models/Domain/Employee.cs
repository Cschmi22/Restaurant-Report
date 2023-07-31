namespace Restaurant_Reports.Models.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }
        public DateTime DateOfHire { get; set; }
        public string? Email { get; set; }
        public int Salary { get; set; }
    }
}

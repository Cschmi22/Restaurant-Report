namespace Restaurant_Reports.Models.Domain
{
    public class SalesReport
    {
        public Guid Id { get; set; }
        public DateTime Week { get; set; }
        public int Course { get; set; }
        public string? Name { get; set;}
        public int MonSales { get; set;} 
        public int TuesSales { get; set;} 
        public int WedSales { get; set;}
        public int ThursSales { get; set;}  
        public int FriSales { get; set;}
        public int SatSales { get; set;}
        public int SunSales { get; set;}
        public int TotalSales { get; set;}

    }
}

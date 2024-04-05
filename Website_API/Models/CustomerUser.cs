namespace Website_API.Models
{
    public class CustomerUser
    {

        public int? Id { get; set; }

        public string Name { get; set; } = null!;

        public int Age { get; set; }

        public string? Address { get; set; }

        public decimal? Salary { get; set; }

      

       public string Provice { get; set; }
        
        public string Telephone { get; set; }
       
    }
    public class CustomerGroup
    {
        public List<CustomerUser> CustomerUsers { get; set; }

        public List<Customers> Customer { get; set; }

       
    }
      
}

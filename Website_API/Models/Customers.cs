using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Website_API.Models
{
    public class Customers
    {
        public int Id { get; set; }

        public int Age { get; set; }

        public decimal? Salary { get; set; }

        public int? UserID { get; set; }
    }
}

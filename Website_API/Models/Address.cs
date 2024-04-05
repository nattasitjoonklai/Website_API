namespace Website_API.Models
{
    public class User_Address
    {
        public int ID { get; set; }
        public string Address { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }   

        public string District { get; set; }

        public string Postal_Code { get; set; }

        public string Province { get; set; }
        public int Customer_ID { get; set; }



    }
}

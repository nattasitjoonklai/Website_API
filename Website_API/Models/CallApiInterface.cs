namespace Website_API.Models
{
    public class CallApiInterface
    {

        public string url { get; set; }

        public string method { get; set; }  

        public List<Respone> resposne { get; set; }

    }


    public class Respone
    {
        
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }





    }
    
}

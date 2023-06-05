namespace WebApplication123.Models
{
    public class Response
    {
        public string Request => $"{Guid.NewGuid().ToString()}";
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public object Date { get; set; }
    }
}

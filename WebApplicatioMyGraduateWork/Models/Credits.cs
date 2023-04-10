namespace WebApplicatioMyGraduateWork.Models
{
    public class Credits
    {
        public int Id { get; set; }
        public Guid Number { get; set; }
        public string? TypeOfCredit { get; set; }
        public string? stringClient { get; set; }
        public decimal? Amount { get; set; }
        public string? DataOfIssue { get; set; }
        public string? FullRepayment { get; set; }
        public Repayment? RepaymentId { get; set; }
        public Clients? ClientId { get; set; }
    }
}

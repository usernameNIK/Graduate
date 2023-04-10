namespace WebApplicatioMyGraduateWork.Models
{
    public class TypeOfCredit
    {
        public int Id { get; set; }
        public int TypeOfCode { get; set; }
        public string? Name { get; set; }
        public string? ConditionsOfReceipt { get; set; }
        public int Bet { get; set; }
        public int TempOfDays { get; set; }
        public Credits? Credits { get; set; }
    }
}

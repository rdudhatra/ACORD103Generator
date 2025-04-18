namespace ACORD103Generator
{
    public class RawData
    {
        public string FullName { get; set; }
        public string Birthday { get; set; }
        public string Phone { get; set; }
        public string TransactionId { get; set; }
    }

    public class FixedData
    {
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string FormattedBirthday { get; set; }
        public string NormalizedPhone { get; set; }
    }
}
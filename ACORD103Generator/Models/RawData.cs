namespace ACORD103Generator
{
    public class OwnerRawData
    {
        public string OwnerFirstName { get; set; }
        public string OwnerInitial { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerSuffix { get; set; }
        public string OwnerCompanyName { get; set; }
        public string OwnerGender { get; set; }
        public string OwnerBirthdate { get; set; }
        public string OwnerSSN { get; set; }
        public string OwnerTelephone { get; set; }
        public string OwnerRelationship { get; set; }
        public string OwnerRelationshipOther { get; set; }
        public string OwnerResidenceAddress { get; set; }
        public string OwnerMailingAddress { get; set; }
        public string OwnerCity { get; set; }
        public string OwnerState { get; set; }
        public string OwnerZip { get; set; }
        public string OwnerCountry { get; set; }
        public string OwnerEmail { get; set; }
        public bool OwnerUSCitizen { get; set; }
        public string OwnerCountryOfCitizenship { get; set; }
        public string OwnerVisaType { get; set; }
        public string OwnerGovtIssuedID { get; set; }
        public string OwnerGovtIDIssuingCountry { get; set; }
        public string OwnerGovtIDNumber { get; set; }
        public bool OwnerDriversLicenseIndicator { get; set; }
        public string OwnerDLState { get; set; }
        public string OwnerDLNumber { get; set; }
    }

    public class ProcessedData
    {
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string FormattedDOB { get; set; }
        public string CountryCode { get; set; }
        public string AreaCode { get; set; }
        public string DialNumber { get; set; }
        public string Extension { get; set; }
        public string TransactionId { get; set; }
        public string TransExeDate { get; set; }
        public string TransExeTime { get; set; }

    }

    public class AcordXmlViewModel
    {
        public OwnerRawData OwnerRawData { get; set; }
        public ProcessedData ProcessedData { get; set; }
    }

    //public class RawData
    //{
    //    public string FullName { get; set; }
    //    public string Birthday { get; set; }
    //    public string Phone { get; set; }
    //    public string TransactionId { get; set; }
    //}

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
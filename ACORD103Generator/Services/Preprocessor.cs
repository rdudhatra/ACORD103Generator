using System;
using System.Linq;

namespace ACORD103Generator
{
    public static class Preprocessor
    {
        public static FixedData Process(RawData raw)
        {
            var fixedData = new FixedData();

            var nameParts = raw.FullName?.Split(' ') ?? Array.Empty<string>();
            if (nameParts.Length > 0)
                fixedData.Prefix = nameParts[0].EndsWith(".") ? nameParts[0] : null;

            if (nameParts.Length >= 2)
                fixedData.FirstName = nameParts[1];

            if (nameParts.Length >= 4)
            {
                fixedData.MiddleName = nameParts[2];
                fixedData.LastName = nameParts[3];
            }
            else if (nameParts.Length >= 3)
            {
                fixedData.LastName = nameParts[2];
            }

            if (nameParts.Length >= 5)
                fixedData.Suffix = nameParts[4];

            if (DateTime.TryParseExact(raw.Birthday, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out var dob))
                fixedData.FormattedBirthday = dob.ToString("yyyy-MM-dd");

            fixedData.NormalizedPhone = new string(raw.Phone?.Where(char.IsDigit).ToArray());

            return fixedData;
        }
    }
}
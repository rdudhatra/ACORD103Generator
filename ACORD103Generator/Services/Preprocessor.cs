using Newtonsoft.Json;
using System;
using System.Linq;
using System.Xml;

namespace ACORD103Generator
{
    //public static class Preprocessor
    //{

    //    public static FixedData Process(RawData raw)
    //    {
    //        var fixedData = new FixedData();

    //        var nameParts = raw.FullName?.Split(' ') ?? Array.Empty<string>();
    //        if (nameParts.Length > 0)
    //            fixedData.Prefix = nameParts[0].EndsWith(".") ? nameParts[0] : null;

    //        if (nameParts.Length >= 2)
    //            fixedData.FirstName = nameParts[1];

    //        if (nameParts.Length >= 4)
    //        {
    //            fixedData.MiddleName = nameParts[2];
    //            fixedData.LastName = nameParts[3];
    //        }
    //        else if (nameParts.Length >= 3)
    //        {
    //            fixedData.LastName = nameParts[2];
    //        }

    //        if (nameParts.Length >= 5)
    //            fixedData.Suffix = nameParts[4];

    //        if (DateTime.TryParseExact(raw.Birthday, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out var dob))
    //            fixedData.FormattedBirthday = dob.ToString("yyyy-MM-dd");

    //        fixedData.NormalizedPhone = new string(raw.Phone?.Where(char.IsDigit).ToArray());

    //        return fixedData;
    //    }
    //}

    public class FormDataPreprocessor
    {
        public string ProcessInput(string inputJson, string guid, DateTime inputDateTime)
        {   
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(inputJson);

            // Process Name
            var fullName = data.ContainsKey("annuitant.qa.name") ? data["annuitant.qa.name"] : "";
            var nameParts = string.IsNullOrEmpty(fullName) ? new string[] { } : fullName.Split(' ');

            string prefix = "", firstName = "", middleName = "", lastName = "", suffix = "";

            if (nameParts.Length > 0)
            {
                prefix = (nameParts.Length > 1 && nameParts[0].EndsWith(".")) ? nameParts[0] : "";
                firstName = prefix == "" ? nameParts[0] : nameParts[1];

                if (nameParts.Length > 2)
                {
                    middleName = string.Join(" ", nameParts.Skip(1).Take(nameParts.Length - 2));
                }

                lastName = nameParts.Length > 1 ? nameParts.Last() : "";

                if (nameParts.Length > 2 && nameParts.Last().Length > 1 && !nameParts.Last().EndsWith("."))
                {
                    suffix = nameParts.Last();
                    lastName = nameParts[nameParts.Length - 2];
                }
            }

            // Process DOB
            string formattedDOB = "";
            if (data.ContainsKey("annuitant.qa.birthday") && !string.IsNullOrEmpty(data["annuitant.qa.birthday"]))
            {
                DateTime dob;
                if (DateTime.TryParseExact(data["annuitant.qa.birthday"], "MM/dd/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out dob))
                {
                    formattedDOB = dob.ToString("yyyy-MM-dd");
                }
            }

            // Process Phone
            string phone = data.ContainsKey("annuitant.phone") ? data["annuitant.phone"] : "";
            var cleanedPhone = new string(phone.Where(char.IsDigit).ToArray());

            string countryCode = "1", areaCode = "000", dialNumber = "0000000", ext = "";
            if (cleanedPhone.Length >= 10)
            {
                countryCode = cleanedPhone.Length > 10 ? cleanedPhone.Substring(0, cleanedPhone.Length - 10) : "1";
                areaCode = cleanedPhone.Substring(cleanedPhone.Length - 10, 3);
                dialNumber = cleanedPhone.Substring(cleanedPhone.Length - 7, 7);
                ext = cleanedPhone.Length > 10 ? cleanedPhone.Substring(10) : "";
            }

            // GUID: use provided one
            string transactionId = guid;

            // Extract date and time in the required formats
            string transExeDate = inputDateTime.ToString("yyyy-MM-dd");  // e.g., "2024-03-31"
            string transExeTime = inputDateTime.ToString("HH:mm:ssZ");   // e.g., "14:05:30Z"

            // Create output object
            var outputData = new ProcessedData
            {
                Prefix = prefix,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Suffix = suffix,
                FormattedDOB = formattedDOB,
                CountryCode = countryCode,
                AreaCode = areaCode,
                DialNumber = dialNumber,
                Extension = ext,
                TransactionId = transactionId,
                TransExeDate = transExeDate,
                TransExeTime = transExeTime,
            };

            return JsonConvert.SerializeObject(outputData, Newtonsoft.Json.Formatting.Indented);
        }
    }
}
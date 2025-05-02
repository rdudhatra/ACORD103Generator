using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RazorLight;

namespace ACORD103Generator
{
    class Program
    {


        static async Task Main(string[] args)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
            string rawJsonPath = Path.Combine(projectRoot, "Data", "rawData.json");
            string rawJson = File.ReadAllText(rawJsonPath);

            // Step 1: Deserialize JSON to OwnerData object
            var ownerData = System.Text.Json.JsonSerializer.Deserialize<OwnerRawData>(rawJson);

            // Step 2: Convert OwnerData back to JSON for processing
            string inputJson = System.Text.Json.JsonSerializer.Serialize(ownerData);

            // Step 3: Process using FormDataPreprocessor
            var preprocessor = new FormDataPreprocessor();
            string guid = Guid.NewGuid().ToString();
            DateTime inputDateTime = DateTime.UtcNow;

            string processedJson = preprocessor.ProcessInput(inputJson, guid, inputDateTime);

            // Step 4: Deserialize processed data
            var processedData = JsonConvert.DeserializeObject<ProcessedData>(processedJson);

            // Step 5: Combine into model
            var model = new AcordXmlViewModel
            {
                OwnerRawData = ownerData,
                ProcessedData = processedData
            };


            // Step 6: Load Razor template and render
            var template = Path.Combine(projectRoot, "Templates");

            var engine = new RazorLight.RazorLightEngineBuilder()
                .UseFileSystemProject(template)
                .UseMemoryCachingProvider()
                .Build();

            string result = await engine.CompileRenderAsync("ACORD103.cshtml", model);

            // Step 7: Output result to file
            string outputDir = Path.Combine(projectRoot, "Output");
            string outputFile = Path.Combine(outputDir, "output.xml");

            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            File.WriteAllText(outputFile, result);

            Console.WriteLine("ACORD 103 XML generated successfully.");
        }

        //static async Task Main(string[] args)
        //{
        //    string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
        //    string rawJsonPath = Path.Combine(projectRoot, "Data", "rawData.json");
        //    string rawJson = File.ReadAllText(rawJsonPath);
        //    var rawData = JsonSerializer.Deserialize<RawData>(rawJson);

        //    var fixedData = Preprocessor.Process(rawData);


        //    var model = new { rawData, fixedData };

        //    var template = Path.Combine(projectRoot, "Templates");

        //    var engine = new RazorLightEngineBuilder()
        //        .UseFileSystemProject(template)
        //        .UseMemoryCachingProvider()
        //        .Build();

        //    string result = await engine.CompileRenderAsync("ACORD103.cshtml", model);

        //    string outputDir = Path.Combine(projectRoot, "Output");
        //    string outputFile = Path.Combine(outputDir, "output.xml");

        //    // Ensure the Output directory exists
        //    if (!Directory.Exists(outputDir))
        //        Directory.CreateDirectory(outputDir);

        //    File.WriteAllText(outputFile, result);

        //    Console.WriteLine("ACORD 103 XML generated successfully.");
        //}
    }
}
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
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
            var rawData = JsonSerializer.Deserialize<RawData>(rawJson);

            var fixedData = Preprocessor.Process(rawData);

            var model = new { rawData, fixedData };

            var template = Path.Combine(projectRoot, "Templates");

            var engine = new RazorLightEngineBuilder()
                .UseFileSystemProject(template)
                .UseMemoryCachingProvider()
                .Build();

            string result = await engine.CompileRenderAsync("ACORD103.cshtml", model);

            string outputDir = Path.Combine(projectRoot, "Output");
            string outputFile = Path.Combine(outputDir, "output.xml");
            
            // Ensure the Output directory exists
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);
            
            File.WriteAllText(outputFile, result);

            Console.WriteLine("ACORD 103 XML generated successfully.");
        }
    }
}
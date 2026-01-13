using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GreenGrid
{
    public class CalculateGridEmissions
    {
        private readonly ILogger<CalculateGridEmissions> _logger;

        public CalculateGridEmissions(ILogger<CalculateGridEmissions> logger)
        {
            _logger = logger;
        }

        [Function("CalculateGridEmissions")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            _logger.LogInformation("Processing grid emission request...");

            // Reading: Get the JSON from the request body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            
            // Parsing: Convert JSON to a dynamic object
            var data = JsonConvert.DeserializeObject<dynamic>(requestBody);

            string source = data?.source ?? "Unknown";
            double mwh = data?.mwh ?? 0;

            // 3. Calculation (Emission Factors in g CO2/kWh)
            // Fortum focuses on Nuclear/Renewables, so we highlight those.
            double factor = source.ToLower() switch
            {
                "coal" => 820.0,
                "solar" => 45.0,
                "nuclear" => 12.0,
                "wind" => 11.0,
                _ => 50.0 // Default for unspecified/mixed sources
            };

            // Calculate Tonnes of CO2
            // Formula: (MWh * 1000 to get kWh * grams) / 1,000,000 to get Tonnes
            double totalTonnes = (mwh * 1000 * factor) / 1000000;

            // Return the result
            return new OkObjectResult(new
            {
                Source = source,
                EnergyGeneratedMWh = mwh,
                CarbonFootprintTonnes = Math.Round(totalTonnes, 4),
                Rating = totalTonnes < 1.0 ? "Green Energy" : "High Impact",
                Note = "Data processed via Fortum Integration Prototype"
            });
        }
    }
}
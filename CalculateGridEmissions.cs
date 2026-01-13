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
public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
{
    _logger.LogInformation("Processing grid emission request...");

    // 1. Try to get data from the URL (Browser/GET)
    string source = req.Query["source"];
    string mwhString = req.Query["mwh"];

    // 2. If URL is empty, try to get data from the Body (Postman/POST)
    if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(mwhString))
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic data = JsonConvert.DeserializeObject(requestBody);
        source = source ?? data?.source;
        mwhString = mwhString ?? data?.mwh?.ToString();
    }

    // Convert MWh to double safely
    double.TryParse(mwhString, out double mwh);
    source = source?.ToLower() ?? "unknown";

    // 3. Calculation Logic
    double factor = source switch
    {
        "coal" => 820.0,
        "solar" => 45.0,
        "nuclear" => 12.0,
        "wind" => 11.0,
        _ => 50.0 
    };

    double totalTonnes = (mwh * 1000 * factor) / 1000000;

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
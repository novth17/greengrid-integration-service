# GreenGrid Carbon Integration API
A Azure Functions API designed to calculate energy grid emissions. This project demonstrates modern cloud-native development using the .NET Isolated Worker Model.

## 🛠 Tech Stack
- **Language**: C# (.NET 10 Isolated)
- **Framework**: Azure Functions v4
- **Cloud**: Microsoft Azure (Serverless)
- **Data Handling**: Newtonsoft.Json for payload processing

## 📊 Business Logic
The API ingests energy generation data (MWh) and calculates the carbon footprint based on the following intensity factors:
- **Nuclear**: 12g CO2/kWh
- **Wind**: 11g CO2/kWh
- **Solar**: 45g CO2/kWh
- **Coal**: 820g CO2/kWh

## 🌐 Live API Endpoint
[🚀 Live Demo](https://greengrid-energy-calc-sweden-buezgyc2fvbrd3g0.swedencentral-01.azurewebsites.net/api/CalculateGridEmissions?mwh=50&source=nuclear)

API Usage The endpoint is hosted on Azure Functions (Sweden Central). You can test it by appending energy parameters to the URL: GET /api/CalculateGridEmissions?mwh=[value]&source=[type]

### 🚀 Example (Local or Cloud)
Send a POST request with the following JSON body:
```json
{
    "source": "nuclear",
    "mwh": 50
}
```

### Return
```
{
  "source": "nuclear",
  "energyGeneratedMWh": 50,
  "carbonFootprintTonnes": 0.6,
  "rating": "Green Energy",
  "note": "Data processed via Fortum Integration Prototype"
}
```
### Why this is "Green"
- Source: nuclear → Factor = 12.0 g CO2/kWh.
- Calculation: $(5,000 \text{ kWh} \times 12.0 \text{ g}) / 1,000,000 = \mathbf{0.06} \text{ Tonnes}$.
- Rating: Since 0.06 is less than 1.0, the API returns "Green Energy".

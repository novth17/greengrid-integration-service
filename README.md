# GreenGrid Carbon Integration API
A Azure Functions API designed to calculate energy grid emissions. This project demonstrates modern cloud-native development using the .NET Isolated Worker Model.

## 🛠 Tech Stack
- **Language**: C# (.NET 10 Isolated)
- **Framework**: Azure Functions v4
- **Cloud**: Microsoft Azure (Serverless)
- **Data Handling**: Newtonsoft.Json for payload processing

## 🌐 Live API Endpoint
`https://greengrid-calculator.azurewebsites.net/api/CalculateGridEmissions`

## 📊 Business Logic
The API ingests energy generation data (MWh) and calculates the carbon footprint based on the following intensity factors:
- **Nuclear**: 12g CO2/kWh
- **Wind**: 11g CO2/kWh
- **Solar**: 45g CO2/kWh
- **Coal**: 820g CO2/kWh

## 🚀 Usage (Local or Cloud)
Send a POST request with the following JSON body:
```json
{
    "source": "nuclear",
    "mwh": 500
}

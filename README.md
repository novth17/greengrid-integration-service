# GreenGrid Integration Service
An Azure Functions prototype built for the Fortum Internship application.

## ⚡ Features
- **Serverless API**: Built using C# and .NET 10 (Isolated Worker Model).
- **Sustainability Logic**: Calculates carbon footprint (Tonnes CO2) based on energy source.
- **JSON Integration**: Processes real-time grid data payloads.

## 🚀 How to Run
1. `func start`
2. Send POST request to `/api/CalculateGridEmissions`

## 🌐 Live API
You can test the live production endpoint here:
`https://greengrid-calculator.azurewebsites.net/api/CalculateGridEmissions`

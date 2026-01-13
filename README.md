# 🌍 GreenGrid Carbon Integration API
A simple REST API developed to explore **Azure Functions** and **serverless integration patterns**. 
The project logic focuses on the energy grid reporting, specifically calculating carbon intensity based on generated data.

## 🚀 Key Capabilities
- Serverless Efficiency: Built on the Azure Functions v4 (Isolated Worker Model) for high scalability and minimal idle costs.
- Regional Compliance: Leverages Sweden's green data center infrastructure to minimize the project's own operational footprint.
- Developer-First Design: Features a RESTful interface supporting both JSON payloads for automation and HTTP query parameters for quick browser-based testing.

## 🛠 Tech Stack
- **Language**: C# (.NET 10 Isolated Worker Model)
- **Cloud**: Azure Functions v4 (Serverless)
- **Region**: Sweden Central (100% Carbon-Free Energy Region)
- **Frontend**: GitHub Pages (HTML5/JavaScript)

## 🏗️ System Architecture

### 1. Frontend (The Presentation Layer)
Hosted on: GitHub Pages.
A lightweight HTML5/JavaScript UI that captures user input (MWh and Energy Source) and triggers the backend API via asynchronous fetch calls.

### 2. Backend (The Logic Layer)

- Hosted on: Azure Functions v4 (Isolated Worker Model).
- Region: Sweden Central (Carbon-optimized).
- Role: An event-driven, serverless function that processes incoming HTTP requests. It performs unit conversions and applies the specific carbon intensity factors.

### 3. Integration Flow
- User selects "Nuclear" and enters "50" on the Web UI.
- Frontend sends a GET request to the Azure endpoint: /api/Calculate...
- Azure Function "wakes up," executes the C# logic, and calculates the result.
- JSON Response is sent back to the browser.
- UI displays the result with a dynamic "Green Energy" or "High Impact" rating.

## 🚀 Web Interface
<a href="https://novth17.github.io/greengrid-integration-service/">
  <img src="https://img.shields.io/badge/Live%20Demo%20🌿-2d5a27?style=for-the-badge" 
       width="180"/>
</a>
<br/>
*Note: Initial request may experience a "cold start" delay of ~20s as the serverless instance wakes up.*

### 🛠️ Developer API
**Endpoint:** `GET/POST /api/CalculateGridEmissions`

**Quick Test:** [Calculate 50MWh Nuclear](https://greengrid-energy-calc-sweden-buezgyc2fvbrd3g0.swedencentral-01.azurewebsites.net/api/CalculateGridEmissions?mwh=50&source=nuclear)

```json
{
  "source": "nuclear",
  "energyGeneratedMWh": 50,
  "carbonFootprintTonnes": 0.6,
  "rating": "Green Energy",
  "note": "Data processed via Integration Prototype"
}
```
## 📊 Business Logic
The API calculates $CO_2$ emissions based on MWh generated and determines if the output is 'Green Energy' or 'High Impact' by comparing the total weight against a 1.0 Metric Tonne sustainability threshold.

### ⚡ Emission Factors
- Wind: $11\text{g } CO_2/\text{kWh}$ (Lowest Impact)
- Nuclear: $12\text{g } CO_2/\text{kWh}$
- Solar: $45\text{g } CO_2/\text{kWh}$
- Coal: $820\text{g } CO_2/\text{kWh}$ (Highest Impact)

### 📊 Emission Classifications
The API categorizes results based on a 1.0 Metric Tonne $CO_2$ threshold:
- 🌿 Green Energy (< 1.0 Tonne): Reserved for low-carbon generation. This is typically achieved through high-efficiency renewables (Wind, Solar) or Nuclear power at lower volumes.
- ⚠️ High Impact (≥ 1.0 Tonne): Indicates a significant carbon footprint. This rating is triggered by high-intensity sources (Coal) or extremely large volumes of generation that exceed the sustainability target.

### 🧮 Calculation Logic
To ensure transparency in our carbon reporting, the API follows a standardized conversion flow:

**1. Unit Conversion**

Since emission factors are measured in grams per kilowatt-hour (g/kWh), we first convert Megawatt-hours to Kilowatt-hours:

$$1 \text{ MWh} = 1,000 \text{ kWh}$$

**2. Carbon Intensity Formula**

The total CO2 impact is calculated by multiplying the energy by the source-specific intensity factor and converting the result from grams to metric tonnes:

$$\text{Total Tonnes } CO_2 = \frac{\text{kWh} \times \text{Intensity Factor}}{1,000,000}$$

**3. Steps Walkthrough (50 MWh Nuclear)**

- Step 1:
$50 \text{ MWh} \times 1,000 = 50,000 \text{ kWh}$

- Step 2:
$50,000 \text{ kWh} \times 12\text{g (Nuclear Factor)} = 600,000\text{g } CO_2$

- Step 3:
$600,000\text{g} / 1,000,000 = \mathbf{0.6} \text{ Metric Tonnes}$

## ⚠️ Project Scope & Error Handling
This proof-of-concept emphasizes the end-to-end integration between GitHub Pages and Azure serverless orchestration over complex error handling. 
In a production scenario, robust input validation, middleware and centralized logging via Azure Application Insights would be implemented to ensure system resilience.

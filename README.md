# Industry-Symbiosis: Resource Trading for Industrial Networks

A prototype platform developed for a university seminar, designed to help reduce resource waste efficiently.

This solution enables resource trading within an industrial network by repurposing production waste. Competitive advantages are created through the efficient trading of material and energy flows, which benefits members, the network, and the environment. 

Typically, such trading within networks relies on manual networking and personal discussions. Our solution stands out by providing:
* APIs for automatic material and energy flow recording.
* Clear displays of input and output streams.
* Automated matching between production facilities.

## Repositories & Live Demo

* **Backend (This Repository):** Contains the API and database setup. Currently hosted on Azure.
    * [Live API Demo (Get All Enterprises)](https://enterprisemanagementservice2.azurewebsites.net/api/enterprises/get/all)
* **Frontend:** The user interface is currently under development. 
    * [Frontend GitHub Repository](https://github.com/KonstantinBlank/Industry-Symbiosis-Frontend)

## Concept Previews

Here is a look at the planned user interface, including the main dashboard and the energy/material flow visualizations:

**Dashboard Concept:**
![Dashboard Concept Screenshot](./screenshots/screenshot_dashboard.png)

**Match overview Concept:**
![Match Overview Screenshot](./screenshots/screenshot_matchoverview.png)

**Match details Concept:**
![Match Details Screenshot](./screenshots/screenshot_match_details.png)

**Map Concept:**
![Map Concept Screenshot](./screenshots/screenshot_map.png)

**Sankey Diagram Concept (Resource Flows):**
![Sankey Diagram Concept Screenshot](./screenshots/screenshot_sankey.png)

**Production Line Setup Concept:**
![Production_Setup Concept Screenshot](./screenshots/screenshot_production_setup.png)

## Architecture & Database

**Architecture Poster (German):**
![Architecture Poster](./notes/Architekturposter.svg)

**Entity Relationship Model (ERM):**
![Database ERM](./notes/Industry-Symbiosis-ERM.png)

---

## Tech Stack
* **Framework:** ASP.NET Core (C#)
* **Database:** Azure SQL
* **Cloud Hosting:** Azure (Azure App Service)
* **IDE:** Visual Studio 2022



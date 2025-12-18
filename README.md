# ClinicManagementWeb – ASP.NET Core Clinic Management System

A full-stack web application for managing clinic operations, built with ASP.NET Core and Entity Framework.  
The system supports patient records, appointments, prescriptions, invoices, and role-based access for clinic staff.

---

## Table of Contents
- Introduction
- Technologies
- Features
- Project Structure
- Installation
- Usage
- Notes
- Contact

---

## Introduction

ClinicManagementWeb is a clinic management system designed to streamline operations such as patient registration, appointment scheduling, and medical record handling.  
The system is built entirely with ASP.NET Core and uses Entity Framework for database access.

It supports multiple roles including doctors, receptionists, and pharmacists, each with access to specific modules and dashboards.

---

## Technologies

### Backend & Frontend
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Razor Views
- Bootstrap

### Others
- Git & GitHub
- LINQ
- JSON
- Role-based Authorization

---

## Features

### System Features
- User login & role-based access (doctor, receptionist, pharmacist)
- Patient management (add, edit, view)
- Appointment scheduling
- Prescription creation
- Invoice generation
- Dashboard with statistics
- Data seeding for demo/testing

---

## Project Structure

```
ClinicManagementWeb/
│
├── bin/                     # Build output
├── Controllers/             # MVC controllers
├── Data/                    # Database context
├── Migrations/              # EF Core migrations
├── Models/                  # Data models
├── obj/                     # Temporary build files
├── Properties/              # Project settings
├── Views/                   # Razor views
├── wwwroot/                 # Static files (CSS, JS, images)
│
├── appsettings.json         # Main configuration
├── appsettings.Development.json
├── ClinicManagementWeb.csproj
├── ClinicManagementWeb.csproj.user
├── Program.cs               # Application entry point
└── README.md
```

---

## Installation

### Prerequisites
- .NET SDK (v7.0+ recommended)
- SQL Server
- Visual Studio or VS Code

---

### Backend Setup

1. Navigate to the project folder:
```bash
cd ClinicManagementWeb
```

2. Restore and build the project:
```bash
dotnet restore
dotnet build
```

3. Update database (if using EF Core migrations):
```bash
dotnet ef database update
```

4. (Optional) Seed sample data:
```bash
dotnet run -- seed
```

5. Run the application:
```bash
dotnet run
```

Application will run at:  
`https://localhost:5001` or `http://localhost:5000`

---

## Usage

- Login with the provided roles (doctor, receptionist, pharmacist) to access different dashboards and features.  
- Manage patients, prescriptions, invoices, and appointments through the respective modules.  
- Use the dashboard for statistics and quick access to main functions.

---

## Notes

- Ensure SQL Server is running and accessible before starting the backend.  
- Default backend port: `5000` (check `Program.cs` for custom settings).  
- Update environment variables as needed for production deployment.  
- For demo/testing, sample data is available in `Data/SeedData.cs`.  
- If you encounter CORS or HTTPS issues, check `launchSettings.json` and `appsettings.json`.

---

## Contact

For questions or collaboration, please contact:

Nguyen Duc Khanh GitHub: https://github.com/NguyenDucKhanh123 
Email: khanh.nd11246@sinhvien.hoasen.edu.vn

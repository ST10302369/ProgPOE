# 🌿 Agri-Energy Connect Platform – README

## 📌 Overview
**Agri-Energy Connect** is a web-based prototype built using **ASP.NET Core MVC** and **C#** to link South African farmers with green energy providers. This application supports the digital transformation of sustainable agriculture through structured user roles, clean database integration, and user-friendly UI design.

This README contains instructions for setting up, running, and understanding the system's functionality, aimed at both technical and non-technical stakeholders.

---

## ⚙️ Technologies Used

- ASP.NET Core MVC (.NET 6 or later)
- Entity Framework Core
- SQL Server (LocalDB)
- Microsoft Identity (Authentication & Authorization)
- Visual Studio 2022+
- Bootstrap 5 (for responsive UI)

---

## 🧑‍🤝‍🧑 User Roles

### 👨‍🌾 Farmer
- Register and log in securely.
- Add products (name, category, production date).
- View own products.

### 🧑‍💼 Employee
- Register and log in securely.
- Add new **farmer profiles**.
- View and **filter** all products by:
  - Date range
  - Product type (category)

---

## 🗃️ Database Structure

### Tables
- **AspNetUsers** (from Identity)
- **Farmers** (linked to user)
- **Products** (linked to farmer)

### Sample Fields
**Farmer**
- Id
- Name
- Email
- Location

**Product**
- Id
- Name
- Category
- ProductionDate
- FarmerId (Foreign Key)

---

## 🏁 How to Set Up the Project

### 🔧 1. Prerequisites
- Visual Studio 2022+ with ASP.NET and Web Development workload
- .NET SDK 6 or later
- SQL Server Express / LocalDB

### 📂 2. Clone or Download
```bash
git clone https://github.com/your-username/AgriEnergyConnect.git

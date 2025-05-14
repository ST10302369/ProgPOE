# 🌿 AGRI-CONNECT Platform

## 📌 Overview

**AGRI-CONNECT** is a comprehensive agricultural management system built using **ASP.NET Core MVC** and **C#** to help South African farmers effectively manage their products and inventory. The platform serves as a digital bridge between farmers and agricultural officers, providing tools for product management, inventory tracking, and enhanced farm productivity.

This platform demonstrates sustainable agricultural practices through structured user roles, clean database integration, and an intuitive, user-friendly interface. It aims to support the digital transformation of agriculture in South Africa by providing accessible technology for farm management.

---

## ⚙️ Technologies Used

- **Framework**: ASP.NET Core MVC (.NET 6+)
- **Database**: Entity Framework Core with SQL Server
- **ORM**: Entity Framework Core
- **Frontend**: Bootstrap 5, JavaScript
- **Authentication**: ASP.NET Core Identity & Custom Session Management
- **IDE**: Visual Studio 2022+
- **Libraries**: jQuery, Bootstrap Icons

---

## 🧑‍🤝‍🧑 User Roles & Features

### 👨‍🌾 Farmer

- **Account Management**:
  - Secure login with session management
  - Personalized dashboard with farm information
  
- **Product Management**:
  - Add new products with detailed information
  - Include product images for better visualization
  - Edit existing product details
  - Delete unwanted products
  - View all products in a visually appealing format
  
- **Dashboard**:
  - Overview of total product count
  - Number of product categories
  - Recent products with images
  - Quick access to common actions

### 🧑‍💼 Employee / Agricultural Officer

- **Account Management**:
  - Secure login with session management
  - Administrative dashboard

- **Farmer Management**:
  - Register new farmers in the system
  - View and edit farmer information
  - View all registered farmers

- **Product Monitoring**:
  - View all products across all farmers
  - Advanced filtering options:
    - By date range
    - By product category
    - By farmer name or farm
  - View products from specific farmers

---

## 🗃️ Database Structure

### Core Tables

- **Users**
  - Id (PK)
  - Username
  - Password (hashed)
  - Role
  - FarmerId (FK, nullable)
  - RegistrationDate
  - LastLogin

- **Farmers**
  - Id (PK)
  - FirstName
  - LastName
  - Email
  - PhoneNumber
  - FarmName
  - FarmLocation
  - RegistrationDate
  - Products (navigation property)

- **Products**
  - Id (PK)
  - Name
  - Category
  - ProductionDate
  - Quantity
  - Unit
  - PricePerUnit
  - Description
  - ImageUrl
  - FarmerId (FK)
  - Farmer (navigation property)

### Relationships

- A **Farmer** can have many **Products** (One-to-Many)
- A **User** can be associated with one **Farmer** (One-to-One, optional)

---

## 📋 Technical Implementation

### Authentication & Security

- Custom session-based authentication
- Role-based authorization (Farmer vs. Employee)
- Input validation and sanitization
- CSRF protection with anti-forgery tokens

### Data Access

- Repository pattern for data access
- Entity Framework Core for ORM
- Migrations for database schema management
- Seed data for initial testing

### UI/UX

- Responsive design using Bootstrap 5
- Interactive dashboards for quick data visualization
- Consistent styling and branding
- Image integration for products
- Filtering capabilities for data management

---

## 🏁 Getting Started

### 🔧 Prerequisites

- Visual Studio 2022+ with ASP.NET and Web Development workload
- .NET SDK 6.0 or later
- SQL Server Express / LocalDB
- Git (optional, for cloning)

### 📂 Installation Steps

1. **Clone or Download the Repository**

   ```bash
   git clone https://github.com/your-username/AGRI-CONNECT.git
   ```

   Or download and extract the ZIP file from the repository.

2. **Open the Solution**

   Open the solution file (`ProgPOE.sln`) in Visual Studio.

3. **Configure the Database Connection**

   Update the connection string in `appsettings.json` to match your SQL Server instance:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YourServerName\\SQLEXPRESS;Database=ProgPOEDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
   }
   ```

4. **Apply Database Migrations**

   In the Package Manager Console, run:

   ```
   Update-Database
   ```

   This will create the database and apply all migrations.

5. **Build and Run the Application**

   Press F5 or click the "Start" button in Visual Studio to build and run the application.

### 🚀 First-Time Setup

1. **Access the Application**

   The application will launch in your default browser at a URL like `https://localhost:7171/`.

2. **Login with Seed Accounts**

   Use the pre-created accounts to log in:

   - **Employee Account**:
     - Username: `employee`
     - Password: `employee123`

   - **Farmer Account**:
     - Username: `farmer`
     - Password: `farmer123`

3. **Creating New Users**

   - As an **Employee**, you can create new farmer accounts through the "Add Farmer" functionality
   - The system automatically creates a user account for each new farmer using their email and a default password

---

## 🔍 How to Use the System

### For Farmers

1. **Login to Your Account**
   - Use your username and password to log in
   - You'll be redirected to the Farmer Dashboard

2. **Add a New Product**
   - Click "Add New Product" button on the dashboard
   - Fill in the product details including:
     - Name
     - Category
     - Production Date
     - Quantity
     - Unit
     - Price per Unit
     - Description
     - Image URL (optional)
   - Click "Create" to save the product

3. **View Your Products**
   - Click "View All Products" on the dashboard
   - Browse through your product list
   - Use the "View" button to see detailed information
   - Use the "Edit" button to update product information
   - Use the "Delete" button to remove a product

### For Employees

1. **Login to Your Account**
   - Use your employee credentials to log in
   - You'll be redirected to the Employee Dashboard

2. **Register a New Farmer**
   - Click "Add New Farmer" on the dashboard
   - Fill in the farmer's details:
     - First Name, Last Name
     - Email, Phone Number
     - Farm Name, Location
   - Click "Create" to register the farmer
   - The system will automatically create a user account for the farmer

3. **View All Farmers**
   - Click "Farmers" in the navigation menu
   - Browse through the list of all registered farmers
   - Click "Edit" to update farmer information
   - Click "Products" to view a specific farmer's products

4. **View All Products**
   - Click "All Products" in the navigation menu
   - Use the filtering options to narrow down products:
     - Filter by category
     - Filter by date range
     - Filter by farmer name/farm
   - Click "View All from Farmer" to see all products from a specific farmer

---

## 🔧 Troubleshooting

### Common Issues

1. **Database Connection Errors**
   - Verify your SQL Server is running
   - Check the connection string in `appsettings.json`
   - Ensure your SQL Server instance name is correct

2. **Login Issues**
   - Clear browser cookies and cache
   - Verify you're using the correct username and password
   - For new farmer accounts, the default password is "farmer123"

3. **Image Display Problems**
   - Ensure image URLs are valid and accessible
   - Check if the image format is supported (JPG, PNG, etc.)
   - For placeholder images, internet connectivity is required

### Support

For additional assistance or to report bugs, please contact the system administrator at support@agri-connect.co.za or open an issue on the GitHub repository.

---

## 🔮 Future Enhancements

- **Advanced Analytics**: Implement data visualization and reporting features
- **Mobile Application**: Develop a companion mobile app for on-the-go management
- **Weather Integration**: Add real-time weather data relevant to farm locations
- **Inventory Alerts**: Set up notifications for low stock or expiring products
- **Multi-language Support**: Add support for South African languages
- **Offline Mode**: Enable basic functionality without internet connectivity
- **Marketplace**: Add functionality for farmers to sell products directly through the platform

---

## 📜 License

This project is licensed under the MIT License - see the LICENSE file for details.

---

## 🙏 Acknowledgements

- Bootstrap framework for responsive design
- Microsoft for ASP.NET Core and Entity Framework
- All contributors who helped with testing and feedback

---

*© 2025 AGRI-CONNECT. All Rights Reserved.*

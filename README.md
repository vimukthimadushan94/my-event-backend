# ASP.NET Backend Application

This is a **backend application** built with **ASP.NET Web API**, providing a secure and scalable RESTful API for client applications. It follows modern development practices and integrates seamlessly with frontend applications.

## Video Tutorial

### ASP.NET Core API Overview  
[![ASP.NET Overview](https://img.youtube.com/vi/2KN7_016rXs/maxresdefault.jpg)](https://www.youtube.com/watch?v=2KN7_016rXs)  
(https://www.youtube.com/watch?v=2KN7_016rXs "Click to play ▶")


### Authentication
[![Next.js Overview](https://img.youtube.com/vi/QCRuecPlyMo/maxresdefault.jpg)](https://www.youtube.com/watch?v=QCRuecPlyMo)(https://www.youtube.com/watch?v=QCRuecPlyMo "Click to play ▶")


### Application Overview
[![Next.js Authentication](https://img.youtube.com/vi/lZ8Jgotxo5k/maxresdefault.jpg)](https://www.youtube.com/watch?v=lZ8Jgotxo5k)(https://www.youtube.com/watch?v=lZ8Jgotxo5k "Click to play ▶")

## Tech Stack

- **ASP.NET Core Web API** – RESTful API services
- **Entity Framework Core** – ORM for database interactions
- **JWT Authentication** – Secure user authentication
- **SQL Server / PostgreSQL** – Database storage solutions
- **Cloud Deployment** – Deployable on **Azure, AWS, or DigitalOcean**

## Features

✅ **Secure API endpoints with JWT authentication**  
✅ **Database management with Entity Framework Core**  
✅ **Cross-origin resource sharing (CORS) enabled**  
✅ **Error handling and logging**  
✅ **Scalable and cloud-ready deployment**  

## Getting Started

### 1️⃣ Clone the Repository

```bash
git clone https://github.com/vimukthimadushan94/my-event-backend.git
cd my-event-backend
```

### 2️⃣ Setup & Run the Backend (ASP.NET)

1. Navigate to the backend folder:  
   ```bash
   cd backend
   ```
2. Install dependencies:  
   ```bash
   dotnet restore
   ```
3. Configure the database connection in `appsettings.json`:  
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=mydb;User Id=myuser;Password=mypassword;"
   }
   ```
4. Apply database migrations:  
   ```bash
   dotnet ef database update
   ```
5. Run the backend server:  
   ```bash
   dotnet run
   ```

By default, the API will be available at: `http://localhost:5069`

## Deployment

### 🚀 Deploying the Backend (ASP.NET)

- **Azure App Services**: Deploy using `dotnet publish` and Azure App Services.  
- **AWS Lambda / ECS**: Use Docker to containerize the API for AWS deployment.  
- **Self-hosted (VPS)**: Deploy on DigitalOcean, Linode, or any VPS with a reverse proxy (NGINX, Apache).  

## Learn More

- 📚 [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)  
- 🔐 [JWT Authentication Guide](https://jwt.io/introduction/)  
- 💾 [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)  


# Brand IT Solution - Full-Stack Company Website

A modern, professional full-stack web application built with Svelte frontend and .NET 8 Web API backend.

## 🚀 Tech Stack

### Frontend
- **Svelte** with TypeScript
- **Vite** for development and building
- **Tailwind CSS** for styling
- **Google Fonts** (Poppins, Inter)
- Responsive design with smooth animations

### Backend
- **.NET 8 Web API** with C#
- **Clean Architecture** (Domain, Application, Infrastructure, API layers)
- **PostgreSQL** database with Entity Framework Core
- **JWT Authentication** with role-based authorization
- **Swagger/OpenAPI** documentation

### Features
- 🏠 Modern company website with hero, services, team, blog, contact
- 👨‍💼 Admin panel with dashboard and CRUD operations
- 🔐 Secure authentication and authorization
- 📱 Fully responsive design
- ✨ Smooth animations and modern UI
- 📊 Analytics dashboard for admins

## 📁 Project Structure

```
brand_it/
├── frontend/          # Svelte frontend application
├── backend/           # .NET 8 Web API
├── database/          # SQL scripts and migrations
└── docs/             # Documentation and setup guides
```

## 🛠️ Quick Start

### Prerequisites
- Node.js 18+ and npm
- .NET 8 SDK
- PostgreSQL 13+

### Frontend Setup
```bash
cd frontend
npm install
npm run dev
```

### Backend Setup
```bash
cd backend
dotnet restore
dotnet ef database update
dotnet run
```

### Database Setup
1. Install PostgreSQL
2. Create database 'brandit_db'
3. Update connection string in appsettings.json
4. Run migrations

## 🔧 Environment Variables

Create `.env` files:

**Frontend (.env)**
```
VITE_API_BASE_URL=https://api.brandit.com
```

**Backend (appsettings.json)**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=brandit_db;Username=postgres;Password=yourpassword"
  },
  "JwtSettings": {
    "SecretKey": "your-super-secret-key-here",
    "Issuer": "BrandIT",
    "Audience": "BrandIT-Users",
    "ExpiryMinutes": 60
  }
}
```

## 🚀 Deployment

### Frontend (Vercel/Netlify)
```bash
npm run build
# Deploy dist/ folder
```

### Backend (Linux Server)
```bash
dotnet publish -c Release
# Deploy to /var/www/brandit-api/
# Configure reverse proxy (nginx)
```

## 📝 Default Admin Credentials
- **Email:** admin@brandit.com
- **Password:** Admin@123

## 🔗 Links
- Frontend: http://localhost:5173
- Backend API: http://localhost:5000
- Swagger UI: http://localhost:5000/swagger

## 📞 Support
Contact us at support@brandit.com for any issues.

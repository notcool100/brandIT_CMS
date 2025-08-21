# Brand IT Solution - Setup Guide

This guide will help you set up the Brand IT Solution full-stack application on your local development environment and deploy it to a production server.

## 📋 Prerequisites

### Development Environment
- **Node.js** 18+ and npm
- **.NET 8 SDK**
- **PostgreSQL** 13+
- **Git**

### Production Environment (Linux Server)
- **Ubuntu 20.04+** or **CentOS 8+**
- **Docker** (recommended) or manual installation
- **Nginx** (for reverse proxy)
- **PostgreSQL** server
- **SSL Certificate** (Let's Encrypt recommended)

## 🚀 Local Development Setup

### 1. Clone the Repository
```bash
git clone <repository-url>
cd brand_it
```

### 2. Database Setup
```bash
# Install PostgreSQL (Ubuntu/Debian)
sudo apt update
sudo apt install postgresql postgresql-contrib

# Start PostgreSQL service
sudo systemctl start postgresql
sudo systemctl enable postgresql

# Create database
sudo -u postgres psql
CREATE DATABASE brandit_dev_db;
CREATE USER brandit_user WITH PASSWORD 'your_secure_password';
GRANT ALL PRIVILEGES ON DATABASE brandit_dev_db TO brandit_user;
\q
```

### 3. Backend Setup
```bash
cd backend

# Restore NuGet packages
dotnet restore

# Update connection string in appsettings.Development.json
# Replace 'yourpassword' with your actual PostgreSQL password

# Apply database migrations
cd src/BrandIT.API
dotnet ef database update

# Run the API
dotnet run
```

The API will be available at `https://localhost:5000`
Swagger documentation: `https://localhost:5000/swagger`

### 4. Frontend Setup
```bash
cd frontend

# Install dependencies
npm install

# Create environment file
echo "VITE_API_BASE_URL=http://localhost:5000/api" > .env

# Start development server
npm run dev
```

The frontend will be available at `http://localhost:5173`

### 5. Access Admin Panel
- URL: `http://localhost:5173/admin`
- Email: `admin@brandit.com`
- Password: `Admin@123`

## 🐳 Docker Setup (Alternative)

### 1. Create Docker Compose file
```yaml
# docker-compose.yml
version: '3.8'

services:
  postgres:
    image: postgres:15
    container_name: brandit_postgres
    environment:
      POSTGRES_DB: brandit_db
      POSTGRES_USER: brandit_user
      POSTGRES_PASSWORD: your_secure_password
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data

  api:
    build:
      context: ./backend
      dockerfile: Dockerfile
    container_name: brandit_api
    environment:
      ConnectionStrings__DefaultConnection: "Host=postgres;Database=brandit_db;Username=brandit_user;Password=your_secure_password"
      JwtSettings__SecretKey: "your-super-secret-key-here-with-at-least-32-characters"
    ports:
      - "5000:5000"
    depends_on:
      - postgres

  frontend:
    build:
      context: ./frontend
      dockerfile: Dockerfile
    container_name: brandit_frontend
    environment:
      VITE_API_BASE_URL: http://localhost:5000/api
    ports:
      - "3000:3000"

volumes:
  postgres_data:
```

### 2. Run with Docker
```bash
# Build and start all services
docker-compose up --build

# Run in detached mode
docker-compose up -d
```

## 🌐 Production Deployment

### 1. Server Preparation (Ubuntu 20.04+)
```bash
# Update system
sudo apt update && sudo apt upgrade -y

# Install required packages
sudo apt install -y nginx postgresql postgresql-contrib certbot python3-certbot-nginx

# Install .NET 8
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt update
sudo apt install -y dotnet-sdk-8.0

# Install Node.js 18+
curl -fsSL https://deb.nodesource.com/setup_18.x | sudo -E bash -
sudo apt install -y nodejs
```

### 2. Database Setup
```bash
# Configure PostgreSQL
sudo -u postgres psql
CREATE DATABASE brandit_prod_db;
CREATE USER brandit_prod WITH PASSWORD 'very_secure_production_password';
GRANT ALL PRIVILEGES ON DATABASE brandit_prod_db TO brandit_prod;
\q

# Configure PostgreSQL for remote connections
sudo nano /etc/postgresql/*/main/postgresql.conf
# Uncomment and modify: listen_addresses = '*'

sudo nano /etc/postgresql/*/main/pg_hba.conf
# Add: host all all 0.0.0.0/0 md5

sudo systemctl restart postgresql
```

### 3. Backend Deployment
```bash
# Create application directory
sudo mkdir -p /var/www/brandit-api
sudo chown $USER:$USER /var/www/brandit-api

# Copy and build backend
cd /var/www/brandit-api
git clone <repository-url> .
cd backend

# Update production configuration
sudo nano src/BrandIT.API/appsettings.Production.json

# Build for production
dotnet publish src/BrandIT.API -c Release -o /var/www/brandit-api/published

# Create systemd service
sudo nano /etc/systemd/system/brandit-api.service
```

#### Service Configuration
```ini
[Unit]
Description=Brand IT Solution API
After=network.target

[Service]
Type=notify
WorkingDirectory=/var/www/brandit-api/published
ExecStart=/usr/bin/dotnet /var/www/brandit-api/published/BrandIT.API.dll
Restart=always
RestartSec=10
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

```bash
# Enable and start service
sudo systemctl enable brandit-api
sudo systemctl start brandit-api
sudo systemctl status brandit-api
```

### 4. Frontend Deployment
```bash
# Create frontend directory
sudo mkdir -p /var/www/brandit-frontend
sudo chown $USER:$USER /var/www/brandit-frontend

# Copy and build frontend
cd /var/www/brandit-frontend
git clone <repository-url> .
cd frontend

# Install dependencies and build
npm install
npm run build

# Copy built files to web directory
sudo cp -r dist/* /var/www/html/
```

### 5. Nginx Configuration
```bash
# Create Nginx configuration
sudo nano /etc/nginx/sites-available/brandit
```

#### Nginx Configuration
```nginx
server {
    listen 80;
    server_name your-domain.com www.your-domain.com;

    # Frontend
    location / {
        root /var/www/html;
        index index.html;
        try_files $uri $uri/ /index.html;
    }

    # API
    location /api/ {
        proxy_pass http://localhost:5000/api/;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
        proxy_cache_bypass $http_upgrade;
    }
}
```

```bash
# Enable site
sudo ln -s /etc/nginx/sites-available/brandit /etc/nginx/sites-enabled/
sudo nginx -t
sudo systemctl restart nginx
```

### 6. SSL Certificate
```bash
# Install SSL certificate
sudo certbot --nginx -d your-domain.com -d www.your-domain.com

# Auto-renew setup
sudo crontab -e
# Add: 0 12 * * * /usr/bin/certbot renew --quiet
```

### 7. Firewall Configuration
```bash
# Configure UFW
sudo ufw allow OpenSSH
sudo ufw allow 'Nginx Full'
sudo ufw enable
```

## 🔧 Environment Variables

### Backend (.NET)
Create `appsettings.Production.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=brandit_prod_db;Username=brandit_prod;Password=very_secure_production_password"
  },
  "JwtSettings": {
    "SecretKey": "your-super-secret-production-key-with-at-least-32-characters",
    "Issuer": "BrandIT-Production",
    "Audience": "BrandIT-Users-Production",
    "ExpiryMinutes": "60"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### Frontend (Svelte)
Create `.env.production`:
```
VITE_API_BASE_URL=https://your-domain.com/api
```

## 📊 Monitoring & Maintenance

### Log Locations
- **API Logs**: `/var/www/brandit-api/published/logs/`
- **Nginx Logs**: `/var/log/nginx/`
- **PostgreSQL Logs**: `/var/log/postgresql/`

### Useful Commands
```bash
# Check API status
sudo systemctl status brandit-api

# View API logs
sudo journalctl -u brandit-api -f

# Restart services
sudo systemctl restart brandit-api
sudo systemctl restart nginx

# Database backup
pg_dump -h localhost -U brandit_prod -d brandit_prod_db > backup_$(date +%Y%m%d).sql
```

## 🔒 Security Considerations

1. **Change default passwords** for database and admin user
2. **Use environment variables** for sensitive configuration
3. **Enable firewall** and restrict unnecessary ports
4. **Regular updates** of dependencies and system packages
5. **Setup monitoring** for unusual activity
6. **Regular backups** of database and application files

## 📞 Troubleshooting

### Common Issues

#### API not starting
```bash
# Check logs
sudo journalctl -u brandit-api -f

# Check .NET installation
dotnet --version

# Check database connection
psql -h localhost -U brandit_prod -d brandit_prod_db
```

#### Frontend not loading
```bash
# Check Nginx configuration
sudo nginx -t

# Check if files exist
ls -la /var/www/html/

# Check Nginx logs
sudo tail -f /var/log/nginx/error.log
```

#### Database connection issues
```bash
# Check PostgreSQL status
sudo systemctl status postgresql

# Check connection string in appsettings.json
# Verify database user permissions
```

For additional support, check the logs and ensure all services are running correctly.

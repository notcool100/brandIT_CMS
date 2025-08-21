# Brand IT Solution - API Documentation

This document provides comprehensive documentation for the Brand IT Solution REST API.

## 🔗 Base URL

- **Development**: `http://localhost:5000/api`
- **Production**: `https://your-domain.com/api`

## 🔐 Authentication

The API uses JWT (JSON Web Tokens) for authentication. Include the token in the Authorization header:

```
Authorization: Bearer <your-jwt-token>
```

### Authentication Flow

1. **Login** with credentials to get access token
2. **Include token** in subsequent requests
3. **Refresh token** when it expires
4. **Logout** to invalidate tokens

## 📋 Endpoints Overview

| Category | Endpoint | Description |
|----------|----------|-------------|
| Auth | `/auth/*` | Authentication and user management |
| Services | `/services/*` | Company services CRUD |
| Contact | `/contact/*` | Contact messages management |
| Team | `/team/*` | Team members management |
| Blog | `/blog/*` | Blog posts management |

## 🔑 Authentication Endpoints

### POST /auth/login
Login with email and password.

**Request Body:**
```json
{
  "Email": "admin@brandit.com",
  "Password": "Admin@123",
  "RememberMe": false
}
```

**Response:**
```json
{
  "AccessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "RefreshToken": "refresh-token-string",
  "ExpiresAt": "2024-01-15T15:30:00Z",
  "User": {
    "Id": "guid",
    "Email": "admin@brandit.com",
    "FirstName": "Admin",
    "LastName": "User",
    "FullName": "Admin User",
    "Role": "Admin",
    "IsActive": true
  }
}
```

### POST /auth/refresh
Refresh access token using refresh token.

**Request Body:**
```json
{
  "AccessToken": "expired-access-token",
  "RefreshToken": "valid-refresh-token"
}
```

### POST /auth/logout
Logout and invalidate tokens.

**Headers:** `Authorization: Bearer <token>`

### GET /auth/me
Get current user information.

**Headers:** `Authorization: Bearer <token>`

**Response:**
```json
{
  "Id": "guid",
  "Email": "admin@brandit.com",
  "FirstName": "Admin",
  "LastName": "User",
  "FullName": "Admin User",
  "Role": "Admin",
  "IsActive": true
}
```

## 🛠️ Services Endpoints

### GET /services
Get all active services.

**Response:**
```json
[
  {
    "Id": "guid",
    "Name": "Web Development",
    "Description": "Custom web applications...",
    "ShortDescription": "Modern web solutions",
    "Icon": "code",
    "Features": ["Responsive Design", "SEO Optimized"],
    "Price": 2500.00,
    "PriceDescription": "Starting from",
    "ImageUrl": "https://example.com/image.jpg",
    "IsActive": true,
    "SortOrder": 1,
    "Category": "Development",
    "Technologies": ["React", "Node.js"],
    "CreatedAt": "2024-01-01T00:00:00Z",
    "UpdatedAt": "2024-01-15T10:30:00Z"
  }
]
```

### GET /services/{id}
Get service by ID.

### POST /services
Create new service. **Requires Admin role.**

**Headers:** `Authorization: Bearer <admin-token>`

**Request Body:**
```json
{
  "Name": "New Service",
  "Description": "Detailed description...",
  "ShortDescription": "Brief description",
  "Icon": "icon-name",
  "Features": ["Feature 1", "Feature 2"],
  "Price": 1500.00,
  "PriceDescription": "Starting from",
  "ImageUrl": "https://example.com/image.jpg",
  "IsActive": true,
  "SortOrder": 1,
  "Category": "Development",
  "Technologies": ["Technology 1", "Technology 2"]
}
```

### PUT /services/{id}
Update service. **Requires Admin role.**

**Headers:** `Authorization: Bearer <admin-token>`

### DELETE /services/{id}
Delete service (soft delete). **Requires Admin role.**

**Headers:** `Authorization: Bearer <admin-token>`

### GET /services/categories
Get all service categories.

**Response:**
```json
["Development", "Cloud", "Security", "Analytics"]
```

## 📧 Contact Endpoints

### POST /contact
Submit contact message (public endpoint).

**Request Body:**
```json
{
  "Name": "John Doe",
  "Email": "john@example.com",
  "Company": "Example Corp",
  "PhoneNumber": "+1-555-123-4567",
  "Subject": "Project Inquiry",
  "Message": "I'm interested in your services...",
  "ServiceInterest": "Web Development"
}
```

**Response:**
```json
{
  "Id": "guid",
  "Name": "John Doe",
  "Email": "john@example.com",
  "Subject": "Project Inquiry",
  "Status": "New",
  "Priority": "Normal",
  "CreatedAt": "2024-01-15T10:30:00Z"
}
```

### GET /contact
Get contact messages. **Requires authentication.**

**Headers:** `Authorization: Bearer <token>`

**Query Parameters:**
- `status` (optional): Filter by status (New, InProgress, Responded, Closed)
- `priority` (optional): Filter by priority (Low, Normal, High, Urgent)
- `page` (optional, default: 1): Page number
- `pageSize` (optional, default: 20): Items per page

**Response Headers:**
- `X-Total-Count`: Total number of messages
- `X-Page`: Current page
- `X-Page-Size`: Items per page

### GET /contact/{id}
Get contact message by ID. **Requires authentication.**

### PUT /contact/{id}
Update contact message. **Requires authentication.**

**Request Body:**
```json
{
  "Status": "InProgress",
  "Priority": "High",
  "AssignedToUserId": "user-guid",
  "InternalNotes": "Customer called, following up..."
}
```

### DELETE /contact/{id}
Delete contact message. **Requires Admin role.**

### GET /contact/stats
Get contact message statistics. **Requires authentication.**

**Response:**
```json
{
  "Total": 150,
  "New": 25,
  "InProgress": 30,
  "Responded": 70,
  "Closed": 25,
  "Today": 5,
  "ThisWeek": 20
}
```

## 📝 Data Models

### User
```json
{
  "Id": "guid",
  "Email": "string",
  "FirstName": "string",
  "LastName": "string",
  "PhoneNumber": "string",
  "Role": "Admin | Staff",
  "IsActive": "boolean",
  "CreatedAt": "datetime",
  "UpdatedAt": "datetime"
}
```

### Service
```json
{
  "Id": "guid",
  "Name": "string",
  "Description": "string",
  "ShortDescription": "string",
  "Icon": "string",
  "Features": ["string"],
  "Price": "decimal",
  "PriceDescription": "string",
  "ImageUrl": "string",
  "IsActive": "boolean",
  "SortOrder": "integer",
  "Category": "string",
  "Technologies": ["string"],
  "CreatedAt": "datetime",
  "UpdatedAt": "datetime"
}
```

### ContactMessage
```json
{
  "Id": "guid",
  "Name": "string",
  "Email": "string",
  "Company": "string",
  "PhoneNumber": "string",
  "Subject": "string",
  "Message": "string",
  "ServiceInterest": "string",
  "Status": "New | InProgress | Responded | Closed",
  "Priority": "Low | Normal | High | Urgent",
  "AssignedToUserId": "guid",
  "AssignedToUser": "User",
  "ResponseSentAt": "datetime",
  "InternalNotes": "string",
  "CreatedAt": "datetime",
  "UpdatedAt": "datetime"
}
```

## ⚠️ Error Responses

### 400 Bad Request
```json
{
  "message": "Validation failed",
  "errors": ["Email is required", "Password must be at least 6 characters"]
}
```

### 401 Unauthorized
```json
{
  "message": "Invalid email or password"
}
```

### 403 Forbidden
```json
{
  "message": "Insufficient permissions"
}
```

### 404 Not Found
```json
{
  "message": "Resource not found"
}
```

### 500 Internal Server Error
```json
{
  "message": "An error occurred while processing your request"
}
```

## 🔒 Security

### JWT Token Structure
```json
{
  "sub": "user-id",
  "email": "user@example.com",
  "role": "Admin",
  "iat": 1642234567,
  "exp": 1642238167
}
```

### Role-Based Access Control

| Role | Permissions |
|------|-------------|
| **Admin** | Full access to all endpoints |
| **Staff** | Read/write access to most endpoints except user management |
| **Public** | Contact form submission only |

### Rate Limiting
- **Public endpoints**: 100 requests per hour per IP
- **Authenticated endpoints**: 1000 requests per hour per user
- **Admin endpoints**: 500 requests per hour per admin

## 📊 Response Codes

| Code | Description |
|------|-------------|
| 200 | Success |
| 201 | Created |
| 204 | No Content |
| 400 | Bad Request |
| 401 | Unauthorized |
| 403 | Forbidden |
| 404 | Not Found |
| 429 | Too Many Requests |
| 500 | Internal Server Error |

## 🧪 Testing

### Using cURL

#### Login
```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "Email": "admin@brandit.com",
    "Password": "Admin@123"
  }'
```

#### Get Services
```bash
curl -X GET http://localhost:5000/api/services \
  -H "Authorization: Bearer <your-token>"
```

#### Submit Contact Message
```bash
curl -X POST http://localhost:5000/api/contact \
  -H "Content-Type: application/json" \
  -d '{
    "Name": "Test User",
    "Email": "test@example.com",
    "Subject": "Test Message",
    "Message": "This is a test message"
  }'
```

### Using JavaScript/Fetch

```javascript
// Login
const loginResponse = await fetch('http://localhost:5000/api/auth/login', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    Email: 'admin@brandit.com',
    Password: 'Admin@123'
  })
});

const loginData = await loginResponse.json();
const token = loginData.AccessToken;

// Get services with authentication
const servicesResponse = await fetch('http://localhost:5000/api/services', {
  headers: {
    'Authorization': `Bearer ${token}`
  }
});

const services = await servicesResponse.json();
```

## 📖 Additional Resources

- **Swagger UI**: Available at `/swagger` when running in development mode
- **Postman Collection**: Import the API endpoints for testing
- **OpenAPI Specification**: Available at `/swagger/v1/swagger.json`

For more information or support, please contact the development team.

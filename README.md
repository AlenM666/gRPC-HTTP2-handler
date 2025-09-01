# Custom gRPC/HTTP2 Backend

A comprehensive C# backend service that implements both gRPC and HTTP/2 protocols with a clean Object-Oriented architecture. This project demonstrates advanced .NET concepts including custom interceptors, middleware, dependency injection, and dual protocol support.

## 🚀 Features

- **Dual Protocol Support**: Both gRPC and HTTP/2 REST endpoints
- **Custom gRPC Interceptors**: Request logging, error handling, and custom headers
- **HTTP/2 Middleware**: Custom handling for non-gRPC HTTP/2 requests
- **Clean Architecture**: Repository pattern, dependency injection, and separation of concerns
- **Comprehensive Logging**: Structured logging with Serilog
- **Swagger Integration**: API documentation for HTTP endpoints
- **Docker Support**: Containerized deployment
- **Unit Testing**: Test project structure included

## 🏗️ Architecture

### Project Structure
```
CustomGrpcBackend/
├── src/CustomGrpcBackend/           # Main application
│   ├── Controllers/                 # HTTP/2 REST controllers
│   ├── Extensions/                  # Service registration extensions
│   ├── Interceptors/               # gRPC interceptors
│   ├── Middleware/                 # Custom HTTP/2 middleware
│   ├── Models/                     # Data models and DTOs
│   ├── Protos/                     # Protocol buffer definitions
│   ├── Repositories/               # Data access layer
│   └── Services/                   # Business logic and gRPC services
├── tests/                          # Unit and integration tests
├── tools/                          # Development tools and test clients
└── docker/                         # Docker configuration
```

### Key Components

- **UserGrpcService**: Main gRPC service implementation
- **CustomGrpcInterceptor**: Handles logging, headers, and error management
- **CustomHttp2Middleware**: Processes non-gRPC HTTP/2 requests
- **UserRepository**: In-memory data storage with CRUD operations
- **UserService**: Business logic layer

## 🛠️ Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 or VS Code
- Optional: Docker Desktop

### Installation

1. **Clone the repository**

2. **Navigate to the project directory**

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Build the project**
   ```bash
   dotnet build
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

The service will start on:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

### Using Docker

1. **Build Docker image**
   ```bash
   docker build -f docker/Dockerfile -t custom-grpc-backend .
   ```

2. **Run container**
   ```bash
   docker run -p 5000:80 -p 5001:443 custom-grpc-backend
   ```

## 📡 API Endpoints

### gRPC Services

The service exposes the following gRPC methods:

- `GetUser(GetUserRequest)` → `GetUserResponse`
- `CreateUser(CreateUserRequest)` → `CreateUserResponse`
- `UpdateUser(UpdateUserRequest)` → `UpdateUserResponse`
- `DeleteUser(DeleteUserRequest)` → `DeleteUserResponse`
- `GetAllUsers(GetAllUsersRequest)` → `GetAllUsersResponse`
- `StreamUsers(StreamUsersRequest)` → `stream UserResponse`

### HTTP/2 REST Endpoints

- `GET /api/health` - Health check endpoint
- `GET /api/users` - Get all users (with pagination)
- `GET /api/users/{id}` - Get user by ID
- `POST /api/users` - Create new user
- `GET /api/info` - Server information
- `GET /swagger` - API documentation (Development only)

### Example Requests

**gRPC (using grpcurl):**
```bash
# Get all users
grpcurl -plaintext localhost:5000 user.UserService/GetAllUsers

# Create user
grpcurl -plaintext -d '{"name": "John Doe", "email": "john@example.com"}' \
  localhost:5000 user.UserService/CreateUser

# Stream users
grpcurl -plaintext -d '{"page_size": 5}' \
  localhost:5000 user.UserService/StreamUsers
```

**HTTP/2 REST:**
```bash
# Health check
curl -k https://localhost:5001/api/health

# Get users
curl -k https://localhost:5001/api/users

# Create user
curl -k -X POST https://localhost:5001/api/users \
  -H "Content-Type: application/json" \
  -d '{"name": "Jane Doe", "email": "jane@example.com"}'
```

## 🧪 Testing

### Running Tests
```bash
cd tests/CustomGrpcBackend.Tests
dotnet test
```

### Using Test Client
```bash
cd tools/grpc-client-test
dotnet run
```

This will run a comprehensive test suite against your gRPC endpoints.

## 🔧 Configuration

### appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Grpc": "Debug"
    }
  },
  "Kestrel": {
    "EndpointDefaults": {
      "Protocols": "Http1AndHttp2"
    }
  }
}
```

### Environment Variables

- `ASPNETCORE_ENVIRONMENT` - Set to `Development` for additional features
- `ASPNETCORE_URLS` - Configure listening URLs

## 🏭 Production Deployment

### Performance Considerations

1. **Connection Pooling**: Configure HTTP client connection pooling
2. **Resource Limits**: Set appropriate memory and CPU limits
3. **Load Balancing**: Use nginx or similar for load balancing
4. **Monitoring**: Implement health checks and monitoring

### Security

1. **TLS/SSL**: Always use HTTPS in production
2. **Authentication**: Implement JWT or certificate-based auth
3. **Rate Limiting**: Add rate limiting middleware
4. **CORS**: Configure CORS policies appropriately

### Docker Compose Example
```yaml
version: '3.8'
services:
  grpc-backend:
    build:
      context: .
      dockerfile: docker/Dockerfile
    ports:
      - "5000:80"
      - "5001:443"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ASPNETCORE_URLS=http://+:80;https://+:443
    volumes:
      - ./logs:/app/logs
```

## 🔍 Monitoring and Logging

The application uses structured logging with Serilog:

- **Console Logging**: Real-time log output
- **File Logging**: Rolling file logs in `/logs` directory
- **Request Tracking**: Unique request IDs for tracing
- **Performance Metrics**: Response time logging

### Log Levels

- **Debug**: Detailed information for diagnostics
- **Information**: General operational messages
- **Warning**: Potentially harmful situations
- **Error**: Error events that might still allow the application to continue
- **Fatal**: Very severe error events


### Development Guidelines

- Follow C# coding conventions
- Write unit tests for new features
- Update documentation for API changes
- Use meaningful commit messages

## 📚 Technology Stack

- **.NET 8.0** - Runtime framework
- **ASP.NET Core** - Web framework
- **gRPC** - High-performance RPC framework
- **Protocol Buffers** - Serialization format
- **Serilog** - Structured logging
- **Swagger/OpenAPI** - API documentation
- **xUnit** - Testing framework
- **Docker** - Containerization

## 🐛 Troubleshooting

### Common Issues

1. **Port conflicts**: Ensure ports 5000/5001 are available
2. **Certificate errors**: Use `dotnet dev-certs https --trust` for development
3. **gRPC reflection**: Enable in development for tools like grpcurl
4. **HTTP/2 support**: Ensure client supports HTTP/2

### Debug Tips

- Enable detailed gRPC errors in development
- Check application logs in the `/logs` directory
- Use gRPC reflection for service discovery
- Monitor performance counters

## 📋 TODO / Roadmap

- [ ] Add authentication and authorization
- [ ] Implement database persistence (Entity Framework)
- [ ] Add caching layer (Redis)
- [ ] Implement circuit breaker pattern
- [ ] Add comprehensive integration tests
- [ ] Performance benchmarking
- [ ] Kubernetes deployment manifests
- [ ] OpenTelemetry integration
- [ ] GraphQL endpoint support
- [ ] Message queue integration

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

AlenM666

---



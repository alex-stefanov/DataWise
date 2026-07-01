# DataWise

DataWise is a full-stack learning platform for data structures, algorithms, interview preparation, and data visualization. It combines an Angular client, an ASP.NET Core API, SQL and MongoDB persistence, and a Python AI service for algorithm-category classification and interview-style assistance.

## Features

- Algorithm and data-structure knowledge base
- Interview practice sessions with question, hint, and answer workflows
- AI-powered text classification for algorithm categories
- Data charting endpoint for turning datasets into visual summaries
- User profile, authentication, and session management flows
- SQL Server persistence for users, sessions, and interview data
- MongoDB-backed knowledge modules
- Docker setup for the client, API, and AI service

## Tech Stack

| Layer | Technology |
| --- | --- |
| Client | Angular 18, Angular Material, TypeScript |
| API | ASP.NET Core, C# |
| AI | Python, Flask-style service |
| Data | SQL Server, MongoDB, Entity Framework Core |
| DevOps | Docker, Docker Compose |
| Testing | .NET test project |

## Repository Structure

```text
DataWise.AI/       # Python AI model and service
DataWise.Api/      # ASP.NET Core HTTP API
DataWise.Client/   # Angular frontend
DataWise.Common/   # Shared constants, DTOs, and options
DataWise.Core/     # Application services
DataWise.Data/     # SQL and MongoDB data access
DataWise.Tests/    # Automated tests
docker/            # Dockerfiles and compose configuration
```

## Getting Started

### Prerequisites

- .NET SDK
- Node.js and npm
- Python 3
- SQL Server
- MongoDB
- Docker, optional

### API

```bash
dotnet restore DataWise.Api.sln
dotnet build DataWise.Api.sln
dotnet run --project DataWise.Api
```

### Client

```bash
cd DataWise.Client
npm install
npm start
```

### AI Service

```bash
cd DataWise.AI
python -m venv .venv
source .venv/bin/activate
pip install -r requirements.txt
python app.py
```

### Docker

```bash
cd docker
docker compose up --build
```

## Configuration

Configuration files live in `DataWise.Api/appsettings*.json`. Store database connection strings, API keys, and production secrets in environment variables or a local `.env` file instead of committing them.

## Testing

```bash
dotnet test DataWise.Api.sln
```

## License

This project is licensed under the MIT License.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /source

COPY *.sln ./

COPY ./DataWise.Api/*.csproj ./DataWise.Api/
COPY ./DataWise.Common/*.csproj ./DataWise.Common/
COPY ./DataWise.Core/*.csproj ./DataWise.Core/
COPY ./DataWise.Data/*.csproj ./DataWise.Data/
COPY ./DataWise.Tests/*.csproj ./DataWise.Tests/

RUN dotnet restore

COPY ./DataWise.Api/* ./DataWise.Api/
COPY ./DataWise.Common/* ./DataWise.Common/
COPY ./DataWise.Core/* ./DataWise.Core/
COPY ./DataWise.Data/* ./DataWise.Data/
COPY ./DataWise.Tests/* ./DataWise.Tests/

COPY ./DataWise.Data/DbContexts/Releational/Data/interview_questions.csv /app/data/interview_questions.csv

RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
    
WORKDIR /app

COPY --from=build /app ./

ENTRYPOINT ["dotnet", "DataWise.Api.dll"]

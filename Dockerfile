FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EmployeeSection.API/EmployeeSection.API/EmployeeSection.API.csproj", "EmployeeSection.API/"]
COPY ["EmployeeSection.API/EmployeeSection.Application/EmployeeSection.Application.csproj", "EmployeeSection.Application/"]
COPY ["EmployeeSection.API/EmployeeSection.Core/EmployeeSection.Core.csproj", "EmployeeSection.Core/"]
COPY ["EmployeeSection.API/EmployeeSection.DataAccess/EmployeeSection.DataAccess.csproj", "EmployeeSection.DataAccess/"]
RUN dotnet restore "./EmployeeSection.API/EmployeeSection.API.csproj"
COPY ./EmployeeSection.API .
WORKDIR "/src/EmployeeSection.API"
RUN dotnet build "./EmployeeSection.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EmployeeSection.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeSection.API.dll"]
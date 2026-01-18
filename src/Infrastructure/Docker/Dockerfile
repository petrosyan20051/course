# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Создаем пользователя и группу с определенным UID/GID
RUN groupadd -r appgroup && \
    useradd -r -g appgroup -u 1001 appuser && \
    mkdir -p /app/Infrastructure/logs && \
    chown -R appuser:appgroup /app

# Переключаемся на непривилегированного пользователя
USER appuser

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./", "DbAPI/"]
RUN dotnet restore "DbAPI/DbAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DbAPI/DbAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DbAPI/DbAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# COPY secrets + person data 
COPY ["Core/Person_Data/", "./Person_Data/"]
COPY ["Infrastructure/Secrets/", "./Infrastructure/Secrets/"]

# Устанавливаем права на папку logs
USER root
RUN mkdir -p /app/Infrastructure/logs && \
    chown -R appuser:appgroup /app/Infrastructure/logs && \
    chmod 755 /app/Infrastructure/logs
USER appuser

# Initiate ASP.NET
ENTRYPOINT ["dotnet", "DbAPI.dll"]

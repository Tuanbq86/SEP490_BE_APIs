# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8443

ENV ASPNETCORE_URLS="http://+:8080;https://+:8443"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/lenovo.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=24Quochuyho.

VOLUME ["/https"]

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WorkHive.APIs/WorkHive.APIs.csproj", "WorkHive.APIs/"]
COPY ["WorkHive.APIs/firebase-adminsdk.json", "WorkHive.APIs/"]
COPY ["WorkHive.Services/WorkHive.Services.csproj", "WorkHive.Services/"]
COPY ["WorkHive.BuildingBlocks/WorkHive.BuildingBlocks.csproj", "WorkHive.BuildingBlocks/"]
COPY ["WorkHive.Repository/WorkHive.Repositories.csproj", "WorkHive.Repository/"]
COPY ["WorkHive.Data/WorkHive.Data.csproj", "WorkHive.Data/"]
RUN dotnet restore "./WorkHive.APIs/WorkHive.APIs.csproj"
COPY . .
WORKDIR "/src/WorkHive.APIs"
RUN dotnet build "./WorkHive.APIs.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WorkHive.APIs.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkHive.APIs.dll"]
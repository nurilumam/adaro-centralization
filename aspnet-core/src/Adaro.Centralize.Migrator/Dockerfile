#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Adaro.Centralize.Migrator/Adaro.Centralize.Migrator.csproj", "src/Adaro.Centralize.Migrator/"]
COPY ["src/Adaro.Centralize.EntityFrameworkCore/Adaro.Centralize.EntityFrameworkCore.csproj", "src/Adaro.Centralize.EntityFrameworkCore/"]
COPY ["src/Adaro.Centralize.Core/Adaro.Centralize.Core.csproj", "src/Adaro.Centralize.Core/"]
COPY ["src/Adaro.Centralize.Core.Shared/Adaro.Centralize.Core.Shared.csproj", "src/Adaro.Centralize.Core.Shared/"]
RUN dotnet restore "src/Adaro.Centralize.Migrator/Adaro.Centralize.Migrator.csproj"
COPY . .
WORKDIR "/src/src/Adaro.Centralize.Migrator"
RUN dotnet build "Adaro.Centralize.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Adaro.Centralize.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adaro.Centralize.Migrator.dll", "-s"]

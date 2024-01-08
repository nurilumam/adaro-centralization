#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Adaro.Centralize.Web.Public/Adaro.Centralize.Web.Public.csproj", "src/Adaro.Centralize.Web.Public/"]
COPY ["src/Adaro.Centralize.Web.Core/Adaro.Centralize.Web.Core.csproj", "src/Adaro.Centralize.Web.Core/"]
COPY ["src/Adaro.Centralize.Application/Adaro.Centralize.Application.csproj", "src/Adaro.Centralize.Application/"]
COPY ["src/Adaro.Centralize.Application.Shared/Adaro.Centralize.Application.Shared.csproj", "src/Adaro.Centralize.Application.Shared/"]
COPY ["src/Adaro.Centralize.Core.Shared/Adaro.Centralize.Core.Shared.csproj", "src/Adaro.Centralize.Core.Shared/"]
COPY ["src/Adaro.Centralize.Core/Adaro.Centralize.Core.csproj", "src/Adaro.Centralize.Core/"]
COPY ["src/Adaro.Centralize.EntityFrameworkCore/Adaro.Centralize.EntityFrameworkCore.csproj", "src/Adaro.Centralize.EntityFrameworkCore/"]
COPY ["src/Adaro.Centralize.GraphQL/Adaro.Centralize.GraphQL.csproj", "src/Adaro.Centralize.GraphQL/"]
RUN dotnet restore "src/Adaro.Centralize.Web.Public/Adaro.Centralize.Web.Public.csproj"
COPY . .
WORKDIR "/src/src/Adaro.Centralize.Web.Public"
RUN dotnet build "Adaro.Centralize.Web.Public.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Adaro.Centralize.Web.Public.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adaro.Centralize.Web.Public.dll"]

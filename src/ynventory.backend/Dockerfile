#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ynventory.backend/ynventory.backend.csproj", "ynventory.backend/"]
RUN dotnet restore "ynventory.backend/ynventory.backend.csproj"
COPY . .
WORKDIR "/src/ynventory.backend"
RUN dotnet build "ynventory.backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ynventory.backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ynventory.backend.dll"]
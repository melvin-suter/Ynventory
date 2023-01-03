#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Ynventory.Backend/Ynventory.Backend.csproj", "Ynventory.Backend/"]
RUN dotnet restore "Ynventory.Backend/Ynventory.Backend.csproj"
COPY . .
WORKDIR "/src/Ynventory.Backend"
RUN dotnet build "Ynventory.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ynventory.Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

ENTRYPOINT ["dotnet", "Ynventory.Backend.dll"]
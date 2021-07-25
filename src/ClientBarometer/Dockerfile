#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY ["src/ClientBarometer/ClientBarometer.csproj", "ClientBarometer/ClientBarometer.csproj"]
COPY ["src/", "/"]
RUN dotnet build "ClientBarometer/ClientBarometer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClientBarometer/ClientBarometer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClientBarometer.dll"]
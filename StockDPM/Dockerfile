FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StockDPM/StockDPM.csproj", "StockDPM/"]
RUN dotnet restore "StockDPM/StockDPM.csproj"
COPY . .
WORKDIR "/src/StockDPM"
RUN dotnet build "StockDPM.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StockDPM.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockDPM.dll"]

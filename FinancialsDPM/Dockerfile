FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FinancialsDPM/FinancialsDPM.csproj", "FinancialsDPM/"]
RUN dotnet restore "FinancialsDPM/FinancialsDPM.csproj"
COPY . .
WORKDIR "/src/FinancialsDPM"
RUN dotnet build "FinancialsDPM.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinancialsDPM.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinancialsDPM.dll"]

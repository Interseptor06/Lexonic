FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NewsTwitterDPM/NewsTwitterDPM.csproj", "NewsTwitterDPM/"]
RUN dotnet restore "NewsTwitterDPM/NewsTwitterDPM.csproj"
COPY . .
WORKDIR "/src/NewsTwitterDPM"
RUN dotnet build "NewsTwitterDPM.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewsTwitterDPM.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsTwitterDPM.dll"]

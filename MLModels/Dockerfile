FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MLModels/MLModels.csproj", "MLModels/"]
RUN dotnet restore "MLModels/MLModels.csproj"
COPY . .
WORKDIR "/src/MLModels"
RUN dotnet build "MLModels.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MLModels.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MLModels.dll"]

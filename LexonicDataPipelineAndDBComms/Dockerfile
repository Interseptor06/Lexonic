FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LexonicDataPipelineAndDBComms/LexonicDataPipelineAndDBComms.csproj", "LexonicDataPipelineAndDBComms/"]
RUN dotnet restore "LexonicDataPipelineAndDBComms/LexonicDataPipelineAndDBComms.csproj"
COPY . .
WORKDIR "/src/LexonicDataPipelineAndDBComms"
RUN dotnet build "LexonicDataPipelineAndDBComms.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LexonicDataPipelineAndDBComms.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LexonicDataPipelineAndDBComms.dll"]

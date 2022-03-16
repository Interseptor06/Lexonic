FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CodeTesting/CodeTesting.csproj", "CodeTesting/"]
RUN dotnet restore "CodeTesting/CodeTesting.csproj"
COPY . .
WORKDIR "/src/CodeTesting"
RUN dotnet build "CodeTesting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CodeTesting.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CodeTesting.dll"]

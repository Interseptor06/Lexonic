FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LexonicWebApplication/LexonicWebApplication.csproj", "LexonicWebApplication/"]
RUN dotnet restore "LexonicWebApplication/LexonicWebApplication.csproj"
COPY . .
WORKDIR "/src/LexonicWebApplication"
RUN dotnet build "LexonicWebApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LexonicWebApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LexonicWebApplication.dll"]

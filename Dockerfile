# Alapértelmezett image: .NET SDK (build)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build szakasz
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src/.
COPY ["unityREST.csproj", "./"]
RUN dotnet restore "./unityREST.csproj"
COPY . .
WORKDIR "/src/unityREST"
RUN dotnet build "unityREST.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "unityREST.csproj" -c Release -o /app/publish

# Futtatási szakasz
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "unityREST.dll"]

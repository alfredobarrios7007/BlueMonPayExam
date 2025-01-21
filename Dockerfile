# Build Stage
FROM mcr.mocrosoft.com/dotnet/sdk:8.0-focal AS build
WORKDIR /source
RUN dotnet restore "./CatusersApi/CatusersApi.csproj" --disable-parallel
RUN dotnet restore "./CatusersApi/CatusersApi.csproj" -c release -o /app --no-restore


# Serve Stage
FROM mcr.mocrosoft.com/dotnet/aspnet:8.0-focal 
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT["dotnet", "CatUsersApi.dll"]




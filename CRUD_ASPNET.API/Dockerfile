# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia o csproj e restaura dependências
COPY ["CRUD_ASPNET.API/CRUD_ASPNET.API.csproj", "CRUD_ASPNET.API/"]
COPY ["CRUD_ASPNET.Application/CRUD_ASPNET.Application.csproj", "CRUD_ASPNET.Application/"]
COPY ["CRUD_ASPNET.Domain/CRUD_ASPNET.Domain.csproj", "CRUD_ASPNET.Domain/"]
COPY ["CRUD_ASPNET.Infra/CRUD_ASPNET.Infra.csproj", "CRUD_ASPNET.Infra/"]

RUN dotnet restore "CRUD_ASPNET.API/CRUD_ASPNET.API.csproj"

# Copia o resto do código
COPY . .

# Build e publish
WORKDIR "/src/CRUD_ASPNET.API"
RUN dotnet publish -c Release -o /app/publish

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "CRUD_ASPNET.API.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ShopSphere/*.csproj ShopSphere/
COPY ShopSphere.Business/*.csproj ShopSphere.Business/
COPY ShopSphere.Data/*.csproj ShopSphere.Data/
COPY ShopSphere.Models/*.csproj ShopSphere.Models/

RUN dotnet restore "ShopSphere/ShopSphere.csproj"

COPY . .
WORKDIR "/src/ShopSphere"
RUN dotnet publish "ShopSphere.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ShopSphere.dll"]
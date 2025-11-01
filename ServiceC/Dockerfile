FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Service.Shared/Service.Shared.csproj", "Service.Shared/"]
COPY ["ServiceC/ServiceC.csproj", "ServiceC/"]
RUN dotnet restore "ServiceC/ServiceC.csproj"

COPY . .
WORKDIR "/src/ServiceC"
RUN dotnet build "ServiceC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 7000

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceC.dll"]
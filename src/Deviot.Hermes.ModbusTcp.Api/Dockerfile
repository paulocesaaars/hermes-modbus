#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Deviot.Hermes.ModbusTcp.Api/Deviot.Hermes.ModbusTcp.Api.csproj", "src/Deviot.Hermes.ModbusTcp.Api/"]
COPY ["src/Deviot.Hermes.ModbusTcp.Data/Deviot.Hermes.ModbusTcp.Data.csproj", "src/Deviot.Hermes.ModbusTcp.Data/"]
COPY ["src/Deviot.Hermes.ModbusTcp.Business/Deviot.Hermes.ModbusTcp.Business.csproj", "src/Deviot.Hermes.ModbusTcp.Business/"]
RUN dotnet restore "src/Deviot.Hermes.ModbusTcp.Api/Deviot.Hermes.ModbusTcp.Api.csproj"
COPY . .
WORKDIR "/src/src/Deviot.Hermes.ModbusTcp.Api"
RUN dotnet build "Deviot.Hermes.ModbusTcp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Deviot.Hermes.ModbusTcp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Deviot.Hermes.ModbusTcp.Api.dll"]
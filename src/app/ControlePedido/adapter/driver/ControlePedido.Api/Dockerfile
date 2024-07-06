#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Adapter/Driver/ControlePedido.Api/ControlePedido.Api.csproj", "Adapter/Driver/ControlePedido.Api/"]
COPY ["Adapter/ControlePedido.IOC/ControlePedido.IOC.csproj", "Adapter/ControlePedido.IOC/"]
COPY ["Core/ControlePedido.Application/ControlePedido.Application.csproj", "Core/ControlePedido.Application/"]
COPY ["Core/ControlePedido.Domain/ControlePedido.Domain.csproj", "Core/ControlePedido.Domain/"]
COPY ["Adapter/Driven/ControlePedido.Payment/ControlePedido.Payment.csproj", "Adapter/Driven/ControlePedido.Payment/"]
COPY ["Adapter/Driven/ControlePedido.Infra/ControlePedido.Infra.csproj", "Adapter/Driven/ControlePedido.Infra/"]
RUN dotnet restore "Adapter/Driver/ControlePedido.Api/ControlePedido.Api.csproj"
COPY . .
WORKDIR "/src/Adapter/Driver/ControlePedido.Api"
RUN dotnet build "ControlePedido.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ControlePedido.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ControlePedido.Api.dll"]

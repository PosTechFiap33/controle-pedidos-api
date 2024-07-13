
FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ControlePedido/adapter/driver/ControlePedido.Api/ControlePedido.Api.csproj", "ControlePedido/adapter/driver/ControlePedido.Api/"]
COPY ["ControlePedido/adapter/ControlePedido.IOC/ControlePedido.IOC.csproj", "ControlePedido/adapter/ControlePedido.IOC/"]
COPY ["ControlePedido/core/ControlePedido.Application/ControlePedido.Application.csproj", "ControlePedido/core/ControlePedido.Application/"]
COPY ["ControlePedido/core/ControlePedido.Domain/ControlePedido.Domain.csproj", "ControlePedido/core/ControlePedido.Domain/"]
COPY ["ControlePedido/adapter/driven/ControlePedido.Payment/ControlePedido.Payment.csproj", "ControlePedido/adapter/driven/ControlePedido.Payment/"]
COPY ["ControlePedido/adapter/driven/ControlePedido.Infra/ControlePedido.Infra.csproj", "ControlePedido/adapter/driven/ControlePedido.Infra/"]
RUN dotnet restore "ControlePedido/adapter/driver/ControlePedido.Api/ControlePedido.Api.csproj"
COPY . .
WORKDIR "/src/ControlePedido/adapter/driver/ControlePedido.Api"
RUN dotnet build "ControlePedido.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ControlePedido.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ControlePedido.Api.dll"]

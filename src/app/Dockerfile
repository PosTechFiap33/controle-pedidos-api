
FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ControlePedido/Adapter/Driver/ControlePedido.Api/ControlePedido.Api.csproj", "ControlePedido/Adapter/Driver/ControlePedido.Api/"]
COPY ["ControlePedido/Adapter/ControlePedido.IOC/ControlePedido.IOC.csproj", "ControlePedido/Adapter/ControlePedido.IOC/"]
COPY ["ControlePedido/Core/ControlePedido.Application/ControlePedido.Application.csproj", "ControlePedido/Core/ControlePedido.Application/"]
COPY ["ControlePedido/Core/ControlePedido.Domain/ControlePedido.Domain.csproj", "ControlePedido/Core/ControlePedido.Domain/"]
COPY ["ControlePedido/Adapter/Driven/ControlePedido.Payment/ControlePedido.Payment.csproj", "ControlePedido/Adapter/Driven/ControlePedido.Payment/"]
COPY ["ControlePedido/Adapter/Driven/ControlePedido.Infra/ControlePedido.Infra.csproj", "ControlePedido/Adapter/Driven/ControlePedido.Infra/"]
RUN dotnet restore "ControlePedido/Adapter/Driver/ControlePedido.Api/ControlePedido.Api.csproj"
COPY . .
WORKDIR "/src/ControlePedido/Adapter/Driver/ControlePedido.Api"
RUN dotnet build "ControlePedido.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ControlePedido.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ControlePedido.Api.dll"]

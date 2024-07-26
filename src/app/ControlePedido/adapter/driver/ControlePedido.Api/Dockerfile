#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["adapter/driver/ControlePedido.Api/ControlePedido.Api.csproj", "adapter/driver/ControlePedido.Api/"]
COPY ["adapter/ControlePedido.IOC/ControlePedido.IOC.csproj", "adapter/ControlePedido.IOC/"]
COPY ["core/ControlePedido.Application/ControlePedido.Application.csproj", "core/ControlePedido.Application/"]
COPY ["core/ControlePedido.Domain/ControlePedido.Domain.csproj", "core/ControlePedido.Domain/"]
COPY ["common/ControlePedido.CrossCutting/ControlePedido.CrossCutting.csproj", "common/ControlePedido.CrossCutting/"]
COPY ["adapter/driven/ControlePedido.Infra/ControlePedido.Infra.csproj", "adapter/driven/ControlePedido.Infra/"]
COPY ["adapter/driven/ControlePedido.Payment/ControlePedido.Payment.csproj", "adapter/driven/ControlePedido.Payment/"]
RUN dotnet restore "adapter/driver/ControlePedido.Api/ControlePedido.Api.csproj"
COPY . .
WORKDIR "/src/adapter/driver/ControlePedido.Api"
RUN dotnet build "ControlePedido.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ControlePedido.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ControlePedido.Api.dll"]

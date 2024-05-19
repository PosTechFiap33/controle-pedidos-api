#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV TZ=America/Sao_Paulo

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ControlePedido.Api/ControlePedido.Api.csproj", "ControlePedido.Api/"]
COPY ["ControlePedido.Infra/ControlePedido.Infra.csproj", "ControlePedido.Infra/"]
RUN dotnet restore "ControlePedido.Api/ControlePedido.Api.csproj"
COPY . .
WORKDIR "/src/ControlePedido.Api"
RUN dotnet build "ControlePedido.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ControlePedido.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ControlePedido.Api.dll"]

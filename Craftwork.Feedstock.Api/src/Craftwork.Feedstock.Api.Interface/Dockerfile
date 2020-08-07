#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Craftwork.Feedstock.Api.Interface/Craftwork.Feedstock.Api.Interface.csproj", "src/Craftwork.Feedstock.Api.Interface/"]
COPY ["src/Craftwork.Feedstock.Api.Domain/Craftwork.Feedstock.Api.Domain.csproj", "src/Craftwork.Feedstock.Api.Domain/"]
COPY ["src/Craftwork.Feedstock.Api.Domain.Core/Craftwork.Feedstock.Api.Domain.Core.csproj", "src/Craftwork.Feedstock.Api.Domain.Core/"]
RUN dotnet restore "src/Craftwork.Feedstock.Api.Interface/Craftwork.Feedstock.Api.Interface.csproj"
COPY . .
WORKDIR "/src/src/Craftwork.Feedstock.Api.Interface"
RUN dotnet build "Craftwork.Feedstock.Api.Interface.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Craftwork.Feedstock.Api.Interface.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Craftwork.Feedstock.Api.Interface.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY "Fletchling.sln" "Fletchling.sln"
COPY "Fletchling.Api/" "Fletchling.Api/"
COPY "Fletchling.Business/" "Fletchling.Business/"
COPY "Fletchling.Data/" "Fletchling.Data/"

RUN dotnet restore "Fletchling.Api/Fletchling.Api.csproj"

WORKDIR /src/Fletchling.Api
RUN dotnet build "Fletchling.Api.csproj" --no-restore -c Release -o /app/build

FROM build AS test
WORKDIR /src
COPY "Fletchling.Api.Tests/" "Fletchling.Api.Tests/"
RUN dotnet test Fletchling.Api.Tests --no-build --logger trx

FROM build AS publish
RUN dotnet publish "Fletchling.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fletchling.Api.dll"]

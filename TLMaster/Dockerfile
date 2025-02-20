FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY TLMaster/TLMaster.csproj TLMaster/

RUN dotnet restore "TLMaster/TLMaster.csproj"

COPY TLMaster TLMaster

WORKDIR /src/TLMaster

RUN dotnet build "TLMaster.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "TLMaster.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "TLMaster.dll"]
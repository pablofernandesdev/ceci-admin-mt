#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CeciMT.WebApplication/CeciMT.WebApplication.csproj", "CeciMT.WebApplication/"]
COPY ["CeciMT.Service/CeciMT.Service.csproj", "CeciMT.Service/"]
COPY ["CeciMT.Infra.Data/CeciMT.Infra.Data.csproj", "CeciMT.Infra.Data/"]
COPY ["CeciMT.Infra.CrossCutting/CeciMT.Infra.CrossCutting.csproj", "CeciMT.Infra.CrossCutting/"]
COPY ["CeciMT.Domain/CeciMT.Domain.csproj", "CeciMT.Domain/"]
COPY ["CeciMT.WebApplication/CeciMT.Domain.xml", "CeciMT.WebApplication/"]
COPY ["CeciMT.WebApplication/CeciMT.WebApplication.xml", "CeciMT.WebApplication/"]
RUN dotnet restore "CeciMT.WebApplication/CeciMT.WebApplication.csproj"
COPY . .
WORKDIR "/src/CeciMT.WebApplication"
RUN dotnet build "CeciMT.WebApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CeciMT.WebApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CeciMT.WebApplication.dll"]
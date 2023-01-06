#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /SourceCode
COPY . .
RUN dotnet restore "ProductCatalogAPI.csproj"


RUN dotnet build "ProductCatalogAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductCatalogAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductCatalogAPI.dll"]
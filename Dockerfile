FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Lab05-Ederson_Vilca.csproj ./
RUN dotnet restore Lab05-Ederson_Vilca.csproj

COPY . ./
RUN dotnet publish Lab05-Ederson_Vilca.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish ./


ENTRYPOINT ["sh", "-c", "exec dotnet Lab05-Ederson_Vilca.dll --urls http://+:${PORT:-8080}"]

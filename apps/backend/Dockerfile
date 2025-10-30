FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./
COPY src/ ./src
WORKDIR /app/src/Bootstrapper/YetAnotherECommerce.Bootstrapper
RUN dotnet restore
RUN dotnet build -c Release --no-restore

FROM build AS publish
WORKDIR /app
RUN dotnet publish src/Bootstrapper/YetAnotherECommerce.Bootstrapper -c Release --no-build -o release

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish app/release .
ENTRYPOINT ["dotnet", "YetAnotherECommerce.Bootstrapper.dll"]
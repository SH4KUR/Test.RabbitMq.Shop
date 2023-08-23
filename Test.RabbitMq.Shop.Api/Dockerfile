FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Test.RabbitMq.Shop.Api/Test.RabbitMq.Shop.Api.csproj", "Test.RabbitMq.Shop.Api/"]
RUN dotnet restore "Test.RabbitMq.Shop.Api/Test.RabbitMq.Shop.Api.csproj"
COPY . .
WORKDIR "/src/Test.RabbitMq.Shop.Api"
RUN dotnet build "Test.RabbitMq.Shop.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Test.RabbitMq.Shop.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Test.RabbitMq.Shop.Api.dll"]

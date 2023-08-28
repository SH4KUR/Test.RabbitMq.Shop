FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Test.RabbitMq.Shop.Common.StateMachineService/Test.RabbitMq.Shop.Common.StateMachineService.csproj", "Test.RabbitMq.Shop.Common.StateMachineService/"]
RUN dotnet restore "Test.RabbitMq.Shop.Common.StateMachineService/Test.RabbitMq.Shop.Common.StateMachineService.csproj"
COPY . .
WORKDIR "/src/Test.RabbitMq.Shop.Common.StateMachineService"
RUN dotnet build "Test.RabbitMq.Shop.Common.StateMachineService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Test.RabbitMq.Shop.Common.StateMachineService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Test.RabbitMq.Shop.Common.StateMachineService.dll"]

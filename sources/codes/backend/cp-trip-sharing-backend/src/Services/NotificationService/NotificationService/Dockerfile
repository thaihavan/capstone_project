FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY . NotificationService/

WORKDIR "/src/NotificationService"
RUN dotnet restore "NotificationService.csproj"
RUN dotnet build "NotificationService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NotificationService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "NotificationService.dll"]
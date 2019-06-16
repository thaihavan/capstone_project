FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY . EmailService/

WORKDIR "/src/EmailService"
RUN dotnet restore "EmailService.csproj"
RUN dotnet build "EmailService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EmailService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "EmailService.dll"]
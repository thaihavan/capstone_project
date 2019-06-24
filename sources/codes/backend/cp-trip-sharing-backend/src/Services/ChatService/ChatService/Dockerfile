FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["ChatService/ChatService.csproj", "ChatService/"]
RUN dotnet restore "ChatService/ChatService.csproj"
COPY . .
WORKDIR "/src/ChatService"
RUN dotnet build "ChatService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ChatService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ChatService.dll"]
FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["IdentityProvider.csproj", "IdentityProvider/"]
RUN dotnet restore "IdentityProvider/IdentityProvider.csproj"
WORKDIR "/src/IdentityProvider"
COPY . .
RUN dotnet build "IdentityProvider.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "IdentityProvider.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IdentityProvider.dll"]
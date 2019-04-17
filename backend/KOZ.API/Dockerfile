FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["backend/KOZ.API/KOZ.API.csproj", "backend/KOZ.API/"]
RUN dotnet restore "backend/KOZ.API/KOZ.API.csproj"
COPY . .
WORKDIR "/src/backend/KOZ.API"
RUN dotnet build "KOZ.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "KOZ.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "KOZ.API.dll"]
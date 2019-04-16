FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["KOZ.API/KOZ.API.csproj", "KOZ.API/"]
RUN dotnet restore "KOZ.API/KOZ.API.csproj"
COPY . .
WORKDIR "/src/KOZ.API"
RUN dotnet build "KOZ.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "KOZ.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "KOZ.API.dll"]
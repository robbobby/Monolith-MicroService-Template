FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MonoApp/MonoApp.csproj", "MonoApp/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Identity/IdentityApi.csproj", "Identity/"]
COPY ["RepositoryLayer/RepositoryLayer.csproj", "RepositoryLayer/"]
COPY ["UserApi/UserApi.csproj", "UserApi/"]
RUN dotnet restore "MonoApp/MonoApp.csproj"
COPY . .
WORKDIR "/src/MonoApp"
RUN dotnet build "MonoApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MonoApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MonoApp.dll"]

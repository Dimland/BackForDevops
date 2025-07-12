FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BackGeneration/BackGeneration.csproj", "BackGeneration/"]
RUN dotnet restore "BackGeneration/BackGeneration.csproj"
COPY . .
WORKDIR "/src/BackGeneration"
RUN dotnet build "BackGeneration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BackGeneration.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackGeneration.dll"]
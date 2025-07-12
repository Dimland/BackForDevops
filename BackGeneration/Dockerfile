FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BackGeneration/ImageGenerationAPI.csproj", "ImageGenerationAPI/"]
RUN dotnet restore "ImageGenerationAPI/ImageGenerationAPI.csproj"
COPY . .
WORKDIR "/src/BackGeneration/ImageGenerationAPI"
RUN dotnet build "ImageGenerationAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImageGenerationAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImageGenerationAPI.dll"]
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["Common/Common.csproj", "Common/"]
COPY ["Competences.API/Competences.API.csproj", "Competences.API/"]

RUN dotnet restore "Competences.API/Competences.API.csproj"
COPY . .
WORKDIR "/src/Competences.API"
RUN dotnet build "Competences.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Competences.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Competences.API.dll"]
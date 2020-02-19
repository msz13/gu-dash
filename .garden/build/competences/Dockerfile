FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
#COPY ["CompetencesService/CompetencesService.csproj", "CompetencesService/"]
COPY ["CompetencesService.csproj", "CompetencesService/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "CompetencesService/CompetencesService.csproj"
COPY . .
WORKDIR "/src/CompetencesService"
RUN dotnet build "CompetencesService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CompetencesService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompetencesService.dll"]
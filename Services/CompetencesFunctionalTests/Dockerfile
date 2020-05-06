FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["CompetencesFunctionalTests/CompetencesFunctionalTests.csproj", "CompetencesFunctionalTests/"]
RUN dotnet restore "CompetencesFunctionalTests/CompetencesFunctionalTests.csproj"


COPY ["CompetencesFunctionalTests/", "CompetencesFunctionalTests/"]
WORKDIR "/src/CompetencesFunctionalTests"
RUN dotnet build "CompetencesFunctionalTests.csproj" -c Release --no-restore -o /app

FROM build AS publish
RUN dotnet publish "CompetencesFunctionalTests.csproj" -c Release --no-restore -o /app

FROM build AS final
WORKDIR /app
ENV DOTNET_SYSTEM_NET_HTTP_SOCKETSHTTPHANDLER_HTTP2UNENCRYPTEDSUPPORT=true
ENTRYPOINT [ "dotnet test --no-restore" ]

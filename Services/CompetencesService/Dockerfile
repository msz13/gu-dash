FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
RUN GRPC_HEALTH_PROBE_VERSION=v0.3.1 && \
    wget -qO/bin/grpc_health_probe https://github.com/grpc-ecosystem/grpc-health-probe/releases/download/${GRPC_HEALTH_PROBE_VERSION}/grpc_health_probe-linux-amd64 && \
    chmod +x /bin/grpc_health_probe
WORKDIR /app
EXPOSE 443


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["Common/Common.csproj", "Common/"]
COPY ["CompetencesService/CompetencesService.csproj", "CompetencesService/"]
RUN dotnet restore "CompetencesService/CompetencesService.csproj" 

RUN wget -q -O /etc/apk/keys/sgerrand.rsa.pub https://alpine-pkgs.sgerrand.com/sgerrand.rsa.pub \
    && wget https://github.com/sgerrand/alpine-pkg-glibc/releases/download/2.30-r0/glibc-2.30-r0.apk \
    && apk add glibc-2.30-r0.apk

COPY ["CompetencesService/", "CompetencesService/"]
COPY ["Common/", "Common/"] 
WORKDIR "/src/CompetencesService"

RUN dotnet build "CompetencesService.csproj"  --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish "CompetencesService.csproj"  --no-restore -c Release -o /app

FROM base AS final
#ENV ASPNETCORE_URLS=https://+:443
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CompetencesService.dll"]

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0.100-bullseye-slim AS build

WORKDIR /src
COPY ["Web/BookWise.Web/BookWise.Web.csproj", "Web/BookWise.Web/"]
RUN dotnet restore "Web/BookWise.Web/BookWise.Web.csproj"
COPY . /src
WORKDIR "/src/Web/BookWise.Web"
RUN dotnet build "BookWise.Web.csproj" -c Release -o /app/build

FROM node:14.15-alpine AS client 
ARG skip_client_build=false 
WORKDIR /app 
COPY ["Web/BookWise.Web/ClientApp/", "."]
RUN if [ "$skip_client_build" = "true" ]; then \
      echo "Skipping npm install"; \
    else \
      npm install; \
    fi

RUN if [ "$skip_client_build" = "true" ]; then \
      mkdir -p dist; \
    else \
      npm run-script build -- --prod; \
    fi

FROM build AS publish
RUN dotnet publish "BookWise.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS http://*:80

COPY --from=publish /app/publish .
COPY --from=client /app/dist /app/ClientApp/dist
ENTRYPOINT ["dotnet", "BookWise.Web.dll"]




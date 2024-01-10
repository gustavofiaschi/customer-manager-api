FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5225

ENV ASPNETCORE_URLS=http://+:5225

RUN chown app:app /app

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["CustomerManager.csproj", "./"]
RUN dotnet restore "CustomerManager.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CustomerManager.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "CustomerManager.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerManager.dll"]

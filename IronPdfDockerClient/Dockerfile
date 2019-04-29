FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src



COPY IronPdfDockerClient/IronPdfDockerClient.csproj IronPdfDockerClient/
RUN dotnet restore IronPdfDockerClient/IronPdfDockerClient.csproj
COPY . .
WORKDIR /src/IronPdfDockerClient
RUN dotnet build IronPdfDockerClient.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish IronPdfDockerClient.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IronPdfDockerClient.dll"]

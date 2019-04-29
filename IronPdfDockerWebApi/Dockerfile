FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY IronPdfDockerWebApi/IronPdfDockerWebApi.csproj IronPdfDockerWebApi/
COPY IronPdfDockerLib/IronPdfDockerLib.csproj IronPdfDockerLib/
RUN dotnet restore IronPdfDockerWebApi/IronPdfDockerWebApi.csproj
COPY . .
WORKDIR /src/IronPdfDockerWebApi
RUN dotnet build IronPdfDockerWebApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish IronPdfDockerWebApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IronPdfDockerWebApi.dll"]

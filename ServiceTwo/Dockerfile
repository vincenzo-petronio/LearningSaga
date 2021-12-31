#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Common/*.csproj", "Common/"]
COPY ["ServiceTwo/ServiceTwo.csproj", "ServiceTwo/"]
RUN dotnet restore "ServiceTwo/ServiceTwo.csproj"
COPY . .
WORKDIR "/src/Common"
RUN dotnet build "SagaCommon.csproj" -c Release -o /app/build
WORKDIR "/src/ServiceTwo"
RUN dotnet build "ServiceTwo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceTwo.dll"]
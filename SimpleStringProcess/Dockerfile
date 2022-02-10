FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SimpleStringProcess/SimpleStringProcess.csproj", "SimpleStringProcess/"]
RUN dotnet restore "SimpleStringProcess/SimpleStringProcess.csproj"
COPY . .
WORKDIR "/src/SimpleStringProcess"
RUN dotnet build "SimpleStringProcess.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleStringProcess.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleStringProcess.dll"]

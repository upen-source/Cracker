FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/CLInjected/CLInjected.csproj", "CLInjected/"]
RUN dotnet restore "CLInjected/CLInjected.csproj"
COPY /src .
WORKDIR "/src/CLInjected"
RUN dotnet build "CLInjected.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CLInjected.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CLInjected.dll"]

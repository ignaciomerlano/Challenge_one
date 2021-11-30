FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Challenge_one/Challenge_one/Challenge_one.csproj", "Challenge_one/Challenge_one/"]

RUN dotnet restore "Challenge_one/Challenge_one.csproj"
COPY . .
WORKDIR "/src/Challenge_one"
RUN dotnet build "Challenge_one.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Challenge_one.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Challenge_one.dll"]
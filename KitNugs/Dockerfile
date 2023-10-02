#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080 8081

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["KitNugs/KitNugs.csproj", "KitNugs/"]
RUN dotnet restore "KitNugs/KitNugs.csproj"
COPY . .
WORKDIR "/src/KitNugs"
RUN dotnet build "KitNugs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KitNugs.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

RUN groupadd --gid 11000 appuser && \
    useradd --gid 11000 --uid 11000 -m appuser


WORKDIR /app
COPY --from=publish /app/publish .

RUN chmod 644 /app/appsettings.json

USER 11000

CMD ["dotnet", "KitNugs.dll"]
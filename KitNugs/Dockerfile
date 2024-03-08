# Changing this file most likely also requires changes to Dockerfile-fat.
FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
ARG TARGETARCH
WORKDIR /source

# copy csproj and restore as distinct layers
COPY Documentation/*.csproj Documentation/
RUN dotnet restore -a $TARGETARCH  Documentation

COPY KitNugs/*.csproj KitNugs/
RUN dotnet restore -a $TARGETARCH  KitNugs

# copy everything else and build app
COPY . .
RUN dotnet publish -a $TARGETARCH --no-restore -o /app KitNugs

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-chiseled-extra
EXPOSE 8080 8081
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["./KitNugs"]
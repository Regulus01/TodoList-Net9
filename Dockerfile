FROM mcr.microsoft.com/dotnet/nightly/sdk:9.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/nightly/sdk:9.0
WORKDIR /App
COPY --from=build-env /App/out .

ENTRYPOINT ["dotnet", "TodoListNet9.Api.dll"]
# BUILD
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY . .
RUN dotnet tool install --global dotnet-ef
RUN dotnet restore --disable-parallel
RUN dotnet publish -c release -o /app --no-restore


# SERVE
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./
ENV ASPNETCORE_URLS=http://+:3022
ENV ASPNETCORE_HTTP_PORT=http://+:3022
EXPOSE 3022
ENTRYPOINT ["dotnet", "DotNet.Docker.dll"]

# Copy everything
#COPY . ./
# Restore as distinct layers
#RUN dotnet restore
# Build and publish a release
#RUN dotnet publish -c Release -o out

# Build runtime image
#FROM mcr.microsoft.com/dotnet/aspnet:6.0
#WORKDIR /app
#COPY --from=build-env /app/out .
#EXPOSE 3022
#ENTRYPOINT ["dotnet", "DotNet.Docker.dll"]
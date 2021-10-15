#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PhotoAlbum.ClientPortal/PhotoAlbum.ClientPortal.csproj", "PhotoAlbum.ClientPortal/"]
RUN dotnet restore "PhotoAlbum.ClientPortal/PhotoAlbum.ClientPortal.csproj"
COPY . .
WORKDIR "/src/PhotoAlbum.ClientPortal"
RUN dotnet build "PhotoAlbum.ClientPortal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhotoAlbum.ClientPortal.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhotoAlbum.ClientPortal.dll"]
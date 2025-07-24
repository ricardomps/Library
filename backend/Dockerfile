FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln ./
COPY LibraryApi/*.csproj ./LibraryApi/
RUN dotnet restore ./LibraryApi/LibraryApi.csproj

COPY . .
WORKDIR /src/LibraryApi
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5000

EXPOSE 5000
ENTRYPOINT ["dotnet", "LibraryApi.dll"]

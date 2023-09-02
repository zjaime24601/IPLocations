FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /sln

COPY ./*.sln ./Directory.Build.props ./
COPY ./src/*/*.csproj ./
RUN for file in `ls *.csproj`; do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done

COPY ./tests/*/*.csproj ./
RUN for file in `ls *.csproj`; do mkdir -p tests/${file%.*}/ && mv $file tests/${file%.*}/; done

RUN dotnet restore

COPY ./src ./src
COPY ./tests ./tests

RUN dotnet build -c Release --no-restore

FROM build AS publish

RUN dotnet publish -c Release --no-restore --no-build ./src/IPLocations.Api/IPLocations.Api.csproj -o /sln/dist

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /sln/dist .
ENTRYPOINT ["dotnet", "IPLocations.Api.dll"]
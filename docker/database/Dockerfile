FROM bitnami/dotnet-sdk:5 AS db-build-env
WORKDIR /db
# Copy and build database project
COPY /src/web/Shearlegs.Database ./
RUN dotnet build Shearlegs.Database.Build/Shearlegs.Database.Build.csproj --configuration Release -o out

FROM markhobson/sqlpackage:latest
ARG CONNECTION_STRING
ENV CONNECTION_STRING=$CONNECTION_STRING

WORKDIR /db
COPY --from=db-build-env /db/out .
ENTRYPOINT sqlpackage /a:Publish /tcs:"$CONNECTION_STRING" /sf:Shearlegs.Database.Build.dacpac /p:IgnoreColumnOrder=true

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app
COPY ./AppLockerService/*.csproj ./
RUN dotnet restore AppLockerService.csproj
COPY ./AppLockerService/. ./
RUN dotnet publish AppLockerService.csproj -c Release -o pub
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=0 /app/pub .
ENTRYPOINT ["dotnet", "AppLockerService.dll"]

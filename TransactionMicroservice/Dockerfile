FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["TransactionMicroservice.csproj", "./"]
RUN dotnet restore "./TransactionMicroservice.csproj"
COPY . .
 
RUN dotnet build "TransactionMicroservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TransactionMicroservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TransactionMicroservice.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:latest AS base
WORKDIR /app
EXPOSE 80

FROM  mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ChatApp.sln ./

COPY ChatApp.Contracts/*.csproj ./ChatApp.Contracts/
COPY ChatApp/*.csproj ./ChatApp/

RUN dotnet restore
COPY . .

WORKDIR /src/ChatApp.Contracts
RUN dotnet build -c Release -o /app

	
WORKDIR /src/ChatApp
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ChatApp.dll"]
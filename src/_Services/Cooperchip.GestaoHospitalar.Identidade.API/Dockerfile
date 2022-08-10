#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/_Services/Cooperchip.GestaoHospitalar.Identidade.API/Cooperchip.GestaoHospitalar.Identidade.API.csproj", "src/_Services/Cooperchip.GestaoHospitalar.Identidade.API/"]
RUN dotnet restore "src/_Services/Cooperchip.GestaoHospitalar.Identidade.API/Cooperchip.GestaoHospitalar.Identidade.API.csproj"
COPY . .
WORKDIR "/src/src/_Services/Cooperchip.GestaoHospitalar.Identidade.API"
RUN dotnet build "Cooperchip.GestaoHospitalar.Identidade.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cooperchip.GestaoHospitalar.Identidade.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cooperchip.GestaoHospitalar.Identidade.API.dll"]
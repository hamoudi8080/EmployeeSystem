#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["EmployeeManagmentApi/EmployeeManagmentApi.csproj", "EmployeeManagmentApi/"]
COPY ["EmployeeManagmentModel/EmployeeManagmentModel.csproj", "EmployeeManagmentModel/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "./EmployeeManagmentApi/EmployeeManagmentApi.csproj"
COPY . .
WORKDIR "/src/EmployeeManagmentApi"
RUN dotnet build "./EmployeeManagmentApi.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "./EmployeeManagmentApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeManagmentApi.dll"]





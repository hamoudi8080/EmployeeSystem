#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EmployeeManagmentApi/EmployeeManagmentApi.csproj", "EmployeeManagmentApi/"]
COPY ["EmployeeManagmentModel/EmployeeManagmentModel.csproj", "EmployeeManagmentModel/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["BlazorEmployeeApplication/BlazorEmployeeApplication.csproj", "BlazorEmployeeApplication/"]
RUN dotnet restore "./EmployeeManagmentApi/EmployeeManagmentApi.csproj"
COPY . .
WORKDIR "/src/EmployeeManagmentApi"
RUN dotnet build "./EmployeeManagmentApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EmployeeManagmentApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV DB_SERVER=employeepostgressqldb.postgres.database.azure.com
ENV DB_NAME=EmployeesDB
ENV DB_PORT=5432
ENV DB_USER=techschool
ENV DB_PASSWORD=Aqaq1997914

ENTRYPOINT ["dotnet", "EmployeeManagmentApi.dll"]
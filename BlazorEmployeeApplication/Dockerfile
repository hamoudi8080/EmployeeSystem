FROM nginx AS base
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BlazorEmployeeApplication/BlazorEmployeeApplication.csproj", "BlazorEmployeeApplication/"]
RUN dotnet restore "BlazorEmployeeApplication/BlazorEmployeeApplication.csproj"

COPY . .
WORKDIR "/src/BlazorEmployeeApplication"
RUN dotnet build "BlazorEmployeeApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorEmployeeApplication.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM  base AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY  BlazorEmployeeApplication/nginx.conf /etc/nginx/nginx.conf
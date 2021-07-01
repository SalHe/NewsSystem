FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Whu.BLM.NewsSystem.Client/Whu.BLM.NewsSystem.Client.csproj", "Whu.BLM.NewsSystem.Client/"]
RUN dotnet restore "Whu.BLM.NewsSystem.Client/Whu.BLM.NewsSystem.Client.csproj"
COPY . .
# 覆盖配置文件为Docker专用配置
COPY "Whu.BLM.NewsSystem.Client/wwwroot/appsettings.Docker.json" "Whu.BLM.NewsSystem.Client/wwwroot/appsettings.json"
WORKDIR "/src/Whu.BLM.NewsSystem.Client"
RUN dotnet build "Whu.BLM.NewsSystem.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Whu.BLM.NewsSystem.Client.csproj" -c Release -o /app/publish

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Whu.BLM.NewsSystem.Client.dll"]

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish .
COPY Whu.BLM.NewsSystem.Client/nginx.conf /etc/nginx/nginx.conf
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Whu.BLM.NewsSystem.Spider.Server/Whu.BLM.NewsSystem.Spider.Server.csproj", "Whu.BLM.NewsSystem.Spider.Server/"]
RUN dotnet restore "Whu.BLM.NewsSystem.Spider.Server/Whu.BLM.NewsSystem.Spider.Server.csproj"
COPY . .
# 复制Docker配置
COPY "Whu.BLM.NewsSystem.Spider.Server/appsettings.Docker.json" "Whu.BLM.NewsSystem.Spider.Server/appsettings.json"
WORKDIR "/src/Whu.BLM.NewsSystem.Spider.Server"
RUN dotnet build "Whu.BLM.NewsSystem.Spider.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Whu.BLM.NewsSystem.Spider.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Whu.BLM.NewsSystem.Spider.Server.dll"]

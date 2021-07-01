FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Whu.BLM.NewsSystem.Server/Whu.BLM.NewsSystem.Server.csproj", "Whu.BLM.NewsSystem.Server/"]
RUN dotnet restore "Whu.BLM.NewsSystem.Server/Whu.BLM.NewsSystem.Server.csproj"
COPY . .
# 复制Docker配置
COPY "Whu.BLM.NewsSystem.Server/appsettings.Docker.json" "Whu.BLM.NewsSystem.Server/appsettings.json"
WORKDIR "/src/Whu.BLM.NewsSystem.Server"
RUN dotnet build "Whu.BLM.NewsSystem.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Whu.BLM.NewsSystem.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Whu.BLM.NewsSystem.Server.dll"]

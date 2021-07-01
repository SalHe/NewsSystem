$initStartTime = Get-Date

# docker-compose
$runDockerCompose = Read-Host "是否希望在构建完镜像之时立刻启动服务？(Y/n)"
if ($runDockerCompose.ToUpper() -eq 'Y' ) {
    Write-Host "将在镜像构建完毕之时自动启动服务"
}
else {
    Write-Warning "将不会自动运行服务"
}

# 构建镜像
$buildStartTime = Get-Date
Write-Host -ForegroundColor Blue "正在为后端服务构建镜像: news-server..."
docker build -f ../Whu.BLM.NewsSystem.Server/Dockerfile -t news-server ..
Write-Host -ForegroundColor Blue "正在为前端服务构建镜像: news-client..."
docker build -f ../Whu.BLM.NewsSystem.Client/Dockerfile -t news-client ..
Write-Host -ForegroundColor Blue "正在为爬虫服务构建镜像: news-spider-server..."
docker build -f ../Whu.BLM.NewsSystem.Spider.Server/Dockerfile -t news-spider-server ..

$endTime = Get-Date
Write-Host -ForegroundColor Green "镜像构建完毕，耗时："($endTime - $buildStartTime)

# 运行服务
if ($runDockerCompose.ToUpper() -eq 'Y') {
    Write-Host "正在为您运行或更新服务"
    docker-compose up -d
}

$endTime = Get-Date
Write-Host -ForegroundColor Green "构建完毕，耗时"($endTime - $initStartTime)
# BLM News

## 解决方案结构

- Whu.BLM.NewsSystem.Shared

  共享部分。目前主要是数据实体的定义。

- Whu.BLM.NewsSystem.Data

  数据访问部分。主要用于定义DbContext中各种实体之间的关系，以及实体自身的约束等。

- Whu.BLM.NewsSystem.Client

  用户客户端。主要是Blazor编写的UI，供用户通过浏览器访问。

- Whu.BLM.NewsSystem.Server

  服务端(后端)。主要定义了各种服务的逻辑，并为Client提供了服务接口。

- Whu.BLM.NewsSystem.Spider

  爬虫核心。主要用于编写爬虫的核心逻辑。

- Whu.BLM.NewsSystem.Spider.Server

  爬虫服务。主要用于运行爬虫服务，并向外暴露接口用于调整爬虫的参数以及获取爬虫的状态。

## 文档

- [需求文档](./Docs/项目需求.md)
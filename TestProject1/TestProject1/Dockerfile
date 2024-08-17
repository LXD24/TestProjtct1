
# 使用官方的 .NET Core 3.1 SDK 镜像作为构建环境
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

# 设置工作目录
WORKDIR /app

# 将项目文件复制到容器中
COPY TestProject1.csproj ./
RUN dotnet restore

# 将所有文件复制到工作目录
COPY . ./

# 编译并发布应用程序
RUN dotnet publish -c Release -o out

# 使用官方的 .NET Core 3.1 运行时镜像作为运行环境
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

# 设置工作目录
WORKDIR /app

# 从构建环境复制发布的文件到运行环境
COPY --from=build /app/out .

# 设置环境变量（可选）
ENV ASPNETCORE_URLS=http://+:5000
ENV DOTNET_RUNNING_IN_CONTAINER=true

# 暴露端口
EXPOSE 5000

# 定义容器启动时执行的命令
ENTRYPOINT ["dotnet", "TestProject1.dll"]
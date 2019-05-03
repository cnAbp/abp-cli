# Abp中文网 CLI

目前支持CLI创建新项目.

### 安装

安装 `Cn.Abp.Cli` 作为全局工具：

````
dotnet tool install -g Cn.Abp.Cli
````

如果你已经安装过,请更新CLI:

````
dotnet tool update -g Cn.Abp.Cli
````

### 使用

创建一个新项目:

````
cnabp new Acme.PhoneBook
````

默认模板是`MVC应用程序`. 默认数据库提供程序是`Entity Framework Core`.

指定数据库提供者:

````
cnabp new Acme.PhoneBook -d ef
cnabp new Acme.PhoneBook -d mongodb
````

指定模板:

````
cnabp new Acme.PhoneBook -t mvc
cnabp new Acme.PhoneBook -t mvcmodule
cnabp new Acme.PhoneBook -t service
````
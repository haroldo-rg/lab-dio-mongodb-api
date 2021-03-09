# Prática do curso: Construindo um projeto de uma API .NET integrada ao MongoDB

Este projeto implementa uma Web API em .NET 5 com operações de CRUD acessando uma collection do MongoDB.

**Actions do controller *Infectado***

Cadastrar um novo infectado
> POST ​/infectado

Listar as infromações de todos os infectados cadastrados
>   GET ​/infectado

Atualizar os dados de um infectado
>   PUT ​/infectado/{id}

Excluir os dados de um infectado
>   DELETE ​/infectado/{id}

### Ferramentas necessárias para rodar o projeto

 - MongoDB (https://www.mongodb.com/cloud/atlas)
 - SDK do .NET 5 (https://dotnet.microsoft.com/download/dotnet/5.0)

### Configurações
A string de conexão deve ser configurada no arquivo *appsettings.json* com os dados da sua instância do MongoDB.

    "MongoDBConnectionString": "mongodb+srv://api:<password>@cluster0.mp1bh.mongodb.net/myFirstDatabase?retryWrites=true&w=majority"

### Pacotes NuGet instalados

 - MongoDB.Driver

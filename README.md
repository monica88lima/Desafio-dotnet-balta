# Desafio-dotnet-balta
<h1 align="center">:computer: #Criação de API</h1>

O desafio proposto pelo **Balta**, tinha como próposito a criação de uma API moldada por um repositorio que contém dados de cidades e estados.
Este exercicio apresentou três níveis de entrega, que os participantes poderiam escolher de acordo com seu nível de Senioridade.

Neste etapa escolhi o perfil de Júnior para realizar o proposto.

# :bookmark_tabs: Descrição do Projeto

Dentro do perfil escolhido [Júnior] 

### DoD

- Autenticação e Autorização
- [x] Cadastro de E-mail e Senha
- [x] Login (Token, JWT)

- CRUD de Localidade
- [x] Código, Estado, Cidade (Id, City, State)
- [x] Pesquisa por cidade
- [x] Pesquisa por estado
- [x] Pesquisa por código (IBGE)
	 
- Boas práticas da API
- [x] Versionamento
- [x] Padronização
- [x] Documentação (Swagger)

# :triangular_flag_on_post: Tecnologias utilizadas

 <ul>
  <li>.Net 7.0 </li>
   <li> C# </li>
   <li> SQLServer </li>
   <li> Azure </li>
   
</ul>
   
# :rocket: Funcionalidades do projeto
  
A API dispõe do serviço de Cadastrar usuário, permitindo que este utilize após login os serviços de:
Cadastro de Localidade conforme perfil do IBGE;
Consulta a todos os registro de localidade, além de consultas específicas por ID, Cidade e Sigla do Estado;
Alteração de Cadastro da Localidade;
Remoçao de Registro;
    
# 📁 Acesso ao projeto

**A API esta disponivel em: **

# 🛠️ Para abrir e rodar o projeto, é necessario:

**Criar Banco de Dados**

- Rodar o script abaixo:

  ```bash
   Create database [nomeDoBanco]
  ```
 **Criar as tabelas 'User' e 'IBGE'****
```
USE [nomeDoBanco]

GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[USER](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](80) NOT NULL,
	[LastName] [varchar](80) NOT NULL,
	[Email] [varchar](300) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Profile] [varchar](100) NULL,
 CONSTRAINT [PK_USER] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
GO
CREATE TABLE [dbo].[IBGE](
	[Id] [char](7) NOT NULL,
	[State] [char](2) NULL,
	[City] [nvarchar](80) NULL,
	[CreationDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_IBGE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
        ```
   


```




**Abrir solution com IDE (Visual Studio )ou Editor de código (Visual Studio Code) ou outro conforme preferência:**
- Configurar a string de conexão com o banco de dados no arquivo appsettings.json


**Para configuração do serviço de Autenticação da API, se faz necessário ajustar a configuração da propriedade PrivateKey, caminho: DesafioDotnet-balta\Configuration\Configuration.csConfiguration.cs**
-Utilize o padrão de UserSecrets para configurar esta propriedade, orientações e passo a passo disponivel neste artigo: 
[Armazenamento seguro de segredos de aplicativos em desenvolvimento no ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows)

# :bulb: Pacotes

net7.0;

AutoMapper/12.0.1;

Asp.Versioning.Mvc.ApiExplorer/7.1.0;

Asp.Versioning.Mvc.ApiExplorer/7.1.0;

AutoMapper.Extensions.Microsoft.DependencyInject/12.0.1;

Microsoft.AspNetCore.Authentication.JwtBearer/7.0.12;

Microsoft.AspNetCore.OpenApi/7.0.11;

Microsoft.EntityFrameworkCore.SqlServer/7.0.12;

Microsoft.Extensions.Configuration.UserSecrets/7.0.0;

Swashbuckle.AspNetCore/6.5.0;

Swashbuckle.AspNetCore.Swagger/6.5.0;

Swashbuckle.AspNetCore.SwaggerGen/6.5.0;

Swashbuckle.AspNetCore.SwaggerUI/6.5.0;

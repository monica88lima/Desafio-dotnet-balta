# Desafio-dotnet-balta
<h1 align="center">:computer: #Criação de API</h1>

O desafio proposto pelo professor Balta, tinha como próposito a criação de uma API moldada por um repositorio que contém dados de cidades e estados.
Este exercicio apresentou três níveis de entrega, que os participantes poderiam escolher de acordo com seu nível de Senioridade.

Neste etapa escolhi o perfil de Junior para realizar o proposto.

Desafio: <a href ="https://baltaio.blob.core.windows.net/temp/desafio-dotnet/01-sobre.pdf">

:heavy_check_mark:Descrição do Projeto

Dentro do perfil escolhido [Júnior] 

### DoD

- Autenticação e Autorização
      [x] Cadastro de E-mail e Senha
      [x] Login (Token, JWT)

- CRUD de Localidade
     [x] Código, Estado, Cidade (Id, City, State)
	 [x] Pesquisa por cidade
     [x] Pesquisa por estado
     [x] Pesquisa por código (IBGE)
	 
- Boas práticas da API
     [x] Versionamento
     [x] Padronização
     [x] Documentação (Swagger)

:computer:-[Tecnologia](#tecnologia)

 .Net 7.0
   

#:pushpin:-[Recursos](#recursos)

<ul>
 
   <li> C# </li>
   <li> SQLServer </li>
   <li> Azure </li>
   
</ul>


:file_folder:Como Usar

Criando Banco de Dados

   ```bash
   Create database [nomeDoBanco]

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



[![Alt text] Configurar a string de conexão no arquivo appsettings.json e Configurar a PrivateKey do arquivo Configuration como Secrets]

Guia para configuração da Secrets: <a href ="https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows">


:bulb:-[Pacotes](#Pacotes)

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

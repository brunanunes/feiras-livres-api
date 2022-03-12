# API - Feiras Livres

### Índice
* [Objetivo](#objetivo)
* [Funcionalidades](#funcionalidades)
* [Requisitos](#requisitos)
* [Tecnologias e Ferramentas Utilizadas](#tecnologias-utilizadas)
* [Banco de Dados](#banco-de-dados)
* [Executar a Aplicação](#executar-a-aplicação)
* [Rotas](#rotas)
* [Exemplos](#exemplos)

### Objetivo
Disposibilizar uma API que exponha os dados das feiras livres da cidade de São Paulo utilizando a base de informações DEINFO_AB_FEIRASLIVRES_2014.csv disponíveis em http://www.prefeitura.sp.gov.br/cidade/secretarias/upload/chamadas/feiras_livres_1429113213.zip

### Funcionalidades
- cadastro de uma nova feira
- exclusão de uma feira através de seu código de registro
- alteração dos campos cadastrados de uma feira, exceto seu código de registro
- busca de feiras utilizando ao menos um dos parâmetros abaixo:
    - distrito
    - regiao5
    - nome_feira
    - bairro
    
### Requisitos
- os dados fornecidos pela Prefeitura de São Paulo devem ser armazenados em um banco de dados relacional
- a solução deve conter um script para importar os dados do arquivo DEINFO_AB_FEIRASLIVRES_2014.csv para o banco relacional
- a API deve seguir os conceitos REST
- o Content-Type das respostas da API deve ser application/json
- o código da solução deve conter testes e algum mecanismo documentado para gerar a informação de cobertura dos testes
- a aplicação deve gravar logs estruturados em arquivos texto

### Tecnologias Utilizadas
- ASP.NET Core 5
- C#
- Entity Framework
- Clean Code 
- Swagger
- ReportGenerator

### Ferramentas Utilizadas 
- Visual Studio
- SQL Server

### Banco de dados
 - para a criação do banco de dados é necessário a execução do 1.CreateDataBase disponível no diretório database deste projeto
 - após a criação, deve-se realizar a importação dos dados disponibilizados pela prefeitura utilizando o sccript 2.LoadData presente no mesmo diretório. Atenção: para correto funcionamento do script antes da execução é necessário alterar o caminho para o arquivo, presente na linha 28 ('C:\Users\username\source\repos\feira-livre-api\database\DEINFO_AB_FEIRASLIVRES_2014.csv'), de acordo com o caminho do seu repositório 

### Executar a Aplicação
Para executar a aplicação abra o prompt de comando no \FeirasLivres.API e execute o seguinte comando:
- dotnet run --project FeirasLivres.API.csproj
- Acessar a url: https://localhost:5001/swagger/index.html

### Rotas
Utilize as rotas auxiliares para consulta dos Id's de Distrito e Subprefeitura necessários para as operações de cadastro e alteração

* FeirasController
    * GET /feiras/feirasbyfilter?distrito=&regiao=&nome_feira=&bairro= - Retorna as feiras de acordo com os filtros selecionados
    * DELETE /feiras/id - Deleta feira por Id.    
    * PUT /feiras/id - Altera dos dados da feira por Id    
    * POST /feiras - Cadastra nova feira
* DistritosController
    * GET  /distritos - Retorna todos os distritos cadastrados no banco de dados
    * GET  /distritos/id - Retorna as informações do distrito por Id
    
* SubprefeiturasController
    * GET  /subprefeituras - Retorna todas as subprefeituras cadastradas no banco de dados
    * GET  /subprefeituras/id - Retorna as informações da subprefeitura por Id

### Exemplos
### /api/feiras/feirasbyfilter

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| distrito | query | Nome do distrito | No | string |
| regiao | query | Regiao | No | string |
| nome_feira | query | Nome da feira | No | string |
| bairro | query | Bairro | No | string |

##### Responses

Code = 200
``` json
{
  "succeeded": true,
  "data": [
    {
      "id": 83,
      "longitude": -46442096,
      "latitude": -23599344,
      "setor_censitario": "355030833000015",
      "area_ponderacao": "3550308005275",
      "regiao": "Leste",
      "subregiao": "Leste 2",
      "nome_feira": "JARDIM IGUATEMI",
      "registro": "5125-0",
      "logradouro": "RUA CUBAS DE MENDONCA",
      "numero": "S/N",
      "bairro": "JD IGUATEMI",
      "distrito": {
        "codigo_distrito": 32,
        "nome_distrito": "IGUATEMI"
      },
      "subprefeitura": {
        "codigo_subprefeitura": 30,
        "nome_subprefeitura": "SAO MATEUS"
      }
    }
  ]
}
```

Code = 400
``` json
{
  "succeeded": false,
  "message": "You must use at least one filter"
}
```

Code = 404
``` json
{
  "succeeded": false,
  "message": "No record found for the this filters"
}
```

### /api/feiras/{id}

#### DELETE
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path | Id da Feira | Yes | integer |

##### Responses

Code = 200
``` json
{
  "succeeded": true,
  "message": "Feira deleted successfully"
}
```

Code = 400
``` json
{
  "succeeded": false,
  "message": "Invalid Id"
}
```

Code = 404
``` json
{
  "succeeded": false,
  "message": "Id not found"
}
```

#### PUT
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path | Id da Feira | Yes | integer |

##### Body
``` json
{
  "longitude": -46442096,
  "latitude": -23599344,
  "setor_censitario": "355030833000015",
  "area_ponderacao": "3550308005275",
  "regiao": "Leste",
  "subregiao": "Leste 2",
  "nome_feira": "JARDIM IGUATEMI",
  "registro": "5125-0",
  "logradouro": "RUA CUBAS DE MENDONCA",
  "numero": "",
  "bairro": "JD IGUATEMI",
  "codigo_distrito": 32,
  "codigo_subprefeitura": 30
}
```

##### Responses

Code = 200
``` json
{
  "succeeded": true,
  "data": {
    "id": 83,
    "longitude": -46442096,
    "latitude": -23599344,
    "setor_censitario": "355030833000015",
    "area_ponderacao": "3550308005275",
    "regiao": "Leste",
    "subregiao": "Leste 2",
    "nome_feira": "JARDIM IGUATEMI",
    "registro": "5125-0",
    "logradouro": "RUA CUBAS DE MENDONCA",
    "numero": "",
    "bairro": "JD IGUATEMI",
    "distrito": {
      "codigo_distrito": 32,
      "nome_distrito": "IGUATEMI"
    },
    "subprefeitura": {
      "codigo_subprefeitura": 30,
      "nome_subprefeitura": "SAO MATEUS"
    }
  }
}
```

Code = 400
``` json
{
  "succeeded": false,
  "message": "Invalid Id"
}
```

Code = 400
``` json
{
  "succeeded": false,
  "errors": {
    "NomeFeira": [
      "The NomeFeira field is required"
    ]
  }
}
```

Code = 404
``` json
{
  "succeeded": false,
  "message": "Id not found"
}
```

### /api/feiras
##### Parameters

##### Body
``` json
{
  "longitude": -46442096,
  "latitude": -23599344,
  "setor_censitario": "355030833000015",
  "area_ponderacao": "3550308005275",
  "regiao": "Leste",
  "subregiao": "Leste 2",
  "nome_feira": "JARDIM IGUATEMI",
  "registro": "5125-0",
  "logradouro": "RUA CUBAS DE MENDONCA",
  "numero": "",
  "bairro": "JD IGUATEMI",
  "codigo_distrito": 32,
  "codigo_subprefeitura": 30
}
```

#### POST
##### Responses

Code = 200
``` json
{
  "succeeded": true,
  "data": {
    "id": 83,
    "longitude": -46442096,
    "latitude": -23599344,
    "setor_censitario": "355030833000015",
    "area_ponderacao": "3550308005275",
    "regiao": "Leste",
    "subregiao": "Leste 2",
    "nome_feira": "JARDIM IGUATEMI",
    "registro": "5125-0",
    "logradouro": "RUA CUBAS DE MENDONCA",
    "numero": "",
    "bairro": "JD IGUATEMI",
    "distrito": {
      "codigo_distrito": 32,
      "nome_distrito": "IGUATEMI"
    },
    "subprefeitura": {
      "codigo_subprefeitura": 30,
      "nome_subprefeitura": "SAO MATEUS"
    }
  }
}
```

Code = 400
``` json
{
  "succeeded": false,
  "errors": {
    "NomeFeira": [
      "The NomeFeira field is required"
    ]
  }
}
```

Code = 404
``` json
{
  "succeeded": false,
  "message": "Subprefeitura not found"
}
```

Code = 404
``` json
{
  "succeeded": false,
  "message": "Distrito not found"
}
```

### /api/distritos

#### GET
##### Responses

Code = 200
``` json
{
  "succeeded": true,
  "data": [
    {
      "codigo_distrito": 1,
      "nome_distrito": "AGUA RASA"
    }
  ]
}
```

Code = 404
``` json
{
  "succeeded": false,
  "message": "No Distritos found"
}
```

### /api/distritos/{id}

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path | Id do Distrito | Yes | integer |

##### Responses

Code = 200
``` json
{
  "succeeded": true,
  "data": {
    "codigo_distrito": 1,
    "nome_distrito": "AGUA RASA"
  }
}
```

Code = 400
``` json
{
  "succeeded": false,
  "message": "Invalid Id"
}
```

Code = 404
``` json
{
  "succeeded": false,
  "message": "Distrito not found"
}
```

### /api/subprefeituras/{id}

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path | Id da Subprefeitura | Yes | integer |

##### Responses

Code = 200
``` json
{
  "succeeded": true,
  "data": {
    "codigo_subprefeitura": 1,
      "nome_subprefeitura": "PERUS"
  }
}
```

Code = 400
``` json
{
  "succeeded": false,
  "message": "Invalid Id"
}
```

Code = 404
``` json
{
  "succeeded": false,
  "message": "Subprefeitura not found"
}
```

### /api/subprefeituras

#### GET
##### Responses

Code = 200
``` json
{
  "succeeded": true,
  "data": [
    {
      "codigo_subprefeitura": 1,
      "nome_subprefeitura": "PERUS"
    }
  ]
}
```

Code = 404
``` json
{
  "succeeded": false,
  "message": "No Subprefeituras found"
}
```
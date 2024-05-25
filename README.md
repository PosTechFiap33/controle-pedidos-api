# ControlePedidos

Este repositório foi criado para conter as atividades (Tech Challenge) da Pós Tech em Arquitetura de Software.

## Autores

- [Adriano Morgon Arruda](https://github.com/adrianomorgon)
- [Flávio Roberto Teixeira](https://github.com/FlavioRoberto)

## Tecnologias Utilizadas

- .NET 7
- Entity Framework Core
- Swagger
- PostgreSQL
- Docker

## Documentação

- [Domain Event Storming](https://www.figma.com/board/fHGDc1i4RxCmrrsPomCD4E/Domain-Event-Storming-Tech-Challenge?node-id=0%3A1&t=TI5wBxdhle65UPSn-1): Este link direciona para o Domain Event Storming, fornecendo uma visão geral visual do projeto.
- [Linguagem Ubíqua](https://endurable-saguaro-cb6.notion.site/Tech-challenge-819953d402a349e88708f15e7589e03a): A Linguagem Ubíqua é essencial para o entendimento compartilhado entre todas as partes interessadas, fornecendo um glossário de termos comuns utilizados no projeto.

## Execução do Projeto

Para executar o projeto, siga estas etapas:

1. Navegue até a pasta `src/ControlePedido`.
2. Execute o comando `docker compose up -d`.

Após levantar os containers, acesse a interface do Swagger para explorar a documentação das rotas da API. Para fazer isso, digite `https://localhost:5001/swagger` na barra de endereço do seu navegador.

### Gerando Migrations

Para gerar migrações, siga estas instruções:

1. Navegue até a pasta `src/ControlePedido`.
2. Execute o comando abaixo, substituindo `{NomeDaMigration}` pelo nome desejado para a migração:

```bash
dotnet ef migrations add {NomeDaMigration} --project Adapter/Driven/ControlePedido.Infra -s Adapter/Driver/ControlePedido.Api -c ControlePedidoContext --verbose

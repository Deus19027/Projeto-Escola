# Sistema de Gerenciamento de Produtos

Um sistema de gerenciamento de produtos desenvolvido em C# usando princípios de POO e boas práticas de programação.

## Pré-requisitos

- .NET 9.0 SDK ou superior

## Como Executar

1. Navegue até a pasta do projeto
2. Execute os comandos:

```bash
dotnet build
dotnet run
```

## Funcionalidades

- Cadastro de produtos com validações
- Listagem de produtos (ativos e inativos)
- Atualização de produtos
- Exclusão lógica de produtos
- Busca por categoria
- Geração automática de códigos de produto
- Controle de estoque
- Registro de data de cadastro e última atualização

## Estrutura do Projeto

```
SistemaVendas/
├── Models/
│   └── Produto.cs
├── Interfaces/
│   └── IProdutoRepository.cs
├── Repositories/
│   └── ProdutoRepository.cs
├── Services/
│   └── ProdutoService.cs
├── Program.cs
└── SistemaVendas.csproj
```

## Dados Predefinidos

O sistema já vem com alguns produtos cadastrados para teste:

- Notebook Dell XPS
- Samsung Galaxy S21
- Headphone Sony WH-1000XM4

## Contribuição

Sinta-se à vontade para contribuir com o projeto através de pull requests.

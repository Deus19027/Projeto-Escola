using System;
using System.Linq;
using SistemaVendas.Models;
using SistemaVendas.Services;
using SistemaVendas.Repositories;

namespace SistemaVendas
{
    class Program
    {
        private static ProdutoService _produtoService;

        static void Main(string[] args)
        {
            ConfigurarServicos();

            while (true)
            {
                try
                {
                    ExibirMenuPrincipal();
                    var opcao = Console.ReadLine();

                    switch (opcao?.ToLower())
                    {
                        case "1":
                            CadastrarProduto();
                            break;
                        case "2":
                            ListarProdutos();
                            break;
                        case "3":
                            AtualizarProduto();
                            break;
                        case "4":
                            DeletarProduto();
                            break;
                        case "5":
                            BuscarProdutoPorCategoria();
                            break;
                        case "x":
                            Console.WriteLine("Encerrando o programa...");
                            return;
                        default:
                            Console.WriteLine("Opção inválida!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nErro: {ex.Message}");
                }

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void ConfigurarServicos()
        {
            var produtoRepository = new ProdutoRepository();
            _produtoService = new ProdutoService(produtoRepository);
        }

        private static void ExibirMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE GERENCIAMENTO DE PRODUTOS ===");
            Console.WriteLine("1. Cadastrar Produto");
            Console.WriteLine("2. Listar Produtos");
            Console.WriteLine("3. Atualizar Produto");
            Console.WriteLine("4. Deletar Produto");
            Console.WriteLine("5. Buscar Produtos por Categoria");
            Console.WriteLine("X. Sair");
            Console.Write("\nEscolha uma opção: ");
        }

        private static void CadastrarProduto()
        {
            Console.Clear();
            Console.WriteLine("=== CADASTRO DE PRODUTO ===\n");

            var produto = new Produto();

            Console.Write("Código (deixe em branco para gerar automaticamente): ");
            produto.Codigo = Console.ReadLine();

            Console.Write("Nome: ");
            produto.Nome = Console.ReadLine();

            Console.Write("Descrição: ");
            produto.Descricao = Console.ReadLine();

            Console.Write("Preço: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal preco))
                produto.Preco = preco;

            Console.Write("Quantidade em Estoque: ");
            if (int.TryParse(Console.ReadLine(), out int quantidade))
                produto.QuantidadeEstoque = quantidade;

            Console.Write("Categoria: ");
            produto.Categoria = Console.ReadLine();

            var produtoCadastrado = _produtoService.CadastrarProduto(produto);
            Console.WriteLine($"\nProduto cadastrado com sucesso! ID: {produtoCadastrado.Id}");
        }

        private static void ListarProdutos()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE PRODUTOS ===\n");

            Console.Write("Incluir produtos inativos? (S/N): ");
            var incluirInativos = Console.ReadLine()?.ToUpper() == "S";

            var produtos = _produtoService.ListarProdutos(incluirInativos);

            if (!produtos.Any())
            {
                Console.WriteLine("Nenhum produto encontrado.");
                return;
            }

            foreach (var produto in produtos)
            {
                ExibirProduto(produto);
                Console.WriteLine(new string('-', 50));
            }
        }

        private static void AtualizarProduto()
        {
            Console.Clear();
            Console.WriteLine("=== ATUALIZAÇÃO DE PRODUTO ===\n");

            Console.Write("Digite o ID do produto: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                return;
            }

            var produto = _produtoService.ObterProdutoPorId(id);
            ExibirProduto(produto);

            Console.WriteLine("\nPreencha os novos dados (pressione ENTER para manter o valor atual):");

            Console.Write("Novo nome: ");
            var nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome))
                produto.Nome = nome;

            Console.Write("Nova descrição: ");
            var descricao = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(descricao))
                produto.Descricao = descricao;

            Console.Write("Novo preço: ");
            var precoStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(precoStr) && decimal.TryParse(precoStr, out decimal preco))
                produto.Preco = preco;

            Console.Write("Nova quantidade em estoque: ");
            var quantidadeStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(quantidadeStr) && int.TryParse(quantidadeStr, out int quantidade))
                produto.QuantidadeEstoque = quantidade;

            Console.Write("Nova categoria: ");
            var categoria = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(categoria))
                produto.Categoria = categoria;

            _produtoService.AtualizarProduto(produto);
            Console.WriteLine("\nProduto atualizado com sucesso!");
        }

        private static void DeletarProduto()
        {
            Console.Clear();
            Console.WriteLine("=== EXCLUSÃO DE PRODUTO ===\n");

            Console.Write("Digite o ID do produto: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                return;
            }

            var produto = _produtoService.ObterProdutoPorId(id);
            ExibirProduto(produto);

            Console.Write("\nConfirma a exclusão deste produto? (S/N): ");
            if (Console.ReadLine()?.ToUpper() == "S")
            {
                _produtoService.DeletarProduto(id);
                Console.WriteLine("Produto excluído com sucesso!");
            }
            else
            {
                Console.WriteLine("Operação cancelada.");
            }
        }

        private static void BuscarProdutoPorCategoria()
        {
            Console.Clear();
            Console.WriteLine("=== BUSCA POR CATEGORIA ===\n");

            Console.Write("Digite a categoria: ");
            var categoria = Console.ReadLine();

            var produtos = _produtoService.ListarProdutosPorCategoria(categoria);

            if (!produtos.Any())
            {
                Console.WriteLine("Nenhum produto encontrado nesta categoria.");
                return;
            }

            foreach (var produto in produtos)
            {
                ExibirProduto(produto);
                Console.WriteLine(new string('-', 50));
            }
        }

        private static void ExibirProduto(Produto produto)
        {
            Console.WriteLine($"\nID: {produto.Id}");
            Console.WriteLine($"Código: {produto.Codigo}");
            Console.WriteLine($"Nome: {produto.Nome}");
            Console.WriteLine($"Descrição: {produto.Descricao}");
            Console.WriteLine($"Preço: R$ {produto.Preco:F2}");
            Console.WriteLine($"Quantidade em Estoque: {produto.QuantidadeEstoque}");
            Console.WriteLine($"Categoria: {produto.Categoria}");
            Console.WriteLine($"Data de Cadastro: {produto.DataCadastro:dd/MM/yyyy HH:mm:ss}");
            if (produto.UltimaAtualizacao.HasValue)
                Console.WriteLine($"Última Atualização: {produto.UltimaAtualizacao:dd/MM/yyyy HH:mm:ss}");
            Console.WriteLine($"Status: {(produto.Ativo ? "Ativo" : "Inativo")}");
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SistemaVendas.Models;
using SistemaVendas.Interfaces;

namespace SistemaVendas.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public Produto CadastrarProduto(Produto produto)
        {
            ValidarProduto(produto);

            if (string.IsNullOrEmpty(produto.Codigo))
                produto.Codigo = GerarCodigoProduto();

            _repository.Adicionar(produto);
            return produto;
        }

        public Produto AtualizarProduto(Produto produto)
        {
            ValidarProduto(produto);
            _repository.Atualizar(produto);
            return produto;
        }

        public void DeletarProduto(int id)
        {
            _repository.Deletar(id);
        }

        public IEnumerable<Produto> ListarProdutos(bool incluirInativos = false)
        {
            return _repository.ObterTodos(incluirInativos);
        }

        public IEnumerable<Produto> ListarProdutosPorCategoria(string categoria)
        {
            if (string.IsNullOrEmpty(categoria))
                throw new ArgumentException("Categoria não pode ser vazia");

            return _repository.ObterPorCategoria(categoria);
        }

        public Produto ObterProdutoPorId(int id)
        {
            var produto = _repository.ObterPorId(id);
            if (produto == null)
                throw new InvalidOperationException($"Produto com ID {id} não encontrado");

            return produto;
        }

        private void ValidarProduto(Produto produto)
        {
            if (produto == null)
                throw new ArgumentNullException(nameof(produto));

            var validationContext = new ValidationContext(produto);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(produto, validationContext, validationResults, true))
            {
                var errors = validationResults.Select(r => r.ErrorMessage);
                throw new ValidationException($"Erro na validação do produto: {string.Join(", ", errors)}");
            }

            if (produto.Preco <= 0)
                throw new ValidationException("O preço do produto deve ser maior que zero");

            if (produto.QuantidadeEstoque < 0)
                throw new ValidationException("A quantidade em estoque não pode ser negativa");
        }

        private string GerarCodigoProduto()
        {
            // Gera um código único baseado na data/hora atual
            var timestamp = DateTime.Now.Ticks;
            return $"PROD{timestamp % 100000:D5}";
        }
    }
}
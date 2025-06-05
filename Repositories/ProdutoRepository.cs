using System;
using System.Collections.Generic;
using System.Linq;
using SistemaVendas.Models;
using SistemaVendas.Interfaces;

namespace SistemaVendas.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private static List<Produto> _produtos;
        private static int _proximoId = 1;

        public ProdutoRepository()
        {
            if (_produtos == null)
            {
                _produtos = new List<Produto>();
                InicializarDados();
            }
        }

        private void InicializarDados()
        {
            // Dados predefinidos
            var produtos = new[]
            {
                new Produto
                {
                    Id = _proximoId++,
                    Codigo = "NOT001",
                    Nome = "Notebook Dell XPS",
                    Descricao = "Notebook Dell XPS 13 com processador Intel i7, 16GB RAM, 512GB SSD",
                    Preco = 8999.99m,
                    QuantidadeEstoque = 5,
                    Categoria = "Notebooks",
                },
                new Produto
                {
                    Id = _proximoId++,
                    Codigo = "SMT001",
                    Nome = "Samsung Galaxy S21",
                    Descricao = "Smartphone Samsung Galaxy S21 128GB, 8GB RAM, Tela 6.2\"",
                    Preco = 4499.99m,
                    QuantidadeEstoque = 10,
                    Categoria = "Smartphones",
                },
                new Produto
                {
                    Id = _proximoId++,
                    Codigo = "HED001",
                    Nome = "Headphone Sony WH-1000XM4",
                    Descricao = "Fone de ouvido sem fio com cancelamento de ruído",
                    Preco = 1999.99m,
                    QuantidadeEstoque = 15,
                    Categoria = "Acessórios",
                }
            };

            _produtos.AddRange(produtos);
        }

        public Produto ObterPorId(int id)
        {
            return _produtos.FirstOrDefault(p => p.Id == id);
        }

        public Produto ObterPorCodigo(string codigo)
        {
            return _produtos.FirstOrDefault(p => p.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Produto> ObterTodos(bool incluirInativos = false)
        {
            return incluirInativos
                ? _produtos
                : _produtos.Where(p => p.Ativo);
        }

        public IEnumerable<Produto> ObterPorCategoria(string categoria)
        {
            return _produtos.Where(p => p.Ativo && p.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase));
        }

        public void Adicionar(Produto produto)
        {
            if (ExisteCodigo(produto.Codigo))
                throw new InvalidOperationException($"Já existe um produto com o código {produto.Codigo}");

            produto.Id = _proximoId++;
            _produtos.Add(produto);
        }

        public void Atualizar(Produto produto)
        {
            var produtoExistente = ObterPorId(produto.Id);
            if (produtoExistente == null)
                throw new InvalidOperationException($"Produto com ID {produto.Id} não encontrado");

            if (produto.Codigo != produtoExistente.Codigo && ExisteCodigo(produto.Codigo))
                throw new InvalidOperationException($"Já existe um produto com o código {produto.Codigo}");

            var index = _produtos.IndexOf(produtoExistente);
            produto.UltimaAtualizacao = DateTime.Now;
            _produtos[index] = produto;
        }

        public void Deletar(int id)
        {
            var produto = ObterPorId(id);
            if (produto == null)
                throw new InvalidOperationException($"Produto com ID {id} não encontrado");

            produto.Ativo = false;
            produto.UltimaAtualizacao = DateTime.Now;
        }

        public bool ExisteCodigo(string codigo)
        {
            return _produtos.Any(p => p.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
        }
    }
}
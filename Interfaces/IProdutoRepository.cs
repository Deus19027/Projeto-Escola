using System;
using System.Collections.Generic;
using SistemaVendas.Models;

namespace SistemaVendas.Interfaces
{
    public interface IProdutoRepository
    {
        Produto ObterPorId(int id);
        Produto ObterPorCodigo(string codigo);
        IEnumerable<Produto> ObterTodos(bool incluirInativos = false);
        IEnumerable<Produto> ObterPorCategoria(string categoria);
        void Adicionar(Produto produto);
        void Atualizar(Produto produto);
        void Deletar(int id);
        bool ExisteCodigo(string codigo);
    }
}
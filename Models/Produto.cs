using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaVendas.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O código do produto é obrigatório")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição do produto é obrigatória")]
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O preço do produto é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A quantidade em estoque é obrigatória")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa")]
        public int QuantidadeEstoque { get; set; }

        public string Categoria { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? UltimaAtualizacao { get; set; }
        public bool Ativo { get; set; }

        public Produto()
        {
            // Inicializa as propriedades não nulas com valores padrão
            Codigo = string.Empty;
            Nome = string.Empty;
            Descricao = string.Empty;
            Categoria = string.Empty;
            DataCadastro = DateTime.Now;
            Ativo = true;
        }

        public void AtualizarEstoque(int quantidade)
        {
            if (QuantidadeEstoque + quantidade < 0)
                throw new InvalidOperationException("Quantidade em estoque não pode ficar negativa");

            QuantidadeEstoque += quantidade;
            UltimaAtualizacao = DateTime.Now;
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace GestaoFornecedores.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required, StringLength(14, MinimumLength = 14)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "CNPJ deve conter apenas números")]
        public string CNPJ { get; set; } = string.Empty;

        [Required]
        public string Segmento { get; set; } = string.Empty; // Comércio, Serviço, Indústria

        [Required, StringLength(8, MinimumLength = 8)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "CEP deve conter apenas números")]
        public string CEP { get; set; } = string.Empty;

        [StringLength(255)]
        public string Endereco { get; set; } = string.Empty;

        public string Foto { get; set; } = string.Empty; // Caminho da imagem PNG
    }
}

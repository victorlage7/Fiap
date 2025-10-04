using System.ComponentModel.DataAnnotations;

namespace Producer.Dtos
{
    public class ClienteDto
    {        
        [Required(ErrorMessage = "{0} não pode ser vazio! Tente novamente.")]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter entre 5 e 100 caracteres", MinimumLength = 5)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "{0} não pode ser vazio! Tente novamente.")]
        [EmailAddress(ErrorMessage = "E-mail inválido!")]       
        public string Email { get; set; }
    }
}
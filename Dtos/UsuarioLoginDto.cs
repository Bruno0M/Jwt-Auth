using System.ComponentModel.DataAnnotations;

namespace AutenticacaoJWT.Dtos
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "O campo Email é obrigatório!"), EmailAddress(ErrorMessage = "EmailInválido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo Senha é obrigatório!")]
        public string Senha { get; set; }
    }
}

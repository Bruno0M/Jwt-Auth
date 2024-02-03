using AutenticacaoJWT.Enum;
using System.ComponentModel.DataAnnotations;

namespace AutenticacaoJWT.Dtos
{
    public class UsuarioCriacaoDto
    {
        [Required(ErrorMessage = "O campo Usuário é obrigatório!")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "O campo Email é obrigatório!"), EmailAddress(ErrorMessage = "EmailInválido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo Senha é obrigatório!")]
        public string Senha { get; set; }
        [Compare("Senha", ErrorMessage = "Senhas não coincidem!")]
        public string ConfirmarSenha { get; set; }
        [Required(ErrorMessage = "O campo Cargo é obrigatório!")]
        public CargoEnum Cargo { get; set; }

    }
}

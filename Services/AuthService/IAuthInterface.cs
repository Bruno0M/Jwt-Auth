using AutenticacaoJWT.Dtos;
using AutenticacaoJWT.Models;

namespace AutenticacaoJWT.Services.AuthService
{
    public interface IAuthInterface
    {
        Task<Response<UsuarioCriacaoDto>> Registrar(UsuarioCriacaoDto usuarioRegister);
        Task<Response<string>> Login(UsuarioLoginDto usuarioLogin);
    }
}

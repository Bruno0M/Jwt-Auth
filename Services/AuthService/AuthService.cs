using AutenticacaoJWT.Data;
using AutenticacaoJWT.Dtos;
using AutenticacaoJWT.Models;
using AutenticacaoJWT.Services.SenhaService;
using Microsoft.EntityFrameworkCore;

namespace AutenticacaoJWT.Services.AuthService
{
    public class AuthService : IAuthInterface
    {
        private readonly AppDbContext _context;
        private readonly ISenhaInterface _senhaInterface;
        public AuthService(AppDbContext context, ISenhaInterface senhaInterface)
        {
            _context = context;
            _senhaInterface = senhaInterface;
        }

        public async Task<Response<UsuarioCriacaoDto>> Registrar(UsuarioCriacaoDto usuarioRegister)
        {
            Response<UsuarioCriacaoDto> response = new Response<UsuarioCriacaoDto>();

            try
            {
                if(!VerificaSeEmaileUsuarioExiste(usuarioRegister))
                {
                    response.Dados = null;
                    response.Mensagem = "Email/Usuario já cadastrado";
                    response.Status = false;
                    return response;
                }

                _senhaInterface.CriarSenhaHash(usuarioRegister.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                UsuarioModel usuario = new UsuarioModel()
                {
                    Usuario = usuarioRegister.Usuario,
                    Email = usuarioRegister.Email,
                    Cargo = usuarioRegister.Cargo,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt
                };

                _context.Add(usuario);
                await _context.SaveChangesAsync();

                response.Mensagem = "Usuário criado com Sucesso!";

            }catch (Exception ex)
            {
                response.Dados = null;
                response.Mensagem = ex.Message;
                response.Status = false;
            }

            return response;
        }

        public async Task<Response<string>> Login(UsuarioLoginDto usuarioLogin)
        {
            Response<string> response = new Response<string>();

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(userBanco => userBanco.Email == usuarioLogin.Email);

                if (usuario == null)
                {
                    response.Mensagem = "Credenciais Inválidas!";
                    response.Status = false;
                    return response;
                }

                if(!_senhaInterface.VerificaSenhaHash(usuarioLogin.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    response.Mensagem = "Credenciais Inválidas!";
                    response.Status = false;
                    return response;
                }

                var token = _senhaInterface.CriarToken(usuario);

                response.Dados = token;
                response.Mensagem = "Usuário logado com sucesso!";

            }catch (Exception ex)
            {
                response.Dados = null;
                response.Mensagem = ex.Message;
                response.Status = false;
            }

            return response;
        }

        private bool VerificaSeEmaileUsuarioExiste(UsuarioCriacaoDto usuarioRegister)
        {
            var usuario = _context.Usuarios.FirstOrDefault(userBanco => userBanco.Email == usuarioRegister.Email || userBanco.Usuario == usuarioRegister.Usuario);

            if (usuario != null) return false;

            return true;
        }
    }
}


namespace Application.User
{
    public interface IUserService
    {
        Task<List<Domain.Usuario>> ObtenerUsuarios();
        Task CrearUsuario(Domain.Usuario user);
        Task<Domain.Usuario> Obtener(int id);
        Task<bool> SoftDelete(int id);

    }
}

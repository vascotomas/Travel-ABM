namespace Application.User
{
    public interface IUserRepository
    {
        Task<List<Domain.Usuario>> ObtenerUsuarios();
        Task<bool> CrearUsuario(Domain.Usuario user);
        Task<Domain.Usuario> Obtener(int id);
        Task<bool> SoftDelete(int id);
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LoginService
{
    public interface ILoginRepository
    {
        Task<Domain.Usuario> Get(DTO_s.UserCrudDto usuario);
        Task<Domain.Usuario> GetByToken(string token);
        Task UpdateUserReset(Domain.Usuario usuario);

    }
}

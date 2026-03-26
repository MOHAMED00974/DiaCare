using DiaCare.Domain.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaCare.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResultDto> RegisterAsync (RegisterDto model);

        Task <AuthResultDto> LoginAsync (LoginDto model);

    }
}

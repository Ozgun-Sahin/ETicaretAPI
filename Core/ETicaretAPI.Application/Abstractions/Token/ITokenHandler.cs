using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using t=ETicaretAPI.Application.DTOs;

namespace ETicaretAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        t.Token CreateAccessToken(int second);
    }
}

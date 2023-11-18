using DtoLayer.DTOs.TokenDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICreateTokenService
    {
        TokenDto TokenCreate();
        TokenDto TokenCreateAdmin();
        TokenDto TokenCreateInstructor();
    }
}

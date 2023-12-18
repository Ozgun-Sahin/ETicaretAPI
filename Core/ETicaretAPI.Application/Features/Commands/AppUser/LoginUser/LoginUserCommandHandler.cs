using ETicaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using u = ETicaretAPI.Domain.Entities.Identity;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<u.AppUser> _userManager;
        readonly SignInManager<u.AppUser> _signInManager;

        public LoginUserCommandHandler(UserManager<u.AppUser> userManager, SignInManager<u.AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }



        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            u.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
            }

            if (user == null)
            {
                throw new NotFoundUserException("Kullanıcı adı veya şifre hatalı");
            }


            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                
            }

            return null;
        }
    }
}

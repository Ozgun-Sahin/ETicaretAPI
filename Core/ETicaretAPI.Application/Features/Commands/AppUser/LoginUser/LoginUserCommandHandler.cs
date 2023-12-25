using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
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
        readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandler(UserManager<u.AppUser> userManager, SignInManager<u.AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
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
                throw new NotFoundUserException();
            }


            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(5);

                return new LoginUserSuccessCommandResponse()
                {
                    Token = token,
                };
            }

            throw new AuthenticationErrorException();
        }
    }
}

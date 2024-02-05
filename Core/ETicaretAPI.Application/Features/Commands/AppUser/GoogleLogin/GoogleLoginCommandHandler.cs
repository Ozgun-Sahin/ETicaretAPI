using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p = ETicaretAPI.Domain.Entities.Identity;

namespace ETicaretAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly UserManager<p.AppUser> _useManager;
        readonly ITokenHandler _tokenHandler;

        public GoogleLoginCommandHandler(UserManager<p.AppUser> useManager, ITokenHandler tokenHandler)
        {
            _useManager = useManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var setttings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { "1075721112211-8tkkld4ls9llrvlkj58059l98mi7sjj3.apps.googleusercontent.com" }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, setttings);

            var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);

            p.AppUser user = await _useManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            bool result = user!=null;

            if (user == null) 
            {
                user = await _useManager.FindByEmailAsync(payload.Email);

                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email,
                        NamemSurname = payload.Name,
                    };
                    var identityResult = await _useManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }

            if (result)
                await _useManager.AddLoginAsync(user, info);
            else
                throw new Exception("Invalid external authentication!");

            Token token = _tokenHandler.CreateAccessToken(5);

            return new()
            {
                Token = token
            };
        }
    }
}

﻿using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.DTOs.Facebook;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using u = ETicaretAPI.Domain.Entities.Identity;

namespace ETicaretAPI.Application.Features.Commands.AppUser.FacebokLogin
{
    public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
    {
        readonly UserManager<u.AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly HttpClient _httpClient;

        public FacebookLoginCommandHandler(UserManager<u.AppUser> userManager, ITokenHandler tokenHandler, IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
        {
            string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id=734570465069207&client_secret=0801bd20044228460564e356cc0afbff&grant_type=client_credentials");

            FacebookAccessTokenResponse_DTO facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse_DTO>(accessTokenResponse);

            string userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={request.AuthToken}&access_token={facebookAccessTokenResponse.AccessToken}");

            FacebookUserAccessTokenValidation_DTO validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation_DTO>(userAccessTokenValidation);

            if (validation.Data.IsValid)
            {
                string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={request.AuthToken}");

                FacebookUserInfoResponse_DTO userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse_DTO>(userInfoResponse);

                var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");

                u.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                bool result = user != null;

                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(userInfo.Email);

                    if (user == null)
                    {
                        user = new()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Email = userInfo.Email,
                            UserName = userInfo.Email,
                            NamemSurname = userInfo.Name,
                        };
                        var identityResult = await _userManager.CreateAsync(user);
                        result = identityResult.Succeeded;
                    }
                }

                if (result)
                {
                    await _userManager.AddLoginAsync(user, info);

                    Token token = _tokenHandler.CreateAccessToken(5);
                    return new()
                    {
                        Token = token,
                    };
                }     
            }
            throw new Exception("Invalid external authentication!");
        }
    }
}

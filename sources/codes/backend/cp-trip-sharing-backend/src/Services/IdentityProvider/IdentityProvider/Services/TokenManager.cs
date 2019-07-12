﻿using IdentityProvider.Helpers;
using IdentityProvider.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityProvider.Repositories.Interfaces;
using IdentityProvider.Repositories.DbContext;
using IdentityProvider.Repositories;

namespace IdentityProvider.Services
{
    public class TokenManager : ITokenManager
    {
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<AppSettings> _settings;
        private readonly IBlacklistTokenRepository _blacklistTokenRepository = null;

        public TokenManager(IDistributedCache cache,
                IHttpContextAccessor httpContextAccessor,
                IOptions<AppSettings> setting
            )
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _settings = setting;
            _blacklistTokenRepository = new BlacklistTokenRepository(setting);
        }

        public async Task<bool> IsCurrentActiveToken()
            => await IsActiveAsync(GetCurrentAsync());

        public async Task DeactivateCurrentAsync()
            => await DeactivateAsync(GetCurrentAsync());

        public async Task<bool> IsActiveAsync(string token)
            => await _blacklistTokenRepository.GetTokenAsync(token) == null;

        public async Task DeactivateAsync(string token)
            => await _blacklistTokenRepository.AddAsync(token);

        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }

        
    }
}

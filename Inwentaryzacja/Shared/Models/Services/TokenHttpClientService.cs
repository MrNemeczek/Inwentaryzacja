using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Inwentaryzacja.Shared.Models.Services
{
    public class TokenHttpClientService : HttpClient
    {
        private IAccessTokenProvider accessToken;

        /// <summary>
        /// konstruktor do kontenera DI ktory pobiera <paramref name="token"/> i <paramref name="navigationManager"/>
        /// </summary>
        /// <param name="token"> token zalogowanego uzytkownika </param>
        /// <param name="navigationManager"> do ustawiania BaseAdress </param>
        public TokenHttpClientService(IAccessTokenProvider token, NavigationManager navigationManager)
        {
            accessToken = token;
            BaseAddress = new Uri(navigationManager.BaseUri);
        }

        /// <summary>
        /// Ustawia AuthenticationHeaderValue
        /// </summary>
        public async Task SetToken()
        {
            var tokenResult = await accessToken.RequestAccessToken();

            if (tokenResult.TryGetToken(out var token))
            {
                DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
            }
            else
            {
                Console.WriteLine("nie pobrano tokenu!");
            }
        }
    }
}

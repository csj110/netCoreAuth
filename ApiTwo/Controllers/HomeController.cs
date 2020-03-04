using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace ApiTwo.Controllers
{
    public class HomeController:Controller
    {
        private IHttpClientFactory _clientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _clientFactory = httpClientFactory;
        }
        [Route("/home")]
        public async Task<IActionResult> Index()
        {
            //retieve access_token 
            var serverClient = _clientFactory.CreateClient();
            var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync("https://localhost:44369/");

            TokenResponse tokenResp = await serverClient.RequestClientCredentialsTokenAsync(
               new ClientCredentialsTokenRequest
            {   
                Address=discoveryDocument.TokenEndpoint,
                ClientId= "client_id",
                ClientSecret= "client_secret",
                Scope="ApiOne",
            });

            //retieve secret_data
            var apiClient = _clientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResp.AccessToken);
            var resp=await apiClient.GetAsync("https://localhost:44370/secret");
            var content =await resp.Content.ReadAsStringAsync();

            return Ok(new
            {
                access_token= tokenResp.AccessToken,
                content,
            });
        }
    }
}

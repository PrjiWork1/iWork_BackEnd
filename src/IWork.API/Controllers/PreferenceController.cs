using DotNetEnv;
using MercadoPago.Client.Preference;
using MercadoPago.Client;
using MercadoPago.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using IWork.Domain.Requests;
using PreferenceRequest = IWork.Domain.Requests.PreferenceRequest;

namespace IWork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreferenceController : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Post([FromBody] PreferenceRequest preferenceRequest)
        {
            Env.Load();
            string MercadoPagoToken = Environment.GetEnvironmentVariable("TokenMercadoPago");
            try
            {
                MercadoPagoConfig.AccessToken = MercadoPagoToken;
                var requestOptions = new RequestOptions();
                requestOptions.CustomHeaders.Add("x-idempotency-key", Guid.NewGuid().ToString());

                var request = new MercadoPago.Client.Preference.PreferenceRequest
                {
                    Items = preferenceRequest.Items.Select(item => new MercadoPago.Client.Preference.PreferenceItemRequest
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Quantity = 1,
                        CurrencyId = "BRL",
                        UnitPrice = item.UnitPrice
                    }).ToList(),

                    BackUrls = new PreferenceBackUrlsRequest
                    {
                        Success = "http://localhost:5173/success",
                        Failure = "http://localhost:5173/failure",
                        Pending = "http://localhost:5173/pending"
                    },

                };

                var client = new PreferenceClient();
                var preference = await client.CreateAsync(request, requestOptions);
                return Ok(new { Preference = preference.InitPoint });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

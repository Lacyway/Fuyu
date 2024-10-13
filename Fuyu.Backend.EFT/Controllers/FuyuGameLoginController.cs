using System.Threading.Tasks;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;
using Fuyu.Backend.EFT.Services;
using Fuyu.Backend.Common.DTO.Requests;
using Fuyu.Backend.Common.DTO.Responses;

namespace Fuyu.Backend.EFT.Controllers
{
    public class FuyuGameLoginController : HttpController
    {
        public FuyuGameLoginController() : base("/fuyu/game/login")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            var request = await context.GetJsonAsync<FuyuGameLoginRequest>();
            var sessionId = AccountService.LoginAccount(request.AccountId);
            var response = new FuyuGameLoginResponse()
            {
                SessionId = sessionId
            };

            await context.SendJsonAsync(Json.Stringify(response));
        }
    }
}
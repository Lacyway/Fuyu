using System.Threading.Tasks;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;
using Fuyu.Backend.BSG.DTO.Responses;
using Fuyu.Backend.EFT.DTO.Requests;
using Fuyu.Backend.EFT.DTO.Responses;

namespace Fuyu.Backend.EFT.Controllers
{
    public class GameProfileNicknameValidateController : HttpController
    {
        public GameProfileNicknameValidateController() : base("/client/game/profile/nickname/validate")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            var request = await context.GetJsonAsync<GameProfileNicknameValidateRequest>();

            // TODO:
            // * validate nickname usage
            // -- seionmoya, 2024/08/28

            var response = new ResponseBody<GameProfileNicknameValidateResponse>()
            {
                data = new GameProfileNicknameValidateResponse()
                {
                    status = "ok"
                }
            };

            await context.SendJsonAsync(Json.Stringify(response));
        }
    }
}
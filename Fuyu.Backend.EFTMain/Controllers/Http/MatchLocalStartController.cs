using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Requests;
using Fuyu.Backend.EFTMain.Networking;
using Fuyu.Backend.EFTMain.Services;

namespace Fuyu.Backend.EFTMain.Controllers.Http;

public class MatchLocalStartController : AbstractEftHttpController<MatchLocalStartRequest>
{
    public MatchLocalStartController() : base("/client/match/local/start")
    {
    }

    public override Task RunAsync(EftHttpContext context, MatchLocalStartRequest request)
    {
        var location = request.location;
        var text = LocationService.Instance.GetLoot(location);
        return context.SendJsonAsync(text, true, true);
    }
}
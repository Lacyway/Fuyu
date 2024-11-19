using System.Collections.Generic;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Requests;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.IO;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class MatchLocalStartController : EftHttpController<MatchLocalStartRequest>
    {
        private readonly Dictionary<string, string> _locations;

        public MatchLocalStartController() : base("/client/match/local/start")
        {
            _locations = new Dictionary<string, string>()
            {
                { "bigmap",         Resx.GetText("eft", "database.locations.bigmap.json")          },
                { "factory4_day",   Resx.GetText("eft", "database.locations.factory4_day.json")    },
                { "factory4_night", string.Empty                                                   },
                { "interchange",    Resx.GetText("eft", "database.locations.interchange.json")     },
                { "laboratory",     string.Empty                                                   },
                { "lighthouse",     Resx.GetText("eft", "database.locations.lighthouse.json")      },
                { "rezervbase",     Resx.GetText("eft", "database.locations.rezervbase.json")      },
                { "sandbox",        Resx.GetText("eft", "database.locations.sandbox.json")         },
                { "shorline",       Resx.GetText("eft", "database.locations.shorline.json")        },
                { "tarkovstreets",  Resx.GetText("eft", "database.locations.tarkovstreets.json")   },
                { "woods",          Resx.GetText("eft", "database.locations.woods.json")           }
            };
        }

        public override Task RunAsync(EftHttpContext context, MatchLocalStartRequest request)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var location = request.location;

            var text = _locations[location];
            return context.SendJsonAsync(text, true, true);
        }
    }
}
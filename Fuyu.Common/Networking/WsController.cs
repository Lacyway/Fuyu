using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fuyu.Common.Networking;

public abstract class WsController : AbstractWebController<WsContext>
{
    protected WsController(Regex pattern) : base(pattern)
    {
        // match dynamic paths
    }

    protected WsController(string path) : base(path)
    {
        // match static paths
    }

    public override Task RunAsync(WsContext context)
    {
        context.OnCloseEvent += OnCloseAsync;
        context.OnTextEvent += OnTextAsync;
        context.OnBinaryEvent += OnBinaryAsync;

        return Task.CompletedTask;
    }

    public virtual Task OnCloseAsync(WsContext context)
    {
        return Task.CompletedTask;
    }

    public virtual Task OnTextAsync(WsContext context, string text)
    {
        return Task.CompletedTask;
    }

    public virtual Task OnBinaryAsync(WsContext context, byte[] binary)
    {
        return Task.CompletedTask;
    }
}
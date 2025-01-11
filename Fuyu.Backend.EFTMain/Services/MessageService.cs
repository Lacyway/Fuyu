using System;
using Fuyu.Backend.BSG.Models.Chat;
using Fuyu.Backend.BSG.Services;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.EFTMain.Services;

public class MessageService
{
    public static MessageService Instance => instance.Value;
    private static readonly Lazy<MessageService> instance = new(() => new MessageService());

    private readonly EftOrm _eftOrm;
    private readonly TimeService _timeService;

    private MessageService()
    {
        _eftOrm = EftOrm.Instance;
        _timeService = TimeService.Instance;
    }

    public void SendMessageToPlayer(ChatMessage message)
    {
        var profile = _eftOrm.GetActiveProfile(message.TargetSession); // TODO: Write actual WS send

        message.Id = new MongoId(true);
        message.DateTime = _timeService.TimestampMs;
    }
}

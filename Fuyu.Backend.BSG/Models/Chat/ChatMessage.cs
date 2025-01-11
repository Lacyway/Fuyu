using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Chat;

[DataContract]
public class ChatMessage
{
    [IgnoreDataMember]
    public MongoId TargetSession { get; set; }

    [DataMember(Name = "_id")]
    public MongoId Id { get; set; }

    [DataMember(Name = "templateId")]
    public string TemplateId { get; set; }

    [DataMember(Name = "systemData")]
    public SystemData SystemData { get; set; }

    [DataMember(Name = "type")]
    public EMessageType Type { get; set; }

    [DataMember(Name = "text")]
    public string Text { get; set; }

    [DataMember(Name = "dt")]
    public double DateTime { get; set; }

    [DataMember(Name = "uid")]
    public MongoId UserId { get; set; }

    [DataMember(Name = "replyTo", EmitDefaultValue = false)]
    public ChatMessage ReplyTo { get; set; }

    [DataMember(Name = "hasRewards")]
    public bool HasRewards { get; set; }

    [DataMember(Name = "maxStorageTime", EmitDefaultValue = false)]
    public long MaxStorageTime { get; set; }

    [DataMember(Name = "items", EmitDefaultValue = false)]
    public ItemInstance[] Items { get; set; }

    [DataMember(Name = "ProfileChangeEvents", EmitDefaultValue = false)]
    public ProfileChangeEvent[] ProfileChangeEvents { get; set; }
}

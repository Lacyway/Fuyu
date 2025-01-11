using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Chat;

public class ProfileChangeEvent
{
    [DataMember(Name = "_id")]
    public MongoId Id { get; set; }

    [DataMember(Name = "MessageId")]
    public MongoId MessageId { get; set; }

    [DataMember(Name = "Type")]
    public ENotificationType Type { get; set; }

    [DataMember(Name = "Entity")]
    public string Entity;

    [DataMember(Name = "Value", EmitDefaultValue = false)]
    public double Value;

    [DataMember(Name = "Redeemed")]
    public bool Redeemed;
}
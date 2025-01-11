using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Chat;

[DataContract]
public class SystemData
{
    [DataMember(Name = "date")]
    public string Date { get; set; }

    [DataMember(Name = "time")]
    public string Time { get; set; }

    [DataMember(Name = "location")]
    public string Location { get; set; }

    [DataMember(Name = "buyerNickname")]
    public string BuyerNickname { get; set; }

    [DataMember(Name = "soldItem")]
    public string SoldItem { get; set; }

    [DataMember(Name = "itemCount")]
    public string ItemCount { get; set; }
}

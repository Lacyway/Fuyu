using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles.Bonusses;
using Fuyu.Common.Hashing;
using Newtonsoft.Json.Converters;

namespace Fuyu.Backend.BSG.Models.Profiles
{
    [DataContract]
    public class BonusInfo
    {
        [DataMember]
        public MongoId id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string icon { get; set; }

        [DataMember]
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public EBonusType type { get; set; }

        [DataMember]
        public float value { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public MongoId? templateId { get; set; }

        [DataMember]
        public bool passive { get; set; }

        [DataMember]
        public bool visible { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool? production { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public MongoId?[] filter { get; set; }
    }
}
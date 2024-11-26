using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles.Stats
{
    [DataContract]
    public class DamageHistory
    {
        [DataMember]
        public string LethalDamagePart;

        // TODO: proper type
        [DataMember]
        public object LethalDamage;

        // TODO: proper type
        [DataMember]
        public object[] BodyParts;
    }
}
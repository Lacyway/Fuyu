using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles.Health
{
    [DataContract]
    public class BodyPartInfo
    {
        [DataMember]
        public BodyPart Head;

        [DataMember]
        public BodyPart Chest;

        [DataMember]
        public BodyPart Stomach;

        [DataMember]
        public BodyPart LeftArm;

        [DataMember]
        public BodyPart RightArm;

        [DataMember]
        public BodyPart LeftLeg;

        [DataMember]
        public BodyPart RightLeg;
    }
}
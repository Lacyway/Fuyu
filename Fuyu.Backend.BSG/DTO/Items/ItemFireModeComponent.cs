using Fuyu.Backend.BSG.DTO.Common;
using System.Runtime.Serialization;

namespace Fuyu.Backend.EFT.DTO.Items
{
    [DataContract]
    public class ItemFireModeComponent
    {
        [DataMember]
        public EFireMode FireMode;
    }
}
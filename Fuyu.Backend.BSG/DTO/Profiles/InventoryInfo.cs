using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Backend.EFT.DTO.Items;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.DTO.Profiles
{
    [DataContract]
    public class InventoryInfo
    {
        [DataMember]
        public ItemInstance[] items;

        [DataMember]
        public MongoId equipment;

        [DataMember]
        public MongoId? stash;

        [DataMember]
        public MongoId? sortingTable;

        [DataMember]
        public MongoId? questRaidItems;

        [DataMember]
        public MongoId? questStashItems;

        [DataMember]
        public Dictionary<string, MongoId> fastPanel;

        [DataMember]
        public Dictionary<string, MongoId> hideoutAreaStashes;

        [DataMember]
        public MongoId[] favoriteItems;
    }
}
﻿using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.ItemEvents;

[DataContract]
public class InventoryWarning
{
    [DataMember(Name = "index")]
    public int RequestIndex { get; set; }

    [DataMember(Name = "errmsg")]
    public string ErrorMessage { get; set; }

    [DataMember(Name = "code")]
    public string ErrorCode { get; set; }

    // NOTE: Used only if ErrorCode == EBackendErrorCode.InsufficientNumberInStock
    // -- nexus4880, 2024-10-22
    [DataMember(Name = "data", EmitDefaultValue = false)]
    public InsufficientNumberOfItemsData Data { get; set; }
}
﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FairShareAPI.Models;

public partial class TblGroup
{
    public int FldGroupId { get; set; }

    public string? FldGroupName { get; set; }

    public bool? FldGroupBoolean { get; set; }

    [JsonIgnore]
    public virtual ICollection<TblGroupToTrip> TblGroupToMoneys { get; } = new List<TblGroupToTrip>();

	[JsonIgnore]
	public virtual ICollection<TblUserToGroup> TblUserToGroups { get; } = new List<TblUserToGroup>();
}

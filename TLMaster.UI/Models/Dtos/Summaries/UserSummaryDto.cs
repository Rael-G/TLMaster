using System;

namespace TLMaster.UI.Models.Dtos.Summaries;

public class UserSummaryDto 
{ 
    public Guid Id { get; set; } 
    public string UserName { get; set; } = string.Empty;
}
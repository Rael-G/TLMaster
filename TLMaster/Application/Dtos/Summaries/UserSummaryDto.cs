using System;

namespace TLMaster.Application.Dtos.Summaries;

public class UserSummaryDto 
{ 
    public Guid Id { get; set; } 
    public string UserName { get; set; } = string.Empty;
}